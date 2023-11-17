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
                    <asp:GridView ID="gvReservations" runat="server" AutoGenerateColumns="false" OnRowCommand="gvReservations_RowCommand" OnRowDeleting="gvReservations_RowDeleting" OnRowUpdating="gvReservations_RowUpdating" DataKeyNames="Id">
                        <Columns>
                            <asp:TemplateField HeaderText="Modify">
                                <ItemTemplate>
                                    <asp:Button ID="btnModify" runat="server" Text="Modify" CommandName="Modify" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' />
                                    <asp:TextBox ID="txtRestaurant" runat="server" Text='<%# Bind("Restaurant") %>' />
                                    <asp:TextBox ID="txtDate" runat="server" Text='<%# Bind("Date") %>' />
                                    <asp:TextBox ID="txtTime" runat="server" Text='<%# Bind("Time") %>' />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" CommandArgument='<%# Container.DataItemIndex %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblConfirm" runat="server"></asp:Label>
                </div>
                <asp:Button ID="btnReturnToYourRestaurants" runat="server" Text="Return to Your Restaurants" OnClick="btnReturnToYourRestaurants_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
