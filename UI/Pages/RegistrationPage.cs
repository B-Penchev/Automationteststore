using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace UI.Pages;

public class RegistrationPage : BasePage
{
    private static readonly By FirstName = By.Id("AccountFrm_firstname");
    private static readonly By LastName = By.Id("AccountFrm_lastname");
    private static readonly By Email = By.Id("AccountFrm_email");
    private static readonly By Address = By.Id("AccountFrm_address_1");
    private static readonly By City = By.Id("AccountFrm_city");
    private static readonly By Country = By.Id("AccountFrm_country_id");
    private static readonly By Zone = By.Id("AccountFrm_zone_id");
    private static readonly By Postcode = By.Id("AccountFrm_postcode");
    private static readonly By LoginName = By.Id("AccountFrm_loginname");
    private static readonly By Password = By.Id("AccountFrm_password");
    private static readonly By Confirm = By.Id("AccountFrm_confirm");
    private static readonly By Agree = By.Id("AccountFrm_agree");
    private static readonly By Continue = By.CssSelector("button[title='Continue']");
    private static readonly By LoginError = By.XPath("//*[@id=\"AccountFrm\"]/div[3]/fieldset/div[1]/span");

    public RegistrationPage(IWebDriver driver) : base(driver) { }

    public RegistrationPage FillPersonalDetails(string firstName, string lastName,
        string email, string address, string city)
    {
        WaitForElement(FirstName).SendKeys(firstName);
        Driver.FindElement(LastName).SendKeys(lastName);
        Driver.FindElement(Email).SendKeys(email);
        Driver.FindElement(Address).SendKeys(address);
        Driver.FindElement(City).SendKeys(city);
        return this;
    }

    public RegistrationPage SelectCountry(string countryValue)
    {
        new SelectElement(WaitForElement(Country)).SelectByValue(countryValue);
        return this;
    }

    public RegistrationPage SelectZone(string zoneValue)
    {
        new WebDriverWait(Driver, TimeSpan.FromSeconds(10))
           .Until(d =>
           {
               try
               {
                   var select = new SelectElement(d.FindElement(Zone));
                   return select.Options.Any(o => o.Text == zoneValue);
               }
               catch (StaleElementReferenceException)
               {
                   return false;
               }
           });
        new SelectElement(Driver.FindElement(Zone)).SelectByText(zoneValue);
        return this;
    }

    public RegistrationPage FillPostcode(string postcode)
    {
        WaitForElement(Postcode).SendKeys(postcode);
        return this;
    }

    public RegistrationPage FillAccountDetails(string loginName, string password)
    {
        WaitForElement(LoginName).SendKeys(loginName);
        Driver.FindElement(Password).SendKeys(password);
        Driver.FindElement(Confirm).SendKeys(password);
        return this;
    }

    public RegistrationPage AgreeToTerms()
    {
        WaitForElement(Agree).Click();
        return this;
    }

    public AccountPage Submit()
    {
        WaitForElement(Continue).Click();
        return new AccountPage(Driver);
    }

    public RegistrationPage SubmitAndExpectValidationError()
    {
        WaitForElement(Continue).Click();
        return this;
    }

    public string GetLoginNameErrorText()
        => WaitForElement(LoginError).Text;
}