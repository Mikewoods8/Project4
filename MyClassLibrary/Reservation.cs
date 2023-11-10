using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Utilities;

namespace MyClassLibrary
{
    public class Reservation
    {
        public string Name { get; set; }
        public string Restaurant { get; set; }
        public DateTime SelectedDate { get; set; }
        public string Time { get; set; }

        public void CreateReservation()
        {
            DBConnect db = new DBConnect();
            SqlConnection connection = db.GetConnection();

            using (SqlCommand cmd = new SqlCommand("CreateReservation", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", Name); ;
                cmd.Parameters.AddWithValue("@Restaurant", Restaurant);
                cmd.Parameters.AddWithValue("@Date", SelectedDate);
                cmd.Parameters.AddWithValue("@Time", Time);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
