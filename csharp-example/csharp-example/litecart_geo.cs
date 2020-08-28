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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        }

        [Test]
        public void GeoSorting()
        {
            CountryCheck();
            ZoneCheck();
        }
        /// <summary>
        /// логин
        /// </summary>
        private void LoginAdmin()

        {
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Submit();
        }
        /// <summary>
        /// проверить, что страны расположены в алфавитном порядке, а если количество зон отлично от нуля -
        /// открыть страницу и проверить, что зоны расположены в алфавитном порядке
        /// </summary>
        private void CountryCheck()

        {
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            LoginAdmin();

            //список стран
            var countries = driver.FindElements(By.XPath("//tr/td[5]/a[1]"));

            //кол-во стран в списке
            int countCountries = countries.Count;

            //актуальный список стран
            var actualCountries = new List<string>();

            //добавление стран в список
            for (int i = 1; i <= countCountries; i++)
            {
                string country = driver.FindElement(By.XPath("//tr/td[5]/a[1]")).GetAttribute("text");
                actualCountries.Add(country);
            }

            //ожидаемый список стран
            var expectedCountries = new List<string>(actualCountries);
            expectedCountries.Sort();

            //проверка
            Assert.IsTrue(actualCountries.SequenceEqual(expectedCountries), "Страны расположены в алфавитном порядке");

            //перебор стран с зонами > 0
            for (int i = 1; i <= countCountries; i++)
            {
                //поиск количества зон
                string contentZones = driver.FindElement(By.XPath("//tr/td[@class='text-center' and 6]")).GetAttribute("textContent");
                
                //актуальный список стран с зонами
                var actualZones = new List<string>();

                //условие для стран с зонами
                if (contentZones != "0")
                {
                    //переход на страницу страны с зонами
                    driver.FindElement(By.XPath("//tr/td[5]/a[1]")).Click();

                    //поиск зон на странице
                    var zones = driver.FindElements(By.XPath("//td[3]/input[@class='form-control' and 1]"));
                    
                    //кол-во зон
                    int countZones = zones.Count;

                    //перебор зон
                    for (int j = 1; j <= countZones; j++)
                    {
                        //поиск названия зоны
                        string zone = driver.FindElement(By.XPath("//td[3]/input[@class='form-control' and 1]")).GetAttribute("value");
                        
                        //добавление зоны в актуальный список
                        actualZones.Add(zone);
                    }

                    //ожидаемый список зон
                    var expectedZones = new List<string>(actualZones);
                    expectedZones.Sort();

                    //проверка
                    Assert.IsTrue(actualZones.SequenceEqual(expectedZones), "Страны расположены в алфавитном порядке");
                    driver.Navigate().Back();
                }
            }
        }

        /// <summary>
        /// зайти в каждую из зон и проверить, что страны расположены в алфавитном порядке
        /// </summary>
        private void ZoneCheck()

        {
            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

            //поиск зон
            var zones = driver.FindElements(By.XPath("//td[3]/a[1]"));

            //количество зон
            int countZones = zones.Count;

            //перебор зон
            for (int i = 1; i <= countZones; i++)
            {
                //переход на страницу зоны
                driver.FindElement(By.XPath("//td[3]/a[1]")).Click();

                //поиск стран
                var countries = driver.FindElements(By.XPath("//td[2]"));

                //актуальный список стран
                var actualCountries = new List<string>();

                //перебор стран в зоне
                foreach (IWebElement countrie in countries)
                {
                    //актуальный список названий стран
                    string countrieName = countrie.GetAttribute("text");
                    actualCountries.Add(countrieName);
                }

                //ожидаемый список стран
                var expectedCountries = new List<string>(actualCountries);
                expectedCountries.Sort();

                //проверка
                Assert.IsTrue(actualCountries.SequenceEqual(expectedCountries), "Зоны расположены в алфавитном порядке");
                driver.Navigate().Back();
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
