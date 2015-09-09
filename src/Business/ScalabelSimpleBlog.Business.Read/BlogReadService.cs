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
                       .WhereSearch(model.Search)
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

        public IEnumerable<TResult> GetLatestComments<TResult>(int take, int? tagId)
        {
            return this.context.Comments.OrderByDescending(x => x.CreatedDate)
                .Where(x => (tagId == null || x.Article.Tags.Any(t => t.Id == tagId.Value)))
                .Skip(0)
                .Take(take)
                .ProjectTo<TResult>()
                .ToList();
        }

        public TResult GetArticleById<TResult>(int articleId)
        {
            var article = this.context.Articles.FirstOrDefault(a => a.Id == articleId);

            return this.mapper.Map<TResult>(article);
        }

        public IEnumerable<TResult> GetMostPopular<TResult>(int take, int? tagId, int? days)
        {
            return
                this.context.Articles
                    .OrderByDescending(x => x.StatiscticArticleViews.Count())
                    .WhereTag(tagId)
                    .WhereDays(days)
                    .Skip(0)
                    .Take(take)
                    .ProjectTo<TResult>()
                    .ToList();
        }

        public IEnumerable<TResult> GetArticlesByUser<TResult>(string userId)
        {
            return this.context.Articles.Where(a => a.AuthorId == userId).OrderByDescending(x => x.CreatedDate).ProjectTo<TResult>().ToList();
        }

        public IEnumerable<TResult> GetCommantsForArticle<TResult>(int articleId)
        {
            return this.context.Comments.Where(x => x.ArticleId == articleId)
                .OrderByDescending(x => x.CreatedDate)
                .ProjectTo<TResult>().ToList();
        }

        public IEnumerable<TResult> GetMostCommented<TResult>(int take, int? tag, int? days)
        {
            return this.context.Articles
                .OrderByDescending(x => x.Comments.Count())
                .WhereTag(tag)
                .WhereDays(days)
                .Skip(0)
                .Take(take)
                .Project()
                .To<TResult>()
                .ToList();
        }
    }

    public static class ArticlesQueryableExtension
    {

        public static IQueryable<Article> WhereDays(this IQueryable<Article> articleQuery, int? days)
        {
            if (days.HasValue)
            {
                var date = DateTime.UtcNow.AddDays(- days.Value);

                articleQuery = articleQuery.Where(x => x.CreatedDate > date);
            }
            return articleQuery;            
        }

        public static IQueryable<Article> WhereTag(this IQueryable<Article> articleQuery, int? tagId)
        {
            if (tagId.HasValue)
            {
                articleQuery = articleQuery.Where(x => x.Tags.Any(tag => tag.Id == tagId));
            }
            return articleQuery;
        }

        public static IQueryable<Article> WhereSearch(this IQueryable<Article> articleQuery, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                articleQuery =
                    articleQuery.Where(
                        x =>
                            x.Header.Contains(search) || 
                            x.TeaserText.Contains(search) || 
                            x.Body.Contains(search) ||
                            x.Tags.Any(t => t.Name.Contains(search)));
            }
            return articleQuery;
        }
    }
}
