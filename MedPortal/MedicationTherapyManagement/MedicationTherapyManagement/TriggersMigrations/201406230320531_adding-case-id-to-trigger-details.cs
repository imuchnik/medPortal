namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingcaseidtotriggerdetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TriggerReportDetails", "CaseId", c => c.Int());
            CreateIndex("dbo.TriggerReportDetails", "CaseId");
            AddForeignKey("dbo.TriggerReportDetails", "CaseId", "dbo.Cases", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TriggerReportDetails", "CaseId", "dbo.Cases");
            DropIndex("dbo.TriggerReportDetails", new[] { "CaseId" });
            DropColumn("dbo.TriggerReportDetails", "CaseId");
        }
    }
}
