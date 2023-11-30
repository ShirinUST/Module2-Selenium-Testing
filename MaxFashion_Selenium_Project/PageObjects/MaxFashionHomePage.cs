using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFashion_Selenium_Project.PageObjects
{
    internal class MaxFashionHomePage
    {
        IWebDriver driver;
        public MaxFashionHomePage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }
        //Arrange

        [FindsBy(How =How.Id,Using = "js-site-search-input")]
        IWebElement SearchInputBox { get; set; }

        [FindsBy(How = How.Id, Using = "dept-girls")]
        IWebElement CategoryHover { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[span[div[text()='More']]]")]
        IWebElement MoreHover { get; set; }

        
        //Act
        public SearchResultPage TypeSearchInput(string product)
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(9);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(d => SearchInputBox.Displayed);
            SearchInputBox.Clear();
            SearchInputBox.SendKeys(product);
            SearchInputBox.SendKeys(Keys.Enter);
            return new SearchResultPage(driver);
        }
        public void MouseHoverCategory()
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(9);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(d => CategoryHover.Displayed);
            Actions actions=new Actions(driver);
            Action mouseOverClick = () => actions.MoveToElement(CategoryHover)
            .Build().Perform();
            mouseOverClick.Invoke();

        }
        public SearchResultPage ClickMouseHoverCategoryLink()
        {
            IWebElement hoverElement = driver.FindElement(By.Id("category-menu-2"));
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(9);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(d => hoverElement.Displayed);
            IWebElement webElement = hoverElement.FindElement(By.XPath("//a[text()='Essentials']"));
            webElement.Click();
            return new SearchResultPage(driver);
        }
        public void MouseHoverMore()
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(9);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(d => MoreHover.Displayed);
            Actions actions = new Actions(driver);
            Action mouseOverClick = () => actions.MoveToElement(MoreHover)
            .Build().Perform();
            mouseOverClick.Invoke();

        }
        public OnlineGiftCardPage ClickMouseHoverMoreLink()
        {
            IWebElement hoverElement = driver.FindElement(By.XPath("//div[contains(@class,'subcategory')]"));
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(9);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(d => hoverElement.Displayed);
            IWebElement webElement = hoverElement.FindElement(By.XPath("//a[text()='Online Gift Card']"));
            webElement.Click();
            return new OnlineGiftCardPage(driver);
        }
    }
}
