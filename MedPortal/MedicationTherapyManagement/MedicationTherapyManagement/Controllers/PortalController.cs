using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicationTherapyManagement.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace MedicationTherapyManagement.Controllers
{
    public class PortalController : Controller
    {
        //
        // GET: /Portal/
        [Authorize(Roles = "Admin, CanEdit, User, Coordinator, Provider")]
        public ActionResult Index()
        {
            return View();
        }
           [Authorize(Roles = "Admin, CanEdit, User, Coordinator, Provider")]
        public ActionResult Clients()
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.Find(User.Identity.GetUserId());
           // var clients = Db.Clients.Where(u => u.OrganizationId == user.OrganizationId);
            var clients = User.IsInRole("Admin") ? Db.Clients : Db.Clients.Where(u => u.OrganizationId == user.OrganizationId);


            return Json(clients, JsonRequestBehavior.AllowGet);
        }
    }
}