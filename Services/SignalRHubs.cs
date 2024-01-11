using Microsoft.AspNetCore.SignalR;
using Services.Service.CMS.Service.CMS.ManageSourceBlog.Module;

namespace Services
{
    public class ManageBlogSourceHub : Hub
    {
        public async Task CreateNewBlogSource(BlogSourceDto dto)
        {
            await Clients.All.SendAsync("ReceiveNewBlogSource", dto);
        }
    }

    public class ManageBlogHub : Hub
    {
        public async Task CreateBlogMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessageBlog", message);
        }
    }
}
