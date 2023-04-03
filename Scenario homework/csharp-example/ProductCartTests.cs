using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SeleniumExtras.WaitHelpers;

namespace csharp_example
{
    [TestFixture]
    internal class ProductCartTests
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
            wait = new WebDriverWait(CHdriver, TimeSpan.FromSeconds(3));
            //CHdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        [Test]
        public void ProductCartTest()
        {
            CHdriver.Manage().Cookies.DeleteAllCookies();
            CHdriver.Url = "http://localhost:8080/litecart/en/";
            
            //Добавляем товары в корзину
            for (int i = 0; i < 3; i++)
            {
                var BoxMostPopular = CHdriver.FindElement(By.Id("box-most-popular")); // Ищем блок Most Popular
                BoxMostPopular.FindElement(By.XPath(".//li[starts-with(@class,'product')][1]")).Click(); //Открываем первый товар
                
                //проверяем что у открытого товара есть параметр size, который обязателен к заполнению
                bool isElementPresent = false;
                isElementPresent = IsElementPresent(isElementPresent, By.Name("options[Size]"));

                //Если size у открытого товара есть, то заполняем
                if (isElementPresent == true)
                {
                    CHdriver.FindElement(By.Name("options[Size]")).Click();
                    CHdriver.FindElement(By.XPath("//option[@value='Small']")).Click();
                }
                CHdriver.FindElement(By.Name("add_cart_product")).Click();

                //Ожидаем обновления счетчика товаров в корзине
                wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath("//span[@class='quantity'][.='"+i+"']"), ""+i.ToString()+"")); //Ожидаение что элемент с текстом либо невидим, либо отсутствует в DOM
                
                //Возврпащаемся на главную
                CHdriver.FindElement(By.XPath("//i[@title='Home']")).Click();                                               
            }

            //Открываем корзину
            CHdriver.FindElement(By.XPath("//a[.='Checkout »']")).Click();

            //Последовательно удаляем товары
            for (int i = 0; i < 3; i++)
            {               
                //Пытаемся кликнуть на миниатюру первого товара, если она есть в данный момент
                TryClick(By.XPath("//ul[@class='shortcuts']/li[1]/a"));
                
                //Находим наименование товара и по наименованию ищем его в таблице
                string productName = CHdriver.FindElement(By.XPath("//li[@class='item']//strong")).GetAttribute("textContent").ToString();
                var productInTable = CHdriver.FindElement(By.XPath("//*[@id='order_confirmation-wrapper']//tr[2]/td[@class='item'][.='" + productName + "']"));
                
                //Удаляем товар
                CHdriver.FindElement(By.Name("remove_cart_item")).Click();
                
                //Ждем пока он изчезнет из таблицы
                wait.Until(ExpectedConditions.StalenessOf(productInTable));

                //Проверяем случай что товаров для удаления может больше не быть, и если это так, то завершаем цикл
                bool isElementPresent = false;
                isElementPresent = IsElementPresent(isElementPresent, By.XPath("//em[.='There are no items in your cart.']"));

                if (isElementPresent == true)
                {
                    goto end;
                }
            }
        end:;
        }

        public bool IsElementPresent(bool isElementPresent, By locator)
        {
            try
            {
                CHdriver.FindElement(locator);
                isElementPresent = true;
            }
            catch (Exception)
            {
                isElementPresent = false;
            }
            return isElementPresent;
        }

        public void TryClick(By locator)
        {
            try
            {
                CHdriver.FindElement(locator).Click();
            }
            catch (Exception)
            {
            }
        }

        [OneTimeTearDown]
        public void stop()
        {
            CHdriver.Quit();
        }
    }
}