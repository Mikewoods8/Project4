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
    public partial class Representative : System.Web.UI.Page
    {
        string webApiUrl = "https://localhost:7060/api/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ShowRestaurants();
            }
        }

        //Method to bind restaurants to the grid view using web api
        private void ShowRestaurants()
        {
            WebRequest request = WebRequest.Create(webApiUrl + "RestaurantService/GetRestaurant/");
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            RestaurantModel[] restaurants = js.Deserialize<RestaurantModel[]>(data);

            foreach (var restaurant in restaurants)
            {
                switch (restaurant.Category)
                {
                    case "American":
                        restaurant.Image = "~/images/American.jfif";
                        break;
                    case "Italian":
                        restaurant.Image = "~/images/Italian.jfif";
                        break;
                    case "Barbecue":
                        restaurant.Image = "~/images/Barbecue.jfif";
                        break;
                    case "Mexican":
                        restaurant.Image = "~/images/Mexican.jfif";
                        break;
                    case "Chinese":
                        restaurant.Image = "~/images/Chinese.jfif";
                        break;
                    default:
                        restaurant.Image = "~/images/American.jfif";
                        break;
                }
            }
            gvRestaurants.DataSource = restaurants;
            gvRestaurants.DataBind();
        }

        //method to handle searching for restaurants based on category using web api
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<string> selectedCategories = new List<string>();

            foreach (ListItem item in chkListCategory.Items)
            {
                if (item.Selected)
                {
                    selectedCategories.Add(item.Value);
                }
            }

            if (selectedCategories.Count == 0)
            {
                ShowRestaurants();
            }
            else
            {
                WebRequest request = WebRequest.Create(webApiUrl + "RestaurantService/GetRestaurant/");
                WebResponse response = request.GetResponse();

                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                JavaScriptSerializer js = new JavaScriptSerializer();
                RestaurantModel[] allRestaurants = js.Deserialize<RestaurantModel[]>(data);

                foreach (var restaurant in allRestaurants)
                {
                    switch (restaurant.Category)
                    {
                        case "American":
                            restaurant.Image = "~/images/American.jfif";
                            break;
                        case "Italian":
                            restaurant.Image = "~/images/Italian.jfif";
                            break;
                        case "Barbecue":
                            restaurant.Image = "~/images/Barbecue.jfif";
                            break;
                        case "Mexican":
                            restaurant.Image = "~/images/Mexican.jfif";
                            break;
                        case "Chinese":
                            restaurant.Image = "~/images/Chinese.jfif";
                            break;
                        default:
                            restaurant.Image = "~/images/American.jfif";
                            break;
                    }
                }

                List<RestaurantModel> filteredRestaurants;

                if (selectedCategories.Count > 0)
                {
                    filteredRestaurants = allRestaurants
                        .Where(r => selectedCategories.Contains(r.Category))
                        .ToList();
                }
                else
                {
                    filteredRestaurants = allRestaurants.ToList();
                }

                gvRestaurants.DataSource = filteredRestaurants;
                gvRestaurants.DataBind();
            }
        }

        //method to handle row commands 
        protected void gvRestaurants_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            SessionManagement sessionID = new SessionManagement();
            string userID = sessionID.GetUserID();

            if (e.CommandName == "ViewReview")
            {
                string selectedName = gvRestaurants.Rows[rowIndex].Cells[0].Text;

                Response.Redirect($"Reviews.aspx?Name={selectedName}");
            }
            else if (e.CommandName == "MakeReservation")
            {
                string selectedName = gvRestaurants.Rows[rowIndex].Cells[0].Text;

                Response.Redirect($"CreateReservation.aspx?Name={selectedName}");
            }
            else if (e.CommandName == "WriteReview")
            {
                int selectedIndex = Convert.ToInt32(e.CommandArgument);

                string selectedName = gvRestaurants.Rows[selectedIndex].Cells[0].Text;

                Response.Redirect($"CreateReview.aspx?Name={selectedName}&UserID={userID}");
            }
            else if (e.CommandName == "ViewDetails")
            {
                string selectedName = gvRestaurants.Rows[rowIndex].Cells[0].Text;
                ViewInfo service = new ViewInfo();
                Info details = service.GetRestaurantDetails(selectedName);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowDetails", $"alert('Name: {details.Name}\\nInformation: {details.Information}');", true);

            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
        }

        //method to handle clicking the 'Your Reviews' button
        protected void btnViewPersonalReviews_Click(object sender, EventArgs e)
        {
            SessionManagement sessionID = new SessionManagement();
            string userID = sessionID.GetUserID();
            if (!string.IsNullOrEmpty(userID))
            {
                Response.Redirect($"ViewPersonalReviews.aspx?UserID={userID}");
            }
            else
            {
                Response.Redirect("LogIn.aspx");
            }
        }


        protected void btnAddRestaurant_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateRestaurant.aspx");
        }

        //method to view 'your restuarants'
        protected void btnRestaurants_Click(object sender, EventArgs e)
        {
            SessionManagement sessionID = new SessionManagement();
            string userID = sessionID.GetUserID();
            if (!string.IsNullOrEmpty(userID))
            {
                Response.Redirect($"RepresentativesRestaurants.aspx?UserID={userID}");
            }
            else
            {
                Response.Redirect("LogIn.aspx");
            }
        }
    }
}