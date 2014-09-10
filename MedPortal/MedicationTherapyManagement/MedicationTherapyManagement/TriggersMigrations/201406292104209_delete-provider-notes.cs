namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteprovidernotes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cases", "ProviderNotes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cases", "ProviderNotes", c => c.String());
        }
    }
}
