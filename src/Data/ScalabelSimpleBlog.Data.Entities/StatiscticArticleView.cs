using System;
using ScalabelSimpleBlog.Entities;

namespace ScalabelSimpleBlog.Data.Entities
{
    public class StatiscticArticleView
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public Article Article { get; set; }

        public int? ArticleId { get; set; }

        public string UserId { get; set; }
    }
}
