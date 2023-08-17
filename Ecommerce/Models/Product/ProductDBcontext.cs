using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Product
{
    public class ProductDBcontext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        //to view data 

        public List<Product> GetData(int sellerId)
        {
            List<Product> Product_data = new List<Product>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("GetProductsForSeller", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter for seller_id
                cmd.Parameters.Add(new SqlParameter("@seller_id", SqlDbType.Int) { Value = sellerId });

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Product pro = new Product();
                        pro.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                        pro.Price = Convert.ToInt32(dr.GetValue(1).ToString());
                        pro.ProductName = dr.GetValue(2).ToString();
                        pro.Description = dr.GetValue(3).ToString();
                        pro.Count = Convert.ToInt32(dr.GetValue(4).ToString());
                        pro.Category = dr.GetValue(5).ToString();

                        // Retrieve image data as byte array
                        if (!dr.IsDBNull(6))
                        {
                            pro.ImageDataBytes = (byte[])dr.GetValue(6);
                        }

                        pro.sellerId = Convert.ToInt32(dr.GetValue(7).ToString());

                        Product_data.Add(pro);
                    }

                }
            }
            return Product_data;
        }



        // to insert data
        public bool CreateData(Product pro, int sellerId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("InsertProductData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@code", pro.Price);
                cmd.Parameters.AddWithValue("@Name", pro.ProductName);
                cmd.Parameters.AddWithValue("@Description", pro.Description);
                cmd.Parameters.AddWithValue("@count", pro.Count);
                cmd.Parameters.AddWithValue("@category", pro.Category);
                cmd.Parameters.AddWithValue("@img", pro.ImageDataBytes);
                cmd.Parameters.AddWithValue("@seller_id", sellerId); // Add seller_id parameter

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


        // to update data in table 
        public bool UpdateData(Product pro)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("UpdateProductData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", pro.Id);
            cmd.Parameters.AddWithValue("@code", pro.Price);
            cmd.Parameters.AddWithValue("@Name", pro.ProductName);
            cmd.Parameters.AddWithValue("@Description", pro.Description);
            cmd.Parameters.AddWithValue("@count", pro.Count);
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
        public bool DeleteData(Product pro)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("DeleteProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", pro.Id);
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