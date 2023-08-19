using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Cart
{
    public class CartDbcontext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        //to view data 

        public List<Cart> GetCartData(int UserId)
        {
            List<Cart> Cart_data = new List<Cart>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("GetCartData", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter for seller_id 
                cmd.Parameters.Add(new SqlParameter("@user_id", SqlDbType.Int) { Value = UserId });

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Cart pro = new Cart();
                        pro.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                        pro.Price = Convert.ToInt32(dr.GetValue(2).ToString());
                        pro.ProductName = dr.GetValue(3).ToString();
                        pro.Description = dr.GetValue(4).ToString();

                        // Retrieve image data as byte array
                        if (!dr.IsDBNull(7))
                        {
                            pro.ImageDataBytes = (byte[])dr.GetValue(7);
                        }

                        Cart_data.Add(pro);
                    }

                }
            }
            return Cart_data;
        }
        // to insert data
        public bool CreateCartData(int pro_id,int UserId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("InsertCartData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@product_id", pro_id);
                cmd.Parameters.AddWithValue("@user_id", UserId);

                conn.Open();
                int i = cmd.ExecuteNonQuery();

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
}