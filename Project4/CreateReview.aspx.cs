using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Script.Serialization;
using System.IO;
using System.Net;
using System.Data;
using Utilities;
using MyClassLibrary;
using RestaurantSoapService;

namespace Project4
{
    public partial class CreateReview : System.Web.UI.Page
    {
        string webApiUrl = "https://localhost:7060/api/";
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

        //Method to Create a review using web api
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ReviewModel newReview = new ReviewModel();
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
                newReview.UserId = txtUserID.Text;
                newReview.Name = txtName.Text;
                newReview.Restaurant = txtRestaurant.Text;
                newReview.FoodRating = int.Parse(Request.Form["radFood"]);
                newReview.ServiceRating = int.Parse(Request.Form["radService"]);
                newReview.AtmosphereRating = int.Parse(Request.Form["radAtmosphere"]);
                newReview.PriceRating = int.Parse(Request.Form["radPrice"]);
                newReview.Comments = txtComments.Text;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonUser = js.Serialize(newReview);

            try
            {
                WebRequest request = WebRequest.Create(webApiUrl + "ReviewService/AddReview/");
                request.Method = "POST";
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
                    lblConfirm.Text = "Review succesfully created.";
                else
                    lblConfirm.Text = "A problem occured. Review not created.";

            }
            catch (Exception ex)
            {
                lblConfirm.Text = "Error: " + ex.Message;

            }

        }
        protected void btnReturnToRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reviewer.aspx");
        }
    }
}