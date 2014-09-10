using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MedicationTherapyManagement.Models;


namespace MedicationTherapyManagement.Controllers
{
    public class OrganizationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Organozation/
        public ActionResult Index()
        {
            return View(db.Organizations.ToList());
        }

        // GET: /Organozation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // GET: /Organozation/Create
        public ActionResult Create()
        {
            return View();
        }

       
       


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="OrganizationId,OrganizationName")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Organizations.Add(organization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organization);
        }

        // GET: /Organozation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
          
            return View(organization);
        }

        // POST: /Organozation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="OrganizationId,OrganizationName")] Organization organization, FormCollection form, int[] selectedClients)
        {
            var orgToUpdate = db.Organizations
                .Include(i => i.Clients)
                .Where(i => i.OrganizationId == organization.OrganizationId)
                .Single();
            if (ModelState.IsValid)
            {
                UpdateSelectedClients(selectedClients, orgToUpdate);
                db.Entry(orgToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(orgToUpdate);
        }

        private void UpdateSelectedClients(int[] selectedClients, Organization organization)
        {
            if (selectedClients == null)
            {
                organization.Clients =new List<Client>();
                return;
            }
            var selectedClientsHS = new HashSet<int>(selectedClients);
            var orgClients = new HashSet<int>(organization.Clients.Select(c => c.ClientId));

            foreach (var client in db.Clients)
            {
                if (selectedClientsHS.Contains(client.ClientId))
                {
                    if (!orgClients.Contains(client.ClientId))
                    {
                        organization.Clients.Add(client);
                    }
                }
                else
                {
                    if (orgClients.Contains(client.ClientId))
                    {
                        organization.Clients.Remove(client);
                    }
                }

            }

        }

        // GET: /Organozation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: /Organozation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organization organization = db.Organizations.Find(id);
            db.Organizations.Remove(organization);
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
