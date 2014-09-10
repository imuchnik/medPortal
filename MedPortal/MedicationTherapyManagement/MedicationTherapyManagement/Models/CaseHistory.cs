using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using Microsoft.SqlServer.Server;

namespace MedicationTherapyManagement.Models
{
    public class CaseHistory{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public DateTime DateModified { get; set; }
        public String Status { get; set; }
        public String AutorId { get; set; }
        public String Notes { get; set; }
        public int CaseId { get; set; }
    }
}


