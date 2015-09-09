using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ScalabelSimpleBlog.Data.Entities;

namespace ScalabelSimpleBlog.Entities
{
    public class Article
    {
        public Article()
        {
            this.Tags = new List<Tag>();
            this.Comments = new List<Comment>();
        }
        public int Id { get; set; }

        public ApplicationUser Author { get; set; }

        public string AuthorId { get; set; }

        public string Header { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Body { get; set; }

        public string TeaserText { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? EditedDate { get; set; }

        public bool IsPublished { get; set; }

        public Guid Stamp { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<StatiscticArticleView> StatiscticArticleViews { get; set; }
        
    }
}