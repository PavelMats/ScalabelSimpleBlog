using System.Collections.Generic;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;

namespace ScalabelSimpleBlog.Models.BlogControllerModels
{
    public class BlogControllerLetestArticlesModel
    {
        public IEnumerable<LatestArticlesDto> Articles { get; set; }
    }
}