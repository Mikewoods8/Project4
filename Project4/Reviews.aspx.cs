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

namespace Project4
{
    public partial class Reviews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Name"] != null)
                {
                    string selectedName = Request.QueryString["Name"];

                    PopulateReviews(selectedName);
                }
            }
        }

        private void PopulateReviews(string restaurantName)
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "GetRestaurantReviews";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RestaurantName", restaurantName);

            DataSet dataSet = db.GetDataSet(cmd);

            if (dataSet.Tables.Count > 0)
            {
                gvReviews.DataSource = dataSet.Tables[0];
                gvReviews.DataBind();
            }
        }
    }
}
