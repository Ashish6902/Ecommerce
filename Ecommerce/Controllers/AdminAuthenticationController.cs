using Ecommerce.Models.EntityAuthentication.DBoperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ecommerce.Controllers
{
    public class AdminAuthenticationController : Controller
    {
        LoginRepository repository = null;
        // constructor
        public AdminAuthenticationController()
        {
            repository = new LoginRepository();
        }
        //for register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Logins model)
        {
            if (ModelState.IsValid)
            {
                int i = repository.AddAdmin(model);
                if (i > 0)
                {
                    TempData["Created"] = "New user is created";
                    return RedirectToAction("Login", "SellerAuthetication");
                }
            }
            return View();
        }

        // for login 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Logins model)
        {
            using (var context = new EcommerceEntities())
            {
                bool isValid = context.Logins.Any(x => x.UserName == model.UserName && x.HashedPassword == model.HashedPassword);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("index", "Home");
                }
            }
            return View();

        }
        //for logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    
    }
}