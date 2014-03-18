<%@ Page Title="重置密码" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true"
    CodeFile="NewPassword.aspx.cs" Inherits="Account_NewPassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        更改密码
    </h2>
    <form id="Form1" runat="Server"> 
    <p>
        请使用以下表单更改密码。(*为必填项)
    </p>
    <p>
        新密码的长度至少必须为 <%= Membership.MinRequiredPasswordLength %> 个字符。
    </p>
    <div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="912px" 
                    BackColor="Red" ForeColor="White"></asp:Label>
            </div>
            <asp:ValidationSummary ID="ChangePasswordValidationSummary" 
        runat="server" CssClass="failureNotification"
                ValidationGroup="RegisterValidationGroup" DisplayMode="List" 
        BackColor="Red" ForeColor="White" />
            <div class="accountInfo">
        
            
                <fieldset class="changePassword">
                    <legend>帐户信息</legend>
                    <p>
                         <asp:Label ID="LoginNameLabel" runat="server" AssociatedControlID="LoginName">用户名 *</asp:Label>
                    </p>
                     <p>
                         <asp:TextBox ID="LoginName" runat="server" CssClass="textEntry" Width="278px"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="LoginNameRequired" runat="server" ControlToValidate="LoginName"
                             CssClass="failureNotification" ErrorMessage="必须填写“用户名”" ToolTip="必须填写“用户名”"
                             ValidationGroup="RegisterUserValidationGroup" Display="None" />
                    </p>
                    <p>
                         <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">电子邮件 *</asp:Label>
                         </p>
                         <p>
                         <asp:TextBox ID="Email" runat="server" CssClass="textEntry" Width="272px"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                               ErrorMessage="必须填写“电子邮件”" ToolTip="必须填写“电子邮件”"
                               ValidationGroup="RegisterUserValidationGroup" Display="None" />
                         <asp:RegularExpressionValidator ID="EmailRequired2" runat="server" ControlToValidate="Email"
                               ErrorMessage="“电子邮件”请填写正确格式" ToolTip="“电子邮件”请填写正确格式"
                               ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="RegisterUserValidationGroup" Display="None" />
                   </p>
                    <p>
                         
                        <asp:Label ID="NewPasswordLabel" runat="server" 
                            AssociatedControlID="NewPassword" Width="300px">新密码 *</asp:Label>
                    </p>
                    <p>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" 
                            TextMode="Password" Width="273px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                             CssClass="failureNotification" ErrorMessage="必须填写“新密码”。" ToolTip="必须填写“新密码”。" 
                             ValidationGroup="ChangePasswordValidationSummary">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" 
                            AssociatedControlID="ConfirmNewPassword" Width="300px">确认新密码 *</asp:Label>
                            </p>
                            <p>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" 
                            TextMode="Password" Width="269px" Height="16px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="必须填写“确认新密码”。"
                             ToolTip="必须填写“确认新密码”。" ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="“确认新密码”与“新密码”项必须匹配。"
                             ValidationGroup="ChangePasswordValidationSummary">*</asp:CompareValidator>
                    </p>
                    <p class="submitButton" align="right">
                    <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"/>
                    <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="更改密码" 
                         ValidationGroup="ChangePasswordValidationSummary" OnClick="OK_Button_Click" CssClass="OKButton"/>
                </p>
                </fieldset>
                
            </div>
     
     </form>
</asp:Content>