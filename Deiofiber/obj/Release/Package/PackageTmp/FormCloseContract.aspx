<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormCloseContract.aspx.cs" Inherits="Deiofiber.FormCloseContract" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table table-striped table-hover" style="width: 50%; margin-left: 25%;">
        <tbody>
            <tr>
                <td colspan="2" class="text-center">Thanh lý hợp đồng</td>
            </tr>
            <tr>
                <td class="text-right">Hợp đồng hết hạn ngày</td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" Enabled="false" CssClass="form-control input-sm text-right"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Số ngày quá hạn</td>
                <td>
                    <asp:TextBox ID="txtOverDate" runat="server" Enabled="false" CssClass="form-control input-sm text-right"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Giá trị thanh lý</td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" Enabled="false" CssClass="form-control input-sm text-right"></asp:TextBox></td>
            </tr>
<%--            <tr>
                <td class="text-right">Phí phải trả</td>
                <td>
                    <asp:TextBox ID="txtPayFee" ClientIDMode="Static" runat="server" Enabled="false" CssClass="form-control input-sm text-right"></asp:TextBox></td>
            </tr>--%>
            <tr>
                <td class="text-right">Thừa phí</td>
                <td>
                    <asp:TextBox ID="txtReduceAmount" ClientIDMode="Static" runat="server" CssClass="form-control input-sm text-right" onChange="ComputeCosts();"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Thực thu</td>
                <td>
                    <asp:TextBox ID="txtRealIncome" ClientIDMode="Static" runat="server" CssClass="form-control input-sm text-right" AutoPostBack="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Ghi chú</td>
                <td>
                    <asp:TextBox ID="txtMoreInfo" runat="server" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Lưu & thoát" CssClass="btn btn-primary" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Quay lại" CssClass="btn btn-primary" OnClick="btnCancel_Click" /></td>
            </tr>
        </tbody>
    </table>
    <script>
        function calculate() {
            var amount = document.getElementById('txtAmount').value;
            var reduce = document.getElementById('txtReduceAmount').value;
            document.getElementById('txtRealIncome').value = amount - reduce;
        }

        $(document).ready(function () {
            $("#txtReduceAmount").priceFormat({ prefix: '', suffix: '', centsLimit: 0 });
            $("#txtRealIncome").priceFormat({ prefix: '', suffix: '', centsLimit: 0 });
        });

        function ComputeCosts() {
            var ra = $('#txtReduceAmount').val()
            var reduceAmount = parseFloat(ra.replace(/\,/g, ''));
            var pf = $('#txtPayFee').val()
            var payfee = parseFloat(pf.replace(/\,/g, ''));
            var real = payfee - reduceAmount;
            $('#txtRealIncome').val(real);
            $("#txtRealIncome").priceFormat({ prefix: '', suffix: '', centsLimit: 0 });
        }
    </script>    
</asp:Content>
