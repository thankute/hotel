<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="hotel.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h3>
            Logon Page
        </h3>
        <table>
            <tr>
                <td>Email:</td>
                <td>
                    <asp:TextBox ID="txtUserName" type="text" runat="server" /></td>
                <td>
                  
            </tr>
            <tr>
                <td>Password:</td>
                <td>
                    <asp:TextBox ID="txtUserPass" type="password" runat="server" /></td>
                <td>
                </td>
            </tr>
        </table>
        <asp:Button type="submit" Value="Login" runat="server" ID="cmdLogin" OnClick="onSubmit" Text="Login"/>
        <p></p>
        <asp:Label ID="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
    </form>
</body>
</html>
