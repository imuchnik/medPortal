using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using MedicationTherapyManagement.DAL;
using MedicationTherapyManagement.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MedicationTherapyManagement.Controllers
{
    public class NotesController : Controller
    {
        private readonly TriggerReportContext db = new TriggerReportContext();

        // GET: /Notes/
        public String Index(int Id)
        {
            var notes = db.Notes.Where(i => i.caseId == Id);
           return JsonConvert.SerializeObject(notes.ToList());
        }

        // GET: /Notes/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Note note = db.Notes.Find(id);
//            if (note == null)
//            {
//                return HttpNotFound();
//            }
//            return View(note);
//        }

        // GET: /Notes/Create
        public ActionResult Create()
        {
            return Json("");
        }

        // POST: /Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Save()
        {
            var @note = new Note();
            if (ModelState.IsValid)
            {
                Request.InputStream.Position = 0;
                var reqMemStream = new MemoryStream(HttpContext.Request.BinaryRead(HttpContext.Request.ContentLength));

                string result;
                using (var reader = new StreamReader(reqMemStream, Encoding.UTF8))
                    result = reader.ReadToEnd();

                var json = (JObject) JsonConvert.DeserializeObject(result);


                @note.caseId = (int) json["caseId"];
                @note.CreateTime = DateTime.Now;
                @note.NoteText = (String) json["noteText"];
                @note.Author = User.Identity.GetUserName();


                db.Notes.Add(@note);
                db.SaveChanges();
            }

            return Json(@note, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
      
        public ActionResult Create([Bind(Include = "id,CreateTime,caseId,NoteText")] Note note)
        {
            if (ModelState.IsValid)
            {
                db.Notes.Add(note);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Json("status:200");
        }


        // GET: /Notes/Edit/5
        public
            ActionResult Edit(int? id)
        {
            if (
                id == null)
            {
                return
                    new
                        HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);

            if (note == null)
            {
                return
                    HttpNotFound();
            }
            return View(note);
        }

        // POST: /Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CreateTime,caseId,NoteText")] Note note)
        {
            if (ModelState.IsValid)
            {
                db.Entry(note).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Json(note);
        }

        // GET: /Notes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return Json(note);
        }

        // POST: /Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = db.Notes.Find(id);
            db.Notes.Remove(note);
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

