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
    public class MySeventhTest
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
        public void AddProduct()
        {
            driver.Url = startUrl;

            FillOut();
        }

        
        /// <summary>
        /// Сделайте сценарий для добавления нового товара (в админке)
        ///открыть меню Catalog, нажать кнопку "Add New Product", заполнить поля и сохранить
        ///Достаточно заполнить информацию General, Information и Prices. 
        ///после переключения можно сделать небольшую паузу
        ///Картинку нужно уложить в репозиторий вместе с кодом.
        ///средствами языка программирования преобразовать относительный путь в абсолютный
        ///После сохранения товара нужно убедиться, что он появился в каталоге (в админке). 
        /// </summary>
        private void FillOut()

        {

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
