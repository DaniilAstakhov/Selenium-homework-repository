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

        [OneTimeSetUp]
        public void StartBrowser()
        {
            options = new ChromeOptions();
            options.AddArguments(/*"--start-maximized", */"incognito"/*, "headless"*/);
            CHdriver = new ChromeDriver(options);
            wait = new WebDriverWait(CHdriver, TimeSpan.FromSeconds(10));
            CHdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void FirstTest()
        {
            CHdriver.Manage().Cookies.DeleteAllCookies();
            CHdriver.Url = "http://localhost:8080/litecart/admin/";
            CHdriver.FindElement(By.Name("username")).SendKeys("admin");
            CHdriver.FindElement(By.Name("password")).SendKeys("admin");
            CHdriver.FindElement(By.Name("login")).Click(); //login

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Appearence')]")).Click(); //appearence
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Template')]")).Click(); //appearence-template
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Logotype')]")).Click(); //appearence-logotype
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Catalog')]")).Click(); //catalog
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//li[@id='doc-catalog']//span[contains(text(),'Catalog')]")).Click(); //catalog-catalog
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Product Groups')]")).Click(); //catalog-product groupd
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Option Groups')]")).Click(); //catalog-option groupd
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Manufacturers')]")).Click(); //catalog-Manufacturers
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Suppliers')]")).Click(); //catalog-Suppliers
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Delivery Statuses')]")).Click(); //catalog-Delivery Statuses
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Quantity Units')]")).Click(); //catalog-Quantity Units
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'CSV Import/Export')]")).Click(); //catalog-CSV Import/Export
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Countries')]")).Click(); //Countries
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Currencies')]")).Click(); //Currencies
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Customers')]")).Click(); //Customers
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//li[@id='doc-customers']//span[contains(text(),'Customers')]")).Click(); //Customers-Customers
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'CSV Import/Export')]")).Click(); //Customers-CSV Import/Export
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Newsletter')]")).Click(); //Customers-Newsletter
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Geo Zones')]")).Click(); //Geo Zones
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Languages')]")).Click(); //Languages
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//li[@id='doc-languages']//span[contains(text(),'Languages')]")).Click(); //Languages-Languages
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Storage Encoding')]")).Click(); //Languages-Storage Encoding
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Modules')]")).Click(); //Modules
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Background Jobs')]")).Click(); //Modules-Background Jobs
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//li[@id='doc-customer']//span[contains(text(),'Customer')]")).Click(); //Modules-Customer
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Shipping')]")).Click(); //Modules-Shipping
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Payment')]")).Click(); //Modules-Payment
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Order Total')]")).Click(); //Modules-Order Total
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Order Success')]")).Click(); //Modules-Order Success
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Order Action')]")).Click(); //Modules-Order Action
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Orders')]")).Click(); //Orders
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//li[@id='doc-orders']//span[contains(text(),'Orders')]")).Click(); //Orders-Orders
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Order Statuses')]")).Click(); //Orders-Order Statuses
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Pages')]")).Click(); //Pages
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Reports')]")).Click(); //Reports
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Monthly Sales')]")).Click(); //Reports-Monthly Sales
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Most Sold Products')]")).Click(); //Reports-Most Sold Products
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Most Shopping Customers')]")).Click(); //Reports-Most Shopping Customers
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Settings')]")).Click(); //Settings
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Store Info')]")).Click(); //Settings-Store Info
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Defaults')]")).Click(); //Settings-Defaults
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'General')]")).Click(); //Settings-General
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Listings')]")).Click(); //Settings-Listings
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Images')]")).Click(); //Settings-Images
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Checkout')]")).Click(); //Settings-Checkout
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Advanced')]")).Click(); //Settings-Advanced
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Security')]")).Click(); //Settings-Security
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Slides')]")).Click(); //Slides
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Tax')]")).Click(); //Tax
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Tax Classes')]")).Click(); //Tax-Tax Classes
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Tax Rates')]")).Click(); //Tax-Tax Rates
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Translations')]")).Click(); //Translations
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Search Translations')]")).Click(); //Translations-Search Translations
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Scan Files')]")).Click(); //Translations-Scan Files
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'CSV Import/Export')]")).Click(); //Translations-CSV Import/Export
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'Users')]")).Click(); //Users
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//span[contains(text(),'vQmods')]")).Click(); //vQmods
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1

            CHdriver.FindElement(By.XPath("//li[@id='doc-vqmods']//span[contains(text(),'vQmods')]")).Click(); //vQmods-vQmods
            Assert.NotNull(CHdriver.FindElement(By.CssSelector("h1"))); //проверка h1
        }

        [TearDown]
        public void stop()
        {
            CHdriver.Quit();
        }
    }
}