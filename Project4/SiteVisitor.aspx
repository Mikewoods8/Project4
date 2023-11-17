<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteVisitor.aspx.cs" Inherits="Project4.SiteVisitor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Site Visitor</title>
    <link rel="stylesheet" type="text/css" href="stylesheets/SiteVisitor.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="title">
                    <asp:Button ID="btnHome" runat="server" Text="Return Home" OnClick="btnHome_Click" BackColor="#FF3300" BorderStyle="Solid" Font-Size="Medium" />
                    <h1>Welcome, You are Viewing as a Site Visitor</h1>
                </div>
                <div id="SearchFunction">
                    <asp:Label ID="lblSearch" runat="server" Text="Filter Search:" Font-Size="Larger"></asp:Label>
                    <asp:CheckBoxList ID="chkListCategory" runat="server" Font-Bold="True" Font-Size="Large">
                        <asp:ListItem>American</asp:ListItem>
                        <asp:ListItem>Italian</asp:ListItem>
                        <asp:ListItem>Chinese</asp:ListItem>
                        <asp:ListItem>Barbecue</asp:ListItem>
                        <asp:ListItem>Mexican</asp:ListItem>
                    </asp:CheckBoxList>
                    <br />
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
                </div>
                <div id="GridView">
                    <asp:GridView ID="gvRestaurants" runat="server" AutoGenerateColumns="False" OnRowCommand="gvRestaurants_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" />
                            <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="true" />
                            <asp:ImageField DataImageUrlField="Image" HeaderText="Image" ControlStyle-Width="50" ControlStyle-Height="50" />
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
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
