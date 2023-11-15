using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Script.Serialization;
using System.IO;
using System.Net;
using System.Data;
using Utilities;
using MyClassLibrary;

namespace Project4
{
    public partial class LogIn : System.Web.UI.Page
    {
        //Get this from local browser to test, then use actual URL when publishing. 
        string webApiUrl = "https://localhost:44300/api/";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            lblCreateAccountMessage.Visible = false;
            lblID.Visible = false;
            txtUserID.Visible = false;
            txtUserID.Text = "";
            lblPassword.Visible = false;
            txtPassword.Visible = false;
            txtPassword.Text = "";
            lblFirstName.Visible = false;
            txtFirstName.Visible = false;
            lblLastName.Visible = false;
            txtLastName.Visible = false;
            lblEmail.Visible = false;
            txtEmail.Visible = false;
            lblPhone.Visible = false;
            txtPhone.Visible = false;
            lblAccountType.Visible = false;
            radRepresentative.Visible = false;
            radReviewer.Visible = false;
            btnSubmitAccount.Visible = false;
            lblCreateUserConfirm.Visible = false;
            lblErrorId.Visible = false;
            lblErrorPassword.Visible = false;
            lblErrorFirst.Visible = false;
            lblErrorLast.Visible = false;
            lblErrorEmail.Visible = false;
            lblErrorPhone.Visible = false;
            lblErrorRole.Visible = false;
            lblErrorLogIn.Visible = false;


            lblMessage.Visible = true;
            lblID.Visible = true;
            txtUserID.Visible = true;
            lblPassword.Visible = true;
            txtPassword.Visible = true;
            btnSubmit.Visible = true;
        }

        protected void btnGuest_Click(object sender, EventArgs e)
        {
            Response.Redirect("SiteVisitor.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userID = txtUserID.Text;
            string password = txtPassword.Text;

            Session["UserID"] = userID;
            AccountConfirmation.Login(userID, password, lblErrorLogIn);
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            btnSubmit.Visible = false;
            lblErrorLogIn.Visible = false;


            lblCreateAccountMessage.Visible = true;
            lblID.Visible = true;
            txtUserID.Visible = true;
            lblPassword.Visible = true;
            txtPassword.Visible = true;
            lblFirstName.Visible = true;
            txtFirstName.Visible = true;
            lblLastName.Visible = true;
            txtLastName.Visible = true;
            lblEmail.Visible = true;
            txtEmail.Visible = true;
            lblPhone.Visible = true;
            txtPhone.Visible = true;
            lblAccountType.Visible = true;
            radRepresentative.Visible = true;
            radReviewer.Visible = true;
            btnSubmitAccount.Visible = true;
        }

        protected void btnSubmitAccount_Click(object sender, EventArgs e)
        {
            lblErrorId.Visible = false;
            lblErrorPassword.Visible = false;
            lblErrorFirst.Visible = false;
            lblErrorLast.Visible = false;
            lblErrorEmail.Visible = false;
            lblErrorPhone.Visible = false;
            lblErrorRole.Visible = false;

            if (!InputValidation.ValidateCreateUser(txtUserID, txtPassword, txtFirstName, txtLastName, txtEmail, txtPhone, radReviewer, radRepresentative, lblErrorId, lblErrorPassword, lblErrorFirst, lblErrorLast, lblErrorPhone, lblErrorEmail, lblErrorRole))
                return;

            UserModel newUser = new UserModel();

            newUser.UserId = txtUserID.Text;
            newUser.Password = txtPassword.Text;
            newUser.FirstName = txtFirstName.Text;
            newUser.LastName = txtLastName.Text;
            newUser.Email = txtEmail.Text;
            newUser.Phone = txtPhone.Text;

            if (radReviewer.Checked)
            {
                newUser.Role = "Reviewer";
            }
            else if (radRepresentative.Checked)
            {
                newUser.Role = "Representative";
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonUser = js.Serialize(newUser);

            try
            {
                WebRequest request = WebRequest.Create(webApiUrl + "/UserService/AddUser/");
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
                    lblCreateUserConfirm.Text = "Account Created. Please return to the login page.";
                else
                    lblCreateUserConfirm.Text = "A problem occured. Account not created.";

            }
            catch (Exception ex)
            {
                lblCreateUserConfirm.Text = "Error: " + ex.Message;

            }
        }
    }
}
