using System;
using System.Data.Entity;
using System.Linq;
using WordsCount.Helpers;
using WordsCount.Models;

namespace WordsCount.Data
{
    public class DbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            // if users list is empty add some default users
            if (context.Users.Any()) return;

            context.Users.Add(new User
            {
                UserName = "lost",
                FirstName = "Sergiy",
                LastName = "Sukharskyi",
                Email = "sergiy@gmail.com",
                HashPassword = DataHelper.Hash("somepass"),
                LastVisit = DateTime.Now
            });

            context.Users.Add(new User
            {
                UserName = "lbodia",
                FirstName = "Bogdan",
                LastName = "Liba",
                Email = "bodia@gmail.com",
                HashPassword = DataHelper.Hash("somepass2"),
                LastVisit = DateTime.Now
            });

            context.SaveChanges();
        }
    }
}