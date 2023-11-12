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
        protected void btnReturnToRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reviewer.aspx");
        }
    }
}

////Function to display avgs of all reviews per restaurant
//protected void GetAverageScores()
//{
//    db = new DBConnect();
//    cmd.CommandType = CommandType.StoredProcedure;
//    cmd.CommandText = "GetAverageScores";
//    DataSet averagesDataSet = db.GetDataSetUsingCmdObj(cmd);
//    db.CloseConnection();
//    if (averagesDataSet.Tables.Count > 0 && averagesDataSet.Tables[0].Rows.Count > 0)
//    {
//        gvAverages.DataSource = averagesDataSet.Tables[0];
//        gvAverages.DataBind();
//    }
//}

////Function to calculate the avg for each row
//protected double GetAverage(object foodQuality, object service, object atmosphere, object priceLevel)
//{
//    double total = 0.0;
//    int count = 0;
//    if (foodQuality != DBNull.Value)
//    {
//        total += Convert.ToDouble(foodQuality);
//        count++;
//    }
//    if (service != DBNull.Value)
//    {
//        total += Convert.ToDouble(service);
//        count++;
//    }
//    if (atmosphere != DBNull.Value)
//    {
//        total += Convert.ToDouble(atmosphere);
//        count++;
//    }
//    if (priceLevel != DBNull.Value)
//    {
//        total += Convert.ToDouble(priceLevel);
//        count++;
//    }
//    if (count > 0)
//    {
//        return total / count;
//    }
//    return 0.0;
//}

////Function to calculate to overall avg for each restaurant
//protected double CalculateOverallAverage(object avgFoodQuality, object avgService, object avgAtmosphere, object avgPriceLevel)
//{
//    double overallAverage = 0.0;
//    int count = 0;
//    if (avgFoodQuality != DBNull.Value)
//    {
//        overallAverage += Convert.ToDouble(avgFoodQuality);
//        count++;
//    }
//    if (avgService != DBNull.Value)
//    {
//        overallAverage += Convert.ToDouble(avgService);
//        count++;
//    }
//    if (avgAtmosphere != DBNull.Value)
//    {
//        overallAverage += Convert.ToDouble(avgAtmosphere);
//        count++;
//    }
//    if (avgPriceLevel != DBNull.Value)
//    {
//        overallAverage += Convert.ToDouble(avgPriceLevel);
//        count++;
//    }
//    if (count > 0)
//    {
//        overallAverage /= count;
//    }
//    return overallAverage;
//}
