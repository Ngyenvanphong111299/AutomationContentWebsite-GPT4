using Infrastructures.UploadImageService;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Percistences;
using Percistences.EntityRepositories.Manager;
using Services;
using Services.Service.CMS.Service.CMS.ManageBlog.Module;
using Services.Service.CMS.Service.CMS.ManageSourceBlog.Module;
namespace MyWebSite.CMS.Extensions;

public static class BuilderServiceExtension
{
    public static void CustomUseControllerWithView(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
    }

    public static void CustomUseSqlServer(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    }

    public static void CustomUseRepository(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void CustomUseAutoMapper(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(Services.Service.CMS.MapperProfile).Assembly);
    }

    public static void CustomUservice(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IManageBlogService, ManageBlogService>();
        builder.Services.AddScoped<IManageSourceBlogService, ManageSourceBlogService>();
        builder.Services.AddScoped<IUploadImageService, S3UploadImageService>();
    }

    public static void CustomUseSignalR(this WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR();
    }   
}
