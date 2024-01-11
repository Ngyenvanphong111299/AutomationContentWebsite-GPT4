using MyWebSite.CMS.CustomConfigs;
using MyWebSite.CMS.Extensions;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.CustomUseControllerWithView();
builder.CustomUseSqlServer();
builder.CustomUseRepository();
builder.CustomUservice();
builder.CustomUseAutoMapper();
builder.CustomUseSignalR();

var app = builder.Build();
app.UseCustomEnviromentConfig();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseCustomRoute();

app.MapHub<ManageBlogSourceHub>("/ManageBlogSourceHub");
app.MapHub<ManageBlogHub>("/ManageBlogHub");

app.Run();
