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
    internal class UserRegistration
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
        public void UserRegistrationTest()
        {
            CHdriver.Manage().Cookies.DeleteAllCookies();
            CHdriver.Url = "http://localhost:8080/litecart/en/";

            //Переход в форму регистрации
            var loginBox = CHdriver.FindElement(By.Id("box-account-login"));
            loginBox.FindElement(By.XPath(".//a[contains(text(),'New customers click here')]")).Click();

            //Заполнение формы регистрации
            var createAccountBox = CHdriver.FindElement(By.Id("create-account"));
            createAccountBox.FindElement(By.Name("firstname")).SendKeys("TestName");
            createAccountBox.FindElement(By.Name("lastname")).SendKeys("TestLastName");
            createAccountBox.FindElement(By.Name("address1")).SendKeys("TestAddress");
            createAccountBox.FindElement(By.Name("city")).SendKeys("TestCity");

            createAccountBox.FindElement(By.XPath(".//span[@class='select2-selection select2-selection--single']")).Click();
            CHdriver.FindElement(By.XPath("//li[@class='select2-results__option'][contains(text(),'United States')]")).Click();

            string eMailValue = "test";
            eMailValue = EmailCreate(eMailValue);

            createAccountBox.FindElement(By.Name("email")).SendKeys(eMailValue);

            createAccountBox.FindElement(By.Name("phone")).SendKeys("+19991112233");
            createAccountBox.FindElement(By.Name("postcode")).SendKeys("00000");

            string pass = "TestPassword-12345";
            createAccountBox.FindElement(By.Name("password")).SendKeys(pass);
            createAccountBox.FindElement(By.Name("confirmed_password")).SendKeys(pass);

            //Создание аккаунта (Кнопка создать)
            createAccountBox.FindElement(By.Name("create_account")).Click();

            //Logout
            CHdriver.FindElement(By.XPath("//a[contains(text(),'Logout')]")).Click();

            //Повторный логин
            loginBox = CHdriver.FindElement(By.Id("box-account-login"));
            loginBox.FindElement(By.Name("email")).SendKeys(eMailValue);
            loginBox.FindElement(By.Name("password")).SendKeys(pass);
            loginBox.FindElement(By.Name("login")).Click();

            //Повторный logout
            CHdriver.FindElement(By.XPath("//a[contains(text(),'Logout')]")).Click();
        }

        public string EmailCreate(string eMail)
        {
            Random uniValue = new Random();
            for (int i = 0; i < 6; i++)
            {
                int value = uniValue.Next(0, 9);
                eMail += value.ToString();
            }
            eMail += "@mail.test";
            return eMail;
        }

        [OneTimeTearDown]
        public void stop()
        {
            CHdriver.Quit();
        }
    }
}
