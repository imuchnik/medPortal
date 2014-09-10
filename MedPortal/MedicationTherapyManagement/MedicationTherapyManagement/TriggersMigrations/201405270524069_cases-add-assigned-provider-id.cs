namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class casesaddassignedproviderid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cases", "AssignedProvider", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cases", "AssignedProvider", c => c.Int());
        }
    }
}
