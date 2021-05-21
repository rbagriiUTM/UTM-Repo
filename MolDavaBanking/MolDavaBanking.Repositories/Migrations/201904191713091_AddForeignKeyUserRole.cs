namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyUserRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "RoleId");
        }
    }
}
