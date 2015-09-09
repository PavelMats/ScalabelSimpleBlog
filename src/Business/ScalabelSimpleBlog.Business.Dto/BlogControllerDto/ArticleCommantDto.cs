using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Business.Dto.BlogControllerDto
{
    public class ArticleCommantDto
    {
        public string AuthorName { get; set; }

        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
