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
        /// Проверить, что страны расположены в алфавитном порядке, а если количество зон отлично от нуля -
        /// открыть страницу и проверить, что зоны расположены в алфавитном порядке
        /// </summary>
        private void CountryCheck()

        {
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            LoginAdmin();

            //Получение списка веб-элементов, содержащих страны
            var countries = driver.FindElements(By.XPath("//td[5]/a[1]"));

            //Кол-во стран в списке
            int countCountries = countries.Count;

            //Актуальный список стран
            var actualCountries = new List<string>();

            if (AreElementsPresent(By.XPath("//td[5]/a[1]")))
            {
                //Добавление названий стран в актуальный список
                for (int i = 1; i <= countCountries; i++)
                {
                    string country = driver.FindElement(By.XPath("//td[5]/a[1]")).GetAttribute("text");
                    actualCountries.Add(country);
                }

                //Ожидаемый список стран
                var expectedCountries = new List<string>(actualCountries);
                expectedCountries.Sort();

                //Проверка равнозначности актуального и ожидаемого списков
                Assert.IsTrue(actualCountries.SequenceEqual(expectedCountries), "Страны расположены не в алфавитном порядке");

                //Перебор стран с зонами > 0
                for (int i = 1; i <= countCountries; i++)
                {
                    //поиск количества зон
                    string contentZones = driver.FindElement(By.XPath("//tr/td[@class='text-center' and 6]")).GetAttribute("textContent");
                    
                    //Актуальный список стран с зонами
                    var actualZones = new List<string>();

                    //Условие для стран с зонами
                    if (contentZones != "0")
                    {
                        //Переход на страницу страны с зонами
                        var link = driver.FindElement(By.XPath("//td[5]/a[1]")).GetAttribute("href");
                        driver.Navigate().GoToUrl(link);

                        //Получение списка веб-элементов, содержащих зоны
                        var zones = driver.FindElements(By.XPath("//td[3]/input[@class='form-control' and 1]"));

                        //Кол-во зон
                        int countZones = zones.Count;

                        //Перебор списка веб-элементов, содержащих зоны
                        for (int j = 1; j <= countZones; j++)
                        {
                            //Поиск названия зоны
                            string zone = driver.FindElement(By.XPath("//td[3]/input[@class='form-control' and 1]")).GetAttribute("value");

                            //Добавление зоны в актуальный список
                            actualZones.Add(zone);
                        }

                        //Ожидаемый список зон
                        var expectedZones = new List<string>(actualZones);
                        expectedZones.Sort();

                        //Проверка равнозначности ожидаемого и актуального списков
                        Assert.IsTrue(actualZones.SequenceEqual(expectedZones), "Страны расположены не в алфавитном порядке");
                        driver.Navigate().Back();
                    }
                }
            }
        }

        /// <summary>
        /// зайти в каждую из зон и проверить, что страны расположены в алфавитном порядке
        /// </summary>
        private void ZoneCheck()

        {
            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

            //Получение списка веб - элементов, содержащих зоны
            var zones = driver.FindElements(By.XPath("//td[3]/a[1]"));

            //Количество веб-элементов, содержащих зоны
            int countZones = zones.Count;

            if (AreElementsPresent(By.XPath("//td[3]/a[1]")))
            {
                //Перебор веб-элементов, содержащих зоны
                for (int i = 1; i <= countZones; i++)
                {
                    //Переход на страницу зоны
                    driver.FindElement(By.XPath("//td[3]/a[1]")).Click();

                    //Получение списка веб-элементов, содержащих страны
                    var countries = driver.FindElements(By.XPath("//td[2]"));

                    //Актуальный список стран
                    var actualCountries = new List<string>();

                    //Перебор стран в зоне
                    foreach (IWebElement countrie in countries)
                    {
                        //Получение названий стран
                        string countrieName = countrie.GetAttribute("text");

                        //Добавление названий стран в актуальный список
                        actualCountries.Add(countrieName);
                    }

                    //Ожидаемый список стран
                    var expectedCountries = new List<string>(actualCountries);
                    expectedCountries.Sort();

                    //Проверка равнозначности ожидаемого и актуального списков
                    Assert.IsTrue(actualCountries.SequenceEqual(expectedCountries), "Зоны расположены не в алфавитном порядке");
                    driver.Navigate().Back();
                }
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
