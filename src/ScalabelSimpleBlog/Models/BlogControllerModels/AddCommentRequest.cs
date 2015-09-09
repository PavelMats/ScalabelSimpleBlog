namespace ScalabelSimpleBlog.Models.BlogControllerModels
{
    public class AddCommentRequest
    {
        public int ArticleId { get; set; }

        public string Body { get; set; }
    }
}