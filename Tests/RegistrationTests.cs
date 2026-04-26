using FluentAssertions;
using Tests;

namespace Tests;

public class RegistrationTests : BaseTest
{
    [Theory]
    [ClassData(typeof(BrowserData))]
    public void NewAccount_WithValidDetails_ShowsWelcomeMessage(string browser)
    {
        InitBrowser(browser);

        var accountPage = HomePage
            .ClickLoginOrRegister()
            .ClickContinueToRegister()
            .FillPersonalDetails(
                firstName: "Boris",
                lastName: "Penchev",
                email: $"test{Guid.NewGuid()}@test.com",
                address: "Mladost",
                city: "Sofia")
            .SelectCountry("33")
            .SelectZone("Sofia - town")
            .FillPostcode("1712")
            .FillAccountDetails(
                loginName: $"Boris{Guid.NewGuid()}",
                password: "0000")
            .AgreeToTerms()
            .Submit();

        accountPage.GetWelcomeText()
            .Should().Contain("Welcome back Boris");
        accountPage.GetSidebarHeaderText()
            .Should().Be("MY ACCOUNT");
    }

    [Theory]
    [ClassData(typeof(BrowserData))]
    public void RegistrationForm_WithEmptyFields_ShowsLoginNameValidationError(string browser)
    {
        InitBrowser(browser);

        var registrationPage = HomePage
            .ClickLoginOrRegister()
            .ClickContinueToRegister()
            .AgreeToTerms()
            .SubmitAndExpectValidationError();

        registrationPage.GetLoginNameErrorText()
            .Should().Contain(
                "Login name must be alphanumeric only and between 5 and 64 characters!");
    }
}