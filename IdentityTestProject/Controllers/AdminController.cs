
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using IdentityTestProject.Infrastructure;
using System.Web;

namespace IdentityTestProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: List of users
        public ActionResult Index()
        {

            return View(UserManager.Users);
        }




        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}