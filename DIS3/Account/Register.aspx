<%@ Page Title="注册" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        $(function () {
            $("a:eq(2)").css({ "color": "#E65D06" });
        });

        function Refresh() {
            var num = Math.random();
            var VerifyCode = document.getElementById("VerfyCode");
            VerifyCode.src = "<%=ResolveUrl("~/Account/VerifyCode.aspx?")%>" + num;
        }
    </script>
    <style type="text/css">
        .style2
        {
            width: 108px;
        }
        .style3
        {
            width: 204px;
        }
        .style4
        {
            width: 270px;
        }
        .DetailForm
        {
            width: 744px;
        }
    .register
    {
        width: 714px;
    }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="content">
            <h2>注册
            </h2>
            <form id="Form1" runat="Server"> 
            <p>
                填写以下表单注册新账户( * 为必填项)
            </p>
            <div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="912px" 
                    BackColor="Red" ForeColor="White"></asp:Label>
            </div>
            <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                ValidationGroup="RegisterUserValidationGroup" DisplayMode="List" 
                BackColor="Red" ForeColor="White" />
            <div class="accountInfo">
                <fieldset class="register">
                    <legend>帐户信息</legend>
                    <table class="DetailForm">
                        <tr>
                            <td class="style2">
                                <p>
                                    <asp:Label ID="LoginNameLabel" runat="server" AssociatedControlID="LoginName">用户名 *</asp:Label>
                                </p>
                            </td>
                            <td class="style3">
                                <p>
                                    <asp:TextBox ID="LoginName" runat="server"  Width="300"></asp:TextBox>
                                </p>
                            </td>
                            <td class="style4">
                                <p>
                                    <asp:RequiredFieldValidator ID="LoginNameRequired" runat="server" ControlToValidate="LoginName"
                                        CssClass="failureNotification" ErrorMessage="必须填写“用户名”" ToolTip="必须填写“用户名”"
                                        ValidationGroup="RegisterUserValidationGroup" Display="None" />
                                </p>
                            </td>
                        </tr>
                   <tr>
                            <td class="style2">
                                <p>
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">密码 *</asp:Label>
                                </p>
                            </td>

                            <td class="style3">
                                <p>
                                    <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" 
                                        TextMode="Password" Width="300px" ontextchanged="Password_TextChanged1" AutoPostBack=true></asp:TextBox>
                                </p>
                            </td>
                            <td class="style4">
                                <p style="color: gray;" align="center">
                                    6~16个字符
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        CssClass="failureNotification" ErrorMessage="必须填写“密码”" ToolTip="必须填写“密码”"
                                        ValidationGroup="RegisterUserValidationGroup" Display="None" />
                                </p>
                            </td>
                        </tr>
          
                        <tr>
                            <td class="style2">
                                <p>
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">确认密码 *</asp:Label>
                                </p>
                            </td>
                            <td class="style3">
                                <p>
                                    <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password" Width="300"></asp:TextBox>
                                </p>
                            </td>
                            <td class="style4">
                                <p>
                                    <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification"
                                        ErrorMessage="必须填写“确认密码”" ID="ConfirmPasswordRequired" runat="server"
                                        ToolTip="必须填写“确认密码”" ValidationGroup="RegisterUserValidationGroup" Display="None" />
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                        CssClass="failureNotification" Display="None" ErrorMessage="“密码”和“确认密码”必须匹配"
                                        ValidationGroup="RegisterUserValidationGroup" />
                                    <asp:RegularExpressionValidator ID="PasswordLengthRequired" runat="server" ControlToValidate="Password"
                                        CssClass="failureNotification" ErrorMessage="密码长度不应少于6个字符或多于16个字符" ToolTip="密码长度不应少于6个字符或多于16个字符"
                                        ValidationExpression="[a-zA-Z0-9-_&]{6,16}" ValidationGroup="RegisterUserValidationGroup" Display="None" />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <p>
                                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">电子邮件 *</asp:Label>
                                </p>
                            </td>
                            <td class="style3">
                                <p>
                                    <asp:TextBox ID="Email" runat="server" CssClass="textEntry" Width="300"></asp:TextBox>
                                </p>
                            </td>
                            <td class="style4">
                                <p>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                        ErrorMessage="必须填写“电子邮件”" ToolTip="必须填写“电子邮件”"
                                        ValidationGroup="RegisterUserValidationGroup" Display="None" />
                                    <asp:RegularExpressionValidator ID="EmailRequired2" runat="server" ControlToValidate="Email"
                                        ErrorMessage="“电子邮件”请填写正确格式" ToolTip="“电子邮件”请填写正确格式"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="RegisterUserValidationGroup" Display="None" />
                                </p>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="style2">
                                <p>
                                    <asp:Label ID="VerificationCodeLabel" runat="server" AssociatedControlID="VerificationCode">验证码 (不区分大小写) *</asp:Label>
                                    <asp:RequiredFieldValidator ControlToValidate="VerificationCode"
                                        ErrorMessage="验证码不能为空" ID="VerificationCodeRequired" runat="server"
                                        ToolTip="验证码不能为空" ValidationGroup="RegisterUserValidationGroup" Display="None" />
                                </p>
                            </td>

                            <td class="style3">
                                <p>
                                    <asp:TextBox ID="VerificationCode" runat="server" CssClass="textEntry" Width="300"></asp:TextBox>
                                </p>

                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <p>点击右边刷新验证码</p>
                            </td>

                            <td class="style3">
                                <p>
                                    <img id="VerfyCode" alt="点击修改" src="<%=ResolveUrl("~/Account/VerifyCode.aspx?")%>" onclick="javascript:Refresh();" />
                                </p>
                            </td>

                        </tr>
                        <tr>
                            <td class="style2">
  
                         
                            </td>
                            <td class="style3">
                                <p align="right">
                                    <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="提交"
                                        ValidationGroup="RegisterUserValidationGroup" 
                                        OnClick="CreateUserButton_Click" CssClass="submitButton" Width="60px" />
                                </p>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </div>
        </form>
</asp:Content>