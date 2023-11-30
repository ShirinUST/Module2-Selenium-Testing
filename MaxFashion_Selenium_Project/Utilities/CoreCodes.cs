using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFashion_Selenium_Project.Utilities
{
    internal class CoreCodes
    {
        
        public IWebDriver driver;

        public ExtentReports extent;
        ExtentSparkReporter sparkReporter;
        public ExtentTest test;
        
        [OneTimeSetUp]
        public void InitializeBrowser()
        {
            string currdir = Directory.GetParent(@"../../../").FullName;
            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currdir + "/ExtentReports/extent-report"
                + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");

            extent.AttachReporter(sparkReporter);

            ReadConfig.ReadConfigSettings();
            if (ReadConfig.properties?["browser"].ToLower() == "chrome")
            {
                driver = new ChromeDriver();
            }
            else if (ReadConfig.properties?["browser"].ToLower() == "edge")
            {
                driver = new EdgeDriver();
            }
            driver.Url = ReadConfig.properties?["baseUrl"];
            driver.Manage().Window.Maximize();
        }
        
        public static void ScrollIntoView(IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);

        }

        [OneTimeTearDown]
        public void Destruct()
        {
            driver.Quit();
            extent.Flush();
        }
    }
}
