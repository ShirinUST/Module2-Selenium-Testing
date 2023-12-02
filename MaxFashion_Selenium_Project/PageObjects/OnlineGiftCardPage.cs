using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MaxFashion_Selenium_Project.PageObjects
{
    internal class OnlineGiftCardPage
    {
        IWebDriver driver;
       
        public OnlineGiftCardPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        private DefaultWait<IWebDriver> CreateWait()
        {
           
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(9);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            return wait;
        }
        //Arrange

        [FindsBy(How = How.Id, Using = "quantity")]
        IWebElement QuantityInput { get; set; }

        [FindsBy(How = How.Id, Using = "firstname")]
        IWebElement FirstNameInput { get; set; }

        [FindsBy(How = How.Id, Using = "lastname")]
        IWebElement LastNameInput { get; set; }

        [FindsBy(How = How.Id, Using = "email")]
        IWebElement EmailInput { get; set; }

        [FindsBy(How = How.XPath, Using = "(//input[@id='telephone'])[1]")]
        IWebElement MobileInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[span[div[label[input[@id='sameAsSender']]]]]")]
        IWebElement DetailsCheckbox { get; set; }

        [FindsBy(How = How.Id, Using = "giftMessage")]
        IWebElement MessageInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[label[input[@id='agreeTerms']]]")]
        IWebElement AgreeCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text()='PREVIEW E-GIFT-CARD']")]
        IWebElement PreviewButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[text()='×']")]
        public IWebElement CloseButton { get; set; }

        [FindsBy(How = How.Id, Using = "pay-now-button")]
        IWebElement PayNowButton { get; set; }
        public void ClickMoneyBox(string money)
        {
            IWebElement moneyBoxElement = driver.FindElement(By.XPath("//p[text()='₹"+money+".00']"));
            CreateWait().Until(d => moneyBoxElement.Displayed);
            moneyBoxElement.Click();
        }
        public void EnterQuantityInput(string qty)
        {
           
            CreateWait().Until(d => QuantityInput.Displayed);
            QuantityInput.SendKeys(qty);
        }
        public void ClickDeliveryOptions(string delivery)
        {
            IWebElement deliveryElement = driver.FindElement(By.XPath("//button[div[label[text()='"+delivery+"']]]"));
            
            CreateWait().Until(d => deliveryElement.Displayed);
            deliveryElement.Click();
        }
        public void ClickModeOfDelivery(string mode)
        {
            IWebElement modeElement = driver.FindElement(By.XPath("//button[div[label[text()='" + mode + "']]]"));
         
            CreateWait().Until(d => modeElement.Displayed);
            modeElement.Click();
        }
        public void EnterFirstNameInput(string fname)
        {
           
            CreateWait().Until(d => FirstNameInput.Displayed);
            FirstNameInput.SendKeys(fname);
        }
        public void EnterLastNameInput(string lname)
        {
            
            CreateWait().Until(d => LastNameInput.Displayed);
            LastNameInput.SendKeys(lname);
        }
        public void EnterEmailInput(string email)
        {

            CreateWait().Until(d => EmailInput.Displayed);
            EmailInput.SendKeys(email);
        }
        public void EnterMobileInput(string phone)
        {

            CreateWait().Until(d => MobileInput.Displayed);
            MobileInput.SendKeys(phone);
        }
        public void ClickDetailsCheckbox()
        {

            CreateWait().Until(d => DetailsCheckbox.Displayed);
            DetailsCheckbox.Click();
        }
        public void EnterMessageInput(string message)
        {

            CreateWait().Until(d => MessageInput.Displayed);
            MessageInput.SendKeys(message);
        }
        public void ClickPreviewButton()
        {

            CreateWait().Until(d => PreviewButton.Displayed);
            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //js.ExecuteScript("arguments[0].click();", PreviewButton);
            PreviewButton.Click();
            //driver.FindElement(By.CssSelector("button:contains('PREVIEW E-GIFT-CARD')")).Click();

        }
        public void ClickCloseButton()
        {

            CreateWait().Until(d => CloseButton.Displayed);
            CloseButton.Click();
        }
        public void ClickAgreeCheckbox()
        {

            CreateWait().Until(d => AgreeCheckbox.Displayed);
            AgreeCheckbox.Click();
        }
        public GiftPaymentPage ClickPayNowButton()
        {

            CreateWait().Until(d => PayNowButton.Displayed);
            PayNowButton.Click();
            return new GiftPaymentPage(driver);
        }

    }
}
