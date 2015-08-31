using System.Collections.Generic;
using ScalabelSimpleBlog.Business.Read.Models;

namespace ScalabelSimpleBlog.Business.Read
{
    public interface IBlogReadService
    {
        IEnumerable<TResult> GetArticles<TResult>(GetBlogArticlesModel model);

        IEnumerable<TResult> GetLatest<TResult>(int take, int? tagId);
    }
}
