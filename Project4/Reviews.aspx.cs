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
    public partial class Reviews : System.Web.UI.Page
    {
        string webApiUrl = "https://localhost:7060/api/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Name"] != null)
                {
                    string selectedName = Request.QueryString["Name"];

                    PopulateReviews(selectedName);
                }
                gvReviews.RowDataBound += gvReviews_RowDataBound;
            }
        }

        private void PopulateReviews(string restaurantName)
        {
            WebRequest request = WebRequest.Create(webApiUrl + $"ReviewService/GetReviewByRestaurant?restaurantName={restaurantName}");
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            ReviewModel[] reviews = js.Deserialize<ReviewModel[]>(data);

            gvReviews.DataSource = reviews;
            gvReviews.DataBind();
        }
        

        protected void gvReviews_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void btnReturnToRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reviewer.aspx");
        }
    }
}
