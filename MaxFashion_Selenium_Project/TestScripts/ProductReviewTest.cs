using MaxFashion_Selenium_Project.PageObjects;
using MaxFashion_Selenium_Project.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MaxFashion_Selenium_Project.TestScripts
{
    [TestFixture]
    internal class ProductReviewTest:CoreCodes
    {
        [Test]
        [Author("Shirin", "shirin@gmail.com")]
        [Order(1)]
        [Category("Regression Test")]
        public void ReviewProductTest()
        {
            MaxFashionHomePage max = new(driver);

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

                max.MouseHoverCategory();
                Thread.Sleep(2000);
                var searchPage=max.ClickMouseHoverCategoryLink();
                Thread.Sleep(2000);
                var desiredProduct=searchPage.ClickDesiredProduct(id);
                Thread.Sleep(2000);
                //var searchResultPage = max.TypeSearchInput(searchtext);
                //Thread.Sleep(1000);
                //var desiredProduct = searchResultPage.ClickDesiredProduct(id);

                List<string> lswindow = driver.WindowHandles.ToList();
                if (lswindow.Count > 0)
                {
                    driver.SwitchTo().Window(lswindow[1]);
                }

                Thread.Sleep(3000);

                //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
              
                //js.ExecuteScript($"window.scrollTo(0, {yOffset});");
                //js.ExecuteScript("arguments[0].scrollIntoView();", desiredProduct.ReviewButton);
                //Actions actions = new Actions(driver);
                //actions.MoveByOffset(653, 3077).Build().Perform();
               
                Thread.Sleep(3000);
                //ScrollIntoView(driver, driver.FindElement(By.XPath("//div[text()='You may also like']")));
                 //ScrollIntoView(driver, desiredProduct.ReviewButton);
                Console.WriteLine(desiredProduct.ReviewButton.Location);
                desiredProduct.ClickReviewButton();
                //driver.SwitchTo().DefaultContent();
                Thread.Sleep(2000);
                desiredProduct.ClickRatingStar(rating);
                desiredProduct.EnterDescriptionInput(description);
                desiredProduct.EnterTitleInput(title);
                Thread.Sleep(2000);
                desiredProduct.ClickSaveButton();
                IWebElement SaveReview = driver.FindElement(By.XPath("//div[text()='Sign up or Sign in']"));
                
                try
                {
                    Assert.That(SaveReview.Text.Contains("Sign up"));
                    //Log.Information("CheckOut Test Passed");
                    test = extent.CreateTest("Review a Product Test - Pass");
                    test.Pass("Review  success");
                    //Console.WriteLine("checkout test-passed");
                }
                catch
                {
                    test = extent.CreateTest("Review a Product Test - Fail");
                    //Log.Error("CheckOut Test Failed!!");
                    test.Fail("Review failed");
                    Console.WriteLine("test-failed");

                }

            }

            }
    }
}
