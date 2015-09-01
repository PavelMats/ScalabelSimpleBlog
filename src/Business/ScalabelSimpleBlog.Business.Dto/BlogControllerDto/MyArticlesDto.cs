using System;

namespace ScalabelSimpleBlog.Business.Dto.BlogControllerDto
{
    public class MyArticlesDto
    {
        public string Header { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ClicksCount { get; set; }

        public int Id { get; set; }
    }
}
