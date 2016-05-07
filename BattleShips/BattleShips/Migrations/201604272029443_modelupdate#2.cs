namespace BattleShips.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelupdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Password");
        }
    }
}
