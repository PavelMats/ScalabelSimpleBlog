using System.Collections.Generic;
using ScalabelSimpleBlog.Business.Dto.TagControllerDto;

namespace ScalabelSimpleBlog.Models.TagControllerModels
{
    public class TagsByArticleViewModel
    {

        public IEnumerable<TagByArticleId> Tags { get; set; }
    }
}