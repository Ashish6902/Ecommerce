using Ecommerce.Models.Authentication;
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
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string userName = form["UserName"];
            string password = form["Password"];

            if (IsValidUser(userName, password))
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        public bool IsValidUser(string username, string password)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

            // Check in Seller table
            string sellerQuery = "SELECT COUNT(*) FROM Seller WHERE Seller_UserName = @Username AND Seller_Password = @Password";
            int sellerCount = ExecuteQuery(cs, sellerQuery, username, password);

            if (sellerCount > 0)
            {
                return true;
            }

            // Check in AdminData table
            string adminQuery = "SELECT COUNT(*) FROM AdminData WHERE Admin_UserName = @Username AND Admin_Passwod = @Password";
            int adminCount = ExecuteQuery(cs, adminQuery, username, password);

            if (adminCount > 0)
            {
                return true;
            }

            // Check in Users table
            string usersQuery = "SELECT COUNT(*) FROM Users WHERE User_UserName = @Username AND User_Password = @Password";
            int usersCount = ExecuteQuery(cs, usersQuery, username, password);

            return usersCount > 0;
        }

        private int ExecuteQuery(string connectionString, string query, string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }



    }
}