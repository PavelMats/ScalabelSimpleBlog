using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Business.Services
{
    public interface IArticleStatiscticService
    {
        void LogAnonymus(int articleId);

        void LogUser(int articleId, string userId);
    }
}
