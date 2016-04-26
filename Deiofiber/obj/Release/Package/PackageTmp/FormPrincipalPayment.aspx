<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormPrincipalPayment.aspx.cs" Inherits="Deiofiber.FormPrincipalPayment" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4 class="text-left">TRẢ GỐC (Dành cho những hợp đồng không thể trả lãi)</h4>
    <asp:Repeater ID="rptContractInOut" runat="server">
        <HeaderTemplate>
            <table class="table table-striped table-hover ">
                <thead>
                    <tr class="success">
                        <th>#</th>
                        <th class="text-right">Số tiền trả</th>
                        <th class="text-center">Ngày trả</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                <td class="text-center"><%# string.Format("{0:0,0}", (Eval("IN_AMOUNT").ToString() == "0" ? string.Empty : Eval("IN_AMOUNT"))) %></td>
                <td class="text-center"><%# string.Format("{0:dd/MM/yyyy}", Eval("CREATED_DATE")) %></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr class="danger">
                <td>Giá trị hợp đồng:</td>
                <td>
                    <asp:Label ID="lblContractAmout" runat="server" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr class="info">
                <td>Tổng số đã thanh toán:</td>
                <td>
                    <asp:Label ID="lblTotalPaid" runat="server" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr class="warning">
                <td>Số tiền còn thiếu:</td>
                <td>
                    <asp:Label ID="lblAmountLeft" runat="server" CssClass="text-right"></asp:Label></td>
            </tr>
            </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <br />
    <table class="table table-striped table-hover" style="width: 50%; margin-left: 25%;">
        <tbody>
            <tr>
                <td colspan="2" class="text-center">Chi tiết khoản trả phí</td>
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
            $('input, textarea').keypress(function (e) {
                if (e.which == 13) {
                    $('#<%=btnSave.ClientID %>').click();
                    return false;
                }
            });
        });
    </script>
</asp:Content>
