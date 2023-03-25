using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;
namespace csharp_example
{
    internal class ZonesTest
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
            wait = new WebDriverWait(CHdriver, TimeSpan.FromSeconds(10));
            CHdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void ZonesSortTest()
        {
            CHdriver.Manage().Cookies.DeleteAllCookies();
            CHdriver.Url = "http://localhost:8080/litecart/admin";
            Login();
            CHdriver.Url = "http://localhost:8080/litecart/admin/?app=geo_zones&doc=geo_zones";

            IReadOnlyCollection<IWebElement> CountriesCount = CHdriver.FindElements(By.XPath("//tr[@class='row']"));

            for (int i = 2; i <= CountriesCount.Count + 1; i++)
            {
                CHdriver.FindElement(By.XPath("//td[@id='content']//tr["+i+"]/td[3]/a")).Click();
                
                List<string> ActualZonesSort = new List<string>();
                List<string> ActualZonesSort2 = new List<string>();

                IReadOnlyCollection<IWebElement> ZonesCount = CHdriver.FindElements(By.XPath("//table[@id='table-zones']//tr/td[3]"));

                GetZonesList(ZonesCount, ActualZonesSort);
                GetZonesList(ZonesCount, ActualZonesSort2);

                ActualZonesSort.Sort();

                Assert.IsTrue(ActualZonesSort2.SequenceEqual(ActualZonesSort), "Некорректная сортировка алфавитного порядка зон");
                CHdriver.FindElement(By.Name("cancel")).Click();
            }
        }

        private void GetZonesList(IReadOnlyCollection<IWebElement> LocalZonesCount, List<string> ActualZonesList)
        {
            for (int a = 2; a <= LocalZonesCount.Count + 1; a++)
            {
                IWebElement zone = CHdriver.FindElement(By.XPath("//tr["+a+"]//select[contains(@name,'zone_code')]//option[@selected='selected']"));
                ActualZonesList.Add(zone.GetAttribute("textContent"));
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
