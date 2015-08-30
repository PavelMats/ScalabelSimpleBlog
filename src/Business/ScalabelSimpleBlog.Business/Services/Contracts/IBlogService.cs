using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Business.Services.Contracts
{
    public interface IBlogService
    {
        IEnumerable<T> GetLatest<T>(int take);
    }
}
