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
    [TestFixture]
    internal class SearchProductAndAddToClassTest:CoreCodes
    {
        [Test]
        [Author("Shirin","shirin@gmail.com")]
        [Order(1)]
        [Category("Regression Test")]
        public void ProductSearchAndAddToCartTest()
        {
            //For Log
            string directory = Directory.GetParent(@"../../../").FullName;
            string logfilepath = directory + "/Logs/log_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            Log.Logger = new LoggerConfiguration().
                WriteTo.Console().
                WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day).
                CreateLogger();

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
                Console.WriteLine("Searching item: "+searchtext);
                Thread.Sleep(2000);
                Screenshots.TakeScreenShot(driver);
                try
                {
                    Assert.That(driver.Url.Contains(searchtext));
                    Log.Information("Search Test Passed");
                    test = extent.CreateTest("Search a Product Test - Pass");
                    test.Pass("searching a " + searchtext + " success");
                    Console.WriteLine("Search test-passed");
                }
                catch
                {
                    test = extent.CreateTest("Search a Product Test- Fail");
                    Log.Error("Search Test Failed!!");
                    test.Fail("searching a " + searchtext + " failed");
                    Console.WriteLine("Search test-failed");

                }

                searchResultPage.ClickCategoryOfYourChoice(category);
                Thread.Sleep(2000);
                searchResultPage.ClickSortByButton();
                Thread.Sleep(2000);
                searchResultPage.ClickSortByButtonCategory();
                Thread.Sleep(2000);
                searchResultPage.ClickSizePlusButton();
                Thread.Sleep(2000);
                searchResultPage.SelectSize();
                Thread.Sleep(2000);
                //ScrollIntoView(driver, driver.FindElement(By.XPath("//h6[text()='color']")));
                searchResultPage.ClickColorPlusButton();
                Thread.Sleep(2000);
                searchResultPage.SelectColor();
                Thread.Sleep(2000);
                Screenshots.TakeScreenShot(driver);
                IWebElement productname = driver.FindElement(By.XPath("//div[@id='product-"+productId+"']/div/div[3]/a"));
                Console.WriteLine(productname.Text);
                var desiredProductpage=searchResultPage.ClickDesiredProduct(productId);
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
                Thread.Sleep(4000);
                Screenshots.TakeScreenShot(driver);
                try
                {
                    Assert.That(driver.FindElement(By.XPath("//h1[text()='Your Shopping Basket']")).Text.Contains("Basket"));
                    Log.Information("Added to Basket Test Passed");
                    test = extent.CreateTest("Added to Basket Test - Pass");
                    test.Pass("Added to cart success");
                    //Console.WriteLine("checkout test-passed");
                }
                catch
                {
                    test = extent.CreateTest("Added to Basket Test - Fail");
                    Log.Error("Added to Basket Test Failed!!");
                    test.Fail("Added to cart failed");
                    Console.WriteLine("test-failed");

                }

                checkoutPage.ClickCheckOutButton();
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
                    test.Fail("checkout failed");
                    Console.WriteLine("test-failed");

                }
                Log.CloseAndFlush();
            }
        }
    }
}
