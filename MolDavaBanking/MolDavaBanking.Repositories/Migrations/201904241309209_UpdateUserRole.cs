namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserRole : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserRoles", newName: "UserRole");
            RenameColumn(table: "dbo.UserRole", name: "User_UserId", newName: "UserRefId");
            RenameColumn(table: "dbo.UserRole", name: "Role_RoleId", newName: "RoleRefId");
            RenameIndex(table: "dbo.UserRole", name: "IX_User_UserId", newName: "IX_UserRefId");
            RenameIndex(table: "dbo.UserRole", name: "IX_Role_RoleId", newName: "IX_RoleRefId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserRole", name: "IX_RoleRefId", newName: "IX_Role_RoleId");
            RenameIndex(table: "dbo.UserRole", name: "IX_UserRefId", newName: "IX_User_UserId");
            RenameColumn(table: "dbo.UserRole", name: "RoleRefId", newName: "Role_RoleId");
            RenameColumn(table: "dbo.UserRole", name: "UserRefId", newName: "User_UserId");
            RenameTable(name: "dbo.UserRole", newName: "UserRoles");
        }
    }
}
