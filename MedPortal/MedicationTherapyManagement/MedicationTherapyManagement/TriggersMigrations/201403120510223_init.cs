namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TriggerReportDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        claim_Patient = c.String(),
                        claim_Pharmacy = c.String(),
                        claim_Prescriber = c.String(),
                        claim_PrescriberLastName = c.String(),
                        claim_GPI = c.String(),
                        claim_PharmacyCount = c.Int(),
                        claim_PrescriberCount = c.Int(),
                        claim_PaidAmt = c.Decimal(precision: 18, scale: 2),
                        claim_DateFilled = c.DateTime(nullable: false),
                        claim_GPIDescription = c.String(),
                        claim_MetricQuantity = c.Decimal(precision: 18, scale: 2),
                        claim_DaysSupply = c.String(),
                        claim_TotalDollars = c.Decimal(precision: 18, scale: 2),
                        claim_RxCount = c.Int(),
                        claim_Client = c.String(),
                        ReportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TriggerReports", t => t.ReportId, cascadeDelete: true)
                .Index(t => t.ReportId);
            
            CreateTable(
                "dbo.TriggerReports",
                c => new
                    {
                        ReportId = c.Int(nullable: false, identity: true),
                        claimsCount = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Author = c.String(),
                        ClientName = c.String(),
                        GeneratedParameters = c.String(),
                    })
                .PrimaryKey(t => t.ReportId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TriggerReportDetails", "ReportId", "dbo.TriggerReports");
            DropIndex("dbo.TriggerReportDetails", new[] { "ReportId" });
            DropTable("dbo.TriggerReports");
            DropTable("dbo.TriggerReportDetails");
        }
    }
}
