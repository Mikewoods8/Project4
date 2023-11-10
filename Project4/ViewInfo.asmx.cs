using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using MyClassLibrary;
using Utilities;

namespace Project4
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ViewInfo : System.Web.Services.WebService
    {
        [WebMethod]
        public Info GetRestaurantDetails(string restaurantName)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ViewInformation";
            cmd.Parameters.AddWithValue("@RestaurantName", restaurantName);
            DataSet restaurantDataSet = db.GetDataSetUsingCmdObj(cmd);
            Info details = new Info();
            if (restaurantDataSet.Tables.Count > 0 && restaurantDataSet.Tables[0].Rows.Count > 0)
            {
                int informationColumnIndex = restaurantDataSet.Tables[0].Columns.Count - 1;
                object informationValue = restaurantDataSet.Tables[0].Rows[0][informationColumnIndex];
                details.Name = restaurantName;
                details.Information = informationValue != DBNull.Value ? informationValue.ToString() : "No information available";
            }
            else
            {
                details.Name = restaurantName;
                details.Information = "No information found.";
            }
            return details;
        }
    }
}
