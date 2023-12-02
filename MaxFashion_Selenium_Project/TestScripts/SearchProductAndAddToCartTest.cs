using MaxFashion_Selenium_Project.PageObjects;
using MaxFashion_Selenium_Project.Utilities;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFashion_Selenium_Project.TestScripts
{
    [TestFixture,Order(1)]
    internal class SearchProductAndAddToCartTest:CoreCodes
    {
        [Test]
        [Author("Shirin","shirin@gmail.com")]
        [Order(1)]
        [Category("Regression Test")]
        public void ProductSearchAndAddToCartTest()
        {
            //For Log
            LogUpdates();
            MaxFashionHomePage max = new(driver);

            //For excel
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/TestData/InputData.xlsx";
            string? sheetName = "ProductSearch";

            List<ExcelProductSearch> excelDataList = ExcelUtils.ReadExcelData(excelFilePath, sheetName);
            foreach (var item in excelDataList)
            {
                string? searchtext=item.SearchText;
                string? category=item.Category;
                string? productId=item.ProductId;

                Log.Information("Search Test Started");
                Thread.Sleep(2000);
                var searchResultPage=max.TypeSearchInput(searchtext);
                Log.Information("Search Initiated");
                Thread.Sleep(2000);
                Screenshots.TakeScreenShot(driver);
                Log.Information("Search Finished");
                searchResultPage.ClickCategoryOfYourChoice(category);
                Thread.Sleep(2000);
                searchResultPage.ClickSortByButton();
                Log.Information("Sort started");
                Thread.Sleep(2000);
                Log.Information("Sorting by category");
                searchResultPage.ClickSortByButtonCategory();
                Log.Information("Sorting completed");
                Thread.Sleep(2000);
                searchResultPage.ClickSizePlusButton();
                Thread.Sleep(2000);
                searchResultPage.SelectSize();
                Log.Information("Size selected");
                Thread.Sleep(2000);
                //ScrollIntoView(driver, driver.FindElement(By.XPath("//h6[text()='color']")));
                searchResultPage.ClickColorPlusButton();
                Thread.Sleep(2000);
                searchResultPage.SelectColor();
                Log.Information("Color selected");
                Thread.Sleep(2000);
                Screenshots.TakeScreenShot(driver);
                IWebElement productname = driver.FindElement(By.XPath("//div[@id='product-"+productId+"']/div/div[3]/a"));
                Console.WriteLine(productname.Text);
                var desiredProductpage=searchResultPage.ClickDesiredProduct(productId);
                Log.Information("Product Selected - "+productname.Text);
                Thread.Sleep(10000);
                Screenshots.TakeScreenShot(driver);

                List<string> lswindow = driver.WindowHandles.ToList();
                if (lswindow.Count > 0)
                {
                    driver.SwitchTo().Window(lswindow[1]);
                }

                desiredProductpage.SelectSizeBox();
                Thread.Sleep(2000);

                ScrollIntoView(driver, desiredProductpage.AddToBasketButton);

                var checkoutPage = desiredProductpage.ClickAddToBasketButton();
                Log.Information("Add to basket button clicked");
                Thread.Sleep(4000);
                Screenshots.TakeScreenShot(driver);

                checkoutPage.ClickCheckOutButton();
                Log.Information("CheckOut button clicked");
                Thread.Sleep(3000);
                Screenshots.TakeScreenShot(driver);
                //Console.WriteLine("signup "+confirmCheckout);
                try
                {
                    Assert.That(checkoutPage.CheckOutConfirm.Text.Contains("Sign up"));
                    Log.Information("CheckOut Test Passed");
                    test = extent.CreateTest("CheckOut a Product Test - Pass");
                    test.Pass("Checkout  success");
                    //Console.WriteLine("checkout test-passed");
                }
                catch
                {
                    test = extent.CreateTest("CheckOut a Product Test - Fail");
                    Log.Error("CheckOut Test Failed!!");
                    test.Fail("Checkout failed");
                    Console.WriteLine("test-failed");

                }
               // Log.CloseAndFlush();
            }
        }
    }
}
