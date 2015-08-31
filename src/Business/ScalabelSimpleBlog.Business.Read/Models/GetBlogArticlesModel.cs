namespace ScalabelSimpleBlog.Business.Read.Models
{
    public class GetBlogArticlesModel
    {
        public GetBlogArticlesModel(int take, int skip, int? tag)
        {
            this.Take = take;
            this.Skip = skip;
            this.Tag = tag;
        }

        public int? Tag { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }
    }
}
