<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormLogin.aspx.cs" Inherits="Deiofiber.FormLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" type="text/css" href="\App_Themes\Theme1\css\stylesheet.css" />
    <link rel="stylesheet" type="text/css" href="\App_Themes\Theme1\css\bootstrap.min.css" />
</head>
<body>
    <form id="formlogin" runat="server">
        <table class="table table-striped table-hover" style="width: 40%; margin-left: 30%;">
            <tbody>
                <tr>
                    <td colspan="2" class="text-center text-info"><strong>Đăng nhập hệ thống</strong></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-center text-warning"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="text-right text-info">
                        <strong>Tài khoản</strong></td>
                    <td>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="text-right text-info">
                        <strong>Mật khẩu</strong></td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:CheckBox ID="chkRememberMe" runat="server" Text="&nbsp;&nbsp;Lưu đăng nhập" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" OnClick="btnLogin_Click" CssClass="btn btn-primary" /></td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
