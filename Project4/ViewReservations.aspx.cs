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
                SessionManagement sessionID = new SessionManagement();
                string name = sessionID.GetRestaurantName();
                if (name != null)
                {
                    PopulateReservations(name);
                }
                else if (Request.QueryString["RestaurantName"] != null)
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
            UpdateReservationModel[] reservations = js.Deserialize<UpdateReservationModel[]>(data);

            gvReservations.DataSource = reservations;
            gvReservations.DataBind();

        }

        protected void gvReservations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SessionManagement sessionID = new SessionManagement();
            string name = sessionID.GetRestaurantName();


            if (e.CommandName == "Delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                int reservationId = Convert.ToInt32(gvReservations.DataKeys[rowIndex].Value);

                DeleteReview(reservationId);

                string selectedName = name;
                PopulateReservations(selectedName);
            }
            else if (e.CommandName == "Modify")
            {

            }
            else if (e.CommandName == "Update")
            {

            }
        }

        private void DeleteReview(int reservationId)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonUser = js.Serialize(reservationId);

            try
            {
                WebRequest request = WebRequest.Create(webApiUrl + $"ReservationService/DeleteReservation?reservationId={reservationId}");
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
                    lblConfirm.Text = "Reservation succesfully deleted.";
                else
                    lblConfirm.Text = "A problem occured. Reservation not deleted.";

            }
            catch (Exception ex)
            {
                lblConfirm.Text = "Error: " + ex.Message;

            }

        }

        private void UpdateReservations()
        {

        }

        protected void gvReservations_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            return;
        }

        protected void gvReservations_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            return;
        }

        protected void btnReturnToYourRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("RepresentativesRestaurants.aspx");
        }
    }
}