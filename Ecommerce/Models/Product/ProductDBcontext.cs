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

       public List<Product> GetData()
{
    List<Product> Product_data = new List<Product>();
    using (SqlConnection conn = new SqlConnection(cs))
    {
        SqlCommand cmd = new SqlCommand("GetproductData", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        conn.Open();

        using (SqlDataReader dr = cmd.ExecuteReader())
        {
            while (dr.Read())
            {
                Product pro = new Product();
                pro.Id = Convert.ToInt32(dr["Id"]);
                pro.Price = Convert.ToInt32(dr["ProductCode"]);
                pro.ProductName = dr["ProductName"].ToString();
                pro.Description = dr["Description"].ToString();
                pro.Count = Convert.ToInt32(dr["Count"]);
                pro.Category = dr["Category"].ToString();

                // Check if the image data is not DBNull
                if (dr["ImageData"] != DBNull.Value)
                {
                    pro.ImageDataBytes = (byte[])dr["ImageData"];
                }

                Product_data.Add(pro);
            }
        }
    }

    return Product_data;
}


        // to insert data
        public bool createData(Product pro)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("InsertProductData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@code", pro.Price);
            cmd.Parameters.AddWithValue("@Name", pro.ProductName);
            cmd.Parameters.AddWithValue("@Description", pro.Description);
            cmd.Parameters.AddWithValue("@count", pro.Count);
            cmd.Parameters.AddWithValue("@category", pro.Category);
            cmd.Parameters.AddWithValue("@img", pro.ImageDataBytes);

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