using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScalabelSimpleBlog.Models.BlogControllerModels
{
    public class AddArticleRequest
    {
        [Required]
        public string Header { get; set; }

        [Required]
        public string TeaserText { get; set; }

        [AllowHtml]
        public string Body { get; set; }
    }
}