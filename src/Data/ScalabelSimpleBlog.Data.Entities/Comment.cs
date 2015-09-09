using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScalabelSimpleBlog.Entities;

namespace ScalabelSimpleBlog.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public ApplicationUser Author { get; set; }

        public string AuthorId { get; set; }

        public Article Article { get; set; }

        public int ArticleId { get; set; }

        public string Body { get; set; }

        public Guid Stamp { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
