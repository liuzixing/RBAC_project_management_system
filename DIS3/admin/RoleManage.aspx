<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleManage.aspx.cs" Inherits="RoleManage" MasterPageFile="~/Master/AdminConsole.master"%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
    <style type="text/css">
        .style1
        {
            width: 189px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
当前位置：角色管理
<div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="807px" 
                    BackColor="Red" ForeColor="White" Height="22px"></asp:Label>
            </div>
     <div align="center">
     <p>角色管理</p>
         <div>
             <br />
         </div>
         <div>
             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                 BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
                 CellPadding="2" DataKeyNames="ID" DataSourceID="AccessDataSource1" 
                 ForeColor="Black" GridLines="None" Width="719px">
                 <AlternatingRowStyle BackColor="PaleGoldenrod" />
                 <Columns>

                     <asp:HyperLinkField  HeaderText="角色名称"  DataNavigateUrlFields="ID" 
DataNavigateUrlFormatString="~/admin/PermissionManagement.aspx?RoleId={0}" DataTextField="RoleName" />

                     <asp:CommandField HeaderText="操作" ShowDeleteButton="True" >

                     <ItemStyle HorizontalAlign="Right" />
                     </asp:CommandField>

                 </Columns>
                 <FooterStyle BackColor="Tan" />
                 <HeaderStyle BackColor="Tan" Font-Bold="True" />
                 <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                     HorizontalAlign="Center" />
                 <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                 <SortedAscendingCellStyle BackColor="#FAFAE7" />
                 <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                 <SortedDescendingCellStyle BackColor="#E1DB9C" />
                 <SortedDescendingHeaderStyle BackColor="#C2A47B" />
             </asp:GridView>
             <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/DIS.mdb" 
                 DeleteCommand="DELETE FROM [RolePermission] WHERE [ID] = ?" 
                 InsertCommand="INSERT INTO [RolePermission] ([ID], [RoleName], [ReadFile], [DeleteFile], [Upload], [DownLoad], [ReadSelf]) VALUES (?, ?, ?, ?, ?, ?, ?)" 
                 OldValuesParameterFormatString="original_{0}" 
                 SelectCommand="SELECT * FROM [RolePermission]" 
                 
                 
                 
                 
                 UpdateCommand="UPDATE [RolePermission] SET [RoleName] = ?, [ReadFile] = ?, [DeleteFile] = ?, [Upload] = ?, [DownLoad] = ?, [ReadSelf] = ? WHERE [ID] = ?">
                 <DeleteParameters>
                     <asp:Parameter Name="original_ID" Type="Int32" />
                 </DeleteParameters>
                 <InsertParameters>
                     <asp:Parameter Name="ID" Type="Int32" />
                     <asp:Parameter Name="RoleName" Type="String" />
                     <asp:Parameter Name="ReadFile" Type="Boolean" />
                     <asp:Parameter Name="DeleteFile" Type="Boolean" />
                     <asp:Parameter Name="Upload" Type="Boolean" />
                     <asp:Parameter Name="DownLoad" Type="Boolean" />
                     <asp:Parameter Name="ReadSelf" Type="Boolean" />
                 </InsertParameters>
                 <UpdateParameters>
                     <asp:Parameter Name="RoleName" Type="String" />
                     <asp:Parameter Name="ReadFile" Type="Boolean" />
                     <asp:Parameter Name="DeleteFile" Type="Boolean" />
                     <asp:Parameter Name="Upload" Type="Boolean" />
                     <asp:Parameter Name="DownLoad" Type="Boolean" />
                     <asp:Parameter Name="ReadSelf" Type="Boolean" />
                     <asp:Parameter Name="original_ID" Type="Int32" />
                 </UpdateParameters>
             </asp:AccessDataSource>
             <br />
             <asp:Button ID="createRole" runat="server" Text="创建角色" Width="78px" 
                 onclick="createRole_Click" />
             <div id="createField" runat="Server">
                 <table style="width:100%;" >
                     <tr>
                         <td class="style1">
                             角色名称：>
                               <td>
                             <asp:TextBox ID="roleName" runat="server"></asp:TextBox>
                         </td>
   
                     </tr>
                     <tr>
                         <td class="style1">
                             用户权限：</td>
                         <td>
                             <asp:CheckBoxList ID="permCheck" runat="server" 
                                 RepeatDirection="Horizontal">
                             </asp:CheckBoxList>
                         </td>
                     </tr>
                     <tr>
                         <td class="style1">
                             &nbsp;</td>
                         <td>
                             <asp:Button ID="create" runat="server" Text="创建" onclick="create_Click" />
                             
                         </td>
                    
                     </tr>
                 </table>
                         
             </div>
         </div>
     </div>
</asp:Content>