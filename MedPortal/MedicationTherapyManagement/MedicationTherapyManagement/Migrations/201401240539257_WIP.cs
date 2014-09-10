namespace MedicationTherapyManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WIP : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "ClientName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "ClientName", c => c.String());
        }
    }
}
