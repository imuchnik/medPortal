namespace MedicationTherapyManagement.TriggersMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MedicationTherapyManagement.DAL.TriggerReportContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"TriggersMigrations";
        }

        protected override void Seed(MedicationTherapyManagement.DAL.TriggerReportContext context)
        {
          
        }
    }
}
