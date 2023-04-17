using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_example
{
    internal class ProductPage : Page
    {
        public ProductPage(IWebDriver driver) : base(driver) { }

        private By size = By.Name("options[Size]");
        private By smallSize = By.XPath("//option[@value='Small']");
        private By addProductButton = By.Name("add_cart_product");
        private By homeButton = By.XPath("//i[@title='Home']");
        internal void addProduct(int num)
        {
            //проверяем что у открытого товара есть параметр size, который обязателен к заполнению
            bool isElementPresent = false;
            isElementPresent = IsElementPresent(isElementPresent, size);

            //Если size у открытого товара есть, то заполняем
            if (isElementPresent == true)
            {
                driver.FindElement(size).Click();
                driver.FindElement(smallSize).Click();
            }
            driver.FindElement(addProductButton).Click();

            //Ожидаем обновления счетчика товаров в корзине
            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath("//span[@class='quantity'][.='" + num + "']"), "" + num.ToString() + "")); //Ожидаение что элемент с текстом либо невидим, либо отсутствует в DOM

            //Возврпащаемся на главную
            driver.FindElement(homeButton).Click();
        }
    }
}
