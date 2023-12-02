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
        private DefaultWait<IWebDriver> CreateWait()
        {

            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(9);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            return wait;
        }
        //Arrange

        [FindsBy(How =How.Id,Using = "js-site-search-input")]
        IWebElement SearchInputBox { get; set; }

        [FindsBy(How = How.Id, Using = "dept-girls")]
        IWebElement CategoryHover { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[span[div[text()='More']]]")]
        IWebElement MoreHover { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[span[div[text()='Free Shipping']]]")]
        IWebElement ShippingHover { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[span[div[text()='Return to Store']]]")]
        IWebElement StoreHover { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[span[div[text()='Online Gift Card']]]")]
        IWebElement CardHover { get; set; }

        [FindsBy(How = How.Id, Using = "dept-women")]
        IWebElement WomenCategoryHover { get; set; }

        [FindsBy(How = How.Id, Using = "dept-men")]
        IWebElement MenCategoryHover { get; set; }

        [FindsBy(How = How.Id, Using = "dept-boys")]
        IWebElement BoysCategoryHover { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='root-nav-mini-basket']/div/button")]
        IWebElement CartButton{ get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@aria-label='max'][1]")]
        IWebElement MaxLogo { get; set; }

        //Act
        public SearchResultPage TypeSearchInput(string product)
        {
            
            CreateWait().Until(d => SearchInputBox.Displayed);
            SearchInputBox.Clear();
            SearchInputBox.SendKeys(product);
            SearchInputBox.SendKeys(Keys.Enter);
            return new SearchResultPage(driver);
        }
        public void MouseHoverCategory()
        {
           
            CreateWait().Until(d => CategoryHover.Displayed);
            Actions actions=new Actions(driver);
            Action mouseOverClick = () => actions.MoveToElement(CategoryHover)
            .Build().Perform();
            mouseOverClick.Invoke();

        }
        public SearchResultPage ClickMouseHoverCategoryLink()
        {
            IWebElement hoverElement = driver.FindElement(By.Id("category-menu-2"));
            CreateWait().Until(d => hoverElement.Displayed);
            IWebElement webElement = hoverElement.FindElement(By.XPath("//a[text()='Essentials']"));
            webElement.Click();
            return new SearchResultPage(driver);
        }
        public void MouseHoverMore()
        {
            CreateWait().Until(d => MoreHover.Displayed);
            Actions actions = new Actions(driver);
            Action mouseOverClick = () => actions.MoveToElement(MoreHover)
            .Build().Perform();
            mouseOverClick.Invoke();

        }
        public OnlineGiftCardPage ClickMouseHoverMoreLink()
        {
            IWebElement hoverElement = driver.FindElement(By.XPath("//div[contains(@class,'subcategory')]"));
            CreateWait().Until(d => hoverElement.Displayed);
            IWebElement webElement = hoverElement.FindElement(By.XPath("//a[text()='Online Gift Card']"));
            webElement.Click();
            return new OnlineGiftCardPage(driver);
        }
        public void ClickShippingOver()
        {
           
            CreateWait().Until(d => ShippingHover.Displayed);
            Actions actions = new Actions(driver);
            Action mouseOverClick = () => actions.MoveToElement(ShippingHover)
            .Build().Perform();
            mouseOverClick.Invoke();
        }
        public void ClickStoreHover()
        {
            Actions actions = new Actions(driver);
            Action mouseOverClick = () => actions.MoveToElement(StoreHover)
            .Build().Perform();
            mouseOverClick.Invoke();
        }
        public void ClickCardHover()
        {
            Actions actions = new Actions(driver);
            Action mouseOverClick = () => actions.MoveToElement(CardHover)
            .Build().Perform();
            mouseOverClick.Invoke();
        }
        public void MouseHoverWomenCategory()
        {
            Actions actions = new Actions(driver);
            Action mouseOverClick = () => actions.MoveToElement(WomenCategoryHover)
            .Build().Perform();
            mouseOverClick.Invoke();

        }
        public void MouseHoverMenCategory()
        {
            Actions actions = new Actions(driver);
            Action mouseOverClick = () => actions.MoveToElement(MenCategoryHover)
            .Build().Perform();
            mouseOverClick.Invoke();

        }
        public void MouseHoverBoysCategory()
        {
            Actions actions = new Actions(driver);
            Action mouseOverClick = () => actions.MoveToElement(BoysCategoryHover)
            .Build().Perform();
            mouseOverClick.Invoke();
        }
        public void ClickCartButton()
        {
            CartButton.Click();
        }
        public void ClickLogoButton()
        {
            MaxLogo.Click();
        }

    }
}
