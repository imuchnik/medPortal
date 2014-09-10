namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addindReviewedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TriggerReportDetails", "Reviewed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TriggerReportDetails", "Reviewed");
        }
    }
}
