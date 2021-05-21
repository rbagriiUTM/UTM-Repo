namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTransactionTableStructure_AddEnumsAsDataTypeForSomeProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Transactions", "Currency", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "Currency", c => c.String());
            AlterColumn("dbo.Transactions", "Status", c => c.String());
        }
    }
}
