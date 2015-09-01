using System.Data.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ScalabelSimpleBlog.Business.Models.BlogService;
using ScalabelSimpleBlog.Business.Services.Contracts;
using ScalabelSimpleBlog.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScalabelSimpleBlog.Entities;

namespace ScalabelSimpleBlog.Business.Services
{
    public class BlogCommandService : IBlogCommandService
    {
        private ApplicationDbContext context { get; set; }
        private IMappingEngine mapper { get; set; }

        public BlogCommandService(ApplicationDbContext context, IMappingEngine mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void UpdateArticle(int articleId, UpdateArticleModel articleUpdateModel)
        {
            var article = context.Articles.FirstOrDefault(x => x.Id == articleId);
            article.Body = articleUpdateModel.Body;
            article.Header = articleUpdateModel.Header;
            article.TeaserText = articleUpdateModel.TeaserText;
            article.AuthorId = articleUpdateModel.AuthorId;
            article.EditedDate = DateTime.Now;

            var entry = context.Entry(article);
            entry.State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<T> GetLatest<T>(int take)
        {
            return this.context.Articles
                .Where(a => a.IsPublished)
                .OrderBy(x => x.CreatedDate)
                .Take(take)
                .Project().To<T>()
                .ToList();
        }
    }
}
