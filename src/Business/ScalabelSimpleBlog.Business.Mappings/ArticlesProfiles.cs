using AutoMapper;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;
using ScalabelSimpleBlog.Business.Dto.HomeCotrollerDto;
using ScalabelSimpleBlog.Entities;

namespace ScalabelSimpleBlog.Business.Mappings
{
    public class ArticlesProfiles : Profile
    {
        protected override void Configure()
        {
            CreateMap<Article, LatestArticlesDto>();
            CreateMap<Article, ArticlesIndexDto>();
        }
    }
}
