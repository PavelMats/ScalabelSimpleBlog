using AutoMapper;
using AutoMapper.QueryableExtensions;
using ScalabelSimpleBlog.Business.Services.Contracts;
using ScalabelSimpleBlog.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Business.Services
{
    public class BlogService : IBlogService
    {
        public ApplicationDbContext Context { get; private set; }
        public IMappingEngine Mapper { get; private set; }

        public BlogService(ApplicationDbContext context, IMappingEngine mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }

        public IEnumerable<T> GetLatest<T>(int take)
        {
            return this.Context.Articles
                .Where(a => a.IsPublished)
                .OrderBy(x => x.CreatedDate)
                .Take(take)
                .Project().To<T>()
                .ToList();
        }
    }
}
