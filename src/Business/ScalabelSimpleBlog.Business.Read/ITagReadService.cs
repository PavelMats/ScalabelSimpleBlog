using System.Collections.Generic;

namespace ScalabelSimpleBlog.Business.Read
{
    public interface ITagReadService
    {
        IEnumerable<TResult> GetTags<TResult>();
    }
}
