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
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Project4
{
    public partial class CreateReservation : System.Web.UI.Page
    {
        string webApiUrl = "https://localhost:7060/api/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Name"] != null)
                {
                    string selectedName = Request.QueryString["Name"];

                    txtRestaurant.Text = selectedName;
                    txtRestaurant.ReadOnly = true;
                }
            }
        }

        protected void btnCreateReservation_Click(object sender, EventArgs e)
        {
            lblErrorDate.Visible = false;
            lblErrorName.Visible = false;

            if (InputValidation.ValidateReservation(txtName, lblErrorName, CalendarReserve, lblErrorDate) == true)
            {

                ReservationModel reservation = new ReservationModel();

                reservation.Name = txtName.Text;
                reservation.Restaurant = txtRestaurant.Text;
                reservation.SelectedDate = CalendarReserve.SelectedDate;
                reservation.Time = ddlTime.SelectedValue;

                JavaScriptSerializer js = new JavaScriptSerializer();
                String jsonUser = js.Serialize(reservation);

                try
                {
                    WebRequest request = WebRequest.Create(webApiUrl + "ReservationService/AddReservation/");
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
                        lblConfirm.Text = "Reservation Created.";
                    else
                        lblConfirm.Text = "A problem occured. Reservation not created.";

                }
                catch (Exception ex)
                {
                    lblConfirm.Text = "Error: " + ex.Message;

                }

            }
        }
        protected void btnReturnToRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reviewer.aspx");
        }
    }
}