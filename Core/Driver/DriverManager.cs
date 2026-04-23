using OpenQA.Selenium;

namespace Core.Driver;

public class DriverManager : IDisposable
{
    private IWebDriver? _driver;

    public IWebDriver Driver
    {
        get
        {
            if (_driver is null)
                throw new InvalidOperationException("Driver has not been initialised. Call Initialise() first.");
            return _driver;
        }
    }

    public void Initialise()
    {
        _driver = DriverFactory.Create();
    }

    public void Dispose()
    {
        _driver?.Quit();
        _driver?.Dispose();
        _driver = null;
    }
}