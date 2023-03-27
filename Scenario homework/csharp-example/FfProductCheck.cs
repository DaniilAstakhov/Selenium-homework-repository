using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Assert = NUnit.Framework.Assert;
using System.Globalization;

namespace csharp_example
{
    internal class FfProductCheck
    {
        private IWebDriver FFdriver;
        private WebDriverWait wait;

        [OneTimeSetUp]
        public void StartBrowser()
        {
            FFdriver = new FirefoxDriver();
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"C:\Program Files\Firefox Nightly\firefox.exe";
            wait = new WebDriverWait(FFdriver, TimeSpan.FromSeconds(10));
            FFdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void IeProductСheckTest()
        {
            FFdriver.Manage().Cookies.DeleteAllCookies();
            FFdriver.Url = "http://localhost:8080/litecart/en/";

            IReadOnlyCollection<IWebElement> CampaignsProducts = FFdriver.FindElements(By.XPath("//div[@id='box-campaigns']//li[starts-with(@class,'product')]"));
            //Нашли первый товар блока campaigns
            var firstCampaignsProduct = FFdriver.FindElement(By.XPath("//div[@id='box-campaigns']//li[starts-with(@class,'product')][1]"));

            //Получаем его название cнаружи
            var productName = firstCampaignsProduct.FindElement(By.ClassName("name"));
            string outName = productName.GetAttribute("textContent"); //название снаружи

            //Получаем его обычную и акционную цену снаружи
            var regPrice = firstCampaignsProduct.FindElement(By.ClassName("regular-price"));
            var campPrice = firstCampaignsProduct.FindElement(By.ClassName("campaign-price"));

            string outRegPrice = regPrice.GetAttribute("textContent"); //обычная цена снаружи
            string outCampPrice = campPrice.GetAttribute("textContent"); //акционная цена снаружи

            //Получаем стили цен и проверяем соответствие требованиям             
            var regPriceStyle = regPrice.GetCssValue("text-decoration-line"); //перечёркнутость обычной цены

            string lineThroughStyle = "line-through"; //стандарт перечёркнутого стиля для проверки
            string regPriceLineThrough = regPriceStyle.ToString();

            Assert.AreEqual(lineThroughStyle, regPriceLineThrough, "Обычная цена не перечёркнута"); //проверка для обычной цены

            var campPriceStyle = campPrice.GetCssValue("font-weight"); //жирность акционной цены

            int thickness = 700; //стандарт жирности для проверки //в фаерфоксе для акционной оказывается 900 а не 700 (но это только снаружи, внутри товара все равно 700)
            int campPriceThickness = Convert.ToInt32(campPriceStyle);

            Assert.GreaterOrEqual(campPriceThickness, thickness, "Акционная цена не жирная"); // проверка для акционной цены

            //Получаем цвет цен
            var regPriceColor = regPrice.GetCssValue("color"); //цвет обычной цены
            var campPriceColor = campPrice.GetCssValue("color"); //цвет акционной цены

            string regPriceActualColor = regPriceColor.ToString(); //цвет в строки
            string campPriceActualColor = campPriceColor.ToString(); //цвет в строки

            string[] regColorValues = regPriceActualColor.Split(',', '(', ')', ' '); //цвет в корявые массивы
            string[] campColorValues = campPriceActualColor.Split(',', '(', ')', ' '); //цвет в корявые массивы
            int[] regPriceColorView = new int[3]; //нормальные массивы для цвета
            int[] campPriceColorView = new int[3]; //нормальные массивы для цвета

            //перемещение только нужных элементов из корявого массивова в нормальный для цвета обычной цены
            ArrayFill(regPriceColorView, regColorValues);

            //перемещение только нужных элементов из корявого массивова в нормальный для цвета акционной цены
            ArrayFill(campPriceColorView, campColorValues);

            //Проверка того что у обычной цены серый цвет
            Assert.AreEqual(regPriceColorView[0], regPriceColorView[1], regPriceColorView[2], "(Снаружи) Цвет обычной цены у товара не серый");

            //Проверка того что у акционной цены красный цвет
            Assert.AreEqual(campPriceColorView[1], campPriceColorView[2], 0, "(Снаружи) Цвет акционной цены у товара не красный");
            Assert.AreNotEqual(campPriceColorView[0], 0, "(Снаружи) Цвет акционной цены у товара не красный"); // дополнительная проверка что он точно красный

            //Получаем размер цен снаружи и проверяем что размер акционной цены больше чем размер обычной
            string regFontSize = regPrice.GetCssValue("font-size"); // Размер шрифта обычной цены
            string campFontSize = campPrice.GetCssValue("font-size"); // Размер шрифта акционной цены

            double regFontSizeFull = double.Parse(regFontSize.Replace("px", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            double campFontSizeFull = double.Parse(campFontSize.Replace("px", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

            Assert.True(campFontSizeFull > regFontSizeFull, "(Снаружи) Размер акционной цены меньше размера обычной!");

            //Переходим внутрь товара
            firstCampaignsProduct.Click();

            //находим блок продукта
            var inProductBox = FFdriver.FindElement(By.Id("box-product"));

            //Получаем его название внутри
            var inProductName = inProductBox.FindElement(By.ClassName("title"));
            string inName = inProductName.GetAttribute("textContent"); //Название внутри

            //Получаем его обычную и акционную цену внутри
            var inRegPrice = inProductBox.FindElement(By.ClassName("regular-price"));
            var inCampPrice = inProductBox.FindElement(By.ClassName("campaign-price"));

            string inRegularPrice = inRegPrice.GetAttribute("textContent"); // обычная цена внутри
            string inCampaignPrice = inCampPrice.GetAttribute("textContent"); // акционная цена внутри

            //Получаем стили цен и проверяем соответствие требованиям             
            var inRegPriceStyle = inRegPrice.GetCssValue("text-decoration-line"); //перечёркнутость обычной цены
            string inRegPriceLineThrough = inRegPriceStyle.ToString();

            Assert.AreEqual(lineThroughStyle, inRegPriceLineThrough, "Обычная цена внутри не перечёркнута"); //проверка для обычной цены внутри

            var inCampPriceStyle = inCampPrice.GetCssValue("font-weight"); //жирность акционной цены
            int inCampPriceThickness = Convert.ToInt32(inCampPriceStyle);

            int insideThickness = 700; // (Тут по факту она почему-то 700, а снаружи 900)
            Assert.GreaterOrEqual(inCampPriceThickness, insideThickness, "Акционная цена внутри не жирная"); // проверка для акционной цены внутри )

            //Получаем размер цен внутри и проверяем что размер акционной цены больше чем размер обычной
            var inRegFontSize = inRegPrice.GetCssValue("font-size");
            var inCampFontSize = inCampPrice.GetCssValue("font-size");

            double inRegFontSizeFull = double.Parse(inRegFontSize.Replace("px", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            double inCampFontSizeFull = double.Parse(inCampFontSize.Replace("px", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

            Assert.True(inCampFontSizeFull > inRegFontSizeFull, "(Внутри) Размер акционной цены меньше размера обычной!");


            //Получаем цвет цен внутри
            var inRegPriceColor = inRegPrice.GetCssValue("color"); //цвет обычной цены внутри
            var inCampPriceColor = inCampPrice.GetCssValue("color"); //цвет акционной цены внутри

            string inRegPriceActualColor = inRegPriceColor.ToString(); //цвет в строки
            string inCampPriceActualColor = inCampPriceColor.ToString(); //цвет в строки

            string[] inRegColorValues = inRegPriceActualColor.Split(',', '(', ')', ' '); //цвет в корявые массивы
            string[] inCampColorValues = inCampPriceActualColor.Split(',', '(', ')', ' '); //цвет в корявые массивы
            int[] inRegPriceColorView = new int[3]; //нормальные массивы для цвета
            int[] inCampPriceColorView = new int[3]; //нормальные массивы для цвета

            //перемещение только нужных элементов из корявого массивова в нормальный для цвета обычной цены внутри
            ArrayFill(inRegPriceColorView, inRegColorValues);

            //перемещение только нужных элементов из корявого массивова в нормальный для цвета акционной цены внутри
            ArrayFill(inCampPriceColorView, inCampColorValues);

            //Проверка того что у обычной цены внутри серый цвет
            Assert.AreEqual(inRegPriceColorView[0], inRegPriceColorView[1], inRegPriceColorView[2], "(Внутри) Цвет обычной цены у товара не серый");

            //Проверка того что у акционной цены внутри красный цвет
            Assert.AreEqual(inCampPriceColorView[1], inCampPriceColorView[2], 0, "(Внутри) Цвет акционной цены у товара не красный");
            Assert.AreNotEqual(inCampPriceColorView[0], 0, "(Внутри) Цвет акционной цены у товара не красный"); // дополнительная проверка что он точно красный

            //Проверяем что название товара снаружи и внутри совпадает
            Assert.AreEqual(inName, outName, "Название товара снаружи и внутри не совпадает");

            //Проверяем что цены внутри товара и снаружи совпадают
            Assert.AreEqual(inRegularPrice, outRegPrice, "Обычная цена снаружи и внутри не совпадает");
            Assert.AreEqual(inCampaignPrice, outCampPrice, "Акционная цена снаружи и внутри не совпадает");
        }

        public void ArrayFill(int[] FilledInArray, string[] ExtractableArray)
        {
            int B = 0;
            for (int i = 0; i < ExtractableArray.Length; i++)
            {

                try
                {
                    FilledInArray[B] = Convert.ToInt32(ExtractableArray[i]);
                }
                catch (Exception)
                {
                    if (i == 0)
                        goto start;

                    if (B == 2)
                        goto end2;
                    B += 1;
                }
            start:;
            }
        end2:;
        }

        [OneTimeTearDown]
        public void stop()
        {
            FFdriver.Quit();
        }
    }
}
