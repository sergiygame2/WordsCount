using System.Data.Entity;
using WordsCount.Models;

namespace WordsCount.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("MsSqlConnectionString")
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<User> Users { get; set; }

        public DbSet<TextRequest> TextRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new TextRequestMap());
        }
    }
}
