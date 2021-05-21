namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDatabaseStructure_AddSuportMessagesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SupportMessages",
                c => new
                    {
                        SupportMessageId = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SupportMessageId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupportMessages", "UserId", "dbo.Users");
            DropIndex("dbo.SupportMessages", new[] { "UserId" });
            DropTable("dbo.SupportMessages");
        }
    }
}
