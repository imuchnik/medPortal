namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incorporatingNewClaimsTableChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TriggerReportDetails", "claim_DateOfBirth", c => c.DateTime());
            AlterColumn("dbo.TriggerReportDetails", "claim_DaysSupply", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TriggerReportDetails", "claim_DaysSupply", c => c.String());
            AlterColumn("dbo.TriggerReportDetails", "claim_DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
