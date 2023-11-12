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
using Project4;

namespace Project3
{
    public partial class Representative : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ShowRestaurants();

                BindCategoryListBox();
            }
        }

        private void ShowRestaurants()
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "GetRestaurants";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet dataSet = db.GetDataSet(cmd);

            gvRestaurants.DataSource = dataSet;
            gvRestaurants.DataBind();


        }
        private void BindCategoryListBox()
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "GetCategories";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet dataSet = db.GetDataSet(cmd);

            if (dataSet.Tables.Count > 0)
            {
                chkListCategory.DataSource = dataSet.Tables[0];
                chkListCategory.DataTextField = "Category";
                chkListCategory.DataValueField = "Category";
                chkListCategory.DataBind();
            }
        }

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

            DataTable filteredData = new DataTable();

            if (selectedCategories.Count == 0)
            {
                ShowRestaurants();
            }
            else
            {

                foreach (string category in selectedCategories)
                {

                    DBConnect db = new DBConnect();

                    string storedProcedureName = "GetRestaurantByCategory";

                    SqlCommand cmd = new SqlCommand(storedProcedureName);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Category", category);

                    DataSet dataSet = db.GetDataSet(cmd);

                    if (dataSet.Tables.Count > 0)
                    {
                        filteredData.Merge(dataSet.Tables[0]);
                    }
                }

                gvRestaurants.DataSource = filteredData;
                gvRestaurants.DataBind();
            }
        }

        protected void gvRestaurants_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

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

                Response.Redirect($"CreateReview.aspx?Name={selectedName}&UserID={Session["UserID"]}");
            }
            else if (e.CommandName == "ViewDetails")
            {
                string selectedName = gvRestaurants.Rows[rowIndex].Cells[0].Text;
                ViewInfo service = new ViewInfo();
                Info details = service.GetRestaurantDetails(selectedName);
                ClientScript.RegisterStartupScript(this.GetType(), "ShowDetails", $"alert('Name: {details.Name}\\nInformation: {details.Information}');", true);
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
        }

        protected void btnViewPersonalReviews_Click(object sender, EventArgs e)
        {
            Response.Redirect($"ViewPersonalReviews.aspx?UserID={Session["UserID"]}");
        }

        protected void btnAddRestaurant_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateRestaurant.aspx");
        }

        protected void btnRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect($"RepresentativesRestaurants.aspx?UserID={Session["UserID"]}");
        }
    }
}