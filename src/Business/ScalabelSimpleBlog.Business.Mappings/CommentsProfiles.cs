using AutoMapper;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;
using ScalabelSimpleBlog.Data.Entities;

namespace ScalabelSimpleBlog.Business.Mappings
{
    public class CommentsProfiles : Profile
    {
        protected override void Configure()
        {
            CreateMap<Comment, ArticleCommantDto>()
                .ForMember(x => x.AuthorName, opt => opt.MapFrom(x => x.Author.FirstName + " " + x.Author.LastName));

            CreateMap<Comment, LatestCommentDto>()
                .ForMember(dest => dest.ArticleHeader, opt => opt.MapFrom(src => src.Article.Header))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(x => x.Author.FirstName + " " + x.Author.LastName));
        }
    }
}
