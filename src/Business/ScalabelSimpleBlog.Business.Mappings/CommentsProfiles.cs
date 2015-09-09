using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
