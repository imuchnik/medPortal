namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class casesfinetune : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases", "SexCode", c => c.String());
            AlterColumn("dbo.Cases", "AllowablePBU", c => c.Int());
            AlterColumn("dbo.Cases", "AuthorizedNDC", c => c.Int());
            DropColumn("dbo.Cases", "Sex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cases", "Sex", c => c.Int(nullable: false));
            AlterColumn("dbo.Cases", "AuthorizedNDC", c => c.Int(nullable: false));
            AlterColumn("dbo.Cases", "AllowablePBU", c => c.Int(nullable: false));
            DropColumn("dbo.Cases", "SexCode");
        }
    }
}
