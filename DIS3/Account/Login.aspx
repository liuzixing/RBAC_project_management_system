<%@ Page Title="登录" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

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
        .style1
        {
            width: 153px;
        }
    .DetailForm
    {
        width: 648px;
    }
    .accountInfo
    {
        width: 661px;
    }
    .style2
    {
        width: 153px;
        height: 24px;
    }
    .style3
    {
        height: 24px;
    }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="Server" ContentPlaceHolderID="MainContent">
    <h2>登录</h2>
    <form id="Form1" runat="Server"> 
            <p>
                请输入用户名和密码。如果您没有帐户,请点击 
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false" NavigateUrl="~/Account/Register.aspx">注册</asp:HyperLink>
        (*为必填项)
            </p>
          <div>
                <asp:Label ID="care" runat="server" Visible="False" Width="912px" 
                    BackColor="#66FF66" ForeColor="White"></asp:Label>
            </div>
            <div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="912px" 
                    BackColor="Red" ForeColor="White"></asp:Label>
            </div>
            
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                ValidationGroup="LoginUserValidationGroup" DisplayMode="List" 
        BackColor="Red" ForeColor="White" />
       
           
            <div class="accountInfo">

                <fieldset class="login">
                    <legend>帐户信息</legend>
                    <table class="DetailForm">
                        <tr>
                            <td class="style2">
                                <p>
                                    <asp:Label ID="LoginNameLabel" runat="server" AssociatedControlID="LoginName" 
                                        Width="130px">用户名 *</asp:Label>
                                </p>
                            </td>
                            <td class="style3">
                                <p>
                                    <asp:TextBox ID="LoginName" runat="server" CssClass="textEntry" Width="227px" 
                                        ></asp:TextBox>
                                </p>
                            </td>
                            <td class="style3">
                                <p>
                                    <asp:RequiredFieldValidator ID="LoginNameRequired" runat="server" ControlToValidate="LoginName"
                                        ErrorMessage="必须填写“用户名”" ToolTip="必须填写“用户名”"
                                        ValidationGroup="LoginUserValidationGroup" Display="None"></asp:RequiredFieldValidator>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">

                                <p>
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">密码 *</asp:Label>
                                </p>
                            </td>
                            <td>
                                <p>
                                    <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" 
                                         TextMode="Password" Width="225px"></asp:TextBox>
                                </p>
                            </td>
                            <td>
                                <p>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        ErrorMessage="必须填写“密码”" ToolTip="必须填写“密码”"
                                        ValidationGroup="LoginUserValidationGroup" Display="None" />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <p>
                                    <asp:Label ID="VerificationCodeLabel" runat="server" AssociatedControlID="VerificationCode">验证码 (不区分大小写) *</asp:Label>
                                    <asp:RequiredFieldValidator ControlToValidate="VerificationCode"
                                        ErrorMessage="验证码不能为空" ID="VerificationCodeRequired" runat="server"
                                        ToolTip="验证码不能为空" ValidationGroup="RegisterUserValidationGroup" Display="None" />
                                </p>
                            </td>

                            <td class="style3">
                                <p>
                                    <asp:TextBox ID="VerificationCode" runat="server" CssClass="textEntry" 
                                        Width="231px"></asp:TextBox>
                                </p>

                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <p>点击右边刷新验证码</p>
                            </td>

                            <td class="style3">
                                <p>
                                    &nbsp;<asp:ImageButton ID="ImageButton1" 
                                        runat="server" ImageUrl="~/Account/VerifyCode.aspx" 
                                        onclick="ImageButton1_Click" Height="23px" Width="113px" />
                                  
                                    
                                </p>
                            </td>

                        </tr>
                        <tr>
                        <td>
                                <p>
                                    <asp:CheckBox ID="RememberMe" runat="server" />
                                    <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">记住用户名和密码</asp:Label>
                                </p>
                            </td>

                            <td colspan="2">
                                <p>
                                <span style="float:left;margin-right:30px; width: 76px;"><asp:Button ID="LoginButton" 
                                        runat="server" Text="登录" ValidationGroup="LoginUserValidationGroup" 
                                        CssClass="submitButton" OnClick="LoginButton_Click" Width="59px"/></span>

                                    <span style="float:left;margin-left:30px; height: 16px; width: 136px;" ><asp:HyperLink  runat="server" NavigateUrl="~/Account/FindPassword.aspx"  Text="忘记密码？"></asp:HyperLink></span>
                                    
                                </p>
                            </td>
                        </tr>
                    </table>
                    
                </fieldset>

            </div>
            </form>
            </asp:Content>
