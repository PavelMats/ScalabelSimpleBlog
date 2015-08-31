using System.Collections.Generic;
using ScalabelSimpleBlog.Business.Dto.BlogControllerDto;

namespace ScalabelSimpleBlog.Models.BlogControllerModels
{
    public class BlogControllerIndexModel
    {
        public BlogControllerIndexModel()
        {
            this.Articles = new List<ArticlesIndexDto>();
        }
         
        public IEnumerable<ArticlesIndexDto> Articles { get; set; }

        public int CurrentPage { get; set; }
    }
}