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
                SessionManagement sessionRestaurantName = new SessionManagement();
                string restaurantName = sessionRestaurantName.GetRestaurantName();
                if (restaurantName != null)
                {
                    string selectedName = Request.QueryString["RestaurantName"];
                    PopulateReservations(selectedName);
                }
            }
        }

        //method to handle seeing reservations for a representatives restuarnat (need to modify)
        private void PopulateReviews(string restaurantName)
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
        protected void btnReturnToYourRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("RepresentativesRestaurants.aspx");
        }
    }
}