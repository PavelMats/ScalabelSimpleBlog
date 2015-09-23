using System.Web.Mvc;
using ScalabelSimpleBlog.Business.Dto.TagControllerDto;
using ScalabelSimpleBlog.Business.Read;
using ScalabelSimpleBlog.Models.TagControllerModels;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagReadService tagReadService;

        public TagController(ITagReadService tagReadService)
        {
            this.tagReadService = tagReadService;
        }

        //
        // GET: /Tag/
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> TagsByArticle(int articleId)
        {
            var model = new TagsByArticleViewModel();

            model.Tags = await this.tagReadService.GetTagsByArticleAsync<TagByArticleId>(articleId);

            return PartialView(model);
        }

        public async Task<ActionResult> TagsCounts(int? tag = null)
        {
            var model = new TagControllerTagsCountsModel
            {
                TagsList = await this.tagReadService.GetTagsAsync<TagsListWithCountDto>(),
                CurrentTag = tag
            };

            return PartialView(model);
        }

	}
}