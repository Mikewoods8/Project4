<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="Project4.LogIn" %>

<!DOCTYPE html>
<%-- To Do:
    Averages (Done)
    Finish Web API DB calls (Working)
    Make responsive(script manager ==> update panel ==> content templeate ==> put all controls inside)
    Manage session via web API 
    Modify Reservations
    Add Images
--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Restaurant Review System</title>
    <link rel="stylesheet" type="text/css" href="stylesheets/login.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="title">
            <h1>Restaurant Review System</h1>
        </div>
        <div id="btnLogin">
            <asp:Button ID="btnGuest" runat="server" Text="Cotinue as a Guest" OnClick="btnGuest_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
            <asp:Button ID="btnLogIn" runat="server" Text="Log In" OnClick="btnLogIn_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
            <asp:Button ID="btnCreateAccount" runat="server" Text="Create an Account" OnClick="btnCreateAccount_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
        </div>
        <div id="userLogin">
            <asp:Label ID="lblMessage" runat="server" Text="Please log into your account" Visible="False" ForeColor="#800000"></asp:Label>
            <asp:Label ID="lblCreateAccountMessage" runat="server" Text="Please enter account information" Visible="False" ForeColor="#800000"></asp:Label>
            <br />
            <asp:Label ID="lblID" runat="server" Text="User ID:" Visible="False"></asp:Label>
            <asp:TextBox ID="txtUserID" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblErrorId" runat="server" ForeColor="#800000" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="lblPassword" runat="server" Text="Password:" Visible="False"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblErrorPassword" runat="server" ForeColor="#800000" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="lblFirstName" runat="server" Text="First Name:" Visible="False"></asp:Label>
            <asp:TextBox ID="txtFirstName" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblErrorFirst" runat="server" ForeColor="#800000" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" Visible="False"></asp:Label>
            <asp:TextBox ID="txtLastName" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblErrorLast" runat="server" ForeColor="#800000" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="lblEmail" runat="server" Text="Email:" Visible="False"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblErrorEmail" runat="server" ForeColor="#800000" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="lblPhone" runat="server" Text="Phone:" Visible="False"></asp:Label>
            <asp:TextBox ID="txtPhone" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblErrorPhone" runat="server" ForeColor="#800000" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="lblAccountType" runat="server" Text="Account Type:" Visible="False"></asp:Label>
            <asp:RadioButton ID="radReviewer" runat="server" GroupName="AccountType" Text="Reviewer" Visible="False" />
            <asp:RadioButton ID="radRepresentative" runat="server" GroupName="AccountType" Text="Representative" Visible="False" />
            <asp:Label ID="lblErrorRole" runat="server" ForeColor="#800000" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="False" OnClick="btnSubmit_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
            <asp:Button ID="btnSubmitAccount" runat="server" OnClick="btnSubmitAccount_Click" Text="Create Account" Visible="False" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
            <asp:Label ID="lblErrorLogIn" runat="server" ForeColor="#800000" Visible="False"></asp:Label>
            <asp:Label ID="lblCreateUserConfirm" runat="server" ForeColor="Maroon"></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
