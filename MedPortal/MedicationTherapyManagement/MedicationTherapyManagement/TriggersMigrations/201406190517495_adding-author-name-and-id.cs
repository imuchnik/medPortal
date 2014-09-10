namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingauthornameandid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases", "AuthorId", c => c.String());
            AddColumn("dbo.Cases", "AuthorName", c => c.String());
            DropColumn("dbo.Cases", "Author");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cases", "Author", c => c.String());
            DropColumn("dbo.Cases", "AuthorName");
            DropColumn("dbo.Cases", "AuthorId");
        }
    }
}
