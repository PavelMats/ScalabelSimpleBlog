using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;

namespace ScalabelSimpleBlog.Models.BlogControllerModels
{
    public class BlogControllerMostPopularViewModel
    {
        public IEnumerable<MostPopularArticleDto> Articles { get; set; }
    }
}