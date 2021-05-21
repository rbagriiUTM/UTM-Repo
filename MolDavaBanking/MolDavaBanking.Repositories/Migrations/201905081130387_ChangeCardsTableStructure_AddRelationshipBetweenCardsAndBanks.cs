namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCardsTableStructure_AddRelationshipBetweenCardsAndBanks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "BankId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cards", "BankId");
            AddForeignKey("dbo.Cards", "BankId", "dbo.Banks", "BankId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "BankId", "dbo.Banks");
            DropIndex("dbo.Cards", new[] { "BankId" });
            DropColumn("dbo.Cards", "BankId");
        }
    }
}
