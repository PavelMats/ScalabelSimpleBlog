namespace ScalabelSimpleBlog.Business.Dto.TagControllerDto
{
    public class TagsListWithCountDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ArticlesCount { get; set; }
    }
}
