using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;

namespace csharp_example
{
    internal class StoreMainPage : Page
    {
        public StoreMainPage(IWebDriver driver) : base(driver) { }

        private By cartButton = By.XPath("//a[.='Checkout »']");
        private By boxMostPopular = By.Id("box-most-popular");

        internal void CartOpen()
        {
            //Открываем корзину
            driver.FindElement(cartButton).Click();
        }

        internal StoreMainPage Open()
        {
            driver.Url = "http://localhost:8080/litecart/en/";
            return this;
        }

        internal void OpenFirstProduct()
        {
            var BoxMostPopular = driver.FindElement(boxMostPopular); // Ищем блок Most Popular
            BoxMostPopular.FindElement(By.XPath(".//li[starts-with(@class,'product')][1]")).Click(); //Открываем первый товар
        }
    }
}
