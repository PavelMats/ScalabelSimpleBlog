using System.Collections.Generic;
using ScalabelSimpleBlog.Business.Read.Models;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Business.Read
{
    public interface IBlogReadService
    {
        Task<IEnumerable<TResult>> GetArticlesAsync<TResult>(GetBlogArticlesModel model);

        Task<IEnumerable<TResult>> GetLatest<TResult>(int take, int? tagId);

        Task<IEnumerable<TResult>> GetLatestComments<TResult>(int take, int? tagId);

        Task<TResult> GetArticleByIdAsync<TResult>(int articleId);

        Task<IEnumerable<TResult>> GetMostPopular<TResult>(int take, int? tagId, int? days);

        Task<IEnumerable<TResult>> GetArticlesByUserAsync<TResult>(string userId);

        Task<IEnumerable<TResult>> GetCommantsForArticleAsync<TResult>(int articleId);

        Task<IEnumerable<TResult>> GetMostCommented<TResult>(int take, int? tag, int? days);
    }
}
