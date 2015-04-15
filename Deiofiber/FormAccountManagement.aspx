<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormAccountManagement.aspx.cs" Inherits="Deiofiber.FormAccountManagement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>QUẢN LÝ TÀI KHOẢN</h2>
    <table class="table table-striped table-hover ">
        <tbody>
            <tr>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></td>
                <td>
                    <asp:Button ID="btnNew" runat="server" Text="Tạo tài khoản" CssClass="btn btn-primary" OnClick="btnNew_Click" /></td>
            </tr>
        </tbody>
    </table>
    <div>
        <asp:Repeater ID="rptAccount" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-hover">
                    <thead>
                        <tr class="success">
                            <th>#</th>
                            <th>Tên tài khoản*</th>
                            <th>Tên người dùng*</th>
                            <th>Cửa hàng</th>
                            <th>Chức năng</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Container.ItemIndex + 1 %></td>
                    <td><%# Eval("ACC") %></td>
                    <td><%# Eval("NAME") %></td>
                    <td><%# GetStoreName(Convert.ToInt32(Eval("STORE_ID"))) %></td>
                    <td>
                        <asp:HyperLink ID="hplAccountUpdate" runat="server" Text="Cập nhật" NavigateUrl='<%# Eval("ID","FormAccountUpdate.aspx?ID={0}") %>'></asp:HyperLink></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                    </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:DropDownList ID="ddlPager" runat="server" CssClass="form-control dropdown-pager-width" OnSelectedIndexChanged="ddlPager_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <%--<ul class="pagination pagination-sm">
          <li class="disabled"><a href="#">«</a></li>
          <li class="active"><a href="#">1</a></li>
          <li><a href="#">2</a></li>
          <li><a href="#">3</a></li>
          <li><a href="#">4</a></li>
          <li><a href="#">5</a></li>
          <li><a href="#">»</a></li>
        </ul>--%>
    </div>
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
