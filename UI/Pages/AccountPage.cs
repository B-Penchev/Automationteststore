using OpenQA.Selenium;
using UI.Pages;

public class AccountPage : BasePage
{
    private static readonly By SidebarHeader = By.XPath("//*[@id=\"maincontainer\"]/div/div[2]/div[1]/h2/span");
    private static readonly By WelcomeText = By.CssSelector(".menu_text");

    public AccountPage(IWebDriver driver) : base(driver) { }

    public string GetWelcomeText()
        => WaitForElement(WelcomeText).Text;

    public string GetSidebarHeaderText()
        => WaitForElement(SidebarHeader).Text;
}