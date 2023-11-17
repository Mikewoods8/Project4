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

        protected void gvReservations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SessionManagement sessionID = new SessionManagement();
            string userID = sessionID.GetUserID();
            if (e.CommandName == "Delete")
            {
                int reviewId = Convert.ToInt32(e.CommandArgument);

                DeleteReview(reviewId);

                string selectedName = userID;
                PopulateReviews(selectedName);
            }
            else if (e.CommandName == "Modify")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                gvReservations.EditIndex = rowIndex;

                string selectedName = userID;
                PopulateReviews(selectedName);
            }
            else if (e.CommandName == "Update")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

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
                    string selectedName = userID;
                    PopulateReviews(selectedName);
                }
            }
        }

        private void UpdateReservations(int restaurantId, string name, string restaurant, string date, string time)
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "UpdateRestaurantForGV";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Restuarnt", restaurant);
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
            Response.Redirect("Reviewer.aspx");
        }
    }
}