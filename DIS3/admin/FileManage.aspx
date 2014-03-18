<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileManage.aspx.cs" Inherits="FileManage" MasterPageFile="~/Master/AdminConsole.master"%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
   
    当前位置：<a href="Information.aspx">个人信息</a>-&gt;项目资源管理<br />
    <div>
                <asp:Label ID="care" runat="server" Visible="False" Width="912px" 
                    BackColor="#66FF66" ForeColor="White"></asp:Label>
            </div>
    <div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="912px" 
                    BackColor="Red" ForeColor="White"></asp:Label>
            </div>
    <div style="float:left; width:63%">
     <div align=center style="height: 26px"> 
       <asp:TextBox ID="tbFileSearch" runat="server" Height="22px"></asp:TextBox>
&nbsp;&nbsp;
            <asp:Button ID="btnFileSearch" runat="server" Text="文件查找" Height="26px" 
                onclick="btnFileSearch_Click" />

             
            
            <br />
            <br />
            <br />
        <br />
    </div>
    <div align=left runat="Server" id="searchPart" style="float:left; width:%">&nbsp;<br />
        <br />

        <asp:GridView ID="GridView2" 
            runat="server" CellPadding="3" 
            autoGenerateColumns="False" OnRowDeleting="myFileDelete" 
            onRowCommand="downLoad" 
            onselectedindexchanged="myFile_SelectedIndexChanged" 
             DataKeyNames="ID" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellSpacing="2" 
            Width="508px">
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
            <Columns>
                                 <asp:TemplateField HeaderText="操作">
                                  <ItemTemplate> 
                                    <asp:LinkButton ID="TestLinkBtn" runat="server" 
                                      CommandName="dlCommand" 
                                      CommandArgument='<%# Eval("FileURL")%>'
                                      Text="下载"></asp:LinkButton> 
                                  </ItemTemplate>
                                </asp:TemplateField> 
                <asp:CommandField ShowDeleteButton="True" HeaderText="操作" />
                                <asp:HyperLinkField DataNavigateUrlFields="FileName" 
                    DataNavigateUrlFormatString="FileDetails.aspx?FileName={0}" 
                    DataTextField="FileName" DataTextFormatString="{0}" HeaderText="文件名" />
                                <asp:BoundField DataField="FileURL" HeaderText="文件路径" 
                                    SortExpression="FileURL" >
                                </asp:BoundField>
                                <asp:BoundField DataField="UserName" HeaderText="创建者" 
                                    SortExpression="UserName">
                                </asp:BoundField>
                                  
                                <asp:BoundField DataField="CreateTime" HeaderText="创建时间" 
                                    SortExpression="CreateTime" >
                                </asp:BoundField>
                            </Columns>
        </asp:GridView> 
        <br />
    </div>

    <div align="left" style="float:left; width:99%">
        <br />
        <asp:GridView ID="myFile" runat="server" CellPadding="3" 
            autoGenerateColumns="False" OnRowDeleting="myFileDelete" 
            onRowCommand="downLoad" 
            onselectedindexchanged="myFile_SelectedIndexChanged" 
            DataSourceID="AccessDataSource1" DataKeyNames="ID" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellSpacing="2" 
            Width="508px">
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
            <Columns>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="TestLinkBtn" runat="server" 
                                      CommandName="dlCommand" 
                                      CommandArgument='<%# Eval("FileURL")%>'
                                      Text="下载"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" HeaderText="操作" />
                <asp:HyperLinkField DataNavigateUrlFields="FileName" 
                    DataNavigateUrlFormatString="FileDetail.aspx?FileName={0}" 
                    DataTextField="FileName" DataTextFormatString="{0}" HeaderText="文件名" />
                <asp:BoundField DataField="FileURL" HeaderText="文件路径" 
                                    SortExpression="FileURL" ></asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="创建者" 
                                    SortExpression="UserName"></asp:BoundField>
                <asp:BoundField DataField="CreateTime" HeaderText="创建时间" 
                                    SortExpression="CreateTime" ></asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
            DataFile="~/App_Data/DIS.mdb" 
            SelectCommand="SELECT * FROM [File] WHERE ([ProjectName] = ?)" 
            ConflictDetection="CompareAllValues" 
            DeleteCommand="DELETE FROM [File] WHERE [ID] = ? AND (([FileName] = ?) OR ([FileName] IS NULL AND ? IS NULL)) AND (([FileURL] = ?) OR ([FileURL] IS NULL AND ? IS NULL)) AND (([ProjectName] = ?) OR ([ProjectName] IS NULL AND ? IS NULL)) AND (([Description] = ?) OR ([Description] IS NULL AND ? IS NULL)) AND (([CreateTime] = ?) OR ([CreateTime] IS NULL AND ? IS NULL)) AND (([UserName] = ?) OR ([UserName] IS NULL AND ? IS NULL))" 
            InsertCommand="INSERT INTO [File] ([ID], [FileName], [FileURL], [ProjectName], [Description], [CreateTime], [UserName]) VALUES (?, ?, ?, ?, ?, ?, ?)" 
            OldValuesParameterFormatString="original_{0}" 
            UpdateCommand="UPDATE [File] SET [FileName] = ?, [FileURL] = ?, [ProjectName] = ?, [Description] = ?, [CreateTime] = ?, [UserName] = ? WHERE [ID] = ? AND (([FileName] = ?) OR ([FileName] IS NULL AND ? IS NULL)) AND (([FileURL] = ?) OR ([FileURL] IS NULL AND ? IS NULL)) AND (([ProjectName] = ?) OR ([ProjectName] IS NULL AND ? IS NULL)) AND (([Description] = ?) OR ([Description] IS NULL AND ? IS NULL)) AND (([CreateTime] = ?) OR ([CreateTime] IS NULL AND ? IS NULL)) AND (([UserName] = ?) OR ([UserName] IS NULL AND ? IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_FileName" Type="String" />
                <asp:Parameter Name="original_FileName" Type="String" />
                <asp:Parameter Name="original_FileURL" Type="String" />
                <asp:Parameter Name="original_FileURL" Type="String" />
                <asp:Parameter Name="original_ProjectName" Type="String" />
                <asp:Parameter Name="original_ProjectName" Type="String" />
                <asp:Parameter Name="original_Description" Type="String" />
                <asp:Parameter Name="original_Description" Type="String" />
                <asp:Parameter Name="original_CreateTime" Type="DateTime" />
                <asp:Parameter Name="original_CreateTime" Type="DateTime" />
                <asp:Parameter Name="original_UserName" Type="String" />
                <asp:Parameter Name="original_UserName" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="ID" Type="Int32" />
                <asp:Parameter Name="FileName" Type="String" />
                <asp:Parameter Name="FileURL" Type="String" />
                <asp:Parameter Name="ProjectName" Type="String" />
                <asp:Parameter Name="Description" Type="String" />
                <asp:Parameter Name="CreateTime" Type="DateTime" />
                <asp:Parameter Name="UserName" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:QueryStringParameter Name="ProjectName" QueryStringField="ProjectName" 
                    Type="String" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="FileName" Type="String" />
                <asp:Parameter Name="FileURL" Type="String" />
                <asp:Parameter Name="ProjectName" Type="String" />
                <asp:Parameter Name="Description" Type="String" />
                <asp:Parameter Name="CreateTime" Type="DateTime" />
                <asp:Parameter Name="UserName" Type="String" />
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_FileName" Type="String" />
                <asp:Parameter Name="original_FileName" Type="String" />
                <asp:Parameter Name="original_FileURL" Type="String" />
                <asp:Parameter Name="original_FileURL" Type="String" />
                <asp:Parameter Name="original_ProjectName" Type="String" />
                <asp:Parameter Name="original_ProjectName" Type="String" />
                <asp:Parameter Name="original_Description" Type="String" />
                <asp:Parameter Name="original_Description" Type="String" />
                <asp:Parameter Name="original_CreateTime" Type="DateTime" />
                <asp:Parameter Name="original_CreateTime" Type="DateTime" />
                <asp:Parameter Name="original_UserName" Type="String" />
                <asp:Parameter Name="original_UserName" Type="String" />
            </UpdateParameters>
        </asp:AccessDataSource>
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" Height="29px" Width="192px" />
        <br />
        <br />
        <asp:Button ID="uploadClk" runat="server" Text="上传" onclick="uploadClk_Click" 
            Height="24px" />
        <br />
        <asp:AccessDataSource ID="AccessDataSource2" runat="server" 
            DataFile="~/App_Data/DIS.mdb" 
            SelectCommand="SELECT * FROM [UPR] WHERE ([ProjectName] = ?)" 
            OldValuesParameterFormatString="original_{0}" 
            ConflictDetection="CompareAllValues" 
            DeleteCommand="DELETE FROM [UPR] WHERE [ID] = ? AND (([ProjectName] = ?) OR ([ProjectName] IS NULL AND ? IS NULL)) AND (([UserName] = ?) OR ([UserName] IS NULL AND ? IS NULL)) AND (([RoleName] = ?) OR ([RoleName] IS NULL AND ? IS NULL))" 
            InsertCommand="INSERT INTO [UPR] ([ID], [ProjectName], [UserName], [RoleName]) VALUES (?, ?, ?, ?)" 
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
                <asp:QueryStringParameter Name="ProjectName" QueryStringField="ProjectName" 
                    Type="String" DefaultValue="DIS网站" />
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
        <br />
        <br />
    </div>
    <div align="center">
&nbsp;&nbsp;
        <br />
        <br />
        <br />
    </div>
    <div >
        <br />
        项目成员<br />
      
        <br />
        <div style="float:left; width:63%">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="AccessDataSource2" DataKeyNames="ID" Width="508px" 
            BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
            CellPadding="2" ForeColor="Black" GridLines="None" OnRowDeleting="RowDelete_Click">
            <AlternatingRowStyle BackColor="PaleGoldenrod"  />
            <Columns>
                <asp:BoundField DataField="ProjectName" HeaderText="项目名" 
                    SortExpression="ProjectName" />
                <asp:BoundField DataField="UserName" HeaderText="成员" 
                    SortExpression="UserName" />
                <asp:BoundField DataField="RoleName" HeaderText="所属角色" 
                    SortExpression="RoleName" />
                <asp:CommandField HeaderText="操作" ShowDeleteButton="True" ShowHeader="True" />
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
      
        </div>
        <br />
        <br />
        <br />
    </div>
                            <br />
    <br />
    <br />
                        <br />
    <br />
    </div>
    <br />
    </div>
    <div style="float:right;width:33%">
        用户名<br />
    <br />
        <asp:TextBox ID="tbUserSearch" runat="server" AutoPostBack="True" 
            ontextchanged="tbUserSearch_TextChanged"  ></asp:TextBox>&nbsp;<asp:Button 
            ID="Button1" runat="server" Text="搜索" />
&nbsp;<asp:CheckBoxList ID="clistUser" runat="server">
                        </asp:CheckBoxList>
                        <br />
        <br />
        角色<br />
        <asp:CheckBoxList ID="cblistRole" runat="server" 
            DataSourceID="AccessDataSource3" DataTextField="RoleName" 
            DataValueField="RoleName">
        </asp:CheckBoxList>
        <br />
        <asp:Button ID="addMember" runat="server" Text="新增成员" 
            onclick="addMember_Click" />
        <br />
        <asp:AccessDataSource ID="AccessDataSource3" runat="server" 
            DataFile="~/App_Data/DIS.mdb" 
            SelectCommand="SELECT [RoleName] FROM [RolePermission]">
        </asp:AccessDataSource>
        <br />
        </div>
    <br />
    <br />
    <br />

   
</asp:Content>