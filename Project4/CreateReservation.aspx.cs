using System;
using MyClassLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;
using RestaurantSoapService;


namespace Project4
{
    public partial class CreateReservation : System.Web.UI.Page
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

                Reservation reservation = new Reservation();

                reservation.Name = txtName.Text;
                reservation.Restaurant = txtRestaurant.Text;
                reservation.SelectedDate = CalendarReserve.SelectedDate;
                reservation.Time = ddlTime.SelectedValue;

                reservation.CreateReservation();
                lblConfirm.Text = "Reservation Created.";
            }
        }
        protected void btnReturnToRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reviewer.aspx");
        }
    }
}