using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.EnterpriseServices.Internal;
using System.Windows.Forms;

namespace MedicationTherapyManagement.Models
{
    public class TriggerReportDetail
    {   
    [Key]
        public int Id { get; set; }

        public bool Reviewed { get; set; }
        public bool mcaCreated { get; set; }
        public TriggerReportResult claim { get; set; }
       
        public int ReportId { get; set; }
        public int? CaseId { get; set; }
    
        [ForeignKey("ReportId")]
        public virtual TriggerReport TriggerReport { get; set; }
        [ForeignKey("CaseId")]
        public virtual Case Case { get; set; }
   
    }
}