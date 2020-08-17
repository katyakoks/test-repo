using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace csharp_example
{
    [TestFixture]
    public class MyForthTest
    {
        private IWebDriver driver;

            [SetUp]
        public void start()
        {
            //driver = new InternetExplorerDriver();
            //driver = new FirefoxDriver();
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void ForthTest()
        {
            LoginAdmin();
            GeoCheck();
            
        }
        private void LoginAdmin()
        {
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Submit();
        }
        
        private void GeoCheck()
        {
            
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";

            //проверить, что страны расположены в алфавитном порядке



            
            var actualSortCountries = driver.FindElements(By.CssSelector("tr a")).ToList();
            string name = link.Text();

            var expectedSortCountries = actualSortCountries.OrderBy();
            Assert.IsTrue (expectedSortCountries.SequenceEqual(actualSortCountries), "Страны отсортированы в алфавитном порядке")





            /*для тех стран, у которых количество зон отлично от нуля-- 
            открыть страницу этой страны и там проверить, что зоны расположены в алфавитном порядке

            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

            зайти в каждую из стран и проверить, что зоны расположены в алфавитном порядке*/

        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
