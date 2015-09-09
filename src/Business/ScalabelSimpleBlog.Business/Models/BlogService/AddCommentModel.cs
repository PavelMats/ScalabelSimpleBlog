using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Business.Models.BlogService
{
    public class AddCommentModel
    {
        public string AuthorId { get; set; }

        public int ArticleId { get; set; }

        public string Body { get; set; }
    }
}
