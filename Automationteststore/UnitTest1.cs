using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V145.Network;
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
                //Your Personal Details
                driver.FindElement(By.CssSelector("#AccountFrm_firstname")).SendKeys("Boris");
                driver.FindElement(By.CssSelector("#AccountFrm_lastname")).SendKeys("Penchev");
                driver.FindElement(By.CssSelector("#AccountFrm_email")).SendKeys("test" + System.Guid.NewGuid() + "@test.com");
                
                driver.FindElement(By.CssSelector("#AccountFrm_address_1")).SendKeys("Mladost");
                driver.FindElement(By.CssSelector("#AccountFrm_city")).SendKeys("Sofia");
                driver.FindElement(By.Id("AccountFrm_agree")).Click();

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


                driver.FindElement(By.CssSelector("button[title='Continue']")).Click();

                var welcomeText = driver.FindElement(By.CssSelector(".menu_text")).Text;
                welcomeText.Should().Contain("Welcome back Boris", "because the user just registered");

            }
            finally
            {
                driver.Quit();
                driver.Dispose();
            }

        }
    }
}