<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewReservations.aspx.cs" Inherits="Project4.ViewReservations" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Reservations</title>
    <link rel="stylesheet" type="text/css" href="stylesheets/SiteVisitor.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="GridView">

            <asp:GridView ID="gvReservations" runat="server">
            </asp:GridView>

        </div>
    </form>
</body>
</html>
