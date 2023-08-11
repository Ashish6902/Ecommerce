using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.AdminUiImage
{
    public class imgDBcontext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        //to view data
        public List<ImagesUI> GetData()
        {
            List<ImagesUI> students_data = new List<ImagesUI>();
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("Getimg", conn); // cretae db procedure
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ImagesUI stu = new ImagesUI();
                stu.ImageId = Convert.ToInt32(dr.GetValue(0).ToString());
                byte[] imageData = (byte[])dr.GetValue(1);
                stu.ImageData = imageData;
                students_data.Add(stu);
            }
            conn.Close();
            return students_data;
        }
        //to insert image
        public bool createData(ImagesUI stu)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("InsertImgData", conn); // create db porcedrure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ImgData", stu.ImageData);

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
        //UpdateUIImgData for update
        public bool UpdateData(ImagesUI stu)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("UpdateUIImgData", conn); // create db porcedrure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", stu.ImageId);
            cmd.Parameters.AddWithValue("@ImgData", stu.ImageData);

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