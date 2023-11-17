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
using System.Web.Services;
using RestaurantSoapService;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Project4
{
    public partial class Reviewer : System.Web.UI.Page
    {
        string webApiUrl = "https://localhost:7060/api/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetRole getRole = new GetRole();
                bool isReviewer = getRole.GetUserRole();
                gvRestaurants.Columns[gvRestaurants.Columns.Count - 1].Visible = isReviewer;
                ShowRestaurants();
            }
        }


        private void ShowRestaurants()
        {
            WebRequest request = WebRequest.Create(webApiUrl + "RestaurantService/GetRestaurant/");
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            RestaurantModel[] restaurants = js.Deserialize<RestaurantModel[]>(data);

            gvRestaurants.DataSource = restaurants;
            gvRestaurants.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<string> selectedCategories = new List<string>();

            foreach (ListItem item in chkListCategory.Items)
            {
                if (item.Selected)
                {
                    selectedCategories.Add(item.Value);
                }
            }

            DataTable filteredData = new DataTable();

            if (selectedCategories.Count == 0)
            {
                ShowRestaurants();
            }
            else
            {
                foreach (string category in selectedCategories)
                {
                    DBConnect db = new DBConnect();

                    string storedProcedureName = "GetRestaurantByCategory";

                    SqlCommand cmd = new SqlCommand(storedProcedureName);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Category", category);

                    DataSet dataSet = db.GetDataSet(cmd);

                    if (dataSet.Tables.Count > 0)
                    {
                        filteredData.Merge(dataSet.Tables[0]);
                    }
                }

                gvRestaurants.DataSource = filteredData;
                gvRestaurants.DataBind();
            }
        }

        protected void gvRestaurants_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            SessionManagement sessionID = new SessionManagement();
            string userID = sessionID.GetUserID();

            if (e.CommandName == "ViewReview")
            {
                string selectedName = gvRestaurants.Rows[rowIndex].Cells[0].Text;

                Response.Redirect($"Reviews.aspx?Name={selectedName}");
            }
            else if (e.CommandName == "MakeReservation")
            {
                string selectedName = gvRestaurants.Rows[rowIndex].Cells[0].Text;

                Response.Redirect($"CreateReservation.aspx?Name={selectedName}");
            }
            else if (e.CommandName == "WriteReview")
            {

                int selectedIndex = Convert.ToInt32(e.CommandArgument);

                string selectedName = gvRestaurants.Rows[selectedIndex].Cells[0].Text;

                Response.Redirect($"CreateReview.aspx?Name={selectedName}&UserID={userID}");
            }
            else if (e.CommandName == "ViewDetails")
            {
                string selectedName = gvRestaurants.Rows[rowIndex].Cells[0].Text;
                ViewInfo service = new ViewInfo();
                Info details = service.GetRestaurantDetails(selectedName);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowDetails", $"alert('Name: {details.Name}\\nInformation: {details.Information}');", true);
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
        }

        protected void btnViewPersonalReviews_Click(object sender, EventArgs e)
        {
            SessionManagement sessionID = new SessionManagement();
            string userID = sessionID.GetUserID();
            Response.Redirect($"ViewPersonalReviews.aspx?UserID={userID}");
        }

        protected void btnAddRestaurant_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateRestaurant.aspx");
        }
    }
}