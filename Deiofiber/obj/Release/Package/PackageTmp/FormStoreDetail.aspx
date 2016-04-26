<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormStoreDetail.aspx.cs" Inherits="Deiofiber.FormStoreDetail" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4 class="text-center">Thông tin cửa hàng</h4>
    <table class="table table-striped table-hover" style="width: 60%; margin-left: 20%;">
        <tbody>
            <tr>
                <td colspan="2" class="text-center">
                    <strong>
                        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-center text-warning"></asp:Label></strong>
                </td>
            </tr>
            <tr>
                <td class="text-right">Tên cửa hàng</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Địa chỉ</td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Tỉnh/Thành phố</td>
                <td>
                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="text-right">Số điện thoại</td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <%--            <tr>
                <td class="text-right">Số Fax</td>
                <td><asp:TextBox ID="txtFax" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>--%>
            <tr>
                <td class="text-right">Vốn ban đầu</td>
                <td>
                    <asp:TextBox ID="txtStartCapital" runat="server" CssClass="form-control input-sm text-right">0</asp:TextBox></td>
            </tr>
            <!--
           <tr>
                <td class="text-right">Vốn hiện tại</td>
                <td>
                    <asp:TextBox ID="txtCurrentCapital" runat="server" CssClass="form-control input-sm text-right">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Tổng giá trị hợp đồng cho thuê xe</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input-sm text-right">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Tổng giá trị hợp đồng cho thuê thiết bị</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-sm text-right">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Tổng giá trị hợp đồng khác</td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control input-sm text-right">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Tổng giá trị tất cả các hợp đồng cho thuê</td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control input-sm text-right">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Ngày áp dụng</td>
                <td>
                    <asp:TextBox ClientIDMode="Static" ID="txtApplyDate" runat="server" CssClass="form-control input-sm text-right"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right">Doanh thu (trước ngày áp dụng)</td>
                <td>
                    <asp:TextBox ID="txtTotalRevenueBefore" runat="server" CssClass="form-control input-sm text-right">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Tổng chi phí (trước ngày áp dụng)</td>
                <td>
                    <asp:TextBox ID="txtTotalCostBefore" runat="server" CssClass="form-control input-sm text-right">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Tổng đầu tư (trước ngày áp dụng)</td>
                <td>
                    <asp:TextBox ID="txtTotalInvesmentBefore" runat="server" CssClass="form-control input-sm text-right">0</asp:TextBox></td>
            </tr>-->
<%--            <tr>
                <td class="text-right">Ngày đăng ký</td>
                <td>
                    <asp:TextBox ClientIDMode="Static" ID="txtRegisterDate" runat="server" CssClass="form-control input-sm text-right"></asp:TextBox>
                </td>
            </tr>--%>
            <%--             <tr>
                <td class="text-right">Mức phí (1-100)</td>
                <td><asp:TextBox ID="txtFeeRate" runat="server" CssClass="form-control input-sm text-right">0</asp:TextBox></td>
            </tr>--%>
            <tr>
                <td class="text-right">Kích hoạt</td>
                <td>
                    <asp:RadioButton ID="rdbActive" runat="server" Text="Sử dụng" GroupName="activegroup" Checked="True" />
                    -
                    <asp:RadioButton ID="rdbDeActive" runat="server" Text="Ngừng sử dụng" GroupName="activegroup" /></td>
            </tr>
            <tr>
                <td class="text-right">Ghi chú</td>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Lưu & thoát" CssClass="btn btn-primary" OnClick="btnSave_Click" />&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Thoát" CssClass="btn btn-primary" OnClick="btnCancel_Click" /></td>
            </tr>
        </tbody>
    </table>
    <script>
        $(function () {
            $('#<%=txtStartCapital.ClientID %>').priceFormat({ prefix: '', suffix: '', centsLimit: 0 });
<%--            $("#txtApplyDate").datepicker();
            $("#txtRegisterDate").datepicker();
            $('input').keypress(function (e) {
                if (e.which == 13) {
                    $('#<%=btnSave.ClientID %>').click();
                    return false;
                }
            });--%>
        });
    </script>
</asp:Content>
