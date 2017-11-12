using AppModels;

namespace DbAdapter.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            // if users list is empty add some default users
            if (context.Users.Any()) return;
            
            context.Users.Add(new User("lost", "Sergiy", "Sukharskyi", "sergiy@gmail.com", "somepass"));
            context.Users.Add(new User("lbodia", "Bogdan", "Liba", "bodia@gmail.com", "somepass2"));

            context.SaveChanges();
        }
    }
}
