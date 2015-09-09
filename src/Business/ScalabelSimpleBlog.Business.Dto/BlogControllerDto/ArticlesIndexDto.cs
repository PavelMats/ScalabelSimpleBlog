namespace ScalabelSimpleBlog.Business.Dto.BlogControllerDto
{
    public class ArticlesIndexDto
    {
        public int Id { get; set; }

        public string Header { get; set; }

        public string TeaserText { get; set; }

        public string Author { get; set; }
        public int ClicksCount { get; set; }
        public int CommentsCount { get; set; }
    }
}
