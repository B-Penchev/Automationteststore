using OpenQA.Selenium;

namespace UI.Pages;

public class LoginRegisterPage : BasePage
{
    private static readonly By LoginName = By.Id("loginFrm_loginname");
    private static readonly By Password = By.Id("loginFrm_password");
    private static readonly By LoginButton = By.CssSelector("button[title='Login']");
    private static readonly By ContinueButton = By.CssSelector("button[title='Continue']");

    public LoginRegisterPage(IWebDriver driver) : base(driver) { }

    public RegistrationPage ClickContinueToRegister()
    {
        WaitForElement(ContinueButton).Click();
        return new RegistrationPage(Driver);
    }

    public HomePage Login(string loginName, string password)
    {
        WaitForElement(LoginName).SendKeys(loginName);
        Driver.FindElement(Password).SendKeys(password);
        WaitForElement(LoginButton).Click();
        return new HomePage(Driver);
    }
}