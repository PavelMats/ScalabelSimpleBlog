using System.Collections.Generic;
using ScalabelSimpleBlog.Business.Dto.TagControllerDto;

namespace ScalabelSimpleBlog.Models.TagControllerModels
{
    public class TagControllerTagsCountsModel
    {
        public TagControllerTagsCountsModel()
        {
            this.TagsList = new List<TagsListWithCountDto>();
        }

        public IEnumerable<TagsListWithCountDto> TagsList { get; set; }

        public int? CurrentTag { get; set; }
    }
}