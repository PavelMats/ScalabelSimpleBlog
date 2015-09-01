using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;
using ScalabelSimpleBlog.Business.Read;
using ScalabelSimpleBlog.Business.Read.Models;
using ScalabelSimpleBlog.Business.Services;
using ScalabelSimpleBlog.Models.BlogControllerModels;

namespace ScalabelSimpleBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogReadService blogReadService;
        private readonly IArticleStatiscticService articleStatiscticService;

        public BlogController(IBlogReadService blogReadService, IArticleStatiscticService articleStatiscticService)
        {
            this.blogReadService = blogReadService;
            this.articleStatiscticService = articleStatiscticService;
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