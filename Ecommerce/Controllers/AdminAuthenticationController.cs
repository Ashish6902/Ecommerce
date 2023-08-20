using Ecommerce.Models.EntityAuthentication.DBoperations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                using (var context = new EcommerceEntities())
                {
                    bool usernameExists = context.Logins.Any(x => x.UserName == model.UserName);
                    bool PhoneExists = context.Logins.Any(x => x.PhoneNo == model.PhoneNo);
                    bool EmailExists = context.Logins.Any(x => x.Email == model.Email);
                    if (usernameExists)
                    {
                        ModelState.AddModelError("Username", "Username already exists.");
                        return View(model); 
                    }
                    if (PhoneExists)
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone number already exists.");
                    }

                    if (EmailExists)
                    {
                        ModelState.AddModelError("Email", "Email already exists.");
                    }

                    if (usernameExists || PhoneExists || EmailExists)
                    {
                        return View(model);
                    }


                }
                int i = repository.AddAdmin(model);
                if (i > 0)
                {
                    TempData["Created"] = "New user is created";
                    return RedirectToAction("Login", "AdminAuthentication");
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
                    return RedirectToAction("index", "Admin");
                }
            }
            return View();

        }
        //for logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index","Home");
        }

    
    }
}