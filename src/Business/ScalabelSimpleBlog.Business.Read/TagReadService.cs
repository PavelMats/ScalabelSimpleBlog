using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using ScalabelSimpleBlog.Data.Repositories;

namespace ScalabelSimpleBlog.Business.Read
{
    public class TagReadService : ITagReadService
    {
        private readonly ApplicationDbContext context;

        public TagReadService(ApplicationDbContext context)
        {
            this.context = context;
        }


        public IEnumerable<TResult> GetTags<TResult>()
        {
            return context.Tags.Project().To<TResult>().ToList();
        }

        public IEnumerable<TResult> GetTagsByArticle<TResult>(int articleId)
        {
            return
                context.Tags
                    .Where(x => x.Articles.Any(article => article.Id == articleId))
                    .ProjectTo<TResult>()
                    .ToList();
        }
    }
}