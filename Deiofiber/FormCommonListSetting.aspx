<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormCommonListSetting.aspx.cs" Inherits="Deiofiber.FormCommonListSetting" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>CÀI ĐẶT DANH MỤC</h2>
    <table class="table table-striped table-hover ">
        <tbody>
            <tr>
                <td>
                    <asp:Button ID="btnRentTypeCreate" runat="server" Text="Tạo phân loại" CssClass="btn btn-primary" /></td>
            </tr>
        </tbody>
    </table>
    <div>
        <asp:Repeater ID="rptRentType" runat="server">
                <headertemplate>
                    <table class="table table-striped table-hover">
                      <thead>
                        <tr class="success">
                          <th>#</th>
                          <th>Tên phân loại</th>
                            <th>Trạng thái</th>
                          <th>Chức năng</th>
                        </tr>
                      </thead>
                      <tbody>
                </headertemplate>
                <itemtemplate>
                        <tr>
                          <td><%# Container.ItemIndex + 1 %></td>
                          <td><%# Eval("NAME") %></td>
                            <td><%# Eval("ACTIVE") %></td>
                          <td><asp:HyperLink ID="hplAccountUpdate" runat="server" Text="Cập nhật" NavigateUrl='<%# Eval("ID","FormAccountUpdate.aspx?ID={0}") %>'></asp:HyperLink></td>
                        </tr>                        
                </itemtemplate>
                <FooterTemplate>
                      </tbody>
                    </table>
                </FooterTemplate>
        </asp:Repeater>
        <asp:DropDownList ID="ddlPager" runat="server" CssClass="form-control dropdown-pager-width" OnSelectedIndexChanged="ddlPager_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <%--<ul class="pagination pagination-sm">
          <li class="disabled"><a href="#">«</a></li>
          <li class="active"><a href="#">1</a></li>
          <li><a href="#">2</a></li>
          <li><a href="#">3</a></li>
          <li><a href="#">4</a></li>
          <li><a href="#">5</a></li>
          <li><a href="#">»</a></li>
        </ul>--%>
    </div>
</asp:Content>
