using System.Web.Mvc;
using ScalabelSimpleBlog.Business.Dto.TagControllerDto;
using ScalabelSimpleBlog.Business.Read;
using ScalabelSimpleBlog.Models.TagControllerModels;

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

        
        public ActionResult TagsCounts(int? tag = null)
        {
            var model = new TagControllerTagsCountsModel
            {
                TagsList = this.tagReadService.GetTags<TagsListWithCountDto>(),
                CurrentTag = tag
            };

            return PartialView(model);
        }

	}
}