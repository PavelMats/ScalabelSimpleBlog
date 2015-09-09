using System;

namespace ScalabelSimpleBlog.Business.Dto.BlogControllerDto
{
    public class LatestCommentDto
    {
        public string AuthorName { get; set; }

        public string Body { get; set; }

        public string ArticleHeader { get; set; }

        public int ArticleId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
