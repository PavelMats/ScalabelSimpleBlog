using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;

namespace ScalabelSimpleBlog.Models.BlogControllerModels
{
    public class BlogControllerArticleViewModel
    {
        public FullArticleDto Article { get; set; }

        public IEnumerable<ArticleCommantDto> Comments { get; set; }

        public AddCommentRequest Comment { get; set; }
    }
}