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
    public partial class CreateRestaurant : System.Web.UI.Page
    {
        string webApiUrl = "https://localhost:7060/api/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        //Method to create a restaurant using web api
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            RestaurantModel newRestaurant = new RestaurantModel();

             newRestaurant.Name = txtName.Text;
             newRestaurant.RepresentativeID = txtRepID.Text;
             newRestaurant.Category = radListCategory.SelectedItem.Value;

            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonUser = js.Serialize(newRestaurant);

            try
            {
                WebRequest request = WebRequest.Create(webApiUrl + "RestaurantService/AddRestaurant/");
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
                    lblConfirm.Text = "Restaurant succesfully added.";
                else
                    lblConfirm.Text = "A problem occured. Restaurant not created.";

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