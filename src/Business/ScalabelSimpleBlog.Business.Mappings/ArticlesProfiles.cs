using System.Linq;
using AutoMapper;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;
using ScalabelSimpleBlog.Business.Dto.TagControllerDto;
using ScalabelSimpleBlog.Entities;

namespace ScalabelSimpleBlog.Business.Mappings
{
    public class ArticlesProfiles : Profile
    {
        protected override void Configure()
        {
            CreateMap<Article, Dto.HomeCotrollerDto.LatestArticlesDto>();
            CreateMap<Article, Dto.BlogControllerDto.LatestArticlesDto>()
                .ForMember(dest => dest.DateFrom, opt => opt.MapFrom(x => x.EditedDate.HasValue ? x.EditedDate.Value : x.CreatedDate));
            CreateMap<Article, ArticlesIndexDto>();
            CreateMap<Article, FullArticleDto>();
            CreateMap<Article, MostPopularArticleDto>()
                .ForMember(dest => dest.ClicksCount, opt => opt.MapFrom(x => x.StatiscticArticleViews.Count()));
        }
    }
}
