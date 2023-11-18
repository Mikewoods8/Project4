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
                    PopulateRestaurants(userID);
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
            UpdateRestaurantModel[] restaurants = js.Deserialize<UpdateRestaurantModel[]>(data);

            gvRestaurants.DataSource = restaurants;
            gvRestaurants.DataBind();

        }

        //method to handle row commands
        protected void gvRestaurants_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SessionManagement sessionID = new SessionManagement();
            string userID = sessionID.GetUserID();

            if (e.CommandName == "Delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                int restaurantId = Convert.ToInt32(gvRestaurants.DataKeys[rowIndex].Value);

                DeleteReview(restaurantId);

                string selectedName = userID;
                PopulateRestaurants(selectedName);
            }
            else if (e.CommandName == "ViewReservation")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string selectedName = gvRestaurants.Rows[rowIndex].Cells[1].Text;

                Response.Redirect($"ViewReservations.aspx?RestaurantName={selectedName}");
            }
            else if (e.CommandName == "Modify")
            {

            }
            else if (e.CommandName == "Update")
            {

            }
        }

        private void DeleteReview(int restaurantId)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonUser = js.Serialize(restaurantId);

            try
            {
                WebRequest request = WebRequest.Create(webApiUrl + $"RestaurantService/DeleteRestaurant?restaurantId={restaurantId}");
                request.Method = "DELETE";
                request.ContentLength = jsonUser.Length;
                request.ContentType = "application/json";

                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(jsonUser);
                writer.Flush();
                writer.Close();

                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                if (data == "true")
                    lblConfirm.Text = "Restaurant succesfully deleted.";
                else
                    lblConfirm.Text = "A problem occured. Restaurant not deleted.";

            }
            catch (Exception ex)
            {
                lblConfirm.Text = "Error: " + ex.Message;

            }
        }

        //method to handle updating into the database (need to implment web api)
        private bool UpdateRestaurant(UpdateRestaurantModel updateRestaurant)
        {
            return true;
        }

        protected void gvRestaurants_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            return;
        }

        protected void gvRestaurants_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblConfirm.Text = "Restaurant succesfully updated.";
        }

        protected void btnReturnToRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Representative.aspx");
        }
    }
}