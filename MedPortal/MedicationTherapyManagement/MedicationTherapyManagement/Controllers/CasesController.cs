using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using MedicationTherapyManagement.DAL;
using MedicationTherapyManagement.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MedicationTherapyManagement.Controllers
{
    public class CasesController : Controller
    {
        private readonly TriggerReportContext db = new TriggerReportContext();

        // GET: /Cases/
        public String Index()
        {
            var Db = new ApplicationDbContext();
            String userId = User.Identity.GetUserId();
            IQueryable<Case> cases;
            if (User.IsInRole("Provider"))
            {
                cases = db.Cases.Where(c => Equals(c.AssignedProvider, userId));
            }
            else if (User.IsInRole("Coordinator"))
            {
                ApplicationUser user = Db.Users.Find(userId);


                var clients = (from c in Db.Clients.Distinct()
                               where c.OrganizationId == user.OrganizationId
                               select c.ClientName).ToList();
                cases = db.Cases.Where(e => clients.Contains(e.Client));

            }
            else cases = db.Cases;
            var caseLIst = cases.ToList();

            return JsonConvert.SerializeObject(caseLIst);

        }

        // GET: /Cases/Details/5
        public String Details(int? id)
        {
            if (id == null)
            {
                return "Case not found";
            }
            Case @case = db.Cases.Find(id);
            //var @case = db.Cases.Include("TriggerReportDetail").FirstOrDefault(i => i.id==id);
            if (@case == null)
            {
                return "Case not found";
            }
          return JsonConvert.SerializeObject(@case);
        }
        // GET: /Cases/ClaimDetails/5
        public String ClaimDetails(int? id)
        {
            if (id == null)
            {
                return "Not Found";
            }
           // Case @case = db.Cases.Find(id);
            var caseClaims = db.TriggerReportDetails.Where(i => i.CaseId == id);
//            if (!caseClaims.Any())
//            {
//                return HttpNotFound();
//            }
             return JsonConvert.SerializeObject(caseClaims.ToList());
        }


        // POST: /Cases/Save
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Save()
        {
            var @case = new Case();
            if (ModelState.IsValid)
            {
                Request.InputStream.Position = 0;
                var reqMemStream = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength));

                string result;
                using (var reader = new StreamReader(reqMemStream, Encoding.UTF8))
                    result = reader.ReadToEnd();

                var json = (JObject)JsonConvert.DeserializeObject(result);


                @case.Patient = (String)json["mcaParams"]["mcaData"]["Key"];
                @case.PatientId = (String)json["mcaParams"]["mcaData"]["Value"][0]["claim"]["PatientId"];
                @case.DOB = (DateTime)json["mcaParams"]["mcaData"]["Value"][0]["claim"]["DateOfBirth"];
                @case.SexCode = (String)json["mcaParams"]["mcaData"]["Value"][0]["claim"]["SexCode"];
                @case.DateSent = DateTime.Now;
                @case.ProblemList = (String)json["mcaParams"]["problemList"];
                @case.RecomendationList = (String)json["mcaParams"]["recomendationList"];

                @case.Client = (String)json["mcaParams"]["mcaData"]["Value"][0]["claim"]["Client"];
                @case.Referrence = "";
                @case.Status = json["mcaParams"]["assignedProvider"] == null ? "UnAssigned" : "Assigned-Pending Acceptance";
                @case.AuthorId = User.Identity.GetUserId();
                @case.AuthorName = User.Identity.GetUserName();
                @case.ReportId = json["mcaParams"]["reportId"] == null ? 0 : (int)json["mcaParams"]["reportId"];

                @case.AssignedProvider = json["mcaParams"]["assignedProvider"] == null ? "" : (String)json["mcaParams"]["assignedProvider"]["Id"];

                db.Cases.Add(@case);
                db.SaveChanges();
                ReportsController reportsController = new ReportsController();
                //reportsController.setAssignedMcaFlag(@case.PatientId, @case.ReportId);
                reportsController.setAssignedMcaFlag(@case.PatientId, @case.ReportId, @case.id);
                return RedirectToAction("Index");
            }

            return Json(@case, JsonRequestBehavior.AllowGet);
        }

        //        // GET: /Cases/Edit/5
        //        public ActionResult Edit(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Case @case = db.Cases.Find(id);
        //            if (@case == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(@case);
        //        }

        // POST: /Cases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                Include =
                    "id,Patient,DOB,SexCode,DateSent,Client,ProblemList,RecomendationList,AllowablePBU,Referrence,Status,ReportId,AuthorizedNDC,Author"
                )] Case @case)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@case).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Json(@case, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update()
        {
            {
                Request.InputStream.Position = 0;
                var reqMemStream = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength));

                string result;
                using (var reader = new StreamReader(reqMemStream, Encoding.UTF8))
                    result = reader.ReadToEnd();

                var json = (JObject) JsonConvert.DeserializeObject(result);
                Case @case = db.Cases.Find(Int16.Parse((String)json["caseId"]));

                if (ModelState.IsValid)
                {   if (String.Equals(((String)json["field"]).ToLower(),"status"))
                    {
                        @case.Status = (String) json["value"];
                    }
                else if (String.Equals(((String) json["field"]).ToLower(), "assignedprovider"))
                {
                    @case.AssignedProvider = (String) json["value"];
                    @case.Status = "Assigned-Pending Acceptance";
                }
                    {
                    
                }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return Json(@case, JsonRequestBehavior.AllowGet);
            }
        }

        //GET: /Cases/Delete/5
                public ActionResult Delete(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Case @case = db.Cases.Find(id);
                    if (@case == null)
                    {
                        return HttpNotFound();
                    }
                    @case.Status = "Archived";
                    //db.Cases.Remove(@case);
                    db.SaveChanges();
                    return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
                }

        //        // POST: /Cases/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult DeleteConfirmed(int id)
        //        {
        //            Case @case = db.Cases.Find(id);
        //            db.Cases.Remove(@case);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}