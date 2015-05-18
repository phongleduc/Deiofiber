<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormWarning.aspx.cs" Inherits="Deiofiber.FormWarning" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>THÔNG BÁO</h2>
    <table class="table table-striped table-hover ">
        <tbody>
            <tr>
                <td>
                    <div class="col-lg-4">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control input-md" placeholder="Hợp đồng đến hạn trong ngày"></asp:TextBox>
                    </div>
                    <div class="col-lg-7">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-md" placeholder="Tìm kiếm"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></td>
            </tr>
        </tbody>
    </table>
    <asp:Repeater ID="rptWarning" runat="server" OnItemDataBound="rptWarning_ItemDataBound">
        <HeaderTemplate>
            <div class="text-right" style="margin-bottom:5px">
                <a class="print" href="javascript:void(0);">
                    <img src="App_Themes/Theme1/image/printer-blue.png" /></a>&nbsp;&nbsp;
            </div>
            <div id="areaToPrint">
                <table class="table table-striped table-hover">
                    <tr class="success">
                        <th>#</th>
                        <th>Tên khách hàng</th>
                        <th>Loại hình thuê</th>
                        <th>Số ĐT khách hàng</th>
                        <th class="text-right">Giá trị HĐ/Phí</th>
                        <th class="text-right">Số lần đóng phí</th>
                        <th class="text-center">Thông báo</th>
                        <th class="text-center">Số ngày chậm</th>
                        <th class="text-center">Xử lý HĐ</th>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr id='<%# string.Format("HtmlTableRow{0}", Container.ItemIndex) %>'>
                <td><%# Container.ItemIndex + 1 %></td>
                <td><%# Eval("CUSTOMER_NAME") %><br />(<%# Convert.ToDateTime(Eval("BIRTH_DAY")).ToString("dd/MM/yyyy") %>)</td>
                <td><%# Eval("RENT_TYPE_NAME") %></td>
                <td><%# Eval("PHONE") %></td>
                <td class="text-right"><%# string.Format("{0:0,0}", Convert.ToDecimal(Eval("FEE_PER_DAY"))) %></td>
                <td class="text-right"><%# Eval("PAYED_TIME") %> lần</td>
                <td class="text-right"><%# Eval("NOTE") %></td>
                <td class="text-center"><%# Eval("OVER_DATE") %> Ngày <br /><span style="color:red">(<%# Convert.ToDateTime(Eval("PAY_DATE")).ToString("dd/MM/yyyy") %>)</span></td>
                <td>
                    <asp:HyperLink ID="hplUpdateContract" CssClass="text-center" runat="server" Text='<%# Eval("CONTRACT_NO")%>' NavigateUrl='<%# Eval("ID","FormContractUpdate.aspx?ID={0}") %>'></asp:HyperLink></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
                </div>
        </FooterTemplate>
    </asp:Repeater>

    <%--    <asp:DropDownList ID="ddlPager" runat="server" CssClass="form-control dropdown-pager-width" OnSelectedIndexChanged="ddlPager_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <asp:HiddenField ID="hfPager" runat="server" />--%>
    <script>
        $(function () {
            $('#<%=txtDate.ClientID %>').datepicker();

<%--            $('#<%=ddlPager.ClientID %>').change(function () {
                $('#<%=hfPager.ClientID %>').val($(this).val());
            });--%>

            $('#<%=txtSearch.ClientID %>').keypress(function (e) {
                if (e.which == 13) {
                    $('#<%=btnSearch.ClientID %>').click();
                    return false;
                }
            });

            var options = {};
            $('a.print').click(function (e) {
                //$(this).parent().next().printArea(options);
                printDiv();
            });

            function printDiv() {
                var divToPrint = $('#areaToPrint').clone();
                divToPrint.find('table').find("tr").find("th:last, td:last").remove();
                divToPrint.prepend($("<h3 style='text-align:center;'>" + "Ngày <%= SearchDate%>" + "</h3>"));
                divToPrint.prepend($("<h1 style='text-align:center;'>Thông báo</h1>"));
                divToPrint.find('table').css('width', '100%');
                divToPrint.find('table').css('border-collapse', 'collapse');
                divToPrint.find('table').find("tr").css('border', '1px solid black');
                divToPrint.find('table').find("td").css('border', '1px solid black');
                divToPrint.find('table').find("th").css('border', '1px solid black');
                newWin = window.open("");
                newWin.document.write(divToPrint.html());
                newWin.print();
                newWin.close();
            }
        });
    </script>
</asp:Content>
