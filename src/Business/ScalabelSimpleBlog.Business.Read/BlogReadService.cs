using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ScalabelSimpleBlog.Business.Read.Models;
using ScalabelSimpleBlog.Data.Repositories;

namespace ScalabelSimpleBlog.Business.Read
{
    public class BlogReadService : IBlogReadService
    {
        private readonly IMappingEngine mapper;
        private readonly ApplicationDbContext context;

        public BlogReadService(IMappingEngine mapper, ApplicationDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public IEnumerable<TResult> GetArticles<TResult>(GetBlogArticlesModel model)
        {
            return this.context.Articles.OrderBy(x => x.CreatedDate)
                       .Skip(model.Skip)       
                       .Take(model.Take)
                       .Project()
                       .To<TResult>()
                       .ToList();
        }
    }
}
