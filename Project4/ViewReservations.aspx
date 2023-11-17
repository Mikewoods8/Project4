<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewReservations.aspx.cs" Inherits="Project4.ViewReservations" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Reservations</title>
    <link rel="stylesheet" type="text/css" href="stylesheets/SiteVisitor.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="GridView">
                    <asp:GridView ID="gvReservations" runat="server">
                    </asp:GridView>
                </div>
                <asp:Button ID="btnReturnToYourRestaurants" runat="server" Text="Return to Your Restaurants" OnClick="btnReturnToYourRestaurants_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
