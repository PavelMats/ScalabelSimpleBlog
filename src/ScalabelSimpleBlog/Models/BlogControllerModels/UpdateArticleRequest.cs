using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ScalabelSimpleBlog.Models.BlogControllerModels
{
    public class UpdateArticleRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string TeaserText { get; set; }

        [AllowHtml] 
        public string Body { get; set; }
    }
}
