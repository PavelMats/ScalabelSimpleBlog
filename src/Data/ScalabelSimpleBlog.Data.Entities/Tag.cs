using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScalabelSimpleBlog.Entities
{
    public class Tag
    {
        public Tag()
        {
            this.Articles = new List<Article>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}