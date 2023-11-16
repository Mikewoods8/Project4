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
    public partial class CreateReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManagement sessionID = new SessionManagement();
                string userID = sessionID.GetUserID();
                if (Request.QueryString["Name"] != null)
                {
                    string name = Request.QueryString["Name"];
                    txtRestaurant.Text = name;
                    txtRestaurant.ReadOnly = true;
                }
                if (userID != null)
                {
                    txtUserID.Text = userID;
                    txtUserID.ReadOnly = true;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Review newReview = new Review();
            if (txtUserID.Text == null || txtUserID.Text == "" || txtName.Text == null || txtName.Text == "" ||
                txtRestaurant.Text == null || txtRestaurant.Text == "" || txtComments.Text == null || txtComments.Text == "" ||
                string.IsNullOrEmpty(Request.Form["radFood"]) || string.IsNullOrEmpty(Request.Form["radService"]) ||
                string.IsNullOrEmpty(Request.Form["radAtmosphere"]) || string.IsNullOrEmpty(Request.Form["radPrice"]))
            {
                lblConfirm.Text = "You must fill out all fields before submitting.";
            }
            else
            {
                lblConfirm.Text = "";
                newReview.UserID = txtUserID.Text;
                newReview.Name = txtName.Text;
                newReview.Restaurant = txtRestaurant.Text;
                newReview.FoodRating = int.Parse(Request.Form["radFood"]);
                newReview.ServiceRating = int.Parse(Request.Form["radService"]);
                newReview.AtmosphereRating = int.Parse(Request.Form["radAtmosphere"]);
                newReview.PriceRating = int.Parse(Request.Form["radPrice"]);
                newReview.Comments = txtComments.Text;
                newReview.CreateReviews();
                lblConfirm.Text = "Review Submitted.";
            }
        }
        protected void btnReturnToRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reviewer.aspx");
        }
    }
}