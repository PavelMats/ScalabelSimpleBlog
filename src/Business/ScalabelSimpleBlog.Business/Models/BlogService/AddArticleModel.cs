using System.Collections.Generic;

namespace ScalabelSimpleBlog.Business.Models.BlogService
{
    public class AddArticleModel
    {
        public AddArticleModel()
        {
            this.TagsId = new List<int>();
        }
        public string Header { get; set; }
        public string Body { get; set; }
        public string TeaderText { get; set; }
        public string AuthorId { get; set; }

        public List<int> TagsId { get; set; }
    }
}
