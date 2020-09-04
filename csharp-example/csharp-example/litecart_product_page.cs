using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace csharp_example
{
    [TestFixture]
    public class MyFifthTest
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
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        }

        [Test]
        public void ProductPageCheck()
        {
            driver.Url = startUrl;
            NameCheck();
            PriceCheck();
            FontCheck();
            FontCheckProduct();

        }
        /// <summary>
        ///переход на страницу товара
        /// </summary>
        private void PageClick()
        {
            driver.FindElement(By.CssSelector("article.product-column")).Click();
        }

        /// <summary>
        ///выбрать первый товар в категории Campaigns и проверить следующее:
        ///а) на главной странице и на странице товара совпадает текст названия товара
        /// </summary>
        private void NameCheck()

        {
            //получение названия товара на главной странице
            var nameMain = driver.FindElement(By.CssSelector("[class=name]")).GetAttribute("textContent");
          
            PageClick();

            //получение названия товара на странице продукта
            var nameProduct = driver.FindElement(By.CssSelector("h1.title")).GetAttribute("textContent");

            //проверка равнозначности названий
            Assert.AreEqual(nameMain, nameProduct, "Название товара не совпадает");
            driver.Navigate().Back();
        }

        /// <summary>
        /// б) на главной странице и на странице товара совпадают цены
        /// </summary>
        private void PriceCheck()

        {
            //получение цен товара на главной странице
            var priceSaleMain = driver.FindElement(By.CssSelector("strong.campaign-price")).GetAttribute("textContent");
            var priceSimpleMain = driver.FindElement(By.CssSelector("del.regular-price")).GetAttribute("textContent");

            PageClick();

            //получение цены товара на странице продукта
            var priceSaleProduct = driver.FindElement(By.CssSelector("strong.campaign-price")).GetAttribute("textContent");
            var priceSimpleProduct = driver.FindElement(By.CssSelector("del.regular-price")).GetAttribute("textContent");

            //проверка равнозначности цен
            Assert.AreEqual(priceSaleMain, priceSaleProduct, "Цена товара не совпадает");
            Assert.AreEqual(priceSimpleMain, priceSimpleProduct, "Цена товара не совпадает");
            driver.Navigate().Back();
        }

        /// <summary>
        ///в) обычная цена серая и зачёркнутая, а акционная цена красная и жирная 
        ///(это надо проверить на каждой странице независимо, при этом цвета на разных страницах могут не совпадать)
        ///г) акционная цена крупнее, чем обычная(это надо проверить на каждой странице независимо)
        /// </summary>
        private void FontCheck()

        {
            //цвет обычной цены
            var simplePriceColor = driver.FindElement(By.CssSelector("del.regular-price")).GetCssValue("color");
            
            //декоратор обычной цены
            var simplePriceDecor = driver.FindElement(By.CssSelector("del.regular-price")).GetCssValue("text-decoration");

            //проверки Chrome
            Assert.AreEqual(simplePriceColor, "rgba(51, 51, 51, 1)", "Цвет не серый");
            Assert.AreEqual(simplePriceDecor, "line-through solid rgb(51, 51, 51)", "Цена не зачёркнута");

            //проверки Firefox
            //Assert.AreEqual(simplePriceColor, "rgb(51, 51, 51)", "Цвет не серый");
            //Assert.AreEqual(simplePriceDecor, "line-through rgb(51, 51, 51)", "Цена не зачёркнута");

            //проверки IE
            //Assert.AreEqual(simplePriceColor, "rgba(51, 51, 51, 1)", "Цвет не серый");
            //Assert.AreEqual(simplePriceDecor, "line-through", "Цена не зачёркнута");

            //цвет акционной цены
            var salePriceColor = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color");

            //жирность акционной цены
            var salePriceBold = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-weight");

            //проверки Chrome
            Assert.AreEqual(salePriceColor, "rgba(204, 0, 0, 1)", "Цвет не красный");
            Assert.AreEqual(salePriceBold, "700", "Шрифт не жирный");

            //проверки Firefox
            //Assert.AreEqual(salePriceColor, "rgb(204, 0, 0)", "Цвет не красный");
            //Assert.AreEqual(salePriceBold, "700", "Шрифт не жирный");

            //проверки IE
            //Assert.AreEqual(salePriceColor, "rgba(204, 0, 0, 1)", "Цвет не красный");
            //Assert.AreEqual(salePriceBold, "700", "Шрифт не жирный");

            //размер обычной цены
            var simplePriceSize = driver.FindElement(By.CssSelector("del.regular-price")).GetCssValue("font-size");
            string simplePriceSizeToString = simplePriceSize.Replace("px", "").Replace(".", ",");

            //размер акционной цены
            var salePriceSize = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-size");
            string salePriceSizeToString = salePriceSize.Replace("px", "").Replace(".", ",");

            //проверка
            Assert.IsTrue(Convert.ToDouble(salePriceSizeToString) > Convert.ToDouble(simplePriceSizeToString), "Обычная цена не меньше акционной");

        }

        /// <summary>
        /// Проверкa шрифтов на странице продукта
        /// </summary>
        private void FontCheckProduct()

        {
            PageClick();
            FontCheck();
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
