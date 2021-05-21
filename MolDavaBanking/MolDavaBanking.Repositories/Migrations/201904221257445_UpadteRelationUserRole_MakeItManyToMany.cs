namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpadteRelationUserRole_MakeItManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "RoleId", "dbo.Users");
            DropIndex("dbo.Roles", new[] { "RoleId" });
            DropPrimaryKey("dbo.Roles");
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Role_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Role_RoleId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Role_RoleId);
            
            AlterColumn("dbo.Roles", "RoleId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Roles", "RoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_UserId", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "Role_RoleId" });
            DropIndex("dbo.UserRoles", new[] { "User_UserId" });
            DropPrimaryKey("dbo.Roles");
            AlterColumn("dbo.Roles", "RoleId", c => c.Int(nullable: false));
            DropTable("dbo.UserRoles");
            AddPrimaryKey("dbo.Roles", "RoleId");
            CreateIndex("dbo.Roles", "RoleId");
            AddForeignKey("dbo.Roles", "RoleId", "dbo.Users", "UserId");
        }
    }
}
