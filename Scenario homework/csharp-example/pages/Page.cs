using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_example
{
    internal class Page
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public Page(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public bool IsElementPresent(bool isElementPresent, By locator)
        {
            try
            {
                driver.FindElement(locator);
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
                driver.FindElement(locator).Click();
            }
            catch (Exception)
            {
            }
        }
    }
}
