using MaxFashion_Selenium_Project.Utilities;
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
    internal class DesiredProductPage
    {
        IWebDriver driver;
        public DesiredProductPage(IWebDriver driver)
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

        [FindsBy(How = How.XPath, Using = "//button[span[text()='M']]")]
        IWebElement SizeBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[span[text()='ADD TO BASKET']]")]
        public IWebElement AddToBasketButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[span[text()='WRITE A REVIEW']]")]
        [CacheLookup]
        public IWebElement ReviewButton { get; set; }

        [FindsBy(How = How.Id, Using = "review-contain")]
        [CacheLookup]
        public IWebElement DescriptionInput { get; set; }

        [FindsBy(How = How.Id, Using = "review-title")]
        [CacheLookup]
        public IWebElement TitleInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[span[text()='SAVE']]")]
        [CacheLookup]
        public IWebElement SaveButton { get; set; }
        public void SelectSizeBox()
        {
            
            CreateWait().Until(d => SizeBox.Displayed);
            SizeBox.SendKeys(Keys.Enter);
        }
        public CheckoutPage ClickAddToBasketButton()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", AddToBasketButton);
            //AddToBasketButton.Click();
            return new CheckoutPage(driver);
        }
        public void ClickReviewButton()
        {
            
            CreateWait().Until(d => ReviewButton.Displayed);
            ReviewButton.Click();
        }
        public void ClickRatingStar(string rating)
        {
            IWebElement RatingStar = driver.FindElement(By.Id("reviewRating-"+rating));
            
            CreateWait().Until(d => RatingStar.Displayed);
            RatingStar.SendKeys(Keys.Enter);
        }
        public void EnterDescriptionInput(string description)
        {
            DescriptionInput.SendKeys(description);
        }
        public void EnterTitleInput(string title)
        {
            TitleInput.SendKeys(title);
        }
        public void ClickSaveButton()
        {
            SaveButton.Click();
        }
    }
}
