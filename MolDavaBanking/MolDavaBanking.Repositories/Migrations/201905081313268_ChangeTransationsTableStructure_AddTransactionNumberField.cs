namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTransationsTableStructure_AddTransactionNumberField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "TransactionNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "TransactionNumber");
        }
    }
}
