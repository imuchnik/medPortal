namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnotes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CreateTime = c.DateTime(nullable: false),
                        caseId = c.Int(nullable: false),
                        NoteText = c.String(),
                        Author = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notes");
        }
    }
}
