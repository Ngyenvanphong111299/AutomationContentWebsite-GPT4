using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Microsoft.AspNetCore.SignalR;
using HtmlAgilityPack;

namespace Services.Service.CMS.Service.CMS.ManageSourceBlog.Module;

public class ManageSourceBlogService(IHubContext<ManageBlogSourceHub> _hubContext) : IManageSourceBlogService
{
    private static IWebDriver GetWebDriver()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless");
        return new ChromeDriver(options);
    }
  
    public async Task GetBlogFromHackADay(IWebDriver driver)
    {
        driver.Navigate().GoToUrl("https://hackaday.com/blog/");
        var elinks = driver.FindElements(By.CssSelector(".entry-title a")).ToList();
        var eimages = driver.FindElements(By.CssSelector(".entry-featured-image a")).ToList();
        for (int i = 0; i < elinks.Count; i++)
        {
            var title = elinks[i].Text;
            var link = elinks[i].GetAttribute("href");
            var avatarPath = eimages[i].GetAttribute("style")
                .Replace("background-image: url(\"", string.Empty)
                .Replace("\");", string.Empty);

            await _hubContext.Clients.All.SendAsync("ReceiveNewBlogSource", new BlogSourceDto(title, link, avatarPath));
        }
    }

    public async Task GetNewestBlogSourceAsync()
    {
        var driver = GetWebDriver();
        await GetBlogFromHackADay(driver);
        driver.Close();

    }
}
