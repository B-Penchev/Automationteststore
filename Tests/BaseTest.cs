using Core.Config;
using Core.Driver;
using OpenQA.Selenium;
using UI.Pages;

namespace Tests;

public abstract class BaseTest : IDisposable
{
    private DriverManager? _driverManager;
    private bool _disposed;

    protected IWebDriver Driver => _driverManager!.Driver;
    protected TestSettings Settings => ConfigurationLoader.Settings;
    protected HomePage HomePage => new(Driver);

    protected void InitBrowser(string browser)
    {
        _driverManager = new DriverManager();
        _driverManager.Initialise(browser);
        Driver.Url = Settings.BaseUrl;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
            _driverManager?.Dispose();
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}