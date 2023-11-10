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
    public class InputValidation
    {
        public static bool ValidateCreateUser(TextBox userId, TextBox password, TextBox firstName, TextBox lastName, TextBox email, TextBox phone, RadioButton reviewer, RadioButton rep, Label errorId, Label errorPassword, Label errorFirst, Label errorLast, Label errorPhone, Label errorEmail, Label errorRole)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(userId.Text.Trim()))
            {
                errorId.Visible = true;
                errorId.Text = "Please enter a User ID.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(password.Text.Trim()))
            {
                errorPassword.Visible = true;
                errorPassword.Text = "Please enter a password.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(firstName.Text.Trim()))
            {
                errorFirst.Visible = true;
                errorFirst.Text = "Please enter your first name";
                isValid = false;
            }

            if (string.IsNullOrEmpty(lastName.Text.Trim()))
            {
                errorLast.Visible = true;
                errorLast.Text = "Please enter your Last Name.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(email.Text.Trim()))
            {
                errorEmail.Visible = true;
                errorEmail.Text = "Please enter an email.";
                isValid = false;
            }

            if (string.IsNullOrEmpty(phone.Text.Trim()))
            {
                errorPhone.Visible = true;
                errorPhone.Text = "Please enter a phone number.";
                isValid = false;
            }

            if (!reviewer.Checked && !rep.Checked)
            {
                errorRole.Visible = true;
                errorRole.Text = "Please select a role (Reviewer or Representative).";
                isValid = false;
            }

            return isValid;
        }

        public static bool ValidateReservation(TextBox name, Label errorName, Calendar date, Label errorDate)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(name.Text.Trim()))
            {
                errorName.Visible = true;
                errorName.Text = "Please enter a name.";
                isValid = false;
            }

            DateTime minValidDate = new DateTime(2023, 11, 3);

            if (date.SelectedDate < minValidDate)
            {
                errorDate.Visible = true;
                errorDate.Text = "Please select a date.";
                isValid = false;
            }

            return isValid;

        }
    }
}
