using FluentAssertions;

namespace Tests;

public class RegistrationTests : BaseTest
{
    [Fact]
    public void NewAccount_WithValidDetails_ShowsWelcomeMessage()
    {
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
            .Should().Contain("Welcome back Boris",
                because: "the user just completed registration");

        accountPage.GetSidebarHeaderText()
            .Should().Be("MY ACCOUNT");
    }

    [Fact]
    public void RegistrationForm_WithEmptyFields_ShowsLoginNameValidationError()
    {
        var registrationPage = HomePage
            .ClickLoginOrRegister()
            .ClickContinueToRegister()
            .AgreeToTerms()
            .SubmitAndExpectValidationError();

        registrationPage.GetLoginNameErrorText()
            .Should().Contain(
                "Login name must be alphanumeric only and between 5 and 64 characters!",
                because: "the login name field was left empty");
    }
}