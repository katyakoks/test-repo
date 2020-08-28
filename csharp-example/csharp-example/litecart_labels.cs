using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace csharp_example
{
    [TestFixture]
    public class MyThirdTest
    {
        private IWebDriver driver;
        private string startUrl;

        [SetUp]
        public void start()
        {
            //driver = new InternetExplorerDriver();
            //driver = new FirefoxDriver();
            driver = new ChromeDriver();
            startUrl = "http://localhost/litecart/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void ThirdTest()
        {
            driver.Url = startUrl;
            LabelCheck();
        }
        /// <summary>
        /// проверяется, что у каждого товара имеется ровно один стикер.
        /// </summary>
        private void LabelCheck()
        {
            int productWithLabel = 0;

            var productToCheck = driver.FindElements(By.ClassName("product-column"));
            if (AreElementsPresent(By.ClassName("product-column")))
            {
                //считается кол-во товаров с 1 стикером
                for (int i = 0; i < productToCheck.Count; i++)
                {
                    var label = productToCheck[i].FindElements(By.CssSelector("div.sticker"));
                    if (label.Count == 1)
                    {
                        productWithLabel++;
                    }
                }
                Assert.IsTrue(productWithLabel != productToCheck.Count, "Не все товары имеют по 1 стикеру");
            }
        }
        private bool AreElementsPresent(By by)
        { 
            return driver.FindElements(by).Count > 0; 
        }
        
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
