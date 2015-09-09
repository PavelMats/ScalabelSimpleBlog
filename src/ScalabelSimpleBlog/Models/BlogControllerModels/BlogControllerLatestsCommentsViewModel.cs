using System.Collections.Generic;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;

namespace ScalabelSimpleBlog.Models.BlogControllerModels
{
    public class BlogControllerLatestsCommentsViewModel
    {
        public IEnumerable<LatestCommentDto> Comments { get; set; }
    }
}