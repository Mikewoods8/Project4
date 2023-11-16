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
    public partial class ViewPersonalReviews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManagement sessionID = new SessionManagement();
                string userID = sessionID.GetUserID();
                if (userID != null)
                {
                    string selectedName = userID;

                    PopulateReviews(selectedName);
                }
                gvPersonalReviews.RowDataBound += gvPersonalReviews_RowDataBound;
            }
        }

        private void PopulateReviews(string userId)
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "GetReviewsByID";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", userId);

            DataSet dataSet = db.GetDataSet(cmd);

            if (dataSet.Tables.Count > 0)
            {
                gvPersonalReviews.DataSource = dataSet.Tables[0];
                gvPersonalReviews.DataBind();
            }
        }
        protected void gvReviews_RowCommand(object sender, GridViewCommandEventArgs e)
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
                gvPersonalReviews.EditIndex = rowIndex;

                string selectedName = userID;
                PopulateReviews(selectedName);
            }
            else if (e.CommandName == "Update")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                if (rowIndex >= 0 && rowIndex < gvPersonalReviews.Rows.Count)
                {
                    GridViewRow editedRow = gvPersonalReviews.Rows[rowIndex];
                    TextBox txtFoodRating = editedRow.FindControl("txtFoodRating") as TextBox;
                    TextBox txtServiceRating = editedRow.FindControl("txtServiceRating") as TextBox;
                    TextBox txtAtmosphereRating = editedRow.FindControl("txtAtmosphereRating") as TextBox;
                    TextBox txtPriceRating = editedRow.FindControl("txtPriceRating") as TextBox;
                    TextBox txtComments = editedRow.FindControl("txtComments") as TextBox;

                    int reviewId = Convert.ToInt32(gvPersonalReviews.DataKeys[rowIndex].Value);

                    UpdateReview(reviewId, txtFoodRating.Text, txtServiceRating.Text, txtAtmosphereRating.Text, txtPriceRating.Text, txtComments.Text);

                    gvPersonalReviews.EditIndex = -1;
                    string selectedName = userID;
                    PopulateReviews(selectedName);
                }
            }
        }
        protected void gvPersonalReviews_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                TableCell avgRatingHeader = new TableCell();
                avgRatingHeader.Text = "Average Rating";
                e.Row.Cells.AddAt(8, avgRatingHeader);
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int foodRating = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FoodRating"));
                int serviceRating = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ServiceRating"));
                int atmosphereRating = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AtmosphereRating"));
                int priceRating = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PriceRating"));
                double averageRating = (foodRating + serviceRating + atmosphereRating + priceRating) / 4.0;
                TableCell avgRatingCell = new TableCell();
                avgRatingCell.Text = averageRating.ToString("0.00");
                e.Row.Cells.AddAt(8, avgRatingCell);

            }
        }
        private void UpdateReview(int reviewId, string foodRating, string serviceRating, string atmosphereRating, string priceRating, string comments)
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "UpdateReview";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ReviewId", reviewId);
            cmd.Parameters.AddWithValue("@FoodRating", foodRating);
            cmd.Parameters.AddWithValue("@ServiceRating", serviceRating);
            cmd.Parameters.AddWithValue("@AtmosphereRating", atmosphereRating);
            cmd.Parameters.AddWithValue("@PriceRating", priceRating);
            cmd.Parameters.AddWithValue("@Comments", comments);

            db.DoUpdateUsingCmdObj(cmd);
        }

        protected void gvPersonalReviews_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblConfirm.Text = "Review successfully updated.";
        }

        protected void gvReviews_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblConfirm.Text = "Review successfully deleted.";
        }


        private void DeleteReview(int reviewId)
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "DeleteReview";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ReviewId", reviewId);

            db.DoUpdateUsingCmdObj(cmd);
        }

        protected void btnReturnToRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reviewer.aspx");
        }
    }
}
