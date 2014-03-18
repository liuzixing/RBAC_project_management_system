<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PermissionManagement.aspx.cs" Inherits="RoleManage" MasterPageFile="~/Master/AdminConsole.master"%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    当前位置：<a href="RoleManage.aspx">角色管理</a>->权限管理
<div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="806px" 
                    BackColor="Red" ForeColor="White"></asp:Label>
            </div>
     <div align="center">
     <asp:Label ID="RoleName" runat="server" Visible="True" Width="806px" 
                    BackColor="white" ForeColor="black"></asp:Label>
                             <fieldset style="width: 266px"  >
         <legend>文件</legend>
             <asp:CheckBoxList ID="permCheck" runat="server" RepeatDirection="Vertical"
                >
                
             </asp:CheckBoxList>
         </fieldset>
         <div>
             <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="保存" />
             <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="返回上一页" />
             <br />
         </div>
         <div>
             <br />

         </div>
         &nbsp;</div>
</asp:Content>