namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingprovidernotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases", "ProviderNotes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cases", "ProviderNotes");
        }
    }
}
