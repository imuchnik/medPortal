using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MedicationTherapyManagement.Models;

namespace MedicationTherapyManagement.DAL
{
    public class TriggerReportContext : DbContext
    {
        public TriggerReportContext()
            : base("MTM_UCSHIP")
        {
            Configuration.LazyLoadingEnabled = false;
        }


        public DbSet<TriggerReport> TriggerReports { get; set; }
        public DbSet<TriggerReportDetail> TriggerReportDetails { get; set; }
        public DbSet<CaseHistory> CaseHistory{ get; set; }

        public DbSet<Case> Cases { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<TriggerReport>()
                .ToTable("TriggerReports");
            modelBuilder.Entity<TriggerReportDetail>()
                .ToTable("TriggerReportDetails");    
            modelBuilder.Entity<Case>()
                .ToTable("Cases");
            modelBuilder.Entity<Note>()
                .ToTable("Notes");
        }
    }
}