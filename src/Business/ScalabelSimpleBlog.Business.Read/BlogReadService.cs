using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ScalabelSimpleBlog.Business.Read.Models;
using ScalabelSimpleBlog.Data.Repositories;
using ScalabelSimpleBlog.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<TResult>> GetArticlesAsync<TResult>(GetBlogArticlesModel model)
        {
            var query = this.context.Articles
                       .WhereTag(model.Tag)
                       .WhereSearch(model.Search)
                       .OrderByDescending(x => x.CreatedDate)
                       .Skip(model.Skip)
                       .Take(model.Take)
                       .Project()
                       .To<TResult>();

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetLatest<TResult>(int take, int? tag)
        {
            return await this.context.Articles
                               .WhereTag(tag)
                               .OrderByDescending(x => x.CreatedDate)
                               .Skip(0)
                               .Take(take)
                               .Project().To<TResult>()
                               .ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetLatestComments<TResult>(int take, int? tagId)
        {
            return await this.context.Comments.OrderByDescending(x => x.CreatedDate)
                .Where(x => (tagId == null || x.Article.Tags.Any(t => t.Id == tagId.Value)))
                .Skip(0)
                .Take(take)
                .ProjectTo<TResult>()
                .ToListAsync();
        }

        public async Task<TResult> GetArticleByIdAsync<TResult>(int articleId)
        {
            var article = await this.context.Articles.FirstOrDefaultAsync(a => a.Id == articleId);

            return this.mapper.Map<TResult>(article);
        }

        public async Task<IEnumerable<TResult>> GetMostPopular<TResult>(int take, int? tagId, int? days)
        {
            return await 
                this.context.Articles
                    .OrderByDescending(x => x.StatiscticArticleViews.Count())
                    .WhereTag(tagId)
                    .WhereDays(days)
                    .Skip(0)
                    .Take(take)
                    .ProjectTo<TResult>()
                    .ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetArticlesByUserAsync<TResult>(string userId)
        {
            return await this.context.Articles.Where(a => a.AuthorId == userId).OrderByDescending(x => x.CreatedDate).ProjectTo<TResult>().ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetCommantsForArticleAsync<TResult>(int articleId)
        {
            return await this.context.Comments.Where(x => x.ArticleId == articleId)
                .OrderByDescending(x => x.CreatedDate)
                .ProjectTo<TResult>().ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetMostCommented<TResult>(int take, int? tag, int? days)
        {
            return await this.context.Articles
                .OrderByDescending(x => x.Comments.Count())
                .WhereTag(tag)
                .WhereDays(days)
                .Skip(0)
                .Take(take)
                .Project()
                .To<TResult>()
                .ToListAsync();
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
