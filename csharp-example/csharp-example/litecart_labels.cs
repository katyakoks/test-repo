using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class MyThirdTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

            [SetUp]
        public void start()
        {
            //driver = new InternetExplorerDriver();
            //driver = new FirefoxDriver();
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            }

        [Test]
        public void ThirdTest()
        { 
            driver.Url = "http://localhost/litecart/";
            wait.Until(ExpectedConditions.TitleContains("My Store"));

            //наличие у всех товаров одной полоски в левом верхнем углу изображения
            var productToCheck = driver.FindElements(By.ClassName("product-column")).ToList();
            int productCount = productToCheck.Count;
            var label = driver.FindElement(By.ClassName("sticker"));

            for (int i = 0; i <= productCount; i++)
            {
                productToCheck = driver.FindElements(By.ClassName("product-column")).ToList();
                productToCheck.Contains(label);
                Assert.IsTrue(label.Displayed);
            }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
