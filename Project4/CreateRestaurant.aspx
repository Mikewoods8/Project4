<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateRestaurant.aspx.cs" Inherits="Project4.CreateRestaurant" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Restaurant</title>
    <link rel="stylesheet" type="text/css" href="stylesheets/login.css" />
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="title">
                    <h1>Add a Restaurant</h1>
                </div>
                <div id="restaurant">
                    <asp:Label ID="lblName" runat="server" Text="Restaurant Name: " Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblCategory" runat="server" Text="Category:" Font-Bold="True"></asp:Label>
                    <br />
                    <asp:RadioButtonList ID="radListCategory" runat="server" CssClass="centered-radio-list">
                    </asp:RadioButtonList>
                    <br />
                    <asp:Label ID="lblRepId" runat="server" Text="Representative ID:" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtRepID" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" BackColor="#3399FF" Font-Size="Large" />
                    <asp:Label ID="lblConfirm" runat="server"></asp:Label><br />
                    <asp:Button ID="btnReturnToRestaurants" runat="server" Text="Return to the Restaurant Page" OnClick="btnReturnToRestaurants_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" /><br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
