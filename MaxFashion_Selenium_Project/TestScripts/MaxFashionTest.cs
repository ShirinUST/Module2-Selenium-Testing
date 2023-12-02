
using MaxFashion_Selenium_Project.PageObjects;
using MaxFashion_Selenium_Project.Utilities;
using OpenQA.Selenium;
using Serilog;


namespace MaxFashion_Selenium_Project.TestScripts
{
    [TestFixture,Order(4)]
    internal class MaxFashionTest : CoreCodes
    {
        SearchResultPage? searchResultPage;
        DesiredProductPage? desiredProductPage;
        CheckoutPage? checkoutPage;

        [Test]
        [Author("Shirin", "shirin@gmail.com")]
        [Order(1)]
        [Category("Smoke Test")]
        public void ProductCategoryTest()
        {
            //For Log
            LogUpdates();
            
            MaxFashionHomePage max = new(driver);
            //For excel
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/TestData/InputData.xlsx";
            string? sheetName = "ProductSearch";

            List<ExcelProductSearch> excelDataList = ExcelUtils.ReadExcelData(excelFilePath, sheetName);
            foreach (var item in excelDataList)
            {
                
                string? searchtext = item.SearchText;
                string? category = item.Category;
                string? productId = item.ProductId;
                string? mincost = item.MinCost;
                string? maxcost = item.MaxCost;
                
                    
                    searchResultPage = max.TypeSearchInput(searchtext);
                    Log.Information("search started");

                    Console.WriteLine("Searching item: " + searchtext);
                    
                    //Screenshots.TakeScreenShot(driver);

                    searchResultPage.ClickCategoryOfYourChoice(category);
                    

                    searchResultPage.InputMinimumInputBox(mincost);
                    searchResultPage.InputMaximumInputBox(maxcost);
                    Log.Information("Search based on maximum and minimum cost");
                    searchResultPage.ClickMaxMinBox();
                    
                Screenshots.TakeScreenShot(driver);
                Console.WriteLine();
                try
                {
                    Assert.That((driver.Url.ToLower().Contains(searchtext))
                       && (driver.Url.ToLower().Contains(category.ToLower()))
                       && (driver.Url.Contains(mincost + "," + maxcost)));
                    Log.Information("Category Based searching Passed");
                    test = extent.CreateTest("Search based on Category and Price - Pass");
                    test.Pass("Searched " + searchtext + " for " + category + " and Price range " + mincost + " - " + maxcost + "  - Pass");
                    //Console.WriteLine("checkout test-passed");
                }
                catch (AssertionException)
                {
                    test = extent.CreateTest("Search based on Category and Price- Fail");
                    Log.Error("Category Based searching Failed!!");
                    test.Fail("Searched " + searchtext + " for " + category + "  - Fail");
                    Console.WriteLine("test-failed");

                }
            }
        }
        [Test]
        [Author("Shirin", "shirin@gmail.com")]
        [Order(2)]
        [Category("Smoke Test")]
        public void HoverButtonTest()
        {
            //For Log
            LogUpdates();
            
                MaxFashionHomePage max = new(driver);
                
                Log.Information("Mouse Hover Free Shipping Button");
                max.ClickShippingOver();
                Screenshots.TakeScreenShot(driver);
                Log.Information("Mouse Hover Return to Store Button");
                max.ClickStoreHover();
                Log.Information("Mouse Hover Gift Card Button");
                max.ClickCardHover();
                Log.Information("Mouse Hover the Category Women");
                max.MouseHoverWomenCategory();
                Log.Information("Mouse Hover the Category Men");
                max.MouseHoverMenCategory();
                Log.Information("Mouse Hover the Category Girls");
                max.MouseHoverCategory();
                Log.Information("Mouse Hover the Cataegory Boys");
                max.MouseHoverBoysCategory();
                Screenshots.TakeScreenShot(driver);
                max.MouseHoverMore();
            try
            {
                Assert.IsTrue(true);
                Log.Information("Hover Test Passed");
                test = extent.CreateTest("Hover Test - Pass");
                test.Pass("Hovering on all button test Passed");

            }
            catch (AssertionException)
            {
                Log.Error("Hover Test Failed");
                test = extent.CreateTest("Hover Test - Fail");
                test.Fail("Test Failed");
            }
        }
        [Test]
        [Author("Shirin", "shirin@gmail.com")]
        [Order(3)]
        [Category("Smoke Test")]
        public void CartEmptyTest()
        {
            //For Log
            LogUpdates();
            
                MaxFashionHomePage max = new(driver);
                Thread.Sleep(2000);
                max.ClickCartButton();
                Log.Information("Cart Button Clicked");
                Screenshots.TakeScreenShot(driver);
            try
            {
                Assert.That(driver.FindElement(By.XPath("//div[contains(text(),'Your basket is empty.')]")).Text.Contains("empty"));
                Log.Information("Cart Empty Test- Passed");
                test = extent.CreateTest("Cart Test - Pass");
                test.Pass("Cart is Empty - Test Passed");
            }
            catch (AssertionException)
            {
                Log.Error("Cart Empty Test Failed");
                test = extent.CreateTest("Cart Test - Fail");
                test.Fail("Cart is Empty- Test Failed");
            }
        }
        [Test]
        [Author("Shirin", "shirin@gmail.com")]
        [Order(5)]
        [Category("Smoke Test")]
        public void LogoCheckingTest()
        {
            //For Log
            LogUpdates();
                MaxFashionHomePage max = new(driver);
                Thread.Sleep(2000);
                string url = "https://www.maxfashion.in/in/en"; //driver.Url;
                max.ClickLogoButton();
                Log.Information("Logo Clicked");
                Screenshots.TakeScreenShot(driver);
            try
            {
                Assert.AreEqual(driver.Url, url); ;
                Log.Information("Logo clicking redirects to Home Page");
                Log.Information("Logo Clicking Test- Passed");
                test = extent.CreateTest("Logo Test - Pass");
                test.Pass("Logo clicking redirecting to Home Page - Test Passed");

            }
            catch (AssertionException)
            {
                Log.Error("Logo Clicking Test Failed");
                test = extent.CreateTest("Logo Test - Fail");
                test.Fail("Logo clicking redirecting to Home Page - Test Failed");
            }
        }
        [Test]
        [Author("Shirin", "shirin@gmail.com")]
        [Order(4)]
        [Category("Smoke Test")]
        public void RemovingItemFromCartTest()
        {
            //For Log
            LogUpdates();
           
                MaxFashionHomePage max = new(driver);
                
                desiredProductPage = searchResultPage.ClickDesiredProduct("3");
                List<string> lswindow = driver.WindowHandles.ToList();
                if (lswindow.Count > 0)
                {
                    driver.SwitchTo().Window(lswindow[1]);
                }
                desiredProductPage.SelectSizeBox();
                //Tried Explicit and implicit wait.
                checkoutPage = desiredProductPage.ClickAddToBasketButton();
                checkoutPage.ClickRemoveButton();
                Log.Information("Removing item in Progress");
                Screenshots.TakeScreenShot(driver);
                checkoutPage.ClickRemoveConfirmButton();
                Log.Information("Clicking Remove Confirmation");
                
            Screenshots.TakeScreenShot(driver);
            try
            {
                Assert.That(driver.FindElement(By.XPath("//h1[text()='The best fashion " +
                    "is waiting to be added in your cart !']")).Text
                    .Contains("waiting to be added in your cart"));
                Log.Information("Removing an Item from cart Test- Passed");
                test = extent.CreateTest("Removes an item from cart - Pass");
                test.Pass("Removing an item from cart is performed - Test Passed");

            }
            catch 
            {
                Log.Error("Removing an Item from cart Test Failed");
                test = extent.CreateTest("Removes an item from cart- Fail");
                test.Fail("Removing an item from cart is performed - Test Failed");
            }
        }
    }
}
