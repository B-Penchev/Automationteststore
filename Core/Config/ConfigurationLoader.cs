using Microsoft.Extensions.Configuration;

namespace Core.Config;

public static class ConfigurationLoader
{
    private static TestSettings? _settings;

    public static TestSettings Settings => _settings ??= Load();

    private static TestSettings Load()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        var settings = new TestSettings();
        config.Bind(settings);
        return settings;
    }
}