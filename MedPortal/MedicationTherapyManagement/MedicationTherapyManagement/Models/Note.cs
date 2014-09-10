using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicationTherapyManagement.Models
{
    public class Note
    {
        [Key]
        public int id { get; set; }
        public DateTime CreateTime { get; set; }
        public int? caseId { get; set; }
        public String NoteText { get; set; }
        public String Author { get; set; }
    }
}