<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormContractManagement.aspx.cs" Inherits="Deiofiber.ContractManagement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>QUẢN LÝ HỢP ĐỒNG</h2>
    <table class="table table-striped table-hover ">
        <tbody>
            <tr>
                <td style="width: 50%;">
                    <div class="col-lg-6">
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control input-md" placeholder="Ngày bắt đầu"></asp:TextBox>
                    </div>
                    <div class="col-lg-6">
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control input-md" placeholder="Ngày kết thúc"></asp:TextBox>
                    </div>
                </td>

                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnSearch_Click" OnClientClick="return validateSearch();" /></td>
                 <td>
                    <asp:Button ID="btnNew" runat="server" Text="Làm hợp đồng" CssClass="btn btn-primary" OnClick="btnNew_Click" /></td>
            </tr>
        </tbody>
    </table>
    <table class="table table-striped table-hover ">
        <tbody>
            <tr>
                <td class="text-right"><strong>Hợp đồng thuê ô tô, xe máy</strong></td>
                <td class="text-right">
                    <asp:Label ID="lblDeiofiberNo" runat="server" CssClass="text-right"></asp:Label></td>
                <td class="text-right"><strong>Tổng số hợp đồng chưa thanh lý</strong></td>
                <td class="text-right">
                    <asp:Label ID="lblNotFinishedContract" runat="server" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td class="text-right"><strong>Hợp đồng thuê điện thoại, máy tính</strong></td>
                <td class="text-right">
                    <asp:Label ID="lblRentOfficeEquip" runat="server" CssClass="text-right"></asp:Label></td>
                <td class="text-right"><strong>Tổng phí hợp đồng/ngày</strong></td>
                <td class="text-right">
                    <asp:Label ID="lblTotalFeeContract" runat="server" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td class="text-right"><strong>Hợp đồng cho sinh viên thuê</strong></td>
                <td class="text-right">
                    <asp:Label ID="lblRentStudent" runat="server" CssClass="text-right"></asp:Label></td>
                <td class="text-right"><strong>Tổng số tiền hợp đồng chưa thanh lý</strong></td>
                <td class="text-right">
                    <asp:Label ID="lblTotalMoneyOfNotFinishContract" runat="server" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td class="text-right"><strong>Hợp đồng cho vay vốn</strong></td>
                <td class="text-right">
                    <asp:Label ID="lblRentLoan" runat="server" CssClass="text-right"></asp:Label></td>
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
                        <th>Số điện thoại</th>
                        <th class="text-right">Số tiền</th>
                        <th class="text-right">Phí/ngày</th>
                        <th class="text-center">Ngày thuê</th>
                        <th class="text-center">Ngày hết hạn</th>
                        <th class="text-center">Cập nhật</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Container.ItemIndex + 1 %></td>
                <td><%# Eval("CUSTOMER_NAME") %></td>
                <td><%# Eval("PHONE") %></td>
                <td class="text-right"><%# string.Format("{0:0,0}", Eval("CONTRACT_AMOUNT")) %></td>
                <td class="text-right"><%# string.Format("{0:0,0}", Eval("FEE_PER_DAY")) %></td>
                <td class="text-center"><%# String.Format("{0:dd/MM/yyyy}", Eval("RENT_DATE"))%></td>
                <td class="text-center"><%# String.Format("{0:dd/MM/yyyy}", Eval("END_DATE"))%></td>
                <td class="text-center">
                    <asp:HiddenField ID="hdfContract_id" runat="server" Value='<%# Eval("ID") %>' />
                    <asp:HyperLink ID="hplContractUpdate" runat="server" Text="Cập nhật" NavigateUrl='<%# Eval("ID","FormContractUpdate.aspx?ID={0}") %>'></asp:HyperLink>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
           </table>
        </FooterTemplate>
    </asp:Repeater>
    <%--    <asp:DropDownList ID="ddlPager" runat="server" CssClass="form-control dropdown-pager-width" OnSelectedIndexChanged="ddlPager_SelectedIndexChanged" AutoPostBack="true" Visible="false"></asp:DropDownList>--%>
    <b>Loại hợp đồng:</b>
    <asp:DropDownList ID="drpRentType" runat="server" CssClass="form-control drp-renttype" OnSelectedIndexChanged="drpRentType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <script>
        $(function () {
            $('#<%=txtStartDate.ClientID %>').datepicker();
            $('#<%=txtEndDate.ClientID %>').datepicker();

            $('#<%=txtStartDate.ClientID %>').keypress(function (e) {
                if (e.which == 13) {
                    if (validateSearch()) {
                        $('#<%=btnSearch.ClientID %>').click();
                        return false;
                    }
                }
            });

            $('#<%=txtEndDate.ClientID %>').keypress(function (e) {
                if (e.which == 13) {
                    if (validateSearch()) {
                        $('#<%=btnSearch.ClientID %>').click();
                        return false;
                    }
                }
            });
        });
        function validateSearch() {
            if (new Date($('#<%=txtEndDate.ClientID %>').val()) < new Date($('#<%=txtStartDate.ClientID %>').val())) {
                alert("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
