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
        public void FirstTest()
        {
            CHdriver.Manage().Cookies.DeleteAllCookies();
            CHdriver.Url = "http://localhost:8080/litecart/en/";
            
            IWebElement BoxMostPopular = CHdriver.FindElement(By.XPath("//div[@id='box-most-popular']")); //Блок Most Popular
            for (int i = 1; i <= 5; i++)
            {
                BoxMostPopular.FindElement(By.XPath("//li[@class='product column shadow hover-light']["+i+"]//div[@class='sticker new']")); //Проверка наличия одного стикера у товаров в блоке Most Popular
            }

            IWebElement BoxCampaigns = CHdriver.FindElement(By.XPath("//div[@id='box-campaigns']")); //Блок Campaigns
            BoxCampaigns.FindElement(By.XPath("//li[@class='product column shadow hover-light'][1]//div[@class='sticker new']")); //Проверка наличия одного стикера у товара в блоке Campaigns

            IWebElement BoxLatestProducts = CHdriver.FindElement(By.XPath("//div[@id='box-latest-products']")); //Блок Latest Products
            for (int i = 1; i <= 5; i++)
            {
                BoxLatestProducts.FindElement(By.XPath("//li[@class='product column shadow hover-light'][" + i + "]//div[@class='sticker new']")); //Проверка наличия одного стикера у товаров в блоке Most Popular
            }
        }

        [TearDown]
        public void stop()
        {
            CHdriver.Quit();
        }
    }
}
