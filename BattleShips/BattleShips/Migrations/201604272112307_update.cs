namespace BattleShips.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boards", "Width", c => c.Int(nullable: false));
            AddColumn("dbo.Boards", "Height", c => c.Int(nullable: false));
            AddColumn("dbo.Ships", "Alive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ships", "Alive");
            DropColumn("dbo.Boards", "Height");
            DropColumn("dbo.Boards", "Width");
        }
    }
}
