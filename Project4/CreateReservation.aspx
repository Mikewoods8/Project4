<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateReservation.aspx.cs" Inherits="Project3.CreateReservation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Reservation</title>
    <link rel="stylesheet" type="text/css" href="stylesheets/CreateReservation.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="title">
            <h1>Please Specify Your Reservation</h1>
        </div>
        <div id="reservation">
            &nbsp;<br />
            <asp:Label ID="lblName" runat="server" Text="Name:" Font-Bold="True" Font-Size="Large"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <asp:Label ID="lblErrorName" runat="server" ForeColor="Maroon" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="lblRestaurant" runat="server" Text="Restaurant:" Font-Bold="True" Font-Size="Large"></asp:Label>
            <asp:TextBox ID="txtRestaurant" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblDate" runat="server" Text="Date:" Font-Bold="True" Font-Size="Large"></asp:Label>
            <asp:Label ID="lblErrorDate" runat="server" ForeColor="Maroon" Visible="False"></asp:Label>
            <asp:Calendar ID="CalendarReserve" runat="server" BackColor="White" BorderColor="Black" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" Width="400px" DayNameFormat="Shortest" TitleFormat="Month">
                <DayHeaderStyle Font-Bold="True" Font-Size="7pt" BackColor="#CCCCCC" ForeColor="#333333" Height="10pt" />
                <DayStyle Width="14%" />
                <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                <TodayDayStyle BackColor="#CCCC99" />
            </asp:Calendar>
            <asp:Label ID="lblTime" runat="server" Text="Time:" Font-Bold="True" Font-Size="Large"></asp:Label>
            &nbsp;<asp:DropDownList ID="ddlTime" runat="server" Height="16px" Width="290px">
                <asp:ListItem>4:00 pm</asp:ListItem>
                <asp:ListItem>4:30 pm</asp:ListItem>
                <asp:ListItem>5:00 pm</asp:ListItem>
                <asp:ListItem>5:30 pm</asp:ListItem>
                <asp:ListItem>6:00 pm</asp:ListItem>
                <asp:ListItem>6:30 pm</asp:ListItem>
                <asp:ListItem>7:00 pm</asp:ListItem>
                <asp:ListItem>7:30 pm</asp:ListItem>
                <asp:ListItem>8:00 pm</asp:ListItem>
                <asp:ListItem>8:30 pm</asp:ListItem>
                <asp:ListItem>9:00 pm</asp:ListItem>
                <asp:ListItem>9:30 pm</asp:ListItem>
                <asp:ListItem>10:00 pm</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Button ID="btnCreateReservation" runat="server" Text="Create Reservation" OnClick="btnCreateReservation_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" />
            <asp:Button ID="btnReturnToRestaurants" runat="server" Text="Return to the Restaurant Page" OnClick="btnReturnToRestaurants_Click" BackColor="#3399FF" BorderStyle="Solid" Font-Size="Medium" /><br />
            <asp:Label ID="lblConfirm" runat="server"></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
