using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFashion_Selenium_Project.PageObjects
{
    internal class CheckoutPage
    {
        IWebDriver driver;
        public CheckoutPage(IWebDriver driver)
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

        [FindsBy(How = How.XPath, Using = "//button[span[text()='Checkout now']]")]
        IWebElement CheckOutButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[text()='Sign up or Sign in']")]
        public IWebElement CheckOutConfirm { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[span[text()='Remove']]")]
        IWebElement RemoveButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[span[text()='REMOVE']]")]
        IWebElement RemoveConfirmButton { get; set; }

        //Act

        public void ClickCheckOutButton()
        {
            
            CreateWait().Until(d => CheckOutButton.Displayed);
            CheckOutButton.Click();
            
        }
        public void ClickRemoveButton()
        {
            
            CreateWait().Until(d => RemoveButton.Displayed);
            RemoveButton.Click();
        }
        public void ClickRemoveConfirmButton()
        {
            RemoveConfirmButton.Click();
        }
    }
}
