using System.Collections.Generic;
using ScalabelSimpleBlog.Business.Read.Models;

namespace ScalabelSimpleBlog.Business.Read
{
    public interface IBlogReadService
    {
        IEnumerable<TResult> GetArticles<TResult>(GetBlogArticlesModel model);

        IEnumerable<TResult> GetLatest<TResult>(int take, int? tagId);

        IEnumerable<TResult> GetLatestComments<TResult>(int take, int? tagId);

        TResult GetArticleById<TResult>(int articleId);

        IEnumerable<TResult> GetMostPopular<TResult>(int take, int? tagId, int? days);

        IEnumerable<TResult> GetArticlesByUser<TResult>(string userId);

        IEnumerable<TResult> GetCommantsForArticle<TResult>(int articleId);

        IEnumerable<TResult> GetMostCommented<TResult>(int take, int? tag, int? days);
    }
}
