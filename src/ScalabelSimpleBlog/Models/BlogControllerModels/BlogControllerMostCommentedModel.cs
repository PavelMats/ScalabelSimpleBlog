using System.Collections.Generic;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;

namespace ScalabelSimpleBlog.Models.BlogControllerModels
{
    public class BlogControllerMostCommentedModel
    {
        public IEnumerable<MostCommentedArticleDto> Articles { get; set; }
    }
}