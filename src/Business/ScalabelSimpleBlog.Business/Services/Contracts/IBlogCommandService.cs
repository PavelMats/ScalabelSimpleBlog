using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScalabelSimpleBlog.Business.Models.BlogService;

namespace ScalabelSimpleBlog.Business.Services.Contracts
{
    public interface IBlogCommandService
    {
        Task UpdateArticle(int articleId, UpdateArticleModel articleUpdateModel);

        IEnumerable<T> GetLatest<T>(int take);

        Task CreatArticle(AddArticleModel addArticleModel);

        Task CreatComment(AddCommentModel addCommentModel);
    }
}
