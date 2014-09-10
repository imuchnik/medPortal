using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace MedicationTherapyManagement.Models
{
    public class Case
    {
        [Key]
        public int id { get; set; }

        public String Patient { get; set; }
        public String PatientId { get; set; }
        public DateTime DOB { get; set; }
        public String SexCode { get; set; }
        public DateTime DateSent { get; set; }
        public String Client { get; set; }
        public String ProblemList { get; set; }
        public String RecomendationList { get; set; }
        public int? AllowablePBU { get; set; }
        public String Referrence { get; set; }
        public String Status { get; set; }
        public int ReportId { get; set; }
        public int? AuthorizedNDC { get; set; }
        public String AuthorId { get; set; }
        public String AuthorName { get; set; }
       
        public String AssignedProvider { get; set; }

    }
}