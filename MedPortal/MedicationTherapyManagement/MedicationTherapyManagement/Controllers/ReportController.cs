using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using MedicationTherapyManagement.DAL;
using MedicationTherapyManagement.Models;

namespace MedicationTherapyManagement.Controllers
{
    public class ReportController : Controller
    {
        private readonly MTM_UCSHIPContext _context = new MTM_UCSHIPContext();

        public Dictionary<string, string> triggerTypes = new Dictionary<string, string>();

    
      

        public ActionResult runTriggerReport(string reportName)
        {
              triggerTypes.Add("HighDollar", "HighDollarResult");
            List<object> testResultList = new List<object>();
            IDictionary<string, List<HighDollarResult>> patientGroups= new Dictionary<string, List<HighDollarResult>>();


           
           var results = _context.Database.SqlQuery<HighDollarResult>("exec HighDollar'8/1/2009','12/1/2013',30", new object[1]);
            //grouping
            foreach (HighDollarResult highDollarResult in results)
            {
                if (patientGroups.ContainsKey(highDollarResult.Patient))
                {
                    patientGroups[highDollarResult.Patient].Add(highDollarResult);
                }
                else
                {
                    var patients =new List<HighDollarResult> {highDollarResult};
                    patientGroups.Add(highDollarResult.Patient, patients);
                }
            }
           
            return Json(patientGroups.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}