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
    public class MySecondTest
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
        public void SecondTest()
        {
            //авторизация
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Submit();
            wait.Until(ExpectedConditions.TitleIs("Dashboard | My Store"));


            //прокликивает последовательно все пункты меню слева, включая вложенные пункты
            //для каждой страницы проверяет наличие заголовка
            List<IWebElement> menuToClick = driver.FindElements(By.ClassName("app")).ToList();
            int menuCount = menuToClick.Count;
            for (int i = 0; i <= menuCount - 1; i++)
            {  
                menuToClick = driver.FindElements(By.ClassName("app")).ToList();
                menuToClick[i].Click();
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.ClassName("panel-heading")));

                List<IWebElement> submenuToClick = driver.FindElements(By.CssSelector("[class=doc]")).ToList();
                int submenuCount = submenuToClick.Count;
                if (submenuCount > 0)
                { 
                    for (int j = 0; j <= submenuCount - 1; j++)
                    {
                        submenuToClick = driver.FindElements(By.CssSelector("[class=doc]")).ToList();
                        submenuToClick[j].Click();
                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.ClassName("panel-heading")));
                    }
                }
                else
                {   
                    driver.FindElement(By.ClassName("panel-heading"));
                }
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
