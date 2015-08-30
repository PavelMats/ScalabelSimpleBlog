using ScalabelSimpleBlog.Business.Dto.HomeCotrollerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScalabelSimpleBlog.Models
{
    public class HomeIndexModel
    {
        public HomeIndexModel()
        {
            this.LatestArticles = new List<LatestArticlesDto>();
        }

        public IEnumerable<LatestArticlesDto> LatestArticles { get; set; }
    }
}