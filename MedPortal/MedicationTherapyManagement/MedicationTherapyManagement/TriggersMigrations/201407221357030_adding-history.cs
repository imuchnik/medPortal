namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinghistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaseHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateModified = c.DateTime(nullable: false),
                        Status = c.String(),
                        AutorId = c.String(),
                        Notes = c.String(),
                        CaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CaseHistories");
        }
    }
}
