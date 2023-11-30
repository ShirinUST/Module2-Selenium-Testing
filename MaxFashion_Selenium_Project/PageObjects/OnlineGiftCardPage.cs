using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //Arrange

        [FindsBy(How = How.XPath, Using = "//hr[@data-content='Enter Denomination']")]
        IWebElement gfd { get; set; }
    }
}
