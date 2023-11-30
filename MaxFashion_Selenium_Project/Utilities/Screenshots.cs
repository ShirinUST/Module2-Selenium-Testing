using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFashion_Selenium_Project.Utilities
{
    internal class Screenshots
    {
        public static void TakeScreenShot(IWebDriver driver)
        {
            ITakesScreenshot screenshot = (ITakesScreenshot)driver;
            Screenshot shot = screenshot.GetScreenshot();
            string directory = Directory.GetParent(@"../../../").FullName;
            string fileName = directory + "/Screenshots/s_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            shot.SaveAsFile(fileName);
            Console.WriteLine("Screenshot captured");
        }
    }
}
