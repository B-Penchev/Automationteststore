namespace Core.Config;

public class TestSettings
{
    public List<string> Browsers { get; set; } = new();
    public string BaseUrl { get; set; } = string.Empty;
    public ExistingUserSettings ExistingUser { get; set; } = new();
}

public class ExistingUserSettings
{
    public string LoginName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}