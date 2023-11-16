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
    public class ReviewModel
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Restaurant { get; set; }
        public int FoodRating { get; set; }
        public int ServiceRating { get; set; }
        public int AtmosphereRating { get; set; }
        public int PriceRating { get; set; }
        public string Comments { get; set; }

        public void CreateReviews()
        {
            DBConnect db = new DBConnect();
            SqlConnection connection = db.GetConnection();

            using (SqlCommand cmd = new SqlCommand("CreateReview", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Restaurant", Restaurant);
                cmd.Parameters.AddWithValue("@FoodRating", FoodRating);
                cmd.Parameters.AddWithValue("@ServiceRating", ServiceRating);
                cmd.Parameters.AddWithValue("@AtmosphereRating", AtmosphereRating);
                cmd.Parameters.AddWithValue("@PriceRating", PriceRating);
                cmd.Parameters.AddWithValue("@Comments", Comments);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}
