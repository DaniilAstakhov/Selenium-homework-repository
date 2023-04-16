using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using System.Runtime.CompilerServices;

namespace csharp_example
{
    [TestFixture]
    internal class LinksInNewWindowsTests
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
        public void LinksInNewWindowsTest()
        {

            CHdriver.Manage().Cookies.DeleteAllCookies();
            CHdriver.Url = "http://localhost:8080/litecart/admin";
            Login();
            CHdriver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";

            //Нажимаем "Добавить новую страну"
            CHdriver.FindElement(By.XPath("//td[@id='content']//a[@class='button']")).Click();

            //Получаем слисок элементов со ссылкой на внешнюю страницу
            IReadOnlyCollection<IWebElement> links = CHdriver.FindElements(By.XPath("//i[@class='fa fa-external-link']"));
            for (int i = 0; i < links.Count; i++)
            {
                string mainWindow = CHdriver.CurrentWindowHandle;
                ICollection<string> oldWindows = CHdriver.WindowHandles;
                links.ElementAt(i).Click();
                wait.Until(CHdriver => CHdriver.WindowHandles.Count > oldWindows.Count);
                string newWindow = ThereIsWindowOtherThan(oldWindows);
                CHdriver.SwitchTo().Window(newWindow);                
                CHdriver.Close();
                CHdriver.SwitchTo().Window(mainWindow);
            }            
        }
        public string ThereIsWindowOtherThan(ICollection<string> oldWindows)
        {
            List<string> newWindowsCheck2 = CHdriver.WindowHandles.ToList();                                                                        
            bool windowsCountEquality = newWindowsCheck2.Equals(oldWindows);
                if (windowsCountEquality == false)
                {
                    for (int i = 0; i < oldWindows.Count; i++)
                    {
                        bool identicalElementExistence = newWindowsCheck2.Contains(oldWindows.ElementAt(i));
                        if (identicalElementExistence == true)
                        {
                            newWindowsCheck2.Remove(oldWindows.ElementAt(i));
                        }
                    }                    
                }                            
            string newWindow = newWindowsCheck2.ElementAt(0);
            return newWindow;
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
