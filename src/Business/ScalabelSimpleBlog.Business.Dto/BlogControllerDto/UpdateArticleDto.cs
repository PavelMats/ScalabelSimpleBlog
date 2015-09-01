namespace ScalabelSimpleBlog.Business.Dto.BlogControllerDto
{
    public class UpdateArticleDto
    {
        public int Id { get; set; }

        public string Header { get; set; }

        public string TeaserText { get; set; }

        public string Body { get; set; }
    }
}
