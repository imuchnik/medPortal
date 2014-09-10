using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MedicationTherapyManagement.DAL;
using MedicationTherapyManagement.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MedicationTherapyManagement.Controllers
{
    public class ReportsController : Controller
    {
        private readonly MTM_UCSHIPContext _context = new MTM_UCSHIPContext();
        private readonly TriggerReportContext triggerContext = new TriggerReportContext();

        public Dictionary<string, string> triggerTypes = new Dictionary<string, string>();

        public string runTriggerReport()
            //TODO: this will need to be refactored once we have settled the db layer.  
        {
            IDictionary<string, List<TriggerReportResult>> patientGroups =
                new Dictionary<string, List<TriggerReportResult>>();

            String reportName;
            string selectedReport = Request.Params["selectedTemplate"];

            string client = Request.Params["client"];
            string dollarAmt = String.IsNullOrEmpty(Request.Params["dollarAmount"])
                ? "0"
                : Request.Params["dollarAmount"];
            string pharmNum = String.IsNullOrEmpty(Request.Params["numOfPharmacies"])
                ? "0"
                : Request.Params["numOfPharmacies"];
            string prescriberNum = String.IsNullOrEmpty(Request.Params["numOfUniquePrescribers"])
                ? "0"
                : Request.Params["numOfUniquePrescribers"];
            string numDrugs = String.IsNullOrEmpty(Request.Params["numOfUniqueDrugs"])
                ? "0"
                : Request.Params["numOfUniqueDrugs"];

            string query = "exec " + "GenericTrigger" + " " + Request.Params["fromDate"] + "," +
                           Request.Params["toDate"] + "," + numDrugs +
                           "," + dollarAmt + "," + pharmNum + "," + prescriberNum + ",'" + client+"'";


            DbRawSqlQuery<TriggerReportResult> results = _context.Database.SqlQuery<TriggerReportResult>(query,
                new object[1]);
            //grouping                                
            foreach (TriggerReportResult result in results)
            {
                if (patientGroups.ContainsKey(result.Patient))
                {
                    patientGroups[result.Patient].Add(result);
                }
                else
                {
                    var patients = new List<TriggerReportResult> {result};
                    patientGroups.Add(result.Patient, patients);
                }
            }

            return JsonConvert.SerializeObject(patientGroups.ToList());
        }

        public ActionResult saveTriggerResults()
        {
            Request.InputStream.Position = 0;
            var reqMemStream = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength));

            string result;
            using (var reader = new StreamReader(reqMemStream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }

            var json = (JObject) JsonConvert.DeserializeObject(result);
            var report = new TriggerReport
            {
                Author = User.Identity.Name,
                DateCreated = DateTime.Now,
                claimsCount = json["data"].Count(),
                GeneratedParameters = json["triggerParams"].ToString(),
                ClientName = json["triggerParams"]["client"].ToString(),
                Details = new List<TriggerReportDetail>()
            };

            foreach (JToken claimEntry  in json["data"])
            {
                JToken obj = claimEntry["Value"];
                foreach (JToken claimObj in obj)
                {
                    var entry = new TriggerReportDetail
                    {
                        claim = new TriggerReportResult
                        {
                            Client = report.ClientName,
                            DateOfBirth = (DateTime?) claimObj["DateOfBirth"],
                            SexCode = claimObj["SexCode"].ToString(),
                            RelationshipCode = claimObj["RelationshipCode"].ToString(),
                            DateFilled = (DateTime) claimObj["DateFilled"],
                            DaysSupply = (int?)claimObj["DaysSupply"],
                            RefillCode = claimObj["RefillCode"].ToString(),
                            GPI = claimObj["GPI"].ToString(),
                            GPIDescription = (string) claimObj["GPIDescription"],
                            MetricQuantity = (decimal?) claimObj["MetricQuantity"],
                            PaidAmt = (decimal?) claimObj["PaidAmt"],
                            Patient = (string) claimObj["Patient"],
                            PatientId = (string) claimObj["PatientId"],
                            Pharmacy = (string) claimObj["Pharmacy"],
                            PharmacyName = (string) claimObj["PharmacyName"],
                            PharmacyCount = (int?) claimObj["PharmacyCount"],
                            Prescriber = (string) claimObj["Prescriber"],
                            PrescriberCount = (int?) claimObj["PrescriberCount"],
                            PrescriberLastName = claimObj["PrescriberLastName"].ToString(),
                            RxCount = (int?) claimObj["RxCount"],
                            TotalDollars = (decimal?) claimObj["TotalDollars"],
                            Copay = (decimal?) claimObj["Copay"]
                        }
                    };
                    report.Details.Add(entry);
                }
            }

            triggerContext.TriggerReports.Add(report);

            triggerContext.SaveChanges();
            return Json("status:200");
        }

        public String List()
        {

     
            var query =
                from trigger in triggerContext.TriggerReports
                join mca in triggerContext.Cases
                    on trigger.ReportId equals mca.ReportId
                    into triggersCases
                select new
                {
                    trigger,
                    count = triggersCases.Count()
                };

          
            return JsonConvert.SerializeObject(query.ToList());
        }

        public String ReportDetail(int Id)
        {
            IQueryable<TriggerReportDetail> results = triggerContext.TriggerReportDetails.Where(e => e.ReportId == Id);
            IDictionary<string, List<TriggerReportDetail>> patientGroups =
                new Dictionary<string, List<TriggerReportDetail>>();
            foreach (TriggerReportDetail triggerReportDetail in results)
            {
                if (patientGroups.ContainsKey(triggerReportDetail.claim.Patient))
                {
                    patientGroups[triggerReportDetail.claim.Patient].Add(triggerReportDetail);
                }
                else
                {
                    var patients = new List<TriggerReportDetail> {triggerReportDetail};

                    patientGroups.Add(triggerReportDetail.claim.Patient, patients);
                }
            }
            return JsonConvert.SerializeObject(patientGroups.ToList(), Formatting.None,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        public ActionResult markReviewed(string patientId, int reportId)
        {
            var reviewedRows =
                triggerContext.TriggerReportDetails.Where(e => (e.ReportId == reportId && e.claim.PatientId == patientId));
            foreach (TriggerReportDetail row in reviewedRows)
            {
                row.Reviewed = true;
            }
            triggerContext.SaveChanges(); 
            return Json("status:200");
        }
       
        public ActionResult setAssignedMcaFlag(string patientId, int reportId, int caseId)
        {
            var reviewedRows =
                triggerContext.TriggerReportDetails.Where(e => (e.ReportId == reportId && e.claim.PatientId == patientId));
            foreach (TriggerReportDetail row in reviewedRows)
            {
                row.CaseId = caseId;
                row.Reviewed = true;
                row.mcaCreated = true;
            }
            triggerContext.SaveChanges();
            return Json("status:200");
        }
        
        public ActionResult deleteTriggerReport(int id)
        {
            var report=(from s in triggerContext.TriggerReports
                            where s.ReportId == id
                            select s).FirstOrDefault();
            if (report != null)
            {
                triggerContext.TriggerReports.Remove(report);
           
            triggerContext.SaveChanges();
            return Json("status:200", JsonRequestBehavior.AllowGet);
            }
            return Json("status:404", JsonRequestBehavior.AllowGet);
        }
    }
}