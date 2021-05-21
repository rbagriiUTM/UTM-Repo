namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCardsTableStructure : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "User_UserId", "dbo.Users");
            DropIndex("dbo.Cards", new[] { "User_UserId" });
            AlterColumn("dbo.Cards", "User_UserId", c => c.Int());
            CreateIndex("dbo.Cards", "User_UserId");
            AddForeignKey("dbo.Cards", "User_UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "User_UserId", "dbo.Users");
            DropIndex("dbo.Cards", new[] { "User_UserId" });
            AlterColumn("dbo.Cards", "User_UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cards", "User_UserId");
            AddForeignKey("dbo.Cards", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
