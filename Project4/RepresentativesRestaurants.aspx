<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepresentativesRestaurants.aspx.cs" Inherits="Project4.RepresentativesRestaurants" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Representatives Restaurants</title>
    <link rel="stylesheet" type="text/css" href="stylesheets/SiteVisitor.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="GridView">

            <asp:GridView ID="gvRestaurants" runat="server" AutoGenerateColumns="false" OnRowCommand="gvRestaurants_RowCommand" OnRowUpdating="gvRestaurants_RowUpdating" DataKeyNames="Id">

                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="true" />
                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" />
                    <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="true" />
                    <asp:BoundField DataField="RepresentativeID" HeaderText="Representative ID" ReadOnly="true" />

                    <asp:TemplateField HeaderText="Modify">
                        <ItemTemplate>
                            <asp:Button ID="btnModify" runat="server" Text="Modify" CommandName="Modify" CommandArgument='<%# Container.DataItemIndex %>' />

                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' />
                            <asp:TextBox ID="txtCategory" runat="server" Text='<%# Bind("Category") %>' />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" CommandArgument='<%# Container.DataItemIndex %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reservations">
                        <ItemTemplate>
                            <asp:Button ID="btnReservations" runat="server" Text="View Reservations" CommandName="ViewReservation" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblConfirm" runat="server"></asp:Label><br />
            <asp:Button ID="btnReturnToRestaurants" runat="server" Text="Return to the Restaurant Page" OnClick="btnReturnToRestaurants_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
        </div>
    </form>
</body>
</html>
