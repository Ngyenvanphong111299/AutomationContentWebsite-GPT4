using Microsoft.AspNetCore.Http;

namespace Services.Service.CMS.Service.CMS.ManageBlog.Module;
public interface IManageBlogService
{
    Task<BlogDtoForCreate> GetBlogDtoForCreateAsync();
    Task<BaseResult<bool>> CreateAsync(BlogDtoForCreate dto);
    Task<BlogDtoForUpdate> GetBlogDtoForUpdateAsync(Guid id);
    Task<BaseResult<bool>> UpdateAsync(BlogDtoForUpdate dto);
    Task<BaseResult<bool>> DeleteAsync(Guid id);
    Task<BlogDtoForList> GetBlogDtoForListAsync();
    Task<BlogDtoForSource> GetBlogSourceDataAsync(string link);
    Task<string> BuildGPTPromtForCreate(BlogDtoForSource dto);
    Task<string> BuildGPTPromtForCreateAvatarImage(string blogTitle, string blogDescription);
    Task<string> BuildGPTPromtForCreateContentImage(string blogTitle, string subTitle);
    Task<bool> IsCategoryExistAsync(string categoryName);
    Task<bool> IsTagExistAsync(string tagName);
}
