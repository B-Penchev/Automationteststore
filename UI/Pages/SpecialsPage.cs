using OpenQA.Selenium;

namespace UI.Pages;

public class SpecialsPage : BasePage
{
    private static readonly By ProductThumbnails = By.CssSelector(".thumbnail");
    private static readonly By SalePriceLabel = By.CssSelector(".pricenew");

    public SpecialsPage(IWebDriver driver) : base(driver) { }

    /// <summary>
    /// Returns true for every product thumbnail that has a visible sale price element.
    /// </summary>
    public IReadOnlyList<bool> GetSalePricePresencePerProduct()
    {
        var thumbnails = Wait.Until(driver =>
        {
            var items = driver.FindElements(ProductThumbnails);
            return items.Count > 0 ? items : null;
        })!;

        return thumbnails
            .Select(t => t.FindElements(SalePriceLabel).Count > 0)
            .ToList();
    }
}