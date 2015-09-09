using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Business.Dto.BlogControllerDto
{
    public class LatestCommentDto
    {
        public string AuthorName { get; set; }

        public string Body { get; set; }

        public string ArticleHeader { get; set; }
    }
}
