using Core.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace Core.Driver;

public static class DriverFactory
{
    public static IWebDriver Create(string browser)
    {
        return browser.ToLower().Trim() switch
        {
            "chrome" => CreateChromeDriver(),
            "edge" => CreateEdgeDriver(),
            _ => throw new ArgumentException($"Unsupported browser: '{browser}'.")
        };
    }

    private static IWebDriver CreateChromeDriver()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        return new ChromeDriver(options);
    }

    private static IWebDriver CreateEdgeDriver()
    {
        var options = new EdgeOptions();
        options.AddArgument("--start-maximized");
        return new EdgeDriver(options);
    }
}