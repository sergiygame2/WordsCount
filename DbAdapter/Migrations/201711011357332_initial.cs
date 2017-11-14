namespace DbAdapter.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.text_requests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Path = c.String(nullable: false),
                        SymbolsAmount = c.Int(nullable: false),
                        WordsAmount = c.Int(nullable: false),
                        LinesAmount = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        HashPassword = c.String(nullable: false),
                        LastVisit = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.text_requests", "UserId", "dbo.users");
            DropIndex("dbo.text_requests", new[] { "UserId" });
            DropTable("dbo.users");
            DropTable("dbo.text_requests");
        }
    }
}
