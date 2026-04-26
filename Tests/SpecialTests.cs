using FluentAssertions;

namespace Tests;

public class SpecialsTests : BaseTest
{
    [Fact]
    public void SpecialsPage_AllProducts_DisplayDiscountedPrice()
    {
        var specialsPage = HomePage
            .ClickLoginOrRegister()
            .Login(Settings.ExistingUser.LoginName, Settings.ExistingUser.Password)
            .ClickSpecials();

        var salePricePresence = specialsPage.GetSalePricePresencePerProduct();

        salePricePresence.Should().NotBeEmpty(
            because: "the Specials page should always have at least one product");

        salePricePresence.Should().AllSatisfy(hasPrice =>
            hasPrice.Should().BeTrue(
                because: "every product on the Specials page must display a discounted price"));
    }
}