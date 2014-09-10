namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class casesaddassignedprovider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases", "AssignedProvider", c => c.Int());
            AlterColumn("dbo.Cases", "Patient", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cases", "Patient", c => c.String());
            DropColumn("dbo.Cases", "AssignedProvider");
        }
    }
}
