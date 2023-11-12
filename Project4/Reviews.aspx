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
        <asp:Button ID="btnReturnToRestaurants" runat="server" Text="Return to the Restaurant Page" OnClick="btnReturnToRestaurants_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
    </form>
</body>
</html>
<%-- Gridview to display reviews w/ avg score for each --%>
<%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="false" DataKeyNames="Review Number" OnRowEditing="gvReviews_RowEditing" OnRowUpdating="gvReviews_RowUpdating" OnRowCancelingEdit="gvReviews_RowCancelingEdit" OnRowDeleting="gvReviews_RowDeleting" OnRowDataBound="gvReviews_RowDataBound">
    <Columns>
        <asp:BoundField DataField="Review Number" HeaderText="Review Number" />
        <asp:BoundField DataField="ReviewerID" HeaderText="Reviewer ID" />
        <asp:BoundField DataField="RestaurantID" HeaderText="Restaurant ID" />
        <asp:BoundField DataField="Food Quality" HeaderText="Food Quality" />
        <asp:BoundField DataField="Service" HeaderText="Service" />
        <asp:BoundField DataField="Atmosphere" HeaderText="Atmosphere" />
        <asp:BoundField DataField="Price Level" HeaderText="Price Level" />
        <asp:TemplateField HeaderText="Overall Rating">
            <ItemTemplate>
                <asp:Label ID="lblRating" runat="server" Text='<%# GetAverage(Eval("Food Quality"), Eval("Service"), Eval("Atmosphere"), Eval("Price Level")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Modifications">
            <ItemTemplate>
                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
            </EditItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>--%>

<%-- Gridview the shows avg for all reviews for a given restaurant --%>
<%--<asp:GridView ID="gvAverages" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="RestaurantID" HeaderText="Restaurant ID" />
        <asp:TemplateField HeaderText="Average Food Quality">
            <ItemTemplate>
                <asp:Label ID="lblAvgFoodQuality" runat="server" Text='<%# Eval("AvgFoodQuality") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Average Service">
            <ItemTemplate>
                <asp:Label ID="lblAvgService" runat="server" Text='<%# Eval("AvgService") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Average Atmosphere">
            <ItemTemplate>
                <asp:Label ID="lblAvgAtmosphere" runat="server" Text='<%# Eval("AvgAtmosphere") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Average Price Level">
            <ItemTemplate>
                <asp:Label ID="lblAvgPriceLevel" runat="server" Text='<%# Eval("AvgPriceLevel") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Overall Average">
            <ItemTemplate>
                <asp:Label ID="lblOverallAverage" runat="server" Text='<%# CalculateOverallAverage(Eval("AvgFoodQuality"), Eval("AvgService"), Eval("AvgAtmosphere"), Eval("AvgPriceLevel")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>