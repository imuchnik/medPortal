namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCopayField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TriggerReportDetails", "claim_Copay", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TriggerReportDetails", "claim_Copay");
        }
    }
}
