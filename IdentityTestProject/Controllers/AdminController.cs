
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using IdentityTestProject.Infrastructure;
using System.Web;
using System.Threading.Tasks;
using IdentityTestProject.Models;
using Microsoft.AspNet.Identity;
using System;

namespace IdentityTestProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: List of users
        public ActionResult Index()
        {

            return View(UserManager.Users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateModel model)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }

            }
            return View(model);

        }

        private void AddErrorsFromResult(IdentityResult result)
        {
           foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
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