using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using MedicationTherapyManagement.DAL;
using MedicationTherapyManagement.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MedicationTherapyManagement.Controllers
{
    public class ProviderController : Controller
    {
//        public ActionResult Index()
//        {
//            return Json(db.Cases.ToList(), JsonRequestBehavior.AllowGet);
//        }

        [Authorize(Roles = "Coordinator, Admin")]
        public ActionResult Index()
        {
            var db = new IdentityDbContext();
            var organizations = db.Database.SqlQuery<ApplicationUser>("select * from users s join UserRoles ur on s.id=ur.UserId join Roles r on r.Id= ur.RoleId and r.Name='Provider'", new object[1]);
            return Json(organizations.ToList(), JsonRequestBehavior.AllowGet);


        }
    }
}