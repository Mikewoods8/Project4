<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reviews.aspx.cs" Inherits="Project4.Reviews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Restaurant Reviews</title>
    <link rel="stylesheet" type="text/css" href="stylesheets/SiteVisitor.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    &nbsp;<h1>Restaurant Reviews</h1>
                </div>
                <div id="GridView">
                    <asp:GridView ID="gvReviews" runat="server" OnRowDataBound="gvReviews_RowDataBound">
                    </asp:GridView>
                </div>
                <asp:Button ID="btnReturnToRestaurants" runat="server" Text="Return to the Restaurant Page" OnClick="btnReturnToRestaurants_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
