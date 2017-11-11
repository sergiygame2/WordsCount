using System.Data.Entity;
using AppModels;
using DbAdapter.Migrations;

namespace DbAdapter
{   
    // DbContext derives from DBContext class and exposes DbSet properties 
    // for the types that are the part of the model
    // DbSet is a collection of entity classes
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("MsSqlConnectionString")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>("MsSqlConnectionString"));
        }

        public DbSet<User> Users { get; set; }

        public DbSet<TextRequest> TextRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new User.UserMap());
            modelBuilder.Configurations.Add(new TextRequest.TextRequestMap());
        }
    }
}
