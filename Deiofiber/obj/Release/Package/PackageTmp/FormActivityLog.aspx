<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormActivityLog.aspx.cs" Inherits="Deiofiber.FormActivityLog" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>NHẬT KÝ HOẠT ĐỘNG</h2>
    <table class="table table-striped table-hover ">
        <tbody>
            <tr>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></td>
            </tr>
        </tbody>
    </table>
    <asp:Repeater ID="rptActivity" runat="server">
        <HeaderTemplate>
            <table class="table table-striped table-hover ">
                <thead>
                    <tr class="success">
                        <th>#</th>
                        <th>Hoạt động*</th>
                        <th>Tài khoản*</th>
                        <th>Cửa hàng</th>
                        <th>Thông tin hoạt động</th>
                        <th>Thời gian*</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Container.ItemIndex + 1 %></td>
                <td><%# Eval("LOG_ACTION") %></td>
                <td><%# Eval("ACCOUNT") %></td>
                <td><%# Eval("STORE") %></td>
                <td><%# Eval("LOG_MSG") %></td>
                <td><%# Eval("LOG_DATE") %></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
           </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:DropDownList ID="ddlPager" runat="server" CssClass="form-control dropdown-pager-width" OnSelectedIndexChanged="ddlPager_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <script>
        $(function () {
            $('#<%=txtSearch.ClientID %>').keypress(function (e) {
                if (e.which == 13) {
                    $('#<%=btnSearch.ClientID %>').click();
                            return false;
                        }
            });
        });
    </script>
</asp:Content>
