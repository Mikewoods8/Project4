﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Utilities;
using RestaurantSoapService;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;
using System.Data;
using MyClassLibrary;
using System.Diagnostics;

namespace Project4
{
    public partial class CreateReservation : System.Web.UI.Page
    {
        string webApiUrl = "https://localhost:7060/api/";
        string userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Name"] != null)
                {
                    string selectedName = Request.QueryString["Name"];

                    txtRestaurant.Text = selectedName;
                    txtRestaurant.ReadOnly = false;
                }
                SessionManagement sessionID = new SessionManagement();
                userID = sessionID.GetUserID();
                hdnReferringPageUrl.Value = Request.UrlReferrer?.AbsolutePath;
            }
        }

        //Method to create a reservation using web api
        protected void btnCreateReservation_Click(object sender, EventArgs e)
        {
            lblErrorDate.Visible = false;
            lblErrorName.Visible = false;

            ReservationModel newReservation = new ReservationModel();

            if (InputValidation.ValidateReservation(txtName, lblErrorName, CalendarReserve, lblErrorDate) == true)
            {

                newReservation.Name = txtName.Text;
                newReservation.Restaurant = txtRestaurant.Text;
                newReservation.Date = CalendarReserve.SelectedDate.ToString();
                newReservation.Time = ddlTime.SelectedValue;

            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonUser = js.Serialize(newReservation);

            try
            {
                WebRequest request = WebRequest.Create(webApiUrl + "ReservationService/AddReservation");
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

        protected void btnReturnToRestaurants_Click(object sender, EventArgs e)
        {
            string referringPageUrl = hdnReferringPageUrl.Value;
            if (!string.IsNullOrEmpty(referringPageUrl))
            {
                referringPageUrl = referringPageUrl.ToLower(); 
                if (referringPageUrl.Contains("reviewer"))
                {
                    Response.Redirect("Reviewer.aspx");
                }
                else if (referringPageUrl.Contains("representative"))
                {
                    Response.Redirect("Representative.aspx");
                }
                else if (referringPageUrl.Contains("sitevisitor"))
                {
                    Response.Redirect("SiteVisitor.aspx");
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}