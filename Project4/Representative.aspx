<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Representative.aspx.cs" Inherits="Project3.Representative" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Representative</title>
    <link rel="stylesheet" type="text/css" href="stylesheets/SiteVisitor.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="title">
            <asp:Button ID="btnLogOut" runat="server" Text="Log Out" BackColor="#FF3300" BorderStyle="Solid" Font-Size="Large" OnClick="btnLogOut_Click" />
            <asp:Button ID="btnViewPersonalReviews" runat="server" BackColor="#FF3300" BorderStyle="Solid" Font-Size="Large" OnClick="btnViewPersonalReviews_Click" Text="Your Reviews" />
            <asp:Button ID="btnAddRestaurant" runat="server" BackColor="#FF3300" BorderStyle="Solid" Font-Size="Large" Text="Add Restaurant" OnClick="btnAddRestaurant_Click" />
            <asp:Button ID="btnRestaurants" runat="server" BackColor="#FF3300" BorderStyle="Solid" Font-Size="Large" OnClick="btnRestaurants_Click" Text="Your Restaurants" />
            <h1>Welcome, You are Viewing as a Restaurant Representative</h1>
        </div>
        <div id="SearchFunction">
            <asp:Label ID="lblSearch" runat="server" Text="Filter Search:" Font-Size="Larger"></asp:Label>
            <asp:CheckBoxList ID="chkListCategory" runat="server" Font-Bold="True" Font-Size="Large">
            </asp:CheckBoxList>
            <br />
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
        </div>
        <div id="GridView">
            <asp:GridView ID="gvRestaurants" runat="server" AutoGenerateColumns="False" OnRowCommand="gvRestaurants_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" />
                    <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="true" />
                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <asp:Button ID="btnDetails" runat="server" Text="View Details" CommandName="ViewDetails" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reservations">
                        <ItemTemplate>
                            <asp:Button ID="btnReservation" runat="server" Text="Make Reservation" CommandName="MakeReservation" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Reviews">
                        <ItemTemplate>
                            <asp:Button ID="btnReviews" runat="server" Text="View Reviews" CommandName="ViewReview" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Write a Review">
                        <ItemTemplate>
                            <asp:Button ID="btnWriteReviews" runat="server" Text="Write a Review" CommandName="WriteReview" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
