namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_additional_Fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TriggerReportDetails", "claim_DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.TriggerReportDetails", "claim_Sex", c => c.String());
            AddColumn("dbo.TriggerReportDetails", "claim_RelationshipCode", c => c.String());
            AddColumn("dbo.TriggerReportDetails", "claim_PharmacyName", c => c.String());
            AddColumn("dbo.TriggerReportDetails", "claim_RefillCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TriggerReportDetails", "claim_RefillCode");
            DropColumn("dbo.TriggerReportDetails", "claim_PharmacyName");
            DropColumn("dbo.TriggerReportDetails", "claim_RelationshipCode");
            DropColumn("dbo.TriggerReportDetails", "claim_Sex");
            DropColumn("dbo.TriggerReportDetails", "claim_DateOfBirth");
        }
    }
}
