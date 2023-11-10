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

namespace Project4
{
    public partial class CreateReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Name"] != null)
                {
                    string name = Request.QueryString["Name"];
                    txtRestaurant.Text = name;
                    txtRestaurant.ReadOnly = true;
                }

                if (Request.QueryString["UserID"] != null)
                {
                    string userID = Request.QueryString["UserID"];
                    txtUserID.Text = userID;
                    txtUserID.ReadOnly = true;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Review newReview = new Review();

            newReview.UserID = txtUserID.Text;
            newReview.Name = txtName.Text;
            newReview.Restaurant = txtRestaurant.Text;
            newReview.FoodRating = int.Parse(Request.Form["radFood"]);
            newReview.ServiceRating = int.Parse(Request.Form["radService"]);
            newReview.AtmosphereRating = int.Parse(Request.Form["radAtmosphere"]);
            newReview.PriceRating = int.Parse(Request.Form["radPrice"]);
            newReview.Comments = txtComments.Text;

            newReview.Comments = txtComments.Text;

            newReview.CreateReviews();
            lblConfirm.Text = "Review Submitted.";
        }

    }
}