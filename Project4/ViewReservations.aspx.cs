using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using MyClassLibrary;
using RestaurantSoapService;

namespace Project4
{
    public partial class ViewReservations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManagement sessionRestaurantName = new SessionManagement();
                string restaurantName = sessionRestaurantName.GetRestaurantName();
                if (restaurantName != null)
                {
                    string selectedName = restaurantName;

                    PopulateReviews(selectedName);
                }
            }
        }

        private void PopulateReviews(string restaurantName)
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "GetRestaurantByReservation";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RestaurantName", restaurantName);

            DataSet dataSet = db.GetDataSet(cmd);

            if (dataSet.Tables.Count > 0)
            {
                gvReservations.DataSource = dataSet.Tables[0];
                gvReservations.DataBind();
            }
        }
        protected void btnReturnToYourRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reviewer.aspx");
        }
    }
}