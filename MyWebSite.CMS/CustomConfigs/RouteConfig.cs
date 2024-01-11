namespace MyWebSite.CMS.CustomConfigs
{
    public static class RouteConfig
    {
        public static void UseCustomRoute(this WebApplication app)
        {
            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

        }
    }
}
