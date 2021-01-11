using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolTopikHanoi.Libraries;
using IIS.Common;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace IIS.Migrations
{
    public class DataProvider
    {
        private static string ConnectString => ConfigHelper.GetKey(ApplicationSettings.ConnectionString);

        /// <summary>
        /// Hàm lấy dữ liệu dựa trên câu lệnh truy vấn hoặc thủ tục
        /// </summary>
        /// <param name="strSQL">Câu lệnh truy vấn hoặc tên thủ tục</param>
        /// <param name="isProcedured">True nếu là thủ tục</param>
        /// <returns>Kết quả của câu lệnh truy vấn lấy từ db</returns>
        public static DataTable ExcuteGetData(string strSQL, bool isProcedured = false)
        {
            //Khai báo biến để chứa dữ liệu lấy được
            DataTable dt = new DataTable();

            //Khai báo 1 đối tượng để kết nối đến db cần làm việc
            using (SqlConnection conn = new SqlConnection(ConnectString))
            {
                try
                {
                    //Mở kết nối
                    conn.Open();

                    //Khai báo 1 công việc
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    if (isProcedured)
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        comm.CommandType = CommandType.Text;
                    }

                    //Công việc
                    comm.CommandText = strSQL;

                    //Khai báo đối tượng để lưu dữ liệu
                    SqlDataAdapter adapter = new SqlDataAdapter(comm);

                    //Đổ dữ liệu
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    conn.Close();
                    throw ex;
                }
            }

            return dt;
        }
    }
}
