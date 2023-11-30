using AventStack.ExtentReports.Model;
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
    internal class SearchResultPage
    {
        IWebDriver driver;
        
        public SearchResultPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }
        //Arrange

        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'MuiButton-contained')][1]")]
        private IWebElement SortByButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[text()='New Arrivals']")]
        private IWebElement SortByButtonCategory { get; set; }

        [FindsBy(How = How.XPath, Using = "//div/h6[text()='Size']/parent::div/div/button")]
        private IWebElement SizePlusButton { get; set; }

        [FindsBy(How = How.XPath, Using = "(//span[span[input[@type='checkbox']]])[position()=2]")]
        private IWebElement SizeCheckBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//div/h6[text()='color']/parent::div/div/button")]
        private IWebElement ColorPlusButton { get; set; }

        [FindsBy(How = How.XPath, Using = "(//span[span[input[@type='checkbox']]])[position()=6]")]
        private IWebElement ColorCheckBox { get; set; }

        //Act

        public void ClickCategoryOfYourChoice(string category)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement categoryElement = wait.Until(d=>d.FindElement(By.XPath("//a[.//span//div[text()='" + category + "']]")));
            categoryElement.Click();
        }
        public void ClickSortByButton()
        {
            SortByButton.Click();
        }
        public void ClickSortByButtonCategory()
        {
            SortByButtonCategory.Click();
        }
        public void ClickSizePlusButton()
        {
            SizePlusButton.Click();
        }
        public void SelectSize()
        {
            SizeCheckBox.Click();
        }
        public void ClickColorPlusButton()
        {
            ColorPlusButton.Click();
        }
        public void SelectColor()
        {
            ColorCheckBox.Click();
        }
        public DesiredProductPage ClickDesiredProduct(string id)
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(9);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            
            IWebElement desiredProductElement = driver.FindElement(By.XPath("//div[@id='product-"+id+"']/div/div/a[1]"));
            wait.Until(d => desiredProductElement.Displayed);
            desiredProductElement.SendKeys(Keys.Enter);
            return new DesiredProductPage(driver);
        }

    }
}
