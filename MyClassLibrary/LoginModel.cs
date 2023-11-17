using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Utilities;

namespace MyClassLibrary
{
    public class LoginModel
    {
        public static void Login(string id, string userPassword, Label lblError)
        {
            string userID = id;
            string password = userPassword;

            DBConnect db = new DBConnect();

            using (SqlCommand cmd = new SqlCommand("AuthenticateUser", db.GetConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userID);
                cmd.Parameters.AddWithValue("@password", password);


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        string role = dataSet.Tables[0].Rows[0]["Role"].ToString();

                        if (role == "Representative")
                        {
                            HttpContext.Current.Response.Redirect("Representative.aspx");
                        }
                        else if (role == "Reviewer")
                        {
                            HttpContext.Current.Response.Redirect("Reviewer.aspx");
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Username and/or Password Incorrect";
                    }
                }
            }
        }
    }
}


