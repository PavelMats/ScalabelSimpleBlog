using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ScalabelSimpleBlog.Business.Read.Models;
using ScalabelSimpleBlog.Data.Repositories;
using ScalabelSimpleBlog.Entities;

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
            return this.context.Articles
                       .WhereTag(model.Tag)
                       .OrderByDescending(x => x.CreatedDate)
                       .Skip(model.Skip)       
                       .Take(model.Take)
                       .Project()
                       .To<TResult>()
                       .ToList();
        }

        public IEnumerable<TResult> GetLatest<TResult>(int take, int? tag)
        {
            return this.context.Articles
                               .WhereTag(tag)
                               .OrderByDescending(x => x.CreatedDate)
                               .Skip(0)
                               .Take(take)
                               .Project().To<TResult>()
                               .ToList();
        }

        public TResult GetArticleById<TResult>(int articleId)
        {
            var article = this.context.Articles.FirstOrDefault(a => a.Id == articleId);

            return this.mapper.Map<TResult>(article);
        }

        public IEnumerable<TResult> GetMostPopular<TResult>(int take, int? tagId)
        {
            return
                this.context.Articles
                    .OrderByDescending(x => x.StatiscticArticleViews.Count())
                    .WhereTag(tagId)
                    .Skip(0)
                    .Take(take)
                    .ProjectTo<TResult>()
                    .ToList();
        }
    }

    public static class ArticlesЙueryableExtension
    {
        public static IQueryable<Article> WhereTag(this IQueryable<Article> articleQuery, int? tagId)
        {
            if (tagId.HasValue)
            {
                articleQuery = articleQuery.Where(x => x.Tags.Any(tag => tag.Id == tagId));
            }
            return articleQuery;
        }
    }
}
