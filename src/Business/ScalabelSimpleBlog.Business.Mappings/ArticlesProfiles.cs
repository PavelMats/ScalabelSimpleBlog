using AutoMapper;
using ScalabelSimpleBlog.Business.Dto.HomeCotrollerDto;
using ScalabelSimpleBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Business.Mappings
{
    public class ArticlesProfiles : Profile
    {
        protected override void Configure()
        {
            CreateMap<Article, LatestArticlesDto>();
        }
    }
}
