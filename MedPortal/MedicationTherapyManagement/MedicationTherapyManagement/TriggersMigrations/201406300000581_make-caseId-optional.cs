namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makecaseIdoptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "caseId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "caseId", c => c.Int(nullable: false));
        }
    }
}
