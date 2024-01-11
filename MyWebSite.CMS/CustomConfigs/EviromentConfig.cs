namespace MyWebSite.CMS.CustomConfigs;
public static class EviromentConfig
{
    public static void UseCustomEnviromentConfig(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
    }
}
