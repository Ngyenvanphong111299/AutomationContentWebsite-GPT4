using AutoMapper;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Percistences.EntityRepositories.Manager;
using Microsoft.AspNetCore.SignalR;
namespace Services.Service.CMS.Service.CMS.ManageBlog.Module;

public class ManageBlogService(
    IMapper _mapper, 
    IRepositoryManager _repositoryManager) : IManageBlogService
{
    public async Task<string> BuildGPTPromtForCreate(BlogDtoForSource dto)
    {
        var promt = Constant.GPTGenerateBlogPromt(dto);
        return await Task.FromResult(promt);
    }

    public async Task<BaseResult<bool>> CreateAsync(BlogDtoForCreate dto)
    {
        var blog = _mapper.Map<Blog>(dto);
        var result = await _repositoryManager.Blog.CreateAsync(blog);

        return new BaseResult<bool>()
            .WithStatus(result.IsSuccess);
    }

    public async Task<BaseResult<bool>> DeleteAsync(Guid id)
    {
        var result = await _repositoryManager.Blog.DeleteAsync(id);

        return new BaseResult<bool>()
            .WithStatus(result.IsSuccess);
    }

    public async Task<BlogDtoForCreate> GetBlogDtoForCreateAsync()
    {
        await Task.Delay(1);
        var result = new BlogDtoForCreate();

        return result;
    }

    public async Task<BlogDtoForList> GetBlogDtoForListAsync()
    {
        var blogs = await _repositoryManager.Blog
            .StartQuery()
            .Select(blog => new BlogDto
            {
                Id = blog.Id,
                Title = blog.Title,
            })
            .ToListAsync();

        var blogDtos = _mapper.Map<List<BlogDto>>(blogs);
        var result = new BlogDtoForList
        {
            Blogs = blogDtos
        };

        return result;
    }

    public async Task<BlogDtoForUpdate> GetBlogDtoForUpdateAsync(Guid id)
    {
        var blog = await _repositoryManager.Blog.GetByIdAsync(id);
        var dto = _mapper.Map<BlogDtoForUpdate>(blog);

        return dto;
    }

    public async Task<BaseResult<bool>> UpdateAsync(BlogDtoForUpdate dto)
    {
        var blog = _mapper.Map<Blog>(dto);
        var result = await _repositoryManager.Blog.UpdateAsync(blog);

        return new BaseResult<bool>()
            .WithStatus(result.IsSuccess);
    }

    private static IWebDriver GetWebDriver()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless");
        return new ChromeDriver(options);
    }
   
    public async Task<BlogDtoForSource> GetBlogSourceDataAsync(string link)
    {
        await Task.Delay(1);
        var driver = GetWebDriver();
        driver.Navigate().GoToUrl(link);

        var title = driver.FindElement(By.ClassName("entry-title")).Text;
        var content = driver.FindElement(By.ClassName("entry-content")).Text;
        driver.Close();

        return new BlogDtoForSource(title, content);
    }

    public Task<string> BuildGPTPromtForCreateContentImage(string blogTitle, string subTitle)
        => Task.FromResult(Constant.GPTGenerateBlogContentImage(blogTitle, subTitle));
    
    public Task<string> BuildGPTPromtForCreateAvatarImage(string blogTitle, string blogDescription)
        => Task.FromResult(Constant.GPTGenerateBlogAvatarImage(blogTitle, blogDescription));

    public async Task<bool> IsCategoryExistAsync(string categoryName)
    {
        var result = await _repositoryManager.BlogCategory.GetByConditionAsync(x => x.Title == categoryName);
        return result.IsSuccess;
    }

    public async Task<bool> IsTagExistAsync(string tagName)
    {
        var result = await _repositoryManager.Tag.GetByConditionAsync(x => x.Title == tagName);
        return result.IsSuccess;
    }
}
