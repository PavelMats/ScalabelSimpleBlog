namespace ScalabelSimpleBlog.Business.Read.Models
{
    public class GetBlogArticlesModel
    {
        public GetBlogArticlesModel(int take, int skip)
        {
            this.Take = take;
            this.Skip = skip;
        }

        public int Take { get; set; }

        public int Skip { get; set; }
    }
}
