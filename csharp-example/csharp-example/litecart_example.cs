using System;
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
            System.Collections.Generic.List<IWebElement> MenuToClick = driver.FindElements(By.ClassName("app")).ToList();
            int MenuCount = MenuToClick.Count;
            for (int i = 0; i <= MenuCount - 1; i++)
            {  
                MenuToClick = driver.FindElements(By.ClassName("app")).ToList();
                MenuToClick[i].Click();
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.ClassName("panel-heading")));

                System.Collections.Generic.List<IWebElement> SubmenuToClick = driver.FindElements(By.CssSelector("[class=doc]")).ToList();
                int SubmenuCount = SubmenuToClick.Count;
                if (SubmenuCount > 0)
                { 
                    for (int j = 0; j <= SubmenuCount - 1; j++)
                    {
                        SubmenuToClick = driver.FindElements(By.CssSelector("[class=doc]")).ToList();
                        SubmenuToClick[j].Click();
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
