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

namespace Project4
{
    public partial class CreateRestaurant : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindCategoryListBox();
            }
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
                radListCategory.DataSource = dataSet.Tables[0];
                radListCategory.DataTextField = "Category";
                radListCategory.DataValueField = "Category";
                radListCategory.DataBind();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string representativeID = txtRepID.Text;
            string category = radListCategory.SelectedItem.Value;

            DBConnect db = new DBConnect();
            SqlConnection connection = db.GetConnection();

            using (SqlCommand cmd = new SqlCommand("CreateRestaurant", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@RepresentativeID", representativeID);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            lblConfirm.Text = "Restaurant succesfully created.";
        }
    }
}