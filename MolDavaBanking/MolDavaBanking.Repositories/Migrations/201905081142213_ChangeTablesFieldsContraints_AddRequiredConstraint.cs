namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTablesFieldsContraints_AddRequiredConstraint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Banks", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Banks", "Adress", c => c.String(nullable: false));
            AlterColumn("dbo.Cards", "CardNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roles", "Name", c => c.String());
            AlterColumn("dbo.Cards", "CardNumber", c => c.String());
            AlterColumn("dbo.Banks", "Adress", c => c.String());
            AlterColumn("dbo.Banks", "Name", c => c.String());
        }
    }
}
