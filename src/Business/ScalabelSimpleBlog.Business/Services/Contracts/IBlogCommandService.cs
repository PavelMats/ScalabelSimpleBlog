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
        void UpdateArticle(int articleId, UpdateArticleModel articleUpdateModel);

        IEnumerable<T> GetLatest<T>(int take);

        void CreatArticle(AddArticleModel addArticleModel);

        void CreatComment(AddCommentModel addCommentModel);
    }
}
