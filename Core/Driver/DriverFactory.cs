using Core.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace Core.Driver;

public static class DriverFactory
{
    public static IWebDriver Create()
    {
        var browser = ConfigurationLoader.Settings.Browser.ToLower().Trim();

        return browser switch
        {
            "chrome" => CreateChromeDriver(),
            "edge" => CreateEdgeDriver(),
            _ => throw new ArgumentException($"Unsupported browser: '{browser}'. Valid values are 'chrome' or 'edge'.")
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