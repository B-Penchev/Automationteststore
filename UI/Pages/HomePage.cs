using OpenQA.Selenium;

namespace UI.Pages;

public class HomePage : BasePage
{
    private static readonly By LoginRegisterLink = By.LinkText("Login or register");
    private static readonly By SpecialsLink = By.CssSelector("a.menu_specials");
    private static readonly By WelcomeText = By.CssSelector(".menu_text");

    public HomePage(IWebDriver driver) : base(driver) { }

    public LoginRegisterPage ClickLoginOrRegister()
    {
        WaitForElement(LoginRegisterLink).Click();
        return new LoginRegisterPage(Driver);
    }

    public SpecialsPage ClickSpecials()
    {
        WaitForElement(SpecialsLink).Click();
        return new SpecialsPage(Driver);
    }

    public string GetWelcomeText()
        => WaitForElement(WelcomeText).Text;
}