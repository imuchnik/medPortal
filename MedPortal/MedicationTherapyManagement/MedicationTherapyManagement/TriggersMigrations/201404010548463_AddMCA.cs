namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMCA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        patient = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Sex = c.Int(nullable: false),
                        DateSent = c.DateTime(nullable: false),
                        Client = c.String(),
                        ProblemList = c.String(),
                        RecomendationList = c.String(),
                        AllowablePBU = c.Int(nullable: false),
                        Referrence = c.String(),
                        Status = c.String(),
                        ReportId = c.Int(nullable: false),
                        AuthorizedNDC = c.Int(nullable: false),
                        Author = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cases");
        }
    }
}
