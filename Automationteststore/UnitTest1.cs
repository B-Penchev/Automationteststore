using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using FluentAssertions;

namespace Automationteststore
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("chrome")]
        [InlineData("edge")]
        public void NewAccountTest(string browser)
        {
            IWebDriver driver = browser switch
            {
                "chrome" => new ChromeDriver(),
                "edge" => new EdgeDriver(),
                _ => throw new ArgumentException("Unknown browser")
            };

            try
            {
                driver.Url = "https://automationteststore.com/";
                driver.FindElement(By.LinkText("Login or register")).Click();
                driver.FindElement(By.CssSelector("button[title='Continue']")).Click();
                driver.FindElement(By.CssSelector("#AccountFrm_firstname")).SendKeys("Boris");
                driver.FindElement(By.CssSelector("#AccountFrm_lastname")).SendKeys("Penchev");
                driver.FindElement(By.CssSelector("#AccountFrm_email")).SendKeys("test" + System.Guid.NewGuid() + "@test.com");
                driver.FindElement(By.CssSelector("#AccountFrm_address_1")).SendKeys("Mladost");
                driver.FindElement(By.CssSelector("#AccountFrm_city")).SendKeys("Sofia");

                var selectCountry = driver.FindElement(By.Id("AccountFrm_country_id"));
                var selectCoun = new SelectElement(selectCountry);
                selectCoun.SelectByValue("33");

                Thread.Sleep(100);
                var selectRegion = driver.FindElement(By.CssSelector("#AccountFrm_zone_id"));
                var selectReg = new SelectElement(selectRegion);
                selectReg.SelectByText("Sofia - town");

                driver.FindElement(By.CssSelector("#AccountFrm_postcode")).SendKeys("1712");
                driver.FindElement(By.CssSelector("#AccountFrm_loginname")).SendKeys("Boris" + System.Guid.NewGuid());
                driver.FindElement(By.CssSelector("#AccountFrm_password")).SendKeys("0000");
                driver.FindElement(By.CssSelector("#AccountFrm_confirm")).SendKeys("0000");
                driver.FindElement(By.Id("AccountFrm_agree")).Click();
                driver.FindElement(By.CssSelector("button[title='Continue']")).Click();

                var welcomeText = driver.FindElement(By.CssSelector(".menu_text")).Text;
                welcomeText.Should().Contain("Welcome back Boris", "because the user just registered");

                var sideHeader = driver.FindElement(By.XPath("//*[@id=\"maincontainer\"]/div/div[2]/div[1]/h2/span"));
                sideHeader.Text.Should().Be("MY ACCOUNT");

            }
            finally
            {
                driver.Quit();
                driver.Dispose();
            }
        }
        [Theory]
        [InlineData("chrome")]
        [InlineData("edge")]
        public void RegistrationFormValidationMessage(string browser)
        {
            IWebDriver driver = browser switch
            {
                "chrome" => new ChromeDriver(),
                "edge" => new EdgeDriver(),
                _ => throw new ArgumentException("Unknown browser")
            };

            try
            {
                driver.Url = "https://automationteststore.com/";
                driver.FindElement(By.LinkText("Login or register")).Click();
                driver.FindElement(By.CssSelector("button[title='Continue']")).Click();
                driver.FindElement(By.Id("AccountFrm_agree")).Click();
                driver.FindElement(By.CssSelector("button[title='Continue']")).Click();

                var errorMessage = driver.FindElement(By.XPath("//*[@id=\"AccountFrm\"]/div[3]/fieldset/div[1]/span")).Text;
                errorMessage.Should().Contain("Login name must be alphanumeric only and between 5 and 64 characters!", "because the user name do not meet requirement");

                

            }
            finally
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        [Theory]
        [InlineData("chrome")]
        [InlineData("edge")]
        public void SpecialOffersTest(string browser)
        {
            IWebDriver driver = browser switch
            {
                "chrome" => new ChromeDriver(),
                "edge" => new EdgeDriver(),
                _ => throw new ArgumentException("Unknown browser")
            };

            try
            {
                driver.Url = "https://automationteststore.com/";
                driver.Manage().Window.Maximize();
                driver.FindElement(By.LinkText("Login or register")).Click();
                driver.FindElement(By.Id("loginFrm_loginname")).SendKeys("Borisfb9e5845-6b01-4e31-87b5-5f48d475e2c4");
                driver.FindElement(By.Id("loginFrm_password")).SendKeys("0000");
                driver.FindElement(By.CssSelector("button[title='Login']")).Click();
                
                driver.FindElement(By.CssSelector("a.menu_specials")).Click();

                var products = driver.FindElements(By.CssSelector(".thumbnail"));
                foreach (var product in products)
                {
                    var salePrice = product.FindElements(By.CssSelector(".pricenew"));
                    salePrice.Should().NotBeEmpty("Every product on the Specials page must display a discounted price.");
                }

            }
            finally
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}