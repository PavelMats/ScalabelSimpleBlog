using System;
using ScalabelSimpleBlog.Data.Entities;
using ScalabelSimpleBlog.Data.Repositories;

namespace ScalabelSimpleBlog.Business.Services
{
    public class ArticleStatiscticService : IArticleStatiscticService
    {
        private readonly ApplicationDbContext context;

        public ArticleStatiscticService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void LogAnonymus(int articleId)
        {
            var log = new StatiscticArticleView();
            log.ArticleId = articleId;
            log.Time = DateTime.Now;
            context.StatiscticArticleViews.Add(log);
            context.SaveChanges();
        }

        public void LogUser(int articleId, string userId)
        {
            var log = new StatiscticArticleView();
            log.ArticleId = articleId;
            log.UserId = userId;
            log.Time = DateTime.Now;
            context.StatiscticArticleViews.Add(log);
            context.SaveChanges();
        }
    }
}