namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpatientidtocases : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases", "PatientId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cases", "PatientId");
        }
    }
}
