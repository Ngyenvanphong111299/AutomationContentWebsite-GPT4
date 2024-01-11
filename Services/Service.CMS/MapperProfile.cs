using AutoMapper;
using Entities.Models;
using Services.Service.CMS.Service.CMS.ManageBlog.Module;
using Services.Service.CMS.Service.CMS.ManageSourceBlog.Module;

namespace Services.Service.CMS;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Blog, BlogDtoForCreate>().ReverseMap();
        CreateMap<Blog, BlogDtoForList>().ReverseMap();
        CreateMap<Blog, BlogDtoForUpdate>().ReverseMap();

        //manage source blog
        CreateMap<BlogSourceDto, SourceBlog>().ReverseMap();
    }
}
