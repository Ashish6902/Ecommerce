﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.user
{
    public class userDBcontext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        //to display data 
        public List<User> GetUserData()
        {
            List<User> User_data = new List<User>();
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("GetUserData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                User cat = new User();
                cat.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                cat.UserName = dr.GetValue(1).ToString();
                cat.FName = dr.GetValue(3).ToString();
                cat.LName = dr.GetValue(4).ToString();
                cat.Email = dr.GetValue(6).ToString();
                cat.Phone = Convert.ToInt64(dr.GetValue(5).ToString());
                cat.address = dr.GetValue(7).ToString();
                cat.Password =dr.GetValue(2).ToString();
                User_data.Add(cat);
            }
            conn.Close();
            return User_data;
        }
        public bool UpdateUserData(User cat)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("UpdateUserData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", cat.Id);
            cmd.Parameters.AddWithValue("@Password", cat.Password);
            cmd.Parameters.AddWithValue("@Address", cat.address);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteUserData(User cat)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("DeleteUserData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", cat.Id);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}