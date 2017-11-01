using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AppModels;

namespace DbAdapter
{
    public static class GenericEntityWrapper
    {
        public static void AddEntity<T>(T entity) where T : class
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Set<T>().Add(entity);
                dbContext.SaveChanges();
                dbContext.Entry(entity).State = EntityState.Detached;
            }
        }

        public static List<TextRequest> GetTextRequests(Guid userId)
        {
            using (var dbContext = new AppDbContext())
            {
                return dbContext.TextRequests.AsNoTracking().Where(tr => tr.UserId == userId).ToList();
            }
        }

        public static bool IsExistingUsername(string username)
        {
            using (var dbContext = new AppDbContext())
            {
                return dbContext.Users.All(user => user.UserName != username);
            }
        }

        public static User GetUserByName(string userName)
        {
            using (var dbContext = new AppDbContext())
            {
                return dbContext.Users.SingleOrDefault(user => user.UserName == userName);
            }
        }

        public static void EditEntity<T>(T entity) where T : class
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Entry(entity).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
    }
}
