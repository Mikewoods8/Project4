using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;
using MyClassLibrary;

namespace Project4
{
    public partial class LogIn : System.Web.UI.Page
    {
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

            User newUser = new User();

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

            newUser.CreateUser();
            lblCreateUserConfirm.Text = "Account Created. Please Log In.";
            lblCreateUserConfirm.Visible = true;
        }
    }
}
