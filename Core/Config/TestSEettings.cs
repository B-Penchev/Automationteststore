namespace Core.Config;

public class TestSettings
{
    public string Browser { get; set; } = "chrome";
    public string BaseUrl { get; set; } = "https://automationteststore.com/";
    public ExistingUserSettings ExistingUser { get; set; } = new();
}

public class ExistingUserSettings
{
    public string LoginName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}