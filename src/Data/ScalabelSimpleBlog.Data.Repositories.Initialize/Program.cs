using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Data.Repositories.Initialize
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new ApplicationContextInitializer());

            using (var context = new ApplicationDbContext())
            {
                context.Database.Initialize(force: true);
            }
        }
    }
}
