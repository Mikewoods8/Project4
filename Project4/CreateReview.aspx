<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateReview.aspx.cs" Inherits="Project4.CreateReview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Review</title>
    <link rel="stylesheet" type="text/css" href="stylesheets/CreateReservation.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="title">
                    <h1>Please Write your Review</h1>
                </div>
                <div id="review">

                    <asp:Label ID="lblUserId" runat="server" Text="User ID:" Font-Bold="True" Font-Size="Large"></asp:Label>
                    <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblName" runat="server" Text="Name:" Font-Bold="True" Font-Size="Large"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblRestaurant" runat="server" Text="Restaurant: " Font-Bold="True" Font-Size="Large"></asp:Label>
                    <asp:TextBox ID="txtRestaurant" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblFoodRating" runat="server" Text="Food Rating:" Font-Bold="True" Font-Size="Large"></asp:Label>&nbsp;<asp:RadioButton ID="radFood1" runat="server" GroupName="radFood" Text="1" Value="1" />
                    <asp:RadioButton ID="radFood2" runat="server" GroupName="radFood" Text="2" Value="2" />
                    <asp:RadioButton ID="radFood3" runat="server" GroupName="radFood" Text="3" Value="3" />
                    <asp:RadioButton ID="radFood4" runat="server" GroupName="radFood" Text="4" Value="4" />
                    <asp:RadioButton ID="radFood5" runat="server" GroupName="radFood" Text="5" Value="5" />
                    <br />
                    <asp:Label ID="lblService" runat="server" Text="Service Rating:" Font-Bold="True" Font-Size="Large"></asp:Label>
                    <asp:RadioButton ID="radService1" runat="server" GroupName="radService" Text="1" Value="1" />
                    <asp:RadioButton ID="radService2" runat="server" GroupName="radService" Text="2" Value="2" />
                    <asp:RadioButton ID="radService3" runat="server" GroupName="radService" Text="3" Value="3" />
                    <asp:RadioButton ID="radService4" runat="server" GroupName="radService" Text="4" Value="4" />
                    <asp:RadioButton ID="radService5" runat="server" GroupName="radService" Text="5" Value="5" />
                    <br />
                    <asp:Label ID="lblAtmosphereRating" runat="server" Text="Atmosphere Rating:" Font-Bold="True" Font-Size="Large"></asp:Label>
                    <asp:RadioButton ID="radAtmosphere1" runat="server" GroupName="radAtmosphere" Text="1" Value="1" />
                    <asp:RadioButton ID="radAtmosphere2" runat="server" GroupName="radAtmosphere" Text="2" Value="2" />
                    <asp:RadioButton ID="radAtmosphere3" runat="server" GroupName="radAtmosphere" Text="3" Value="3" />
                    <asp:RadioButton ID="radAtmosphere4" runat="server" GroupName="radAtmosphere" Text="4" Value="4" />
                    <asp:RadioButton ID="radAtmosphere5" runat="server" GroupName="radAtmosphere" Text="5" Value="5" />
                    <br />
                    <asp:Label ID="lblPriceRating" runat="server" Text="Price Rating:" Font-Bold="True" Font-Size="Large"></asp:Label>
                    <asp:RadioButton ID="radPrice1" runat="server" GroupName="radPrice" Text="1" Value="1" />
                    <asp:RadioButton ID="radPrice2" runat="server" GroupName="radPrice" Text="2" Value="2" />
                    <asp:RadioButton ID="radPrice3" runat="server" GroupName="radPrice" Text="3" Value="3" />
                    <asp:RadioButton ID="radPrice4" runat="server" GroupName="radPrice" Text="4" Value="4" />
                    <asp:RadioButton ID="radPrice5" runat="server" GroupName="radPrice" Text="5" Value="5" />
                    <br />
                    <asp:Label ID="lblComments" runat="server" Text="Comments:" Font-Bold="True" Font-Size="Large"></asp:Label>
                    <asp:TextBox ID="txtComments" runat="server" Width="588px"></asp:TextBox>

                    <br />
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    <asp:Label ID="lblConfirm" runat="server"></asp:Label><br />
                    <asp:Button ID="btnReturnToRestaurants" runat="server" Text="Return to the Restaurant Page" OnClick="btnReturnToRestaurants_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
