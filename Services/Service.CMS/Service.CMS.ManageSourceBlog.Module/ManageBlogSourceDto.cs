using System;

namespace Services.Service.CMS.Service.CMS.ManageSourceBlog.Module;

public record BlogSourceDto(string Title, string Link, string AvatarPath = "")
{
    public string Domain { get => new Uri(Link).Host ; }
};
