namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedBookModelbyMistake : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Authors", "Book_ID", "dbo.Books");
            DropIndex("dbo.Authors", new[] { "Book_ID" });
            DropColumn("dbo.Authors", "Book_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Authors", "Book_ID", c => c.Int());
            CreateIndex("dbo.Authors", "Book_ID");
            AddForeignKey("dbo.Authors", "Book_ID", "dbo.Books", "ID");
        }
    }
}
