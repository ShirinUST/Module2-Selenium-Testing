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
        //Arrange

        [FindsBy(How = How.XPath, Using = "//button[span[text()='Checkout now']]")]
        IWebElement CheckOutButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[text()='Sign up or Sign in']")]
        public IWebElement CheckOutConfirm { get; set; }

        //Act

        public void ClickCheckOutButton()
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(9);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(d => CheckOutButton.Displayed);
            CheckOutButton.Click();
            
        }
    }
}
