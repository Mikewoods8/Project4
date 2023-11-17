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
    public partial class Reviews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Name"] != null)
                {
                    string selectedName = Request.QueryString["Name"];

                    PopulateReviews(selectedName);
                }
                gvReviews.RowDataBound += gvReviews_RowDataBound;
                hdnReferringPageUrl.Value = Request.UrlReferrer?.AbsolutePath;
            }
        }

        private void PopulateReviews(string restaurantName)
        {
            DBConnect db = new DBConnect();

            string storedProcedureName = "GetRestaurantReviews";

            SqlCommand cmd = new SqlCommand(storedProcedureName);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RestaurantName", restaurantName);

            DataSet dataSet = db.GetDataSet(cmd);

            if (dataSet.Tables.Count > 0)
            {
                gvReviews.DataSource = dataSet.Tables[0];
                gvReviews.DataBind();
            }
        }

        protected void gvReviews_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                TableCell avgRatingHeader = new TableCell();
                avgRatingHeader.Text = "Average Rating";
                e.Row.Cells.AddAt(8, avgRatingHeader);
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int foodRating = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FoodRating"));
                int serviceRating = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ServiceRating"));
                int atmosphereRating = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AtmosphereRating"));
                int priceRating = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PriceRating"));
                double averageRating = (foodRating + serviceRating + atmosphereRating + priceRating) / 4.0;
                TableCell avgRatingCell = new TableCell();
                avgRatingCell.Text = averageRating.ToString("0.00");
                e.Row.Cells.AddAt(8, avgRatingCell);

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
