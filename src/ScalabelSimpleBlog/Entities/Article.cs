using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScalabelSimpleBlog.Entities
{
    public class Article
    { 
        public int Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Body { get; set; }

        public string TeaserText { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime EditedDate { get; set; }

        public bool IsPublished { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}