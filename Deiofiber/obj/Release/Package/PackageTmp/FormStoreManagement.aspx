<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormStoreManagement.aspx.cs" Inherits="Deiofiber.FormShopManagement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>QUẢN LÝ CỬA HÀNG</h2>
    <table class="table table-striped table-hover ">
        <tbody>
            <tr>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></td>
                <td>
                    <asp:Button ID="btnNew" runat="server" Text="Tạo cửa hàng" CssClass="btn btn-primary" OnClick="btnNew_Click" /></td>
            </tr>
        </tbody>
    </table>
    <asp:Repeater ID="rptStore" runat="server" OnItemCommand="rptStore_ItemCommand">
        <HeaderTemplate>
            <table class="table table-striped table-hover ">
                <thead>
                    <tr class="success">
                        <th>#</th>
                        <th class="text-center">Tên cửa hàng*</th>
                        <th class="text-center">Thành phố</th>
                        <th class="text-center">Số điện thoại*</th>
                        <th class="text-center">Ghi chú</th>
                        <th class="text-center">Trạng thái</th>
                        <th class="text-center">Chức năng</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Container.ItemIndex + 1 %></td>
                <td><%# Eval("NAME") %></td>
                <td><%# Eval("CITY") %></td>
                <td><%# Eval("PHONE") %></td>
                <td><%# Eval("NOTE") %></td>
                <td class="text-center"><%# Convert.ToBoolean(Eval("ACTIVE")) ? "Đang hoạt động" : "Ngừng hoạt động" %></td>
                <td class="text-center">
                    <asp:HiddenField ID="hdfStore_id" runat="server" Value='<%# Eval("ID") %>' />
                    <asp:HyperLink ID="hplStoreDetail" runat="server" Text="Cập nhật" NavigateUrl='<%# Eval("ID","FormStoreDetail.aspx?ID={0}") %>'></asp:HyperLink></td>
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
