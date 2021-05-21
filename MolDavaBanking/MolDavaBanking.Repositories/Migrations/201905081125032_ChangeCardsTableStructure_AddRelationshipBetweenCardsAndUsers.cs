namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCardsTableStructure_AddRelationshipBetweenCardsAndUsers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "User_UserId", "dbo.Users");
            DropIndex("dbo.Cards", new[] { "User_UserId" });
            RenameColumn(table: "dbo.Cards", name: "User_UserId", newName: "UserId");
            AlterColumn("dbo.Cards", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cards", "UserId");
            AddForeignKey("dbo.Cards", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "UserId", "dbo.Users");
            DropIndex("dbo.Cards", new[] { "UserId" });
            AlterColumn("dbo.Cards", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Cards", name: "UserId", newName: "User_UserId");
            CreateIndex("dbo.Cards", "User_UserId");
            AddForeignKey("dbo.Cards", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
