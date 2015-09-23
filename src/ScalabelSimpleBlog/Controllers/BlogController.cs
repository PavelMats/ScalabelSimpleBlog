using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;
using ScalabelSimpleBlog.Business.Models.BlogService;
using ScalabelSimpleBlog.Business.Read;
using ScalabelSimpleBlog.Business.Read.Models;
using ScalabelSimpleBlog.Business.Services;
using ScalabelSimpleBlog.Business.Services.Contracts;
using ScalabelSimpleBlog.Models.BlogControllerModels;
using UpdateArticleRequest = ScalabelSimpleBlog.Models.BlogControllerModels.UpdateArticleRequest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogReadService blogReadService;
        private readonly IArticleStatiscticService articleStatiscticService;
        private readonly IBlogCommandService blogCommandService;

        public BlogController(IBlogReadService blogReadService, IArticleStatiscticService articleStatiscticService, IBlogCommandService blogCommandService)
        {
            this.blogReadService = blogReadService;
            this.articleStatiscticService = articleStatiscticService;
            this.blogCommandService = blogCommandService;
        }

        //
        // GET: /Blog/
        public async Task<ActionResult> Index(int page = 1, int perPage = 20, int? tag = null, string search = null)
        {

            var model = new BlogControllerIndexModel();
            var skip = perPage*(page - 1);

            model.Articles = await this.blogReadService.GetArticlesAsync<ArticlesIndexDto>(new GetBlogArticlesModel(perPage, skip, tag, search));
            model.CurrentPage = page;
            model.CurrentTag = tag;
            model.CurrentSearch = search;

            return View(model);
        }

        public async Task<ActionResult> Article(int articleId)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.Identity.GetUserId();
                await this.articleStatiscticService.LogUser(articleId, userId);
            }
            else
            {
                await this.articleStatiscticService.LogAnonymus(articleId);
            }

            var model = new BlogControllerArticleViewModel();

            model.Article = await this.blogReadService.GetArticleByIdAsync<FullArticleDto>(articleId);

            model.Comments = await this.blogReadService.GetCommantsForArticleAsync<ArticleCommantDto>(articleId);
            model.Comment = new AddCommentRequest {ArticleId = articleId};
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> MyArticles()
        {
            var model = new MyArticlesBlodViewModel();

            var userId = this.User.Identity.GetUserId();

            model.Articles = await this.blogReadService.GetArticlesByUserAsync<MyArticlesDto>(userId);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddArticle()
        {
            var model = new AddArticleRequest();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddArticle(AddArticleRequest article)
        {
            if (this.ModelState.IsValid)
            {
                // Set Random tag just to simplify app 
                var random = new Random();
                var tagIds = new List<int>();
                tagIds.Add(random.Next(1, 8));
                tagIds.Add(random.Next(1, 8));

                await this.blogCommandService.CreatArticle(new AddArticleModel
                {
                    Header = article.Header, 
                    Body = article.Body,
                    TeaderText = article.TeaserText,
                    AuthorId =  this.User.Identity.GetUserId(),
                    TagsId = tagIds
                });
                return RedirectToAction("MyArticles"); 
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        [ActionName("UpdateArticle")]
        public async Task<ActionResult> GetForUpdateArtile(int articleId)
        {
            var model = new UpdateArticleViewModel();

            model.Article = await this.blogReadService.GetArticleByIdAsync<UpdateArticleDto>(articleId);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UpdateArticle(UpdateArticleRequest article)
        {
            var model = new UpdateArticleViewModel();

            if (this.ModelState.IsValid)
            {
                await this.blogCommandService.UpdateArticle(article.Id, new UpdateArticleModel
                {
                    Body = article.Body,
                    Header = article.Header,
                    TeaserText = article.TeaserText,
                    AuthorId = this.User.Identity.GetUserId()
                });
                return RedirectToAction("MyArticles");
            }

            model.Article = new UpdateArticleDto
            {
                Id = article.Id,
                Body = article.Body,
                Header = article.Header,
                TeaserText = article.TeaserText
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddComment(AddCommentRequest comment)
        {
            await this.blogCommandService.CreatComment(new AddCommentModel
            {
                ArticleId = comment.ArticleId,
                Body = comment.Body, 
                AuthorId = this.User.Identity.GetUserId()
            });

            return RedirectToAction("Article", new {articleId = comment.ArticleId});
        }

        [ChildActionOnly]
        public async Task<ActionResult> MostCommented(int take = 5, int? tag = null, int? days = 14)
        {
            var model = new BlogControllerMostCommentedModel();

            model.Articles = await this.blogReadService.GetMostCommented<MostCommentedArticleDto>(take, tag, days);

            return PartialView(model);
        }


        [ChildActionOnly]
        public async Task<ActionResult> MostPopular(int take = 10, int? tag = null, int? days = 14)
        {
            var model = new BlogControllerMostPopularViewModel();

            model.Articles = await this.blogReadService.GetMostPopular<MostPopularArticleDto>(take, tag, days);

            return PartialView(model);
        }

        [ChildActionOnly]
        public async  Task<ActionResult> LatestArticles(int take = 10, int? tag = null)
        {
            var model = new BlogControllerLetestArticlesModel();

            model.Articles = await this.blogReadService.GetLatest<LatestArticlesDto>(take, tag);

            return PartialView(model);
        }

        [ChildActionOnly]
        public async Task<ActionResult> LatestsComments(int take = 10, int? tag = null)
        {
            var model = new BlogControllerLatestsCommentsViewModel();

            model.Comments = await this.blogReadService.GetLatestComments<LatestCommentDto>(take, tag);

            return PartialView(model);
        }
    }
}