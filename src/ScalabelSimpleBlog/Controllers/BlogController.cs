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
        public ActionResult Index(int page = 1, int perPage = 20, int? tag = null)
        {

            var model = new BlogControllerIndexModel();
            var skip = perPage*(page - 1);

            model.Articles = this.blogReadService.GetArticles<ArticlesIndexDto>(new GetBlogArticlesModel(perPage, skip, tag));
            model.CurrentPage = page;
            model.CurrentTag = tag;

            return View(model);
        }

        public ActionResult Article(int articleId)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.Identity.GetUserId();
                this.articleStatiscticService.LogUser(articleId, userId);
            }
            else
            {
                this.articleStatiscticService.LogAnonymus(articleId);
            }

            var model = new BlogControllerArticleViewModel();

            model.Article = this.blogReadService.GetArticleById<FullArticleDto>(articleId);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyArticles()
        {
            var model = new MyArticlesBlodViewModel();

            var userId = this.User.Identity.GetUserId();

            model.Articles = this.blogReadService.GetArticlesByUser<MyArticlesDto>(userId);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddArticle()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [ActionName("UpdateArticle")]
        public ActionResult GetForUpdateArtile(int articleId)
        {
            var model = new UpdateArticleViewModel();

            model.Article = this.blogReadService.GetArticleById<UpdateArticleDto>(articleId);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateArticle(UpdateArticleRequest article)
        {
            if (this.ModelState.IsValid)
            {
                this.blogCommandService.UpdateArticle(article.Id, new UpdateArticleModel
                {
                    Body = article.Body,
                    Header = article.Header,
                    TeaserText = article.TeaserText,
                    AuthorId = this.User.Identity.GetUserId()
                });
                return RedirectToAction("MyArticles");
            }
            return View();
        }

        [ChildActionOnly]
        public ActionResult MostPopular(int take = 10, int? tag = null)
        {
            var model = new BlogControllerMostPopularViewModel();

            model.Articles = this.blogReadService.GetMostPopular<MostPopularArticleDto>(take, tag);

            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult LatestArticles(int take = 10, int? tag = null)
        {
            var model = new BlogControllerLetestArticlesModel();

            model.Articles = this.blogReadService.GetLatest<LatestArticlesDto>(take, tag);

            return PartialView(model);
        }
    }
}