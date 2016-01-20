namespace BeiDreamAbp.Infrastructure.Ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("BeiDreamAbp.Users");
            AlterColumn("BeiDreamAbp.Users", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("BeiDreamAbp.Users", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("BeiDreamAbp.Users");
            AlterColumn("BeiDreamAbp.Users", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("BeiDreamAbp.Users", "Id");
        }
    }
}
