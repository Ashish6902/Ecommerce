using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Ecommerce
{
    public class WebRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();

            // Fetch roles from the Seller table based on the username
            roles.AddRange(GetRolesFromSellerTable(username));

            // Fetch roles from the AdminData table based on the username
            roles.AddRange(GetRolesFromAdminDataTable(username));

            // Fetch roles from the Users table based on the username
            roles.AddRange(GetRolesFromUsersTable(username));

            return roles.ToArray();
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        private IEnumerable<string> GetRolesFromSellerTable(string username)
        {
            List<string> sellerRoles = new List<string>();

            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            string query = "SELECT RoleName FROM SellerRoles WHERE Seller_UserName = @Username";

            using (SqlConnection connection = new SqlConnection(cs))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sellerRoles.Add(reader["RoleName"].ToString());
                    }
                }
            }

            return sellerRoles;
        }
        private IEnumerable<string> GetRolesFromAdminDataTable(string username)
        {
            List<string> adminRoles = new List<string>();

            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            string query = "SELECT RoleName FROM AdminRoles WHERE Admin_UserName = @Username";

            using (SqlConnection connection = new SqlConnection(cs))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        adminRoles.Add(reader["RoleName"].ToString());
                    }
                }
            }

            return adminRoles;
        }

        private IEnumerable<string> GetRolesFromUsersTable(string username)
        {
            List<string> userRoles = new List<string>();

            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            string query = "SELECT RoleName FROM UserRoles WHERE User_UserName = @Username";

            using (SqlConnection connection = new SqlConnection(cs))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userRoles.Add(reader["RoleName"].ToString());
                    }
                }
            }

            return userRoles;
        }


    }
}