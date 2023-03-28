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
    internal class ProductCreate
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
        public void ProductCreationTest()
        {
            CHdriver.Manage().Cookies.DeleteAllCookies();
            CHdriver.Url = "http://localhost:8080/litecart/admin/";
            Login();
            CHdriver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog";

            //Фиксируем изначальное количество товаров
            IReadOnlyCollection<IWebElement> productCountList = CHdriver.FindElements(By.XPath("//tr[starts-with(@Class,'row')]"));
            int productCount = productCountList.Count();

            //Нажатие кнопки создать новый товар
            CHdriver.FindElement(By.XPath("//td[@id='content']/div[1]/a[2]")).Click();

            //Заполнение информации о товаре. Вкладка General
            CHdriver.FindElement(By.Name("name[en]")).SendKeys("Test product");

            var qantity = CHdriver.FindElement(By.Name("quantity"));
            qantity.Clear();
            qantity.SendKeys("10");

            string attachedFile = "zu7K-FyzgXs.jpg";
            string absolutePath = Path.GetFullPath(attachedFile);
            CHdriver.FindElement(By.Name("new_images[]")).SendKeys(absolutePath);

            //Заполнение информации о товаре. Вкладка Information
            CHdriver.FindElement(By.XPath("//a[contains(text(),'Information')]")).Click();
            CHdriver.FindElement(By.Name("manufacturer_id")).Click();
            CHdriver.FindElement(By.XPath("//option[contains(text(),'ACME Corp.')]")).Click();
            CHdriver.FindElement(By.Name("keywords")).SendKeys("Test Keywords");
            CHdriver.FindElement(By.Name("short_description[en]")).SendKeys("Test Short Description");
            CHdriver.FindElement(By.ClassName("trumbowyg-editor")).SendKeys("Description Description Description Description Description Description Description");
            CHdriver.FindElement(By.Name("head_title[en]")).SendKeys("Test Head Title");
            CHdriver.FindElement(By.Name("meta_description[en]")).SendKeys("Test Meta Description");

            //Заполнение информации о товаре. Вкладка Prices
            CHdriver.FindElement(By.XPath("//a[contains(text(),'Prices')]")).Click();
            var purchasePrice = CHdriver.FindElement(By.Name("purchase_price"));
            purchasePrice.Clear();
            purchasePrice.SendKeys("15");

            var currency = CHdriver.FindElement(By.Name("purchase_price_currency_code"));
            currency.Click();
            currency.FindElement(By.XPath("//option[@value='USD']")).Click();

            CHdriver.FindElement(By.Name("prices[USD]")).SendKeys("15");
            CHdriver.FindElement(By.Name("prices[EUR]")).SendKeys("13.8");


            //Сохранение товара
            CHdriver.FindElement(By.Name("save")).Click();

            //Проверка что товар добавлен
            productCountList = CHdriver.FindElements(By.XPath("//tr[starts-with(@Class,'row')]"));
            int productCount2 = productCountList.Count();
            Assert.AreNotEqual(productCount, productCount2, "Продукт не сохранён");
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
