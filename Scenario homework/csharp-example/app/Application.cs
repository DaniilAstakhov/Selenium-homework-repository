using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace csharp_example
{
    public class Application
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        private RegistrationPage registrationPage;
        private AdminPanelLoginPage adminPanelLoginPage;
        private CustomerListPage customerListPage;
        private StoreMainPage storeMainPage;
        private ProductPage productPage;
        private CartPage cartPage;

        public Application()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            registrationPage = new RegistrationPage(driver);
            adminPanelLoginPage = new AdminPanelLoginPage(driver);
            customerListPage = new CustomerListPage(driver);
            storeMainPage = new StoreMainPage(driver);
            productPage = new ProductPage(driver);
            cartPage = new CartPage(driver);
        }

        public void Quit()
        {
            driver.Quit();
        }

        internal void RegisterNewCustomer(Customer customer)
        {
            registrationPage.Open();
            registrationPage.FirstnameInput.SendKeys(customer.Firstname);
            registrationPage.LastnameInput.SendKeys(customer.Lastname);
            registrationPage.Address1Input.SendKeys(customer.Address);
            registrationPage.PostcodeInput.SendKeys(customer.Postcode);
            registrationPage.CityInput.SendKeys(customer.City);
            registrationPage.SelectCountry(customer.Country);
            registrationPage.SelectZone(customer.Zone);
            registrationPage.EmailInput.SendKeys(customer.Email);
            registrationPage.PhoneInput.SendKeys(customer.Phone);
            registrationPage.PasswordInput.SendKeys(customer.Password);
            registrationPage.ConfirmedPasswordInput.SendKeys(customer.Password);
            registrationPage.CreateAccountButton.Click();
        }

        internal void AddPorductsInCart(int productsCount)
        {
            storeMainPage.Open();
            for (int i = 0; i <productsCount; i++)
            {
                storeMainPage.OpenFirstProduct();
                productPage.addProduct(i);                                              
            }
        }

        internal void RemovePorductsFromCart(int productsCount) 
        {
            storeMainPage.CartOpen();
            for (int i = 0; i < productsCount; i++)
            {
                cartPage.removeProducts();
                bool isElementPresent = false;
                isElementPresent = cartPage.CheckIfProductNotExists();
                if (isElementPresent == true)
                {
                    break;
                }
            }
        }

        internal ISet<string> GetCustomerIds()
        {
            if (adminPanelLoginPage.Open().IsOnThisPage())
            {
                adminPanelLoginPage.EnterUsername("admin").EnterPassword("admin").SubmitLogin();
            }

            return customerListPage.Open().GetCustomerIds();
        }
    }
}
