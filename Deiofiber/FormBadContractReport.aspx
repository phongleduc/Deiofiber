<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormBadContractReport.aspx.cs" Inherits="Deiofiber.FormBadContractReport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>HỢP ĐỒNG TREO</h2>
    <table class="table table-striped table-hover ">
        <tbody>
            <tr>
                <td class="text-right"><strong>Hợp đồng thuê ô tô, xe máy:</strong></td>
                <td class="text-right">Số lượng:&nbsp;<asp:Label ID="lblDeiofiberCount" runat="server" CssClass="text-right"></asp:Label></td>
                <td class="text-right"><strong>Tổng giá trị:&nbsp;</strong></td>
                <td class="text-right">
                    <asp:Label ID="lblTotalFeeBikeContract" runat="server" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td class="text-right"><strong>Hợp đồng thuê điện thoại, máy tính:</strong></td>
                <td class="text-right">Số lượng:&nbsp;<asp:Label ID="lblRentEquipCount" runat="server" CssClass="text-right"></asp:Label></td>
                <td class="text-right"><strong>Tổng giá trị:&nbsp;</strong></td>
                <td class="text-right">
                    <asp:Label ID="lblTotalFeeEquiqContract" runat="server" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td class="text-right"><strong>Hợp đồng sinh viên thuê:</strong></td>
                <td class="text-right">Số lượng:&nbsp;<asp:Label ID="lblRentStudentCount" runat="server" CssClass="text-right"></asp:Label></td>
                <td class="text-right"><strong>Tổng giá trị:&nbsp;</strong></td>
                <td class="text-right">
                    <asp:Label ID="lblTotalFeeStudentContract" runat="server" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td class="text-right"><strong>Hợp đồng vay vốn:</strong></td>
                <td class="text-right">Số lượng:&nbsp;<asp:Label ID="lblRentLoanCount" runat="server" CssClass="text-right"></asp:Label></td>
                <td class="text-right"><strong>Tổng giá trị:&nbsp;</strong></td>
                <td class="text-right">
                    <asp:Label ID="lblTotalFeeLoanContract" runat="server" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td class="text-right"><strong>Tổng hợp đồng khách hàng xấu:</strong></td>
                <td class="text-right">Số lượng:&nbsp;<asp:Label ID="lblNumberOfBadContract" runat="server" CssClass="text-right"></asp:Label></td>
                <td class="text-right"><strong>Tổng giá trị HĐ (<asp:Label ID="lblPercentBadContract" runat="server" CssClass="text-right"></asp:Label>)</strong></td>
                <td class="text-right">
                    <asp:Label ID="lblTotalBadContract" runat="server" CssClass="text-right"></asp:Label></td>
            </tr>
        </tbody>
    </table>
    <asp:Repeater ID="rptContract" runat="server">
        <HeaderTemplate>
            <table class="table table-striped table-hover ">
                <thead>
                    <tr class="success">
                        <th>#</th>
                        <th>Tên khách hàng</th>
                        <th>Loại hình</th>
                        <th class="text-right">Giá trị HĐ</th>
                        <th class="text-right">Phí/ngày</th>
                        <th class="text-center">Đã trả phí hết ngày</th>
                        <th class="text-center">Số ngày chậm</th>
                        <th class="text-center">Tổng tiền phí</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Container.ItemIndex + 1 %></td>
                <td><%# Eval("CUSTOMER_NAME") %></td>
                <td><%# Eval("RENT_TYPE_NAME") %></td>
                <td class="text-right"><%# string.Format("{0:0,0}", Eval("CONTRACT_AMOUNT")) %></td>
                <td class="text-right"><%# string.Format("{0:0,0}", Eval("FEE_PER_DAY")) %> VNĐ</td>
                <td class="text-center"><%# String.Format("{0:dd/MM/yyyy}", Eval("PAY_DATE"))%></td>
                <td class="text-center red"><%# Eval("OVER_DATE")%> Ngày</td>
                <td class="text-center"><%# string.Format("{0:0,0}", Convert.ToDecimal(Eval("FEE_PER_DAY")) * Convert.ToDecimal(Eval("OVER_DATE"))) %> VNĐ</td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
           </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:DropDownList ID="ddlPager" runat="server" CssClass="form-control dropdown-pager-width" OnSelectedIndexChanged="ddlPager_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
</asp:Content>
