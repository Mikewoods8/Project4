<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reviews.aspx.cs" Inherits="Project4.Reviews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Restaurant Reviews</title>
    <link rel="stylesheet" type="text/css" href="stylesheets/SiteVisitor.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            &nbsp;<h1>Restaurant Reviews</h1>
        </div>
        <div id="GridView">
            <asp:GridView ID="gvReviews" runat="server">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
