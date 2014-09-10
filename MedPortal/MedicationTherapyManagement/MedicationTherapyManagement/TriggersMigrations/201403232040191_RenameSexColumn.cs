namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameSexColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TriggerReportDetails", "claim_SexCode", c => c.String());
            DropColumn("dbo.TriggerReportDetails", "claim_Sex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TriggerReportDetails", "claim_Sex", c => c.String());
            DropColumn("dbo.TriggerReportDetails", "claim_SexCode");
        }
    }
}
