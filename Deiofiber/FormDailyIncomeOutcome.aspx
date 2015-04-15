<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormDailyIncomeOutcome.aspx.cs" Inherits="Deiofiber.DailyIncomeOutcome" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>CHI TIẾT HÀNG NGÀY</h2>
    <table class="table table-striped table-hover ">
        <tbody>
            <tr>
                <td>
                    <div class="col-lg-4">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control input-md" placeholder="Chọn ngày"></asp:TextBox>
                    </div>
                    <div class="col-lg-7">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-md" placeholder="Tìm kiếm"></asp:TextBox>
                    </div>
                </td>

                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></td>
                <td>
                    <asp:Button ID="btnNew" runat="server" Text="Thêm mới" CssClass="btn btn-primary" OnClick="btnNew_Click" /></td>
            </tr>
        </tbody>
    </table>
    <asp:Repeater ID="rptInOut" runat="server">
        <HeaderTemplate>
            <table class="table table-striped table-hover ">
                <thead>
                    <tr class="success">
                        <th class="text-center">#</th>
                        <th class="text-center">Loại chi phí</th>
                        <th class="text-center">Thu</th>
                        <th class="text-center">Chi</th>
                        <th class="text-center">Ngày</th>
                        <th class="text-center">Thông tin thêm</th>
                        <th class="text-center">Chức năng</th>
                        <th class="text-center">Người tạo</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Container.ItemIndex + 1 %></td>
                <td><%# Eval("INOUT_TYPE_NAME") %></td>
                <td class="text-right"><%# string.Format("{0:0,0}", (Eval("IN_AMOUNT").ToString() == "0" ? string.Empty: Eval("IN_AMOUNT"))) %></td>
                <td class="text-right"><%# string.Format("{0:0,0}", (Eval("OUT_AMOUNT").ToString() == "0" ? string.Empty: Eval("OUT_AMOUNT"))) %></td>
                <td class="text-center"><%# string.Format("{0:dd/MM/yyyy}", Eval("INOUT_DATE")) %></td>
                <td><%# Eval("MORE_INFO") %></td>
                <td class="text-center">
                    <asp:HyperLink ID="hplViewContract" runat="server" Text='<%# Eval("CONTRACT_ID").ToString() == "-1" ? "" : string.Format("HĐ Số: {0}", Eval("CONTRACT_NO")) %>' NavigateUrl='<%# Eval("CONTRACT_ID").ToString() == "-1" ? "#" : Eval("CONTRACT_ID","FormContractUpdate.aspx?ID={0}") %>'></asp:HyperLink></td>
                <td class="text-center"><%# Eval("UPDATED_BY") %></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
           </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:DropDownList ID="ddlPager" runat="server" CssClass="form-control dropdown-pager-width" OnSelectedIndexChanged="ddlPager_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <asp:HiddenField ID="hfPager" runat="server" />
    <script>
        $(function () {
            $('#<%=ddlPager.ClientID %>').change(function () {
                $('#<%=hfPager.ClientID %>').val($(this).val());
            });
            $('#<%=txtDate.ClientID %>').datepicker();
            $('#<%=txtSearch.ClientID %>').keypress(function (e) {
                if (e.which == 13) {
                    $('#<%=btnSearch.ClientID %>').click();
                    return false;
                }
            });
        });
    </script>
</asp:Content>
