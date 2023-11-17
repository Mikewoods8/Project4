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
    public partial class ViewReservations : System.Web.UI.Page
    {
        private string webApiUrl = "https://localhost:7060/api/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["RestaurantName"] != null)
                {
                    string selectedName = Request.QueryString["RestaurantName"];
                    PopulateReservations(selectedName);
                }
            }
        }

        //method to handle seeing reservations for a representatives restuarnat (need to modify)
        private void PopulateReservations(string restaurantName)
        {
            WebRequest request = WebRequest.Create(webApiUrl + $"ReservationService/GetReservationByRestaurant?selectedName={restaurantName}");
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            ReservationModel[] reservations = js.Deserialize<ReservationModel[]>(data);

            gvReservations.DataSource = reservations;
            gvReservations.DataBind();

        }

        protected void gvReservations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SessionManagement sessionID = new SessionManagement();
            string userID = sessionID.GetUserID();

            int rowIndex = -1;

            if (e.CommandName == "Delete")
            {
                int restaurantId = Convert.ToInt32(e.CommandArgument);
                DeleteReview(restaurantId);
            }
            else if (e.CommandName == "Modify")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument);
                gvReservations.EditIndex = rowIndex;
            }
            else if (e.CommandName == "Update")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument);

                if (rowIndex >= 0 && rowIndex < gvReservations.Rows.Count)
                {
                    GridViewRow editedRow = gvReservations.Rows[rowIndex];
                    TextBox txtName = editedRow.FindControl("txtName") as TextBox;
                    TextBox txtRestaurant = editedRow.FindControl("txtRestaurant") as TextBox;
                    TextBox txtDate = editedRow.FindControl("txtDate") as TextBox;
                    TextBox txtTime = editedRow.FindControl("txtTime") as TextBox;

                    int restaurantId = Convert.ToInt32(gvReservations.DataKeys[rowIndex].Value);

                    UpdateReservations(restaurantId, txtName.Text, txtRestaurant.Text, txtDate.Text, txtTime.Text);

                    gvReservations.EditIndex = -1;
                }
            }
            string selectedName = Request.QueryString["RestaurantName"];
            PopulateReservations(selectedName);
        }

        private void UpdateReservations(int restaurantId, string name, string restaurant, string date, string time)
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "UpdateRestaurantForGV";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Restaurant", restaurant);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@Time", time);

            db.DoUpdateUsingCmdObj(cmd);
        }

        protected void gvReservations_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblConfirm.Text = "Review successfully updated.";
        }

        protected void gvReservations_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblConfirm.Text = "Review successfully deleted.";
        }

        private void DeleteReview(int restaurantId)
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "DeleteRestaurantReservation";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);

            db.DoUpdateUsingCmdObj(cmd);

        }
        protected void btnReturnToYourRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("RepresentativesRestaurants.aspx");
        }
    }
}