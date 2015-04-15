<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormContractHistoryUpdate.aspx.cs" Inherits="Deiofiber.FormContractHistoryUpdate" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4 class="text-center">Thông tin hợp đồng cũ</h4>
    <table class="table table-striped table-hover" style="width: 60%; margin-left: 20%;">
        <tbody>
            <tr class="info">
                <td colspan="2" class="text-center"><strong>
                Thông tin khách hàng
            </strong></td>
            <tr>
                <td class="text-right">Số CMND/Số GPLX</td>
                <td>
                    <asp:TextBox ID="txtLicenseNumber" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Tên khách hàng</td>
                <td>
                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Số điện thoại</td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
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
            <tr class="success">
                <td colspan="2" class="text-center"><strong>Thông tin hợp đồng</strong></td>
            </tr>
            <tr>
                <td class="text-right">Loại hình</td>
                <td>
                    <asp:DropDownList ID="ddlRentType" runat="server" CssClass="form-control"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="text-right">Cửa hàng</td>
                <td>
                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="form-control"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="text-right">Số tiền</td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control input-sm text-right"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Phí thuê/1 ngày</td>
                <td>
                    <asp:TextBox ID="txtFeePerDay" runat="server" CssClass="form-control input-sm text-right"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Ngày thuê</td>
                <td>
                    <asp:TextBox ClientIDMode="Static" ID="txtRentDate" runat="server" CssClass="form-control input-sm text-right"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right">Ngày hết hạn</td>
                <td>
                    <asp:TextBox ClientIDMode="Static" ID="txtEndDate" runat="server" CssClass="form-control input-sm text-right"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right">Thông báo trả phí</td>
                <td>
                    <asp:TextBox ID="txtPayFeeMessage" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Lịch trả phí</td>
                <td>
                    <asp:Repeater ID="rptPayFeeSchedule" runat="server">
                        <ItemTemplate>
                            <a href="FormInOutUpdate.aspx?ID=<%# Eval("ID")%>">Trả phí ngày <%# Eval("PAY_DATE").ToString().Split(' ')[0] %></a><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td class="text-right">Nợ phí</td>
                <td></td>
            </tr>
            <tr>
                <td class="text-right">Ghi chú</td>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr class="warning">
                <td colspan="2" class="text-center"><strong>Thông tin thêm</strong></td>
            </tr>
            <tr>
                <td class="text-right">Người xác minh</td>
                <td>
                    <asp:TextBox ID="txtReferencePerson" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Loại xe</td>
                <td>
                    <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Biển kiểm soát</td>
                <td>
                    <asp:TextBox ID="txtItemLicenseNo" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Số khung</td>
                <td>
                    <asp:TextBox ID="txtSerial1" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Số máy</td>
                <td>
                    <asp:TextBox ID="txtSerial2" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Chi tiết </td>
                <td>
                    <asp:TextBox ID="txtItemDetail" runat="server" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="Quay lại" CssClass="btn btn-primary" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
