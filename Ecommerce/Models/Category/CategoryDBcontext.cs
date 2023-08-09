using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class CategoryDBcontext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        //to display data 
        public List<Category> GetData()
        {
            List<Category> students_data = new List<Category>();
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("GetCatData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Category cat = new Category();
                cat.ID = Convert.ToInt32(dr.GetValue(0).ToString());
                cat.Name = dr.GetValue(1).ToString();
                cat.Displayorder = Convert.ToInt32(dr.GetValue(2).ToString());
                students_data.Add(cat);
            }
            conn.Close();
            return students_data;
        }
        // to create data in table 
        public bool createData(Category cat)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("CreateCatData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", cat.Name);
            cmd.Parameters.AddWithValue("@DisplayOrder", cat.Displayorder);
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
        // to update data in table 
        public bool UpdateData(Category cat)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("EditCatData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", cat.ID);
            cmd.Parameters.AddWithValue("@Name", cat.Name);
            cmd.Parameters.AddWithValue("@DisplayOrder", cat.Displayorder);
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
        // to delete data from table
        public bool DeleteData(Category cat)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("DeleteCatData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", cat.ID);
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
        // dropdownlist
        public List<Category> listdata()
        {
            List<Category> students_data = new List<Category>();
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("GetCatData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Category cat = new Category();
                cat.ID = Convert.ToInt32(dr.GetValue(0).ToString());
                cat.Name = dr.GetValue(1).ToString();
                students_data.Add(cat);
            }
            conn.Close();
            return students_data;
        }
    }
}