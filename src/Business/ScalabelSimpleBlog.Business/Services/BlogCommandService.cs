using System.Data.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ScalabelSimpleBlog.Business.Models.BlogService;
using ScalabelSimpleBlog.Business.Services.Contracts;
using ScalabelSimpleBlog.Data.Entities;
using ScalabelSimpleBlog.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CreatArticle(AddArticleModel addArticleModel)
        {
            var articleEntity = new Article();
            articleEntity.AuthorId = addArticleModel.AuthorId;
            articleEntity.Body = addArticleModel.Body;
            articleEntity.TeaserText = addArticleModel.TeaderText;
            articleEntity.Header = addArticleModel.Header;
            articleEntity.CreatedDate = DateTime.UtcNow;

            var tags = context.Tags.Where(x => addArticleModel.TagsId.Contains(x.Id));
            foreach(var tag in tags)
            {
                articleEntity.Tags.Add(tag);
                tag.Articles.Add(articleEntity);
            }

            context.Articles.Add(articleEntity);
            context.SaveChanges();
        }

        public void CreatComment(AddCommentModel addCommentModel)
        {
            var commentEntity = new Comment();
            commentEntity.ArticleId = addCommentModel.ArticleId;
            commentEntity.AuthorId = addCommentModel.AuthorId;
            commentEntity.Body = addCommentModel.Body;
            commentEntity.CreatedDate = DateTime.UtcNow;
            context.Comments.Add(commentEntity);
            context.SaveChanges();
        }
    }
}
