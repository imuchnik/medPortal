using System.Linq;
using System.Web.Mvc;
using MedicationTherapyManagement.Models;
using Microsoft.AspNet.Identity;

namespace MedicationTherapyManagement.Controllers
{
    public class SessionUserController : Controller
    {
        //
        // GET: /SessionUser/
        public ActionResult SessionUserInfo()
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
          

            return Json("userId:" + userId + ", userName:" + userName, JsonRequestBehavior.AllowGet);
        }      
        public ActionResult SessionUserRoles()
        {
            var db =new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var properties = db.Users.Where(i=>i.Id== userId);
            var roles = (from applicationUser in properties from role in applicationUser.Roles select role.Role.Name).ToList();

            return Json(roles.ToList(), JsonRequestBehavior.AllowGet);
        }



        
    }
}
