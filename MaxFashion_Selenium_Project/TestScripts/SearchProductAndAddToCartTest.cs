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
                var searchResultPage=max.TypeSearchInput(searchtext);
                Log.Information("Search Initiated");
                Screenshots.TakeScreenShot(driver);
                Log.Information("Search Finished");
                searchResultPage.ClickCategoryOfYourChoice(category);
                
                searchResultPage.ClickSortByButton();
                Log.Information("Sort started");
                
                Log.Information("Sorting by category");
                searchResultPage.ClickSortByButtonCategory();
                Log.Information("Sorting completed");
                
                searchResultPage.ClickSizePlusButton();
                
                searchResultPage.SelectSize();
                Log.Information("Size selected");
                
                //ScrollIntoView(driver, driver.FindElement(By.XPath("//h6[text()='color']")));
                searchResultPage.ClickColorPlusButton();
                
                searchResultPage.SelectColor();
                Log.Information("Color selected");
                
                Screenshots.TakeScreenShot(driver);
                IWebElement productname = driver.FindElement(By.XPath("//div[@id='product-"+productId+"']/div/div[3]/a"));
                Console.WriteLine(productname.Text);
                var desiredProductpage=searchResultPage.ClickDesiredProduct(productId);
                Log.Information("Product Selected - "+productname.Text);
                
                Screenshots.TakeScreenShot(driver);

                List<string> lswindow = driver.WindowHandles.ToList();
                if (lswindow.Count > 0)
                {
                    driver.SwitchTo().Window(lswindow[1]);
                }

                desiredProductpage.SelectSizeBox();
                

                ScrollIntoView(driver, desiredProductpage.AddToBasketButton);

                var checkoutPage = desiredProductpage.ClickAddToBasketButton();
                Log.Information("Add to basket button clicked");
                
                Screenshots.TakeScreenShot(driver);

                checkoutPage.ClickCheckOutButton();
                Log.Information("CheckOut button clicked");
                
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
