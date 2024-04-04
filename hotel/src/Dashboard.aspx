<%@ Page Title="" Language="C#" MasterPageFile="~/CMSLayout.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="hotel.src.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard">
        <div class="flex dashboard-item between" style="background-color: coral">
            <h2>Tổng người dùng</h2>
            <asp:Label ID="txtUser" Text="0" runat="server" />
        </div>
        <div class="flex dashboard-item between" style="background-color: lightgreen">
            <h2>Tổng số phòng</h2>
            <asp:Label ID="txtRoom" Text="0" runat="server" />
        </div>
        <div class="flex dashboard-item between" style="background-color: darksalmon">
            <h2>Tổng số đơn hàng</h2>
            <asp:Label ID="txtBooking" Text="0" runat="server" />
        </div>
    </div>
</asp:Content>
