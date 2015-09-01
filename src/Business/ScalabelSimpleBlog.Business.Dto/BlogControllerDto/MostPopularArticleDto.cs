using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Business.Dto.BlogControllerDto
{
    public class MostPopularArticleDto
    {
        public int ClicksCount { get; set; }
        public int Id { get; set; }
        public string Header { get; set; }
    }
}
