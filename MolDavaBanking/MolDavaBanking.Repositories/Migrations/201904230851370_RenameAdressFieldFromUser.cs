namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAdressFieldFromUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Adress", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Address", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Adress");
        }
    }
}
