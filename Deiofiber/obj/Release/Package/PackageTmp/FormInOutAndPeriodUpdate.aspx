<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormInOutAndPeriodUpdate.aspx.cs" Inherits="Deiofiber.FormInOutAndPeriodUpdate" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center">Cập nhật trả phí</h2>
    <table class="table table-striped table-hover" style="width: 50%; margin-left: 25%;">
        <tbody>
            <tr>
                <td colspan="2" class="text-center"><strong>Chi tiết khoản trả phí ngày <%= InOutDate.ToString("dd/MM/yyyy") %></strong></td>
            </tr>
            <tr>
                <td class="text-right">Loại chi phí</td>
                <td>
                    <asp:DropDownList ID="ddInOutType" Enabled="False" runat="server" CssClass="form-control"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="text-right">Cửa hàng</td>
                <td>
                    <asp:TextBox ID="txtStore" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Số tiền</td>
                <td>
                    <asp:TextBox ID="txtIncome" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Thông tin thêm</td>
                <td>
                    <asp:TextBox ID="txtMoreInfo" runat="server" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Thông tin hợp đồng</td>
                <td>
                    <asp:HyperLink ID="hplContract" runat="server"></asp:HyperLink></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Lưu & thoát" CssClass="btn btn-primary" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Quay lại" CssClass="btn btn-primary" OnClick="btnCancel_Click" /></td>
            </tr>
        </tbody>
    </table>
    <script>
        $(document).ready(function () {
            $('#<%=txtIncome.ClientID %>').priceFormat({ prefix: '', suffix: '', centsLimit: 0 });

            $('input, textarea').keypress(function (e) {
                if (e.which == 13) {
                    $('#<%=btnSave.ClientID %>').click();
                    return false;
                }
            });
        });
    </script>
</asp:Content>
