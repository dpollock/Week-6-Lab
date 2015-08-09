namespace the_Mike_Ro_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class three : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Follows", "Since");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Follows", "Since", c => c.DateTime(nullable: false));
        }
    }
}
