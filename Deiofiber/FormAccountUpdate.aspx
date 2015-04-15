<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormAccountUpdate.aspx.cs" Inherits="Deiofiber.FormAccountUpdate" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4 class="text-center">Thông tin tài khoản</h4>
    <table class="table table-striped table-hover" style="width: 60%; margin-left: 20%;">
        <tbody>
            <tr>
                <td colspan="2" class="text-center">
                    <strong>
                        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-center text-warning"></asp:Label></strong>
                </td>
            </tr>
            <tr>
                <td class="text-right">Tên đăng nhập</td>
                <td>
                    <asp:TextBox ID="txtAccount" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Mật khẩu cũ</td>
                <td>
                    <asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Mật khẩu mới</td>
                <td>
                    <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Xác nhận mật khẩu</td>
                <td>
                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Quyền sử dụng</td>
                <td>
                    <asp:DropDownList ID="ddlPermission" runat="server" CssClass="form-control"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="text-right">Quản lý cửa hàng</td>
                <td>
                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="form-control"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="text-right">Người sử dụng tài khoản</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">Số điện thoại</td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <%--            <tr>
                <td class="text-right">Địa chỉ</td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>--%>
            <tr>
                <td class="text-right">Tỉnh/Thành phố</td>
                <td>
                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="text-right">Ngày đăng ký</td>
                <td>
                    <asp:TextBox ClientIDMode="Static" ID="txtRegisterDate" runat="server" CssClass="form-control input-sm text-right"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right">Kích hoạt</td>
                <td>
                    <asp:RadioButton ID="rdbActive" runat="server" Text="Sử dụng" GroupName="activegroup" Checked="True" />
                    -
                    <asp:RadioButton ID="rdbDeActive" runat="server" Text="Ngừng sử dụng" GroupName="activegroup" /></td>
            </tr>
            <tr>
                <td class="text-right">Ghi chú</td>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Lưu & thoát" CssClass="btn btn-primary" OnClick="btnSave_Click" />&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Thoát" CssClass="btn btn-primary" OnClick="btnCancel_Click" /></td>
            </tr>
        </tbody>
    </table>
    <script>
        $(document).ready(function () {
            $("#txtRegisterDate").datepicker();

            $('input, textarea').keypress(function (e) {
                if (e.which == 13) {
                    $('#<%=btnSave.ClientID %>').click();
                    return false;
                }
            });
        });
    </script>
    <%--   <script>
        $.urlParam = function (name) {
            var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
            if (results == null) {
                return null;
            }
            else {
                return results[1] || 0;
            }
        }

        $(document).ready(function () {
            $("#txtApplyDate").datepicker();
            $("#txtRegisterDate").datepicker();
            $("#txtRegisterDate").datepicker('setDate', new Date());

            // validate signup form on keyup and submit
            if ($.urlParam('ID') != undefined && $.urlParam('ID').length > 0) {
                $("#form1").validate({
                    rules: {
                        '<%= txtAccount.UniqueID %>': {
                             required: true,
                             minlength: 6,
                             maxlength: 30
                         },
                         '<%= txtOldPassword.UniqueID %>': {
                             minlength: 6
                         },
                         '<%= txtNewPassword.UniqueID %>': {
                             minlength: 6
                         },
                         '<%= txtConfirmPassword.UniqueID %>': {
                             minlength: 6,
                             equalTo: '#<%= txtNewPassword.UniqueID %>'
                         },
                         '<%= txtName.UniqueID %>': {
                             required: true
                         },
                         '<%= txtPhone.UniqueID %>': {
                             required: true
                         }
                     },
                     messages: {
                         '<%= txtAccount.UniqueID %>': {
                             required: "Bạn cần nhập thông tin tài khoản.",
                             minlength: "Tên phải nhiều hơn 6 ký tự.",
                             maxlength: "Tên phải nhỏ hơn 30 ký tự."
                         },
                         '<%= txtOldPassword.UniqueID %>': {
                             minlength: "Mật khẩu phải có ít nhất 6 ký tự."
                         },
                         '<%= txtNewPassword.UniqueID %>': {
                             minlength: "Mật khẩu phải có ít nhất 6 ký tự."
                         },
                         '<%= txtConfirmPassword.UniqueID %>': {
                             minlength: "Mật khẩu phải có ít nhất 6 ký tự.",
                             equalTo: "Mật khẩu không khớp nhau."
                         },
                         '<%= txtName.UniqueID %>': "Bạn cần nhập tên người sử dụng.",
                         '<%= txtPhone.UniqueID %>': "Bạn cần nhập số điện thoại."
                     }
                 });
             } else {
                 $("#form1").validate({
                     rules: {
                         '<%= txtAccount.UniqueID %>': {
                             required: true,
                             minlength: 6,
                             maxlength: 30
                         },
                         '<%= txtOldPassword.UniqueID %>': {
                             required: true,
                             minlength: 6
                         },
                         '<%= txtNewPassword.UniqueID %>': {
                             required: true,
                             minlength: 6
                         },
                         '<%= txtConfirmPassword.UniqueID %>': {
                             required: true,
                             minlength: 6,
                             equalTo: '#<%= txtNewPassword.UniqueID %>'
                         },
                         '<%= txtName.UniqueID %>': {
                             required: true
                         },
                         '<%= txtPhone.UniqueID %>': {
                             required: true
                         }
                     },
                     messages: {
                         '<%= txtAccount.UniqueID %>': {
                             required: "Bạn cần nhập thông tin tài khoản.",
                             minlength: "Tên phải nhiều hơn 6 ký tự.",
                             maxlength: "Tên phải nhỏ hơn 30 ký tự."
                         },
                         '<%= txtOldPassword.UniqueID %>': {
                             required: "Bạn cần phải nhập mật khẩu hiện tại.",
                             minlength: "Mật khẩu phải có ít nhất 6 ký tự."
                         },
                         '<%= txtNewPassword.UniqueID %>': {
                             required: "Bạn cần phải nhập mật khẩu.",
                             minlength: "Mật khẩu phải có ít nhất 6 ký tự."
                         },
                         '<%= txtConfirmPassword.UniqueID %>': {
                             required: "Bạn cần phải nhập mật khẩu.",
                             minlength: "Mật khẩu phải có ít nhất 6 ký tự.",
                             equalTo: "Mật khẩu không khớp nhau."
                         },
                         '<%= txtName.UniqueID %>': "Bạn cần nhập tên người sử dụng.",
                         '<%= txtPhone.UniqueID %>': "Bạn cần nhập số điện thoại."
                     }
                 });
             }
         });
    </script>--%>
</asp:Content>
