using Ecommerce.Models.EntityAuthentication.DBoperations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ecommerce.Controllers
{
    public class AccountsController : Controller
    {
        LoginRepository repository = null;
        public AccountsController()
        {
            repository = new LoginRepository();
        }
        public ActionResult Register()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Register(Logins model) 
        { 
            if(ModelState.IsValid) 
            {
                repository.Adduser(model);
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        /*[HttpPost]
        public ActionResult Login(AllDetails model)
        {
                return View();
            
        }*/
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


      

    }
}