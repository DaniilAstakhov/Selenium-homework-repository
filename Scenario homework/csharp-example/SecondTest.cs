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
    public class SecondTest
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
        public void Secondtest()
        {
            CHdriver.Manage().Cookies.DeleteAllCookies();
            CHdriver.Url = "http://localhost:8080/litecart/en/";
            IWebElement BoxMostPopular = CHdriver.FindElement(By.Id("box-most-popular")); // Ищем блок Most Popular
            IReadOnlyCollection<IWebElement> MostPopProducts = BoxMostPopular.FindElements(By.XPath(".//li[starts-with(@class,'product')]")); //Ищем все элементы товаров в блоке Most Popular

            IWebElement BoxCampaigns = CHdriver.FindElement(By.Id("box-campaigns")); // Ищем блок Campaigns
            IReadOnlyCollection<IWebElement> CampaignsProducts = BoxCampaigns.FindElements(By.XPath(".//li[starts-with(@class,'product')]")); //Ищем все элементы товаров в блоке Most Popular

            IWebElement BoxLatestProducts = CHdriver.FindElement(By.Id("box-latest-products")); // Ищем блок Latest Products
            IReadOnlyCollection<IWebElement> LatestProducts = BoxLatestProducts.FindElements(By.XPath(".//li[starts-with(@class,'product')]")); //Ищем все элементы товаров в блоке Most Popular

            for (int i = 1; i <= MostPopProducts.Count; i++) // Проверка на ровно один стикер для товаров блока Most popular
            {
                IReadOnlyCollection<IWebElement> Sticker = BoxMostPopular.FindElements(By.XPath(".//li[starts-with(@class,'product')][" + i + "]//div[starts-with(@class,'sticker')]")); //Получение всех стикеров для одного товара
                string message = "Количество стикеров для товара " + i + " в блоке MostPopular не равно 1"; //Cообщение на случай если количество стикеров не равно единице
                Assert.False(Sticker.Count != 1, message);
            }

            for (int i = 1; i <= CampaignsProducts.Count; i++) // Проверка на ровно один стикер для товаров блока Campaigns
            {
                IReadOnlyCollection<IWebElement> Sticker = BoxCampaigns.FindElements(By.XPath(".//li[starts-with(@class,'product')][" + i + "]//div[starts-with(@class,'sticker')]")); //Получение всех стикеров для одного товара
                string message = "Количество стикеров для товара " + i + " в блоке Campaigns не равно 1"; //Cообщение на случай если количество стикеров не равно единице
                Assert.False(Sticker.Count != 1, message);
            }

            for (int i = 1; i <= LatestProducts.Count; i++) // Проверка на ровно один стикер для товаров блока Latest Products
            {
                IReadOnlyCollection<IWebElement> Sticker = BoxLatestProducts.FindElements(By.XPath(".//li[starts-with(@class,'product')][" + i + "]//div[starts-with(@class,'sticker')]")); //Получение всех стикеров для одного товара
                string message = "Количество стикеров для товара " + i + " в блоке Latest Products не равно 1"; //Cообщение на случай если количество стикеров не равно единице
                Assert.False(Sticker.Count != 1, message);
            }
        }

        [TearDown]
        public void stop()
        {
            CHdriver.Quit();
        }
    }
}
