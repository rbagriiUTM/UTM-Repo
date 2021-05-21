namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationUserRole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "Id", "dbo.Users");
            RenameColumn(table: "dbo.Roles", name: "Id", newName: "RoleId");
            RenameIndex(table: "dbo.Roles", name: "IX_Id", newName: "IX_RoleId");
            DropPrimaryKey("dbo.Users");
            DropColumn("dbo.Users", "Id");
            DropColumn("dbo.Users", "RoleId");
            DropColumn("dbo.Roles", "Type");
            AddColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Users", "IDNP", c => c.String());
            AddColumn("dbo.Users", "Address", c => c.String());
            AddColumn("dbo.Roles", "Name", c => c.String());
            AddPrimaryKey("dbo.Users", "UserId");
            AddForeignKey("dbo.Roles", "RoleId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "Type", c => c.String());
            AddColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Roles", "RoleId", "dbo.Users");
            DropPrimaryKey("dbo.Users");
            DropColumn("dbo.Roles", "Name");
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "IDNP");
            DropColumn("dbo.Users", "UserId");
            AddPrimaryKey("dbo.Users", "Id");
            RenameIndex(table: "dbo.Roles", name: "IX_RoleId", newName: "IX_Id");
            RenameColumn(table: "dbo.Roles", name: "RoleId", newName: "Id");
            AddForeignKey("dbo.Roles", "Id", "dbo.Users", "Id");
        }
    }
}
