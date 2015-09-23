using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Business.Services
{
    public interface IArticleStatiscticService
    {
        Task LogAnonymus(int articleId);

        Task LogUser(int articleId, string userId);
    }
}
