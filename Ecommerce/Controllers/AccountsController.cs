using Ecommerce.Models;
using Ecommerce.Models.EntityAuthentication.DBoperations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
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

                    

                    int loginId = context.Logins.Where(x => x.UserName == model.UserName).Select(x => x.Login_id).FirstOrDefault();
                    int roleId = context.Logins.Where(x => x.Login_id == loginId).Select(x => x.Role_id).FirstOrDefault();
                    var role = context.Roles.FirstOrDefault(r => r.role_id == roleId);
                    string roleName = role.Roles1;
                    switch(roleName)
                    {
                        case "User":
                            var user = context.Logins.FirstOrDefault(x => x.UserName == model.UserName && x.HashedPassword == model.HashedPassword);
                            int userId = user.Login_id;
                            Session["UserId"] = userId;
                            return RedirectToAction("index", "Home");
                            
                        case "Admin":
                            return RedirectToAction("index", "Admin");

                        case "Seller":
                            var users = context.Logins.FirstOrDefault(x => x.UserName == model.UserName && x.HashedPassword == model.HashedPassword);
                            int userIds = users.Login_id;
                            Session["UserId"] = userIds;
                            return RedirectToAction("index", "Seller");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
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