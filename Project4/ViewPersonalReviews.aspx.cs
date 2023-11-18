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
    public partial class ViewPersonalReviews : System.Web.UI.Page
    {
        private string webApiUrl = "https://localhost:7060/api/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManagement sessionID = new SessionManagement();
                string userID = sessionID.GetUserID();
                if (userID != null)
                {

                    PopulateReviews(userID);
                }
                gvPersonalReviews.RowDataBound += gvPersonalReviews_RowDataBound;
            }
        }

        //method to handle viewing your reviews using web api
        private void PopulateReviews(string userId)
        {
                WebRequest request = WebRequest.Create(webApiUrl + $"ReviewService/GetReviewById?userId={userId}");
                WebResponse response = request.GetResponse();

                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                JavaScriptSerializer js = new JavaScriptSerializer();
                PersonalReviewModel[] reviews = js.Deserialize<PersonalReviewModel[]>(data);

                gvPersonalReviews.DataSource = reviews;
                gvPersonalReviews.DataBind();
            }
        
        //method to handle row commands
        protected void gvReviews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SessionManagement sessionID = new SessionManagement();
            string userID = sessionID.GetUserID();

            if (e.CommandName == "Delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                int reviewId = Convert.ToInt32(gvPersonalReviews.DataKeys[rowIndex].Value);

                DeleteReview(reviewId);

                string selectedName = userID;
                PopulateReviews(selectedName);
            }
            else if (e.CommandName == "Modify")
            {

            }
            else if (e.CommandName == "Update")
            {

            }
        }

        //method to handle showing review averages
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

        //method to update reviews to database (need to implement web api)
        private void UpdateReview(UpdateReviewModel updateReview)
        {
            return;

        }

        //method to handle deleting a review from the database (need to implement web api)
        private void DeleteReview(int reviewId)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonUser = js.Serialize(reviewId);

            try
            {
                WebRequest request = WebRequest.Create(webApiUrl + $"ReviewService/DeleteReview?reviewId={reviewId}");
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
                    lblConfirm.Text = "Review succesfully deleted.";
                else
                    lblConfirm.Text = "A problem occured. Review not deleted";

            }
            catch (Exception ex)
            {
                lblConfirm.Text = "Error: " + ex.Message;

            }
        }

        protected void gvPersonalReviews_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            return;
        }

        protected void gvReviews_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            return;
        }

        protected void btnReturnToRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reviewer.aspx");
        }
    }
}
