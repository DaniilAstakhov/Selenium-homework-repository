using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_example
{
    [TestFixture]
    internal class LogsCheckTests
    {
        public IWebDriver CHdriver;
        public WebDriverWait wait;
        public ChromeOptions options;

        [OneTimeSetUp]
        public void StartBrowser()
        {
            options = new ChromeOptions();
            options.AddArguments(/*"--start-maximized", */"incognito"/*, "headless"*/);
            CHdriver = new ChromeDriver(options);
            wait = new WebDriverWait(CHdriver, TimeSpan.FromSeconds(5));
            CHdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        [Test]
        public void LogsCheckTest()
        {
            CHdriver.Manage().Cookies.DeleteAllCookies();
            CHdriver.Url = "http://localhost:8080/litecart/admin";
            Login();
            CHdriver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1";

            IReadOnlyCollection<IWebElement> productsCount = CHdriver.FindElements(By.XPath("//*[@id='content']//td[3]/a[contains(@href, 'product_id')]"));
            
            for (int i = 0; i < productsCount.Count; i++)
            {
                IReadOnlyCollection<IWebElement> products = CHdriver.FindElements(By.XPath("//*[@id='content']//td[3]/a[contains(@href, 'product_id')]"));
                products.ElementAt(i).Click();
                foreach (LogEntry l in CHdriver.Manage().Logs.GetLog("browser"))
                {
                    Console.WriteLine(l + "Номер ссылки по которой было выполнено нажатие =" + i);                    
                }
                
                CHdriver.FindElement(By.Name("cancel")).Click();
                
            }
        }
        public void Login()
        {
            CHdriver.FindElement(By.Name("username")).SendKeys("admin");
            CHdriver.FindElement(By.Name("password")).SendKeys("admin");
            CHdriver.FindElement(By.Name("login")).Click(); //login
        }

        [OneTimeTearDown]
        public void stop()
        {
            CHdriver.Quit();
        }
    }

}
