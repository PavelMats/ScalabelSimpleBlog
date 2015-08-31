using AutoMapper;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;
using ScalabelSimpleBlog.Entities;

namespace ScalabelSimpleBlog.Business.Mappings
{
    public class ArticlesProfiles : Profile
    {
        protected override void Configure()
        {
            CreateMap<Article, Dto.HomeCotrollerDto.LatestArticlesDto>();
            CreateMap<Article, Dto.BlogControllerDto.LatestArticlesDto>();
            CreateMap<Article, ArticlesIndexDto>();
            CreateMap<Article, FullArticleDto>();
        }
    }
}
