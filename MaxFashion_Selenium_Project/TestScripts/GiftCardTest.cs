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
using Serilog;

namespace MaxFashion_Selenium_Project.TestScripts
{
    [TestFixture,Order(3)]
    internal class GiftCardTest:CoreCodes
    {
        [Test]
        [Author("Shirin", "shirin@gmail.com")]
        [Order(1)]
        [Category("Regression Test")]
        public void OnlineGiftCardTest()
        {
            LogUpdates();
            MaxFashionHomePage max = new(driver);

            //for excel
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/TestData/InputData.xlsx";
            string? sheetName = "OnlineGiftCardSheet";
            List<ExcelGiftCard> excelDataList = ExcelUtils.ReadExcelDataGiftCard(excelFilePath, sheetName);
            foreach (var item in excelDataList)
            {
                //if(!driver.Url.Equals("https://www.maxfashion.in/in/en/"))
                //{
                //    driver.Navigate().GoToUrl("https://www.maxfashion.in/in/en/");
                //}

                string? money = item.Money;
                string? quantity = item.Quantity;
                string? delivery = item.Delivery;
                string? mode = item.Mode;
                string? fname = item.FirstName;
                string? lname = item.LastName;
                string? email = item.Email;
                string? mobile = item.Mobile;
                string? message = item.Message;

                Thread.Sleep(1000);
                max.MouseHoverMore();
                Thread.Sleep(1000);
                var giftCardPage=max.ClickMouseHoverMoreLink();
                //Thread.Sleep(1000);
                //ScrollIntoView(driver, driver.FindElement(By.XPath("//hr[@data-content='Enter Denomination']")));
                Log.Information("Entering details for Gift card purchasing");
                giftCardPage.ClickMoneyBox(money);
                giftCardPage.EnterQuantityInput(quantity);
                giftCardPage.ClickDeliveryOptions(delivery);
                giftCardPage.ClickModeOfDelivery(mode);
                Log.Information("Selected deliver options as" + delivery+" and mode of delivery as "+mode);
                giftCardPage.EnterFirstNameInput(fname);
                giftCardPage.EnterLastNameInput(lname);
                giftCardPage.EnterEmailInput(email);
                giftCardPage.EnterMobileInput(mobile);
                if (delivery == "Send As Gift")
                {
                    giftCardPage.ClickDetailsCheckbox();
                    giftCardPage.EnterMessageInput(message);
                }
                Screenshots.TakeScreenShot(driver);
                //giftCardPage.ClickPreviewButton();
                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                //wait.Until(ExpectedConditions.ElementToBeClickable(giftCardPage.CloseButton));
                Thread.Sleep(3000);
                //giftCardPage.ClickCloseButton();
                giftCardPage.ClickAgreeCheckbox();
                var payPage=giftCardPage.ClickPayNowButton();
                Log.Information("Clicked Pay Now button");
                Thread.Sleep(15000);
                Screenshots.TakeScreenShot(driver);
                try
                {
                    Assert.AreEqual(driver.Title,"Payment Page");
                    Log.Information("Gift Card Purchased Completed- Test Passed");
                    test = extent.CreateTest("Gift Card Test - Pass");
                    test.Pass("Purchasing a gift card test -  success");
                    ////Console.WriteLine("checkout test-passed");
                }
                catch
                {
                    test = extent.CreateTest("Gift Card Test - Fail");
                    Log.Error("Gift Card Test Failed!!");
                    test.Fail("Purchasing a gift card test - failed");
                    
                }

            }

        }
    }
}
