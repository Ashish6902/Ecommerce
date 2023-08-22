using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Seller
{
    public class sellerDBcontext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        //to display data 
        public List<Seller> GetSellerData()
        {
            List<Seller> Seller_data = new List<Seller>();
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("GetSellerData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Seller cat = new Seller();
                cat.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                cat.UserName = dr.GetValue(1).ToString();
                cat.FName = dr.GetValue(3).ToString();
                cat.LName = dr.GetValue(4).ToString();
                cat.Email = dr.GetValue(6).ToString();
                cat.Phone = Convert.ToInt64(dr.GetValue(5).ToString());
                cat.address = dr.GetValue(7).ToString();
                cat.Brand =dr.GetValue(8).ToString();
                Seller_data.Add(cat);
            }
            conn.Close();
            return Seller_data;
        }
        public bool DeleteSellerData(Seller cat)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("DeleteSellerData", conn);
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