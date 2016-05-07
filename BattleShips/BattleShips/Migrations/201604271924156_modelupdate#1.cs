namespace BattleShips.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelupdate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ships", "Pos_X", c => c.Int(nullable: false));
            AddColumn("dbo.Ships", "Pos_Y", c => c.Int(nullable: false));
            AddColumn("dbo.Shots", "Pos_X", c => c.Int(nullable: false));
            AddColumn("dbo.Shots", "Pos_Y", c => c.Int(nullable: false));
            DropColumn("dbo.Ships", "X");
            DropColumn("dbo.Ships", "Y");
            DropColumn("dbo.Shots", "X");
            DropColumn("dbo.Shots", "Y");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shots", "Y", c => c.Int(nullable: false));
            AddColumn("dbo.Shots", "X", c => c.Int(nullable: false));
            AddColumn("dbo.Ships", "Y", c => c.Int(nullable: false));
            AddColumn("dbo.Ships", "X", c => c.Int(nullable: false));
            DropColumn("dbo.Shots", "Pos_Y");
            DropColumn("dbo.Shots", "Pos_X");
            DropColumn("dbo.Ships", "Pos_Y");
            DropColumn("dbo.Ships", "Pos_X");
        }
    }
}
