<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormIncomeOutcomeSummary.aspx.cs" Inherits="Deiofiber.FormIncomeOutcomeSummary" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>TỔNG HỢP THU CHI</h2>
        <table class="table table-striped table-hover ">
        <tbody>
            <tr>
                <td style="width:50%;">
                    <div class="col-lg-6">
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control input-md" placeholder="Ngày bắt đầu"></asp:TextBox>
                    </div>
                    <div class="col-lg-6">
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control input-md" placeholder="Ngày kết thúc"></asp:TextBox>
                    </div>
                </td>

                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnSearch_Click" OnClientClick="return validateSearch();"/></td>
            </tr>
        </tbody>
    </table>
    <asp:Repeater ID="rptInOut" runat="server">
        <HeaderTemplate>
            <table class="table table-striped table-hover ">
                <thead>
                    <tr class="success">
                        <th>Ngày/Tháng</th>
                        <th class="text-right">Dư đầu kỳ</th>
                        <th class="text-right">Tổng thu</th>
                        <th class="text-right">Tổng chi</th>
                        <th class="text-right">Dư cuối kỳ</th>
                        <th class="text-right">Chi tiết</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Convert.ToDateTime(Eval("Period")).ToString("dd/MM/yyyy") %></td>
                <td class="text-right"><%# Eval("BeginAmount").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("BeginAmount")) %></td>
                <td class="text-right"><%# Eval("TotalIn").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("TotalIn")) %></td>
                <td class="text-right"><%# Eval("TotalOut").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("TotalOut")) %></td>
                <td class="text-right"><%# Eval("EndAmount").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("EndAmount")) %></td>
                <td class="text-right">
                    <a id='<%# String.Format("a-{0}-{1}", Eval("StoreId"), Convert.ToDateTime(Eval("Period")).ToString("dd/MM/yyyy").Replace("/", "-")) %>' href='<%# String.Format("#detail-{0}-{1}", Eval("StoreId"), Convert.ToDateTime(Eval("Period")).ToString("dd/MM/yyyy").Replace("/", "-")) %>' class="fancybox">Chi tiết...</a>
                    <div id='<%# String.Format("detail-{0}-{1}", Eval("StoreId"), Convert.ToDateTime(Eval("Period")).ToString("dd/MM/yyyy").Replace("/", "-")) %>' style="display: none;">
                        <div class="detail-header">Tổng hợp thu chi</div>
                        <table class="table table-striped table-hover border_table" border="1">
                            <tbody>
                                <tr class="success">
                                    <td colspan="2">Chi tiết ngày: <%# Convert.ToDateTime(Eval("Period")).ToString("dd/MM/yyyy") %></td>
                                </tr>
                                <tr>
                                    <td>Dư đầu ngày:</td>
                                    <td><%# Eval("BeginAmount").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("BeginAmount")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Cho thuê ô tô, xe máy:</td>
                                    <td><%# Eval("ContractFeeCar").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("ContractFeeCar")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Phí thuê ô tô, xe máy:</td>
                                    <td><%# Eval("RentFeeCar").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("RentFeeCar")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Thanh lý thuê ô tô, xe máy:</td>
                                    <td><%# Eval("CloseFeeCar").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("CloseFeeCar")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Thừa phí thuê ô tô, xe máy:</td>
                                    <td><%# Eval("RedundantFeeCar").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("RedundantFeeCar")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Cho thuê điện thoại, máy tính:</td>
                                    <td><%# Eval("ContractFeeEquip").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("ContractFeeEquip")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Phí thuê điện thoại, máy tính:</td>
                                    <td><%# Eval("RentFeeEquip").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("RentFeeEquip")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Thanh lý thuê điện thoại, máy tính:</td>
                                    <td><%# Eval("CloseFeeEquip").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("CloseFeeEquip")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Thừa phí thuê điện thoại, máy tính:</td>
                                    <td><%# Eval("RedundantFeeEquip").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("RedundantFeeEquip")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Cho sinh viên thuê:</td>
                                    <td><%# Eval("ContractFeeStudent").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("ContractFeeStudent")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Phí sinh viên thuê:</td>
                                    <td><%# Eval("RentFeeStudent").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("RentFeeStudent")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Thanh lý sinh viên thuê:</td>
                                    <td><%# Eval("CloseFeeStudent").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("CloseFeeStudent")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Thừa phí sinh viên thuê:</td>
                                    <td><%# Eval("RedundantFeeStudent").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("RedundantFeeStudent")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Cho vay vốn:</td>
                                    <td><%# Eval("ContractFeeLoan").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("ContractFeeLoan")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Phí vay vốn:</td>
                                    <td><%# Eval("RentFeeLoan").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("RentFeeLoan")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Thanh lý vay vốn:</td>
                                    <td><%# Eval("CloseFeeLoan").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("CloseFeeLoan")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Thừa phí vay vốn:</td>
                                    <td><%# Eval("RedundantFeeLoan").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("RedundantFeeLoan")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Nhập vốn:</td>
                                    <td><%# Eval("InCapital").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("InCapital")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Xuất vốn:</td>
                                    <td><%# Eval("OutCapital").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("OutCapital")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Thu khác:</td>
                                    <td><%# Eval("InOther").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("InOther")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Chi khác:</td>
                                    <td><%# Eval("OutOther").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("OutOther")) %> VNĐ</td>
                                </tr>
                                <tr>
                                    <td>Dư cuối ngày:</td>
                                    <td><%# Eval("EndAmount").ToString() == "0"? "0": string.Format("{0:0,0}", Eval("EndAmount")) %> VNĐ</td>
                                </tr>
                            </tbody>
                        </table>
                        <div class="text-right"><a class="print" href="javascript:void(0);"><img src="App_Themes/Theme1/image/printer-blue.png" /></a></div>
                    </div>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td>Tổng hợp cuối kỳ</td>
                <td class="text-right">
                    <asp:Label ID="lblTotalBegin" runat="server" Text=""></asp:Label>
                </td>
                <td class="text-right">
                    <asp:Label ID="lblTotalIn" runat="server" Text=""></asp:Label></td>
                <td class="text-right">
                    <asp:Label ID="lblTotalOut" runat="server" Text=""></asp:Label></td>
                <td class="text-right text-result">
                    <asp:Label ID="lblTotalEnd" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            </tbody>
           </table>
        </FooterTemplate>
    </asp:Repeater>

    <table class="table table-striped table-hover" style="width: 50%; margin-left: 25%;">
        <tbody>
            <tr class="success">
                <td colspan="2" class="text-center"><strong>Tổng kết kinh doanh</strong></td>
            </tr>
            <tr>
                <td>Tổng giá trị hợp đồng thuê ô tô, xe máy</td>
                <td class="text-right">
                    <asp:Label ID="lblDeiofiberAmount" runat="server" Text="" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td>Tổng giá trị hợp đồng cho thuê điện thoại, máy tính</td>
                <td class="text-right">
                    <asp:Label ID="lblRentEquipAmount" runat="server" Text="" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td>Tổng giá trị hợp đồng cho sinh viên thuê</td>
                <td class="text-right">
                    <asp:Label ID="lblRentStudentAmount" runat="server" Text="" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td>Tổng giá trị hợp đồng cho vay vốn</td>
                <td class="text-right">
                    <asp:Label ID="lblLoanAmount" runat="server" Text="" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td>Tổng giá trị tất cả hợp đồng cho thuê</td>
                <td class="text-right">
                    <asp:Label ID="lblRentAll" runat="server" Text="" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td>Tổng phí</td>
                <td class="text-right">
                    <asp:Label ID="lblSumAllIn" runat="server" Text="" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td>Tổng chi phí</td>
                <td class="text-right">
                    <asp:Label ID="lblSumAllOut" runat="server" Text="" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td>Tổng đầu tư</td>
                <td class="text-right">
                    <asp:Label ID="lblTotalInvest" runat="server" Text="" CssClass="text-right"></asp:Label></td>
            </tr>
            <tr>
                <td>Kết quả kinh doanh</td>
                <td class="text-right">
                    <asp:Label ID="lblLastAmount" runat="server" Text="" CssClass="text-right"></asp:Label></td>
            </tr>
        </tbody>
    </table>

    <table class="table table-striped table-hover">
        <tbody>
            <tr>
                <td colspan="3">Tổng hợp theo ngày & cửa hàng</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ClientIDMode="Static" ID="txtViewDate" runat="server" CssClass="form-control input-sm text-right"></asp:TextBox></td>
                <td>
                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="form-control"></asp:DropDownList></td>
                <td>
                    <asp:Button ID="btnSearch1" runat="server" Text="Xem" CssClass="btn btn-primary" OnClick="btnSearch1_Click" /></td>
            </tr>
        </tbody>
    </table>
    <%--<table class="table table-striped table-hover">
        <thead>
            <tr>
                <th class="text-center">Ngày</th>
                <th>Cửa hàng</th>
                <th class="text-right">Dư đầu kỳ</th>
                <th class="text-right">Tổng thu</th>
                <th class="text-right">Tổng chi</th>
                <th class="text-right">Dư cuối kỳ</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="text-center">
                    <asp:Label ID="lblViewDate" runat="server" Text=""></asp:Label></td>
                <td>
                    <asp:Label ID="lblStoreName" runat="server" Text=""></asp:Label></td>
                <td class="text-right">
                    <asp:Label ID="lblStartAmount" runat="server" Text=""></asp:Label></td>
                <td class="text-right">
                    <asp:Label ID="lblTotalIn" runat="server" Text=""></asp:Label></td>
                <td class="text-right">
                    <asp:Label ID="lblTotalOut" runat="server" Text=""></asp:Label></td>
                <td class="text-right">
                    <asp:Label ID="lblEndAmount" runat="server" Text=""></asp:Label></td>
            </tr>
        </tbody>
    </table>--%>
    <asp:Repeater ID="rptInOutDetail" runat="server">
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
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
           </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:DropDownList ID="ddlPager" runat="server" CssClass="form-control dropdown-pager-width" OnSelectedIndexChanged="ddlPager_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <script>
        $(function () {
            $("#txtViewDate").datepicker();
            $('.fancybox').fancybox();
            $('#<%=txtStartDate.ClientID %>').datepicker();
            $('#<%=txtEndDate.ClientID %>').datepicker();

            var options = {};
            $('a.print').click(function (e) {
                $(this).parent().parent().printArea(options);
            });

            $('#<%=txtViewDate.ClientID %>').keypress(function (e) {
                if (e.which == 13) {
                    $('#<%=btnSearch1.ClientID %>').click();
                            return false;
                        }
            });

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
