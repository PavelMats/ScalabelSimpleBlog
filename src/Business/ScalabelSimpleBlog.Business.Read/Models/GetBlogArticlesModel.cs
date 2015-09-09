namespace ScalabelSimpleBlog.Business.Read.Models
{
    public class GetBlogArticlesModel
    {
        public GetBlogArticlesModel(int take, int skip, int? tag, string search)
        {
            this.Take = take;
            this.Skip = skip;
            this.Tag = tag;
            this.Search = search;
        }

        public string Search { get; set; }

        public int? Tag { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }
    }
}
