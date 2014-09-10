using System;

namespace MedicationTherapyManagement.Models
{
    public class TriggerReportResult
    {
        public string Patient { get; set; }
        public string PatientId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string SexCode { get; set; }
        public string RelationshipCode { get; set; }
        public string Pharmacy { get; set; }
        public string PharmacyName { get; set; }
        public string Prescriber { get; set; }
        public string PrescriberLastName { get; set; }
        public string GPI { get; set; }
        public int? PharmacyCount { get; set; }
        public int? PrescriberCount { get; set; }
        public decimal? PaidAmt { get; set; }
        public DateTime DateFilled { get; set; }
        public string GPIDescription { get; set; }
        public decimal? MetricQuantity { get; set; }
        public int? DaysSupply { get; set; }
        public string RefillCode { get; set; }
        public decimal? TotalDollars { get; set; }
        public decimal? Copay { get; set; }
        public int? RxCount { get; set; }
        public string Client { get; set; }
    }
}