<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPersonalReviews.aspx.cs" Inherits="Project4.ViewPersonalReviews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Reviews</title>
    <link rel="stylesheet" type="text/css" href="stylesheets/SiteVisitor.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="GridView">
                    <asp:Button ID="btnReturnToRestaurants" runat="server" Text="Return to the Restaurant Page" OnClick="btnReturnToRestaurants_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" /><br />
                    <asp:GridView ID="gvPersonalReviews" runat="server" AutoGenerateColumns="False" OnRowCommand="gvReviews_RowCommand" OnRowDeleting="gvReviews_RowDeleting" OnRowUpdating="gvPersonalReviews_RowUpdating" OnRowDataBound="gvPersonalReviews_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="UserId" HeaderText="UserId" ReadOnly="true" />
                            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" />
                            <asp:BoundField DataField="Restaurant" HeaderText="Restaurant" ReadOnly="true" />
                            <asp:BoundField DataField="FoodRating" HeaderText="Food Rating" ReadOnly="true" />
                            <asp:BoundField DataField="ServiceRating" HeaderText="Service Rating" ReadOnly="true" />
                            <asp:BoundField DataField="AtmosphereRating" HeaderText="Atmosphere Rating" ReadOnly="true" />
                            <asp:BoundField DataField="PriceRating" HeaderText="Price Rating" ReadOnly="true" />
                            <asp:BoundField DataField="Comments" HeaderText="Comments" ReadOnly="true" />
                            <asp:TemplateField HeaderText="Modify">
                                <ItemTemplate>
                                    <asp:Button ID="btnModify" runat="server" Text="Modify" CommandName="Modify" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFoodRating" runat="server" Text='<%# Bind("FoodRating") %>' />
                                    <asp:TextBox ID="txtServiceRating" runat="server" Text='<%# Bind("ServiceRating") %>' />
                                    <asp:TextBox ID="txtAtmosphereRating" runat="server" Text='<%# Bind("AtmosphereRating") %>' />
                                    <asp:TextBox ID="txtPriceRating" runat="server" Text='<%# Bind("PriceRating") %>' />
                                    <asp:TextBox ID="txtComments" runat="server" Text='<%# Bind("Comments") %>' />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" CommandArgument='<%# Container.DataItemIndex %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btnReturnToRestaurants" runat="server" Text="Return to the Restaurant Page" OnClick="btnReturnToRestaurants_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" /><br />
                    <asp:Label ID="lblConfirm" runat="server"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
