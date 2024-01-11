using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Services;
using Services.Service.CMS.Service.CMS.ManageSourceBlog.Module;

namespace MyWebSite.CMS.Controllers;
public class ManageBlogSourceController : SharedController
{
    private readonly IManageSourceBlogService _manageSourceBlogService;
    public ManageBlogSourceController(IManageSourceBlogService manageSourceBlogService)
    {
        _manageSourceBlogService = manageSourceBlogService;
    }
    public async Task<IActionResult> List()
    {
        return View();
    }

    [HttpGet]
    public async Task<JsonResult> RunGetNewestBlogSource()
    {
        await _manageSourceBlogService.GetNewestBlogSourceAsync();
        return Json(true);
    }
}
