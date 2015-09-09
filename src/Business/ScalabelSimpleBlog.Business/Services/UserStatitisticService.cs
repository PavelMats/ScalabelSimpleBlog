using System;
using ScalabelSimpleBlog.Data.Entities;
using ScalabelSimpleBlog.Data.Repositories;

namespace ScalabelSimpleBlog.Business.Services
{
    public class UserStatitisticService : IUserStatitisticService
    {
        private readonly ApplicationDbContext context;

        public UserStatitisticService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Log(string userId)
        {
            var log = new StatiscticUserLogin();
            log.CreatedDate = DateTime.UtcNow;
            log.UserId = userId;
            this.context.StatiscticUserLogins.Add(log);
            this.context.SaveChanges();
        }
    }

    public interface IUserStatitisticService
    {
        void Log(string userId);
    }
}
