using Microsoft.Extensions.Configuration;

namespace Tests;

public class BrowserData : TheoryData<string>
{
    public BrowserData()
    {
        Add("chrome");
        Add("edge");
    }
}