using FluentAssertions;

namespace Tests;

public class SpecialsTests : BaseTest
{
    [Theory]
    [ClassData(typeof(BrowserData))]
    public void SpecialsPage_AllProducts_DisplayDiscountedPrice(string browser)
    {
        InitBrowser(browser);

        var specialsPage = HomePage
            .ClickLoginOrRegister()
            .Login(Settings.ExistingUser.LoginName, Settings.ExistingUser.Password)
            .ClickSpecials();

        var salePricePresence = specialsPage.GetSalePricePresencePerProduct();

        salePricePresence.Should().NotBeEmpty();
        salePricePresence.Should().AllSatisfy(hasPrice =>
            hasPrice.Should().BeTrue());
    }
}