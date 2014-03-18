<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserCreateProject.aspx.cs" Inherits="admin_UserProject" MasterPageFile="~/Master/AdminConsole.master"%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
    <style type="text/css">
        .style2
        {
            width: 247px;
        }
    </style>
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <br />

<div>
            </div>
            <br />
    
    <div  style="font-size: large"> 
        <br />
        <div>
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" DataKeyNames="ID" DataSourceID="AccessDataSource1" 
                ForeColor="Black" GridLines="Vertical" Width="736px"   OnRowDataBound="DataBound_click">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:TemplateField HeaderText="项目名">
                    <ItemTemplate>  <asp:Label runat="Server" ID="lbProject" Text='<%#Bind("ProjectName") %>' >
                         </asp:Label></ItemTemplate>
                        <EditItemTemplate>
                       
                            <asp:DropDownList ID="ddlProject1" runat="server" width="200px"
                            DataSourceID="AccessDataSource2" DataTextField="OilProjectTitle" 
                             DataValueField="OilProjectTitle" >
                             </asp:DropDownList>
                        </EditItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="角色">
                    <ItemTemplate>  <asp:Label runat="Server" ID="lbRole" Text='<%#Bind("RoleName") %>' >
                         </asp:Label></ItemTemplate>
                    <EditItemTemplate>
                     <asp:DropDownList ID="ddlRole1" runat="server" 
                Width="200px" DataSourceID="AccessDataSource3" DataTextField="RoleName" 
                DataValueField="RoleName">
                </asp:DropDownList>
                    </EditItemTemplate></asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
           
            <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
                ConflictDetection="CompareAllValues" DataFile="~/App_Data/DIS.mdb" 
                DeleteCommand="DELETE FROM [UPR] WHERE [ID] = ? AND (([ProjectName] = ?) OR ([ProjectName] IS NULL AND ? IS NULL)) AND (([UserName] = ?) OR ([UserName] IS NULL AND ? IS NULL)) AND (([RoleName] = ?) OR ([RoleName] IS NULL AND ? IS NULL))" 
                InsertCommand="INSERT INTO [UPR] ([ID], [ProjectName], [UserName], [RoleName]) VALUES (?, ?, ?, ?)" 
                OldValuesParameterFormatString="original_{0}" 
                SelectCommand="SELECT * FROM [UPR] WHERE ([UserName] = ?)" 
                
                
                UpdateCommand="UPDATE [UPR] SET [ProjectName] = ?, [UserName] = ?, [RoleName] = ? WHERE [ID] = ? AND (([ProjectName] = ?) OR ([ProjectName] IS NULL AND ? IS NULL)) AND (([UserName] = ?) OR ([UserName] IS NULL AND ? IS NULL)) AND (([RoleName] = ?) OR ([RoleName] IS NULL AND ? IS NULL))">
                <DeleteParameters>
                    <asp:Parameter Name="original_ID" Type="Int32" />
                    <asp:Parameter Name="original_ProjectName" Type="String" />
                    <asp:Parameter Name="original_ProjectName" Type="String" />
                    <asp:Parameter Name="original_UserName" Type="String" />
                    <asp:Parameter Name="original_UserName" Type="String" />
                    <asp:Parameter Name="original_RoleName" Type="String" />
                    <asp:Parameter Name="original_RoleName" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                    <asp:Parameter Name="ProjectName" Type="String" />
                    <asp:Parameter Name="UserName" Type="String" />
                    <asp:Parameter Name="RoleName" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="lin" Name="UserName" 
                        QueryStringField="UserName" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ProjectName" Type="String" />
                    <asp:Parameter Name="UserName" Type="String" />
                    <asp:Parameter Name="RoleName" Type="String" />
                    
                    <asp:Parameter Name="original_ID" Type="Int32" />
                    <asp:Parameter Name="original_ProjectName" Type="String" />
                    <asp:Parameter Name="original_ProjectName" Type="String" />
                    <asp:Parameter Name="original_UserName" Type="String" />
                    <asp:Parameter Name="original_UserName" Type="String" />
                    <asp:Parameter Name="original_RoleName" Type="String" />
                    <asp:Parameter Name="original_RoleName" Type="String" />
                    
                </UpdateParameters>
            </asp:AccessDataSource>
        </div>
    </div>
    <div>
        
        <br />
        <br />
        <br />
        <br />
        新建项目<br />
        <table>
        <tr>
        <td class="style2">
            <asp:DropDownList ID="ddlProject" runat="server" 
                Width="184px">
            </asp:DropDownList>
        </td>
        <td width="200">
            <asp:DropDownList ID="ddlRole" runat="server" 
                Width="184px"
                >
        </asp:DropDownList>
            <asp:AccessDataSource ID="AccessDataSource3" runat="server" 
                DataFile="~/App_Data/DIS.mdb" 
                SelectCommand="SELECT [RoleName] FROM [RolePermission]">
            </asp:AccessDataSource>
            </td>
        <td width="200"> 
            <asp:Button ID="addProject" runat="server" text = "新增" Width="63px" 
                ForeColor="Red" onclick="addProject_Click" />
            </td>
        </tr></table>
            
        </div>
        <br />
        <asp:AccessDataSource ID="AccessDataSource2" runat="server" 
            DataFile="~/App_Data/DIS.mdb" 
            
        SelectCommand="SELECT [OilProjectTitle] FROM [OilProject] WHERE ([IsDelete] = ?)">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="IsDelete" Type="Int32" />
            </SelectParameters>
        </asp:AccessDataSource>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
    </div>

</asp:Content>
