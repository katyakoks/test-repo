using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace csharp_example
{
    [TestFixture]
    public class MySixthTest
    {
        private IWebDriver driver;
        private string startUrl;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            //driver = new InternetExplorerDriver();
            //driver = new FirefoxDriver();
            driver = new ChromeDriver();
            startUrl = "http://localhost/litecart/en/create_account";
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }
        ///регистрация новой учётной записи с уникальным адресом электронной почты
        ///чтобы не конфликтовало с ранее созданными, в том числе при предыдущих запусках сценария
        ///в качестве страны выбирайте United States, формат индекса - пять цифр.
        [Test]
        public void LoginUser()
        {
            driver.Url = startUrl;       

            //заполнить поля: имя, фамилия, адрес, почтовый индекс...
            driver.FindElement(By.CssSelector("[name=firstname]")).SendKeys("Kevin");
            driver.FindElement(By.CssSelector("[name=lastname]")).SendKeys("Spacey");
            driver.FindElement(By.CssSelector("[name=address1]")).SendKeys("21 Columbus Ave");
            driver.FindElement(By.CssSelector("[name=address2]")).SendKeys("New Providence");
            driver.FindElement(By.CssSelector("[name=postcode]")).SendKeys("07974");
            driver.FindElement(By.Name("city")).SendKeys("NJ");

            //выбрать страну
            var countrySelect = new SelectElement(driver.FindElement(By.Name("country_code")));
            countrySelect.SelectByValue("US");

            //выбрать зону
            var zoneSelect = new SelectElement(driver.FindElement(By.CssSelector("[name=zone_code]")));
            zoneSelect.SelectByValue("NJ");

            //сгенерировать и заполнить почту
            Random random = new Random();
            int num = random.Next(1, 100);
            string emailString = "test" + num.ToString() + "@test.tv";
            driver.FindElement(By.XPath("//div[6]/div[@class='form-group col-xs-6 required' and 1]/div[@class='input-group' and 1]/input[@class='form-control' and 1]")).SendKeys(emailString);
            
            //заполнить номер телефона
            driver.FindElement(By.CssSelector("[name=phone]")).SendKeys("+180671234567");
            
            //заполнить и подтвердить пароль
            driver.FindElement(By.XPath("//div[7]/div[@class='form-group col-xs-6 required' and 1]/div[@class='input-group' and 2]/input[@class='form-control' and 2]")).SendKeys("Password123");
            driver.FindElement(By.CssSelector("[name=confirmed_password]")).SendKeys("Password123");
            
            //установить чекбокс согласия с правилами
            driver.FindElement(By.Name("terms_agreed")).Click();
            
            //нажать кнопку для создания аккаунта
            driver.FindElement(By.CssSelector("[name=customer_form] button[name='create_account']")).Click();

            CheckOut();

            //войти
            driver.FindElement(By.XPath("//li[@class='account dropdown']/a[@class='dropdown-toggle' and 1]")).Click();

            //ввести почту и пароль
            driver.FindElement(By.XPath("//div[@class='form-group required']/div[@class='input-group' and 1]/input[@class='form-control' and 1]")).SendKeys(emailString);
            driver.FindElement(By.XPath("//div[@class='form-group']/div[@class='input-group' and 1]/input[@class='form-control' and 1]")).SendKeys("Password123");

            //войти
            driver.FindElement(By.CssSelector("[name=login_form] button[name=login]")).Click();

            CheckOut();
        }

        /// <summary>
        /// выход
        /// </summary>
        private void CheckOut()

        {
            driver.Url = "http://localhost/litecart/en/logout";
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
