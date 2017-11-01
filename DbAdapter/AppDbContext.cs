using System.Data.Entity;
using AppModels;
using DbAdapter.Migrations;

namespace DbAdapter
{
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
