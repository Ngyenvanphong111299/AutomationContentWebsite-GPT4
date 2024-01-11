using Humanizer;
using Infrastructures.UploadImageService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Services;
using Services.Service.CMS.Service.CMS.ManageBlog.Module;
namespace MyWebSite.CMS.Controllers;
public class ManageBlogController(
    IManageBlogService _manageBlogService,
    IUploadImageService _uploadImageService,
    IHubContext<ManageBlogHub> _hubContext
    ) : SharedController
{
    [HttpGet]
    public async Task<IActionResult> List()
    {

        return View(await _manageBlogService.GetBlogDtoForListAsync());
    }
        
    [HttpGet]
    public async Task<IActionResult> Create(string link)
    {
        ViewBag.Link = link;
        return View(await _manageBlogService.GetBlogDtoForCreateAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogDtoForCreate dto)
    {
        var result = await _manageBlogService.CreateAsync(dto);
        if (result.IsSuccess)
            return RedirectToAction(nameof(List));

        return View(dto);
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
        => View(await _manageBlogService.GetBlogDtoForUpdateAsync(id));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(BlogDtoForUpdate dto)
    {
        var result = await _manageBlogService.UpdateAsync(dto);
        if (result.IsSuccess)
            return RedirectToAction(nameof(List));

        return View(dto);
    }

    [HttpGet]
    public async Task<JsonResult> Delete(Guid id)
        => Json(await _manageBlogService.DeleteAsync(id));

    #region ajax helper requests
    [HttpGet]
    public async Task<JsonResult> SetCreateInitData(string link)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessageBlog", "Geting blog data...");
        var blogSourceData = await _manageBlogService.GetBlogSourceDataAsync(link);

        await _hubContext.Clients.All.SendAsync("ReceiveMessageBlog", "Geting GPT Promt...");
        var gptPromt = await _manageBlogService.BuildGPTPromtForCreate(blogSourceData);

        return Json(new
        {
            GPTPromt = gptPromt,
            SourceTitle = blogSourceData.Title,
            SourceContent = blogSourceData.content
        });
    }

    [HttpGet]
    public async Task<JsonResult> GetCreateImagePromt(string blogTitle, string subTitle)
        => Json(await _manageBlogService.BuildGPTPromtForCreateContentImage(blogTitle, subTitle));

    [HttpGet]
    public async Task<JsonResult> GetCreateAvatarPromt(string blogTitle, string blogDescription)
        => Json(await _manageBlogService.BuildGPTPromtForCreateAvatarImage(blogTitle, blogDescription));

    [HttpGet]
    public async Task<JsonResult> CheckCategoryExist(string categoryName)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessageBlog", $"Checking category {categoryName}...");
        return Json(await _manageBlogService.IsCategoryExistAsync(categoryName));
    }
    

    [HttpGet]
    public async Task<JsonResult> CheckTagExist(string tagName)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessageBlog", $"Checking tag {tagName}...");
        return Json(await _manageBlogService.IsTagExistAsync(tagName));
    }

    [HttpPost]
    public async Task<JsonResult> CreateImage(IFormFile image, string title)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessageBlog", "Creating Image...");
        return Json(await _uploadImageService.Upload(image, Helper.ConvertToUrl(title)));
    }
    

    #endregion
}
