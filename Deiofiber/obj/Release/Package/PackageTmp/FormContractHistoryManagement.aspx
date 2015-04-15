<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormContractHistoryManagement.aspx.cs" Inherits="Deiofiber.ContractHistoryManagement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>HỢP ĐỒNG CŨ</h2>
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
    <asp:Repeater ID="rptContractHistory" runat="server">
        <HeaderTemplate>
            <table class="table table-striped table-hover ">
                <thead>
                    <tr class="success">
                        <th>#</th>
                        <th>Tên khách hàng</th>
                        <th>Loại hình</th>
                        <th class="text-right">Giá trị hàng cho thuê</th>
                        <th class="text-right">Phí/ngày</th>
                        <th class="text-center">Ngày thuê</th>
                        <th class="text-center">Ngày kết thúc</th>
                        <th class="text-center">Ngày thanh lý</th>
                        <th>Sao chép</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Container.ItemIndex + 1 %></td>
                <%--<td><a href="FormContractHistoryUpdate.aspx"><%# Eval("CUSTOMER_NAME") %></a></td>--%>
                <td><%# Eval("CUSTOMER_NAME") %></td>
                <td><%# Eval("RENT_TYPE_NAME") %></td>
                <td class="text-right"><%#  string.Format("{0:0,0}", Eval("CONTRACT_AMOUNT")) %></td>
                <td class="text-right"><%#  string.Format("{0:0,0}", Eval("FEE_PER_DAY")) %></td>
                <td class="text-center"><%# String.Format("{0:dd/MM/yyyy}", Eval("RENT_DATE"))%></td>
                <td class="text-center"><%# String.Format("{0:dd/MM/yyyy}", Eval("END_DATE"))%></td>
                <td class="text-center"><%# String.Format("{0:dd/MM/yyyy}", Eval("CLOSE_CONTRACT_DATE"))%></td>
                <td>
                    <asp:HyperLink ID="hplContractUpdate" runat="server" Text="Sao chép" NavigateUrl='<%# Eval("CONTRACT_ID","FormContractUpdate.aspx?ID={0}&copy=1") %>'></asp:HyperLink></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
           </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:DropDownList ID="ddlPager" runat="server" CssClass="form-control dropdown-pager-width" OnSelectedIndexChanged="ddlPager_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
<%--    <b>Loại hợp đồng:</b>  <asp:DropDownList ID="drpRentType" runat="server" CssClass="form-control drp-renttype" OnSelectedIndexChanged="drpRentType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>--%>
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
