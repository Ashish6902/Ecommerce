using Ecommerce.Models.EntityAuthentication.DBoperations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
// this is only for user register and log in
namespace Ecommerce.Controllers
{
    public class AccountsController : Controller
    {
        LoginRepository repository = null;
        // constructor
        public AccountsController()
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
            if(ModelState.IsValid) 
            {
                int i = repository.Adduser(model);
                if(i>0) 
                {
                    TempData["Created"] = "New user is created";
                    return RedirectToAction("Login", "Accounts");
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
            using(var context =new EcommerceEntities())
            {
                bool isValid = context.Logins.Any(x=>x.UserName == model.UserName&&x.HashedPassword == model.HashedPassword);
                
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
            return RedirectToAction("index", "Home");
        }


      

    }
}