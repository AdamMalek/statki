namespace BattleShips.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ships",
                c => new
                    {
                        ShipId = c.Int(nullable: false, identity: true),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        Board_Id = c.Int(),
                        Player_PlayerId = c.Int(),
                    })
                .PrimaryKey(t => t.ShipId)
                .ForeignKey("dbo.Boards", t => t.Board_Id)
                .ForeignKey("dbo.Players", t => t.Player_PlayerId)
                .Index(t => t.Board_Id)
                .Index(t => t.Player_PlayerId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.Shots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        Hit = c.Boolean(nullable: false),
                        Game_Id = c.Int(),
                        Player_PlayerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .ForeignKey("dbo.Players", t => t.Player_PlayerId)
                .Index(t => t.Game_Id)
                .Index(t => t.Player_PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shots", "Player_PlayerId", "dbo.Players");
            DropForeignKey("dbo.Shots", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Players", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Games", "Id", "dbo.Boards");
            DropForeignKey("dbo.Ships", "Player_PlayerId", "dbo.Players");
            DropForeignKey("dbo.Ships", "Board_Id", "dbo.Boards");
            DropIndex("dbo.Shots", new[] { "Player_PlayerId" });
            DropIndex("dbo.Shots", new[] { "Game_Id" });
            DropIndex("dbo.Players", new[] { "Game_Id" });
            DropIndex("dbo.Ships", new[] { "Player_PlayerId" });
            DropIndex("dbo.Ships", new[] { "Board_Id" });
            DropIndex("dbo.Games", new[] { "Id" });
            DropTable("dbo.Shots");
            DropTable("dbo.Players");
            DropTable("dbo.Ships");
            DropTable("dbo.Boards");
            DropTable("dbo.Games");
        }
    }
}
