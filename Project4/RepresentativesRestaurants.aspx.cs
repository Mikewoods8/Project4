﻿using System;
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

namespace Project4
{
    public partial class RepresentativesRestaurants : System.Web.UI.Page
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

                    PopulateRestaurants(selectedName);
                }
                else if (Request.QueryString["UserID"] != null)
                {
                    string selectedName = Request.QueryString["UserID"];
                    PopulateRestaurants(selectedName);
                }
            }
        }

        private void PopulateRestaurants(string userId)
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "GetRestaurantsByID";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", userId);

            DataSet dataSet = db.GetDataSet(cmd);

            if (dataSet.Tables.Count > 0)
            {
                gvRestaurants.DataSource = dataSet.Tables[0];
                gvRestaurants.DataBind();
            }
        }

        protected void gvRestaurants_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            SessionManagement sessionID = new SessionManagement();
            string userID = sessionID.GetUserID();

            if (e.CommandName == "Modify")
            {

                gvRestaurants.EditIndex = rowIndex;
                PopulateRestaurants(userID);
            }
            else if (e.CommandName == "Update")
            {

                if (rowIndex >= 0 && rowIndex < gvRestaurants.Rows.Count)
                {
                    GridViewRow editedRow = gvRestaurants.Rows[rowIndex];
                    TextBox txtName = editedRow.FindControl("txtName") as TextBox;
                    TextBox txtCategory = editedRow.FindControl("txtCategory") as TextBox;

                    int id = Convert.ToInt32(gvRestaurants.DataKeys[rowIndex].Value);

                    UpdateRestaurant(id, txtName.Text, txtCategory.Text);

                    gvRestaurants.EditIndex = -1;
                    PopulateRestaurants(userID);
                }
            }
            else if (e.CommandName == "ViewReservation")
            {
                string selectedName = gvRestaurants.Rows[rowIndex].Cells[1].Text;

                Response.Redirect($"ViewReservations.aspx?RestaurantName={selectedName}");
            }
        }

        protected void gvRestaurants_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblConfirm.Text = "Restaurant succesfully updated.";
        }

        private void UpdateRestaurant(int id, string name, string category)
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "UpdateRestaurant";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Category", category);

            db.DoUpdateUsingCmdObj(cmd);
        }
        protected void btnReturnToRestaurants_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reviewer.aspx");
        }
    }
}