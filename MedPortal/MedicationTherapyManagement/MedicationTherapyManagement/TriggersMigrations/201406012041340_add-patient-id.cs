namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpatientid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TriggerReportDetails", "mcaCreated", c => c.Boolean(nullable: false));
            AddColumn("dbo.TriggerReportDetails", "claim_PatientId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TriggerReportDetails", "claim_PatientId");
            DropColumn("dbo.TriggerReportDetails", "mcaCreated");
        }
    }
}
