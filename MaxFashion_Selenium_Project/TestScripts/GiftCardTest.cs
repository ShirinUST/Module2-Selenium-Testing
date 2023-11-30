using MaxFashion_Selenium_Project.PageObjects;
using MaxFashion_Selenium_Project.Utilities;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFashion_Selenium_Project.TestScripts
{
    internal class GiftCardTest:CoreCodes
    {
        [Test]
        [Author("Shirin", "shirin@gmail.com")]
        [Order(1)]
        [Category("Regression Test")]
        public void OnlineGiftCardTest()
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
                max.MouseHoverMore();
                Thread.Sleep(1000);
                max.ClickMouseHoverMoreLink();
                Thread.Sleep(1000);
                ScrollIntoView(driver, driver.FindElement(By.XPath("//hr[@data-content='Enter Denomination']")));
                try
                {
                    //Assert.That(SaveReview.Text.Contains("Sign up"));
                    ////Log.Information("CheckOut Test Passed");
                    //test = extent.CreateTest("Review a Product Test - Pass");
                    //test.Pass("Review  success");
                    ////Console.WriteLine("checkout test-passed");
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
