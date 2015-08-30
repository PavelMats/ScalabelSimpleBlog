using Microsoft.AspNet.Identity.EntityFramework;
using ScalabelSimpleBlog.Data.Entities;
using ScalabelSimpleBlog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Data.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
           
        }


        public IDbSet<Tag> Tags { get; set; }

        public IDbSet<Article> Articles { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
