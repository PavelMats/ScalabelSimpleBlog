using System.Web.Mvc;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;
using ScalabelSimpleBlog.Business.Read;
using ScalabelSimpleBlog.Business.Read.Models;
using ScalabelSimpleBlog.Models.BlogControllerModels;

namespace ScalabelSimpleBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogReadService blogReadService;

        public BlogController(IBlogReadService blogReadService)
        {
            this.blogReadService = blogReadService;
        }

        //
        // GET: /Blog/
        public ActionResult Index(int page = 1, int perPage = 20, int? tag = null)
        {
            var model = new BlogControllerIndexModel();
            var skip = perPage*(page - 1);

            model.Articles = this.blogReadService.GetArticles<ArticlesIndexDto>(new GetBlogArticlesModel(perPage, skip));
            model.CurrentPage = page;

            return View(model);
        }

        public ActionResult Article(int articleId)
        {
            return View();
        }
	}
}