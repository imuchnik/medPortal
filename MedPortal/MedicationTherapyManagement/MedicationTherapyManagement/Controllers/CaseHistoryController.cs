using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicationTherapyManagement.Models;
using MedicationTherapyManagement.DAL;

namespace MedicationTherapyManagement.Controllers
{
    public class CaseHistoryController : Controller
    {
        private TriggerReportContext db = new TriggerReportContext();

        // GET: /CaseHistoryController/
        public ActionResult Index()
        {
            return View(db.CaseHistory.ToList());
        }

        // GET: /CaseHistoryController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseHistory casehistory = db.CaseHistory.Find(id);
            if (casehistory == null)
            {
                return HttpNotFound();
            }
            return View(casehistory);
        }

        // GET: /CaseHistoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CaseHistoryController/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,DateModified,Status,AutorId,Notes,CaseId")] CaseHistory casehistory)
        {
            if (ModelState.IsValid)
            {
                db.CaseHistory.Add(casehistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(casehistory);
        }

//        // GET: /CaseHistoryController/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            CaseHistory casehistory = db.CaseHistory.Find(id);
//            if (casehistory == null)
//            {
//                return HttpNotFound();
//            }
//            return View(casehistory);
//        }

        // POST: /CaseHistoryController/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,DateModified,Status,AutorId,Notes,CaseId")] CaseHistory casehistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(casehistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(casehistory);
        }

        // GET: /CaseHistoryController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseHistory casehistory = db.CaseHistory.Find(id);
            if (casehistory == null)
            {
                return HttpNotFound();
            }
            return View(casehistory);
        }

        // POST: /CaseHistoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CaseHistory casehistory = db.CaseHistory.Find(id);
            db.CaseHistory.Remove(casehistory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
