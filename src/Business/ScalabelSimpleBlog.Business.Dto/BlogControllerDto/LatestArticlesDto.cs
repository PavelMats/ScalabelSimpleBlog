using System;

namespace ScalabelSimpleBlog.Business.Dto.BlogControllerDto
{
    public class LatestArticlesDto
    {
        public int Id { get; set; }

        public string Header { get; set; }

        public DateTime DateFrom { get; set; }
    }
}
