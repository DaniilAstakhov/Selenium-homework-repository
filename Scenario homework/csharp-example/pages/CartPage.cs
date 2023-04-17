using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V109.Debugger;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_example
{
    internal class CartPage : Page
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        private By thereIsNoItemsInCart = By.XPath("//em[.='There are no items in your cart.']");
        private By removeProductButton = By.Name("remove_cart_item");

        internal bool CheckIfProductNotExists()
        {
            bool isElementPresent = false;
            isElementPresent = IsElementPresent(isElementPresent, thereIsNoItemsInCart);

            if (isElementPresent == true)
            {
                return isElementPresent;
            }
            return isElementPresent;
        }

        internal void removeProducts()
        {
            //Пытаемся кликнуть на миниатюру первого товара, если она есть в данный момент
            TryClick(By.XPath("//ul[@class='shortcuts']/li[1]/a"));

            //Находим наименование товара и по наименованию ищем его в таблице
            string productName = driver.FindElement(By.XPath("//li[@class='item']//strong")).GetAttribute("textContent").ToString();
            var productInTable = driver.FindElement(By.XPath("//*[@id='order_confirmation-wrapper']//tr[2]/td[@class='item'][.='" + productName + "']"));

            //Удаляем товар
            driver.FindElement(removeProductButton).Click();

            //Ждем пока он изчезнет из таблицы
            wait.Until(ExpectedConditions.StalenessOf(productInTable));
        }
    }
}
