using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Business.Read
{
    public interface ITagReadService
    {
        Task<IEnumerable<TResult>> GetTagsAsync<TResult>();

        Task<IEnumerable<TResult>> GetTagsByArticleAsync<TResult>(int articleId);
    }
}
