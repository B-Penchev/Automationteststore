# Automation Test Store - Selenium WebDriver Test Suite

Automated UI tests for [automationteststore.com](https://automationteststore.com/) using **Selenium WebDriver**, **xUnit**, and **FluentAssertions** in **.NET 8**.

## Scope

This project automates 3 User Cases:

1. **UC-1 Test creation of a new account** - Validates full registration flow
2. **UC-2 Test validation messages on Registration form** - Ensures error messages trigger on empty/invalid inputs
3. **UC-3 Test Special offers** - Verifies all items in 'Specials' have active discounts

---

## Architecture

```
Automationteststore/
├── Core/                    # Core infrastructure (shared library)
│   ├── Config/
│   │   ├── ConfigurationLoader.cs   # Loads appsettings.json via Microsoft.Extensions.Configuration
│   │   └── TestSettings.cs          # Strongly-typed settings (Browsers, BaseUrl, ExistingUser)
│   └── Driver/
│       ├── DriverFactory.cs         # Creates WebDriver instances (Chrome, Edge)
│       └── DriverManager.cs         # Manages driver lifecycle (IDisposable)
│
├── UI/                      # Page Object Model layer
│   └── Pages/
│       ├── BasePage.cs              # Abstract base with WebDriverWait & common waits
│       ├── HomePage.cs              # Navigation to Login/Register, Specials
│       ├── LoginRegisterPage.cs     # Login form + Continue to registration
│       ├── RegistrationPage.cs      # Fluent registration form filling
│       ├── AccountPage.cs           # Post-login account page verification
│       └── SpecialsPage.cs          # Product thumbnails + sale price validation
│
├── Tests/                   # xUnit test project
│   ├── BaseTest.cs                  # Base test class with browser init/teardown
│   ├── BrowserData.cs               # TheoryData for cross-browser (Chrome, Edge)
│   ├── RegistrationTests.cs         # Happy-path & validation error tests
│   └── SpecialTests.cs              # Specials page discount verification
│
├── appsettings.json (per project)   # Test configuration
└── .gitignore
```

---

## Key Design Patterns

### Page Object Model (POM)
Each page is a class encapsulating locators and actions. Page methods return the next page object for fluent navigation.

### Fluent Builder Pattern (RegistrationPage)
```csharp
var accountPage = HomePage
    .ClickLoginOrRegister()
    .ClickContinueToRegister()
    .FillPersonalDetails(...)
    .SelectCountry("33")
    .SelectZone("Sofia - town")
    .FillPostcode("1712")
    .FillAccountDetails(...)
    .AgreeToTerms()
    .Submit();
```

### Cross-Browser Testing
Tests run on **Chrome** and **Edge** via xUnit Theory + ClassData:
```csharp
[Theory]
[ClassData(typeof(BrowserData))]
public void MyTest(string browser) { ... }
```

### Configuration
Strongly-typed settings via `Microsoft.Extensions.Configuration`:
- `Browsers[]` - list of browsers to test
- `BaseUrl` - target site URL
- `ExistingUser.{LoginName,Password}` - credentials for logged-in tests

### Driver Lifecycle
`DriverManager : IDisposable` wraps `IWebDriver`. `BaseTest : IDisposable` ensures cleanup via `Dispose()` pattern.

---

## Dependencies

| Project | Key Packages |
|---------|-------------|
| **Core** | `Selenium.WebDriver`, `Selenium.WebDriver.ChromeDriver`, `Microsoft.Extensions.Configuration.*` |
| **UI** | `Selenium.WebDriver`, `Selenium.Support`, `DotNetSeleniumExtras.WaitHelpers` |
| **Tests** | `xunit`, `xunit.runner.visualstudio`, `FluentAssertions`, `Microsoft.NET.Test.Sdk`, `coverlet.collector` |

---

## How to Run

### Prerequisites
- .NET 8 SDK
- Chrome and/or Edge browser installed

### Run All Tests
```bash
dotnet test Tests/Tests.csproj
```

### Run in Visual Studio Code
- Open the folder
- Use **Testing** panel (beaker icon) → **Run All Tests**

### Run Specific Browser (filter)
```bash
dotnet test Tests/Tests.csproj --filter "FullyQualifiedName~chrome"
```

---

## Test Scenarios

| Test Class | Method | Description |
|------------|--------|-------------|
| `RegistrationTests` | `NewAccount_WithValidDetails_ShowsWelcomeMessage` | Full registration flow → verifies welcome message + "MY ACCOUNT" sidebar |
| `RegistrationTests` | `RegistrationForm_WithEmptyFields_ShowsLoginNameValidationError` | Submits empty form → verifies "Login name must be alphanumeric only and between 5 and 64 characters!" |
| `SpecialsTests` | `SpecialsPage_AllProducts_DisplayDiscountedPrice` | Logs in → navigates to Specials → asserts every product has visible sale price |

---

## Configuration

### Tests/appsettings.json (used at runtime)
```json
{
  "Browser": "chrome",
  "BaseUrl": "https://automationteststore.com/",
  "ExistingUser": {
    "LoginName": "Borisfb9e5845-6b01-4e31-87b5-5f48d475e2c4",
    "Password": "0000"
  }
}
```

---

## Adding New Tests

1. Create test class inheriting from `BaseTest`
2. Use `[Theory][ClassData(typeof(BrowserData))]` for cross-browser
3. Call `InitBrowser(browser)` in test method
4. Navigate via `HomePage` property
5. Use FluentAssertions: `.Should().Contain(...)` / `.Should().Be(...)`

## Adding New Page Objects

1. Create class in `UI/Pages/` inheriting `BasePage`
2. Define `private static readonly By` locators
3. Expose action methods returning next page object
4. Use `WaitForElement(locator)` for reliable waits

---

## Tech Stack

- **Target Framework**: .NET 8.0
- **Test Runner**: xUnit v2.9.3 with Visual Studio Test Adapter v3.1.5
- **Assertions**: FluentAssertions v8.9.0
- **Wait Strategy**: Explicit waits via `WebDriverWait` (10s) + `ExpectedConditions`
- **Parallel Execution**: Enabled by default in xUnit v3
- **No solution file (.sln)** - uses project references directly