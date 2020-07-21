using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class MyFirstTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            //driver = new InternetExplorerDriver();
            //driver = new FirefoxDriver();
            driver = new ChromeDriver();

            // явное ожидание появления элемента
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // настройка неявных ожиданий
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

        }

        [Test]
        public void FirstTest()
        {
            driver.Url = "http://www.google.com/";
            driver.FindElement(By.Name("q")).SendKeys("webdriver");
            driver.FindElement(By.Name("btnK")).Submit();

            // явное ожидание появления элемента
            wait.Until(ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }

        /*bool IsElementPresent(WebDriver driver, By locator)
        {
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }
        bool AreElementsPresent(WebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }*/

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
