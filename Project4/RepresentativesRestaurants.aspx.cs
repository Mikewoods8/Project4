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
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Project4
{
    public partial class RepresentativesRestaurants : System.Web.UI.Page
    {
        string webApiUrl = "https://localhost:7060/api/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManagement sessionID = new SessionManagement();
                string userID = sessionID.GetUserID();
                if (userID != null)
                {
                    string selectedName = userID;

                    PopulateRestaurants(selectedName);
                }
                else if (Request.QueryString["UserID"] != null)
                {
                    string selectedName = Request.QueryString["UserID"];
                    PopulateRestaurants(selectedName);
                }
            }
        }

        //Method to populate restaurants using web api get restaurants by id
        private void PopulateRestaurants(string userId)
        {
            WebRequest request = WebRequest.Create(webApiUrl + $"RestaurantService/GetRestaurantById?userId={userId}");
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

        //method to handle row commands
        protected void gvRestaurants_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            SessionManagement sessionID = new SessionManagement();
            string userID = sessionID.GetUserID();

            if (e.CommandName == "Modify")
            {

                gvRestaurants.EditIndex = rowIndex;
                PopulateRestaurants(userID);
            }
            else if (e.CommandName == "Update")
            {

                if (rowIndex >= 0 && rowIndex < gvRestaurants.Rows.Count)
                {
                    GridViewRow editedRow = gvRestaurants.Rows[rowIndex];
                    TextBox txtName = editedRow.FindControl("txtName") as TextBox;
                    TextBox txtCategory = editedRow.FindControl("txtCategory") as TextBox;

                    int id = Convert.ToInt32(gvRestaurants.DataKeys[rowIndex].Value);

                    UpdateRestaurant(id, txtName.Text, txtCategory.Text);

                    gvRestaurants.EditIndex = -1;
                    PopulateRestaurants(userID);
                }
            }
            else if (e.CommandName == "ViewReservation")
            {
                string selectedName = gvRestaurants.Rows[rowIndex].Cells[1].Text;

                Response.Redirect($"ViewReservations.aspx?RestaurantName={selectedName}");
            }
        }

        protected void gvRestaurants_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblConfirm.Text = "Restaurant succesfully updated.";
        }

        //method to handle updating into the database (need to implment web api)
        private void UpdateRestaurant(int id, string name, string category)
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "UpdateRestaurant";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Category", category);

            db.DoUpdateUsingCmdObj(cmd);
        }
        protected void btnReturnToRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Representative.aspx");
        }
    }
}