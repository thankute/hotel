<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="hotel.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            width: 100%;
            height: 100vh;
            overflow: hidden;
        }

        .center {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .login {
            border: 1px solid black;
            border-radius: 10px;
            padding: 10px 24px;
        }

        table {
            font-size: 20px;
        }

        tr {
            min-width: 300px;
            display: flex;
            flex-direction: column;
            margin-bottom: 12px;
        }

        input {
            width: 300px;
            padding: 6px 8px;
            border: 1px solid black;
            border-radius: 4px;
        }

        .btn {
            width: 100px;
            padding: 8px 12px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            color: white;
            background-color: dimgray;
        }
        .error {
            margin: 0 0 10px 0;
        }
    </style>
</head>
<body class="center">
    <form id="form1" runat="server">
        <div class="login">
            <h1 style="text-align: center">Đăng nhập</h1>
            <table>
                <tr>
                    <td>Tài khoản:</td>
                    <td>
                        <asp:TextBox ID="txtUserName" type="text" runat="server" /></td>
                </tr>
                <tr>
                    <td>Mật khẩu:</td>
                    <td>
                        <asp:TextBox ID="txtUserPass" type="password" runat="server" /></td>
                    <td></td>
                </tr>
            </table>
            <asp:Label ID="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" CssClass="error" />
            <div class="center">
                <asp:Button type="button" Value="Login" runat="server" ID="cmdLogin" OnClick="onSubmit" Text="Đăng nhập" CssClass="btn"/>
            </div>
            <p></p>
        </div>

    </form>
</body>
</html>
