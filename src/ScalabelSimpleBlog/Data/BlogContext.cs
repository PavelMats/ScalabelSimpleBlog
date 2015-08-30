using ScalabelSimpleBlog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ScalabelSimpleBlog.Data
{
    public class BlogContext : DbContext
    {
        public IDbSet<Article> Articles { get; set; }

        public IDbSet<Tag> Tags { get; set; }
    }
}