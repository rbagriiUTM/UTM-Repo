namespace MolDavaBanking.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCardsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(),
                        Currency = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        CardType = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "User_UserId", "dbo.Users");
            DropIndex("dbo.Cards", new[] { "User_UserId" });
            DropTable("dbo.Cards");
        }
    }
}
