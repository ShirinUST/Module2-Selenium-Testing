using MaxFashion_Selenium_Project.PageObjects;
using MaxFashion_Selenium_Project.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MaxFashion_Selenium_Project.TestScripts
{
    [TestFixture,Order(2)]
    internal class ProductReviewTest:CoreCodes
    {
        [Test]
        [Author("Shirin", "shirin@gmail.com")]
        [Order(1)]
        [Category("Regression Test")]
        public void ReviewProductTest()
        {
            MaxFashionHomePage max = new(driver);
            LogUpdates();
            //for excel
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/TestData/InputData.xlsx";
            string? sheetName = "RatingSheet";
            List<ExcelReviewProduct> excelDataList = ExcelUtils.ReadExcelDataReview(excelFilePath, sheetName);
            foreach (var item in excelDataList)
            {
                string? searchtext = item.SearchText;
                string? rating = item.Rating;
                string? description = item.Description;
                string? title = item.Title;
                string? id = item.ProductId;
                Log.Information("Review a product test started");
                max.MouseHoverCategory();
                
                Log.Information("Mouse hovered on category");
                var searchPage=max.ClickMouseHoverCategoryLink();
                
                Screenshots.TakeScreenShot(driver);
                Log.Information("Clicked the Category");
                var desiredProduct=searchPage.ClickDesiredProduct(id);
                

                List<string> lswindow = driver.WindowHandles.ToList();
                if (lswindow.Count > 0)
                {
                    driver.SwitchTo().Window(lswindow[1]);
                }

                
                Log.Information("Clicked the product");
                //ScrollIntoView(driver, driver.FindElement(By.XPath("//div[text()='You may also like']")));
                 //ScrollIntoView(driver, desiredProduct.ReviewButton);
                Console.WriteLine(desiredProduct.ReviewButton.Location);
                desiredProduct.ClickReviewButton();
                Log.Information("Clicked the Review Button");
                //driver.SwitchTo().DefaultContent();
                
                Screenshots.TakeScreenShot(driver);
                Log.Information("Adding review started");
                desiredProduct.ClickRatingStar(rating);
                desiredProduct.EnterDescriptionInput(description);
                desiredProduct.EnterTitleInput(title);
                
                Screenshots.TakeScreenShot(driver);
                Log.Information("Completed giving review");
                desiredProduct.ClickSaveButton();
                Log.Information("Clicked the Save Button");
                Screenshots.TakeScreenShot(driver);
                IWebElement SaveReview = driver.FindElement(By.XPath("//div[text()='Sign up or Sign in']"));
                
                try
                {
                    Assert.That(SaveReview.Text.Contains("Sign up"));
                    Log.Information("Review a Product Test Passed");
                    test = extent.CreateTest("Review a Product Test - Pass");
                    test.Pass("Review test passed");
                    //Console.WriteLine("checkout test-passed");
                }
                catch
                {
                    test = extent.CreateTest("Review a Product Test - Fail");
                    Log.Error("Review a Product Test Failed!!");
                    test.Fail("Review test failed");
                    Console.WriteLine("test-failed");

                }

            }

            }
    }
}
