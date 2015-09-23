using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using ScalabelSimpleBlog.Data.Repositories;
using System.Threading.Tasks;
using System.Data.Entity;
using System;

namespace ScalabelSimpleBlog.Business.Read
{
    public class TagReadService : ITagReadService
    {
        private readonly ApplicationDbContext context;

        public TagReadService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TResult>> GetTagsAsync<TResult>()
        {
            return await context.Tags.OrderByDescending(x => x.Articles.Count()).Project().To<TResult>().ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetTagsByArticleAsync<TResult>(int articleId)
        {
            return
                await context.Tags
                    .Where(x => x.Articles.Any(article => article.Id == articleId))
                    .ProjectTo<TResult>()
                    .ToListAsync();
        }
    }
}