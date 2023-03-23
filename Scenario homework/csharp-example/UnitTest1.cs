using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using Assert = NUnit.Framework.Assert;

namespace csharp_example
{
    [TestFixture]
    public class MyFirstTest
    {
        public IWebDriver CHdriver;
        public WebDriverWait wait;
        public ChromeOptions options;
        bool AreElementsPresent(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }

        [OneTimeSetUp]
        public void StartBrowser()
        {
            options = new ChromeOptions();
            options.AddArguments(/*"--start-maximized", */"incognito"/*, "headless"*/);
            CHdriver = new ChromeDriver(options);
            wait = new WebDriverWait(CHdriver, TimeSpan.FromSeconds(10));
            CHdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        [Test]
        public void FirstTest()
        {
            CHdriver.Manage().Cookies.DeleteAllCookies();
            CHdriver.Url = "http://localhost:8080/litecart/admin";
            CHdriver.FindElement(By.Name("username")).SendKeys("admin");
            CHdriver.FindElement(By.Name("password")).SendKeys("admin");
            CHdriver.FindElement(By.Name("login")).Click(); //login
            IWebElement VerticalMenu = CHdriver.FindElement(By.Id("box-apps-menu-wrapper")); //Элемент бокового меню
            IReadOnlyCollection<IWebElement> Elements = VerticalMenu.FindElements(By.Id("app-")); // получаем список элементов бокового меню для извлечения количества
            int Count = Elements.Count();
            for (int i = 1; i <= Elements.Count(); i++)
            {
                IWebElement VerticalMenu2 = CHdriver.FindElement(By.Id("box-apps-menu-wrapper"));
                VerticalMenu2.FindElement(By.XPath(".//li[@id='app-'][" + i + "]")).Click();
                CHdriver.FindElement(By.CssSelector("h1"));
                bool DocExist = AreElementsPresent(CHdriver, By.XPath("//ul[@class='docs']"));
                if (DocExist == true)
                {
                    IReadOnlyCollection<IWebElement> DocItems = CHdriver.FindElements(By.XPath("//ul[@class='docs']//li[starts-with(@id,'doc')]")); //получаем список внутренних пунктов меню для извлечения количества
                    for (int a = 1; a <= DocItems.Count; a++)
                    {
                        CHdriver.FindElement(By.XPath("//ul[@class='docs']//li[starts-with(@id,'doc')][" + a + "]")).Click();
                        CHdriver.FindElement(By.CssSelector("h1"));
                    }
                }
            }
        }

        [TearDown]
        public void stop()
        {
            CHdriver.Quit();
        }
    }
}