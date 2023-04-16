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
    [TestFixture]
    internal class CountriesTest
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
        public void CountriesAndZonesTest()
        {
            CHdriver.Manage().Cookies.DeleteAllCookies();
            CHdriver.Url = "http://localhost:8080/litecart/admin";
            Login();
            CHdriver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";

            IReadOnlyCollection<IWebElement> CountriesCount = CHdriver.FindElements(By.XPath("//tr[@class='row']"));
            
            List<string> ActualCountriesSort = new List<string>();            
            GetCountriesList(CountriesCount, ActualCountriesSort);
            List<string> ActualCountriesSort2 = new List<string>(ActualCountriesSort);
            ActualCountriesSort.Sort();

            Assert.IsTrue(ActualCountriesSort2.SequenceEqual(ActualCountriesSort), "Некорректная сортировка алфавитного порядка стран");

            //Вторая проверка, по зонам
            for (int i = 2; i <= CountriesCount.Count + 1; i++)
            {
                var zonesCount = CHdriver.FindElement(By.XPath("//td[@id='content']//tr["+i+"]/td[6]"));
                int actualZonesCount = Convert.ToInt32(zonesCount.GetAttribute("textContent"));
                if (actualZonesCount != 0)
                {
                    CHdriver.FindElement(By.XPath("//td[@id='content']//tr[" + i + "]/td[5]/a")).Click();
                    IReadOnlyCollection<IWebElement> LocalZonesCount = CHdriver.FindElements(By.XPath("//tr/td[3]/input[@type='hidden']"));

                    List<string> ActualZonesList = new List<string>();
                    

                    GetZonesList(LocalZonesCount, ActualZonesList);
                    //GetZonesList(LocalZonesCount, ActualZonesList2);
                    List<string> ActualZonesList2 = new List<string>(ActualZonesList);

                    ActualZonesList.Sort();

                    Assert.IsTrue(ActualZonesList.SequenceEqual(ActualZonesList2), "Некорректная сортировка алфавитного порядка зон");
                    CHdriver.FindElement(By.Name("cancel")).Click();
                }
            }
        }
        private void GetZonesList(IReadOnlyCollection<IWebElement> LocalZonesCount, List<string> ActualZounesList)
        {
            for (int a = 2; a <= LocalZonesCount.Count + 1; a++)
            {
                IWebElement zone = CHdriver.FindElement(By.XPath("//table[@id='table-zones']//tr["+a+"]/td[3]"));
                ActualZounesList.Add(zone.Text);
            }
        }
        private void GetCountriesList(IReadOnlyCollection<IWebElement> CountriesCount, List<string> ActualCountriesSort)
        {
            for (int i = 2; i <= CountriesCount.Count + 1; i++)
            {
                var country = CHdriver.FindElement(By.XPath("//td[@id='content']//tr[" + i + "]/td[5]/a"));
                ActualCountriesSort.Add(country.GetAttribute("textContent"));
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
