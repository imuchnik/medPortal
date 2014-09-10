using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicationTherapyManagement.Models
{
    public class TriggerReport

    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public  int ReportId { get; set; } //saved trigger report Id
        public int claimsCount { get; set; }
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
        public string ClientName { get; set; }
        public string GeneratedParameters { get; set; }
       
        public List<TriggerReportDetail> Details { get; set; }

    }
}