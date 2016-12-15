namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedannotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authors", "FirstName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Authors", "FirstName", c => c.String());
        }
    }
}
