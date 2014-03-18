<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManage.aspx.cs" Inherits="UserManage" MasterPageFile="~/Master/AdminConsole.master" %>

<%@ Register src="~/Control/PageBarControl.ascx" tagname="PageBarControl" tagprefix="uc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    当前位置：用户管理
    <div >
   
        <div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="912px" 
                    BackColor="Red" ForeColor="White"></asp:Label>
                <asp:Label ID="care" runat="server" Visible="False" Width="912px" 
                    BackColor="#66FF66" ForeColor="White"></asp:Label>
            <br />
            <div>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                    <asp:ListItem>全部用户</asp:ListItem>
                    <asp:ListItem>已激活用户</asp:ListItem>
                    <asp:ListItem>未激活用户</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="查找" 
                    onclick="btnSearch_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="创建用户" onclick="Button1_Click" />
                
                 <div id="createPart" runat="Server">
                <p align="center">
                    用户名 *<asp:TextBox ID="userName" runat="server" Width="169px"></asp:TextBox>
                </p>
                <br />
                <p align="center">
                    密码 *<asp:TextBox ID="password" runat="server" Width="158px" TextMode="Password"></asp:TextBox>
                </p>
                <br />
                <p align="center">
                    电子邮箱 *<asp:TextBox ID="email" runat="server" Width="188px"></asp:TextBox>
                </p>
                <br />
                <p align="center">
                    <asp:Button ID="registerclk" runat="server" onclick="registerclk_Click" 
                        Text="创建" style="height: 29px" />
                </p>
                 </div>
                <br />
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                         BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="4" GridLines="Vertical" 
                         Width="710px"  OnRowEditing="UserEdit"  OnRowUpdating="UserUpdate" 
                    OnRowCancelingEdit="UserCancel" BackColor="White" BorderStyle="None" 
                   OnRowDeleting="UserDelete2"
                    ForeColor="Black">
                         <AlternatingRowStyle BackColor="White" />
                         <Columns>

                             <asp:HyperLinkField DataNavigateUrlFields="UserName" 
                                 DataNavigateUrlFormatString="UserCreateProject.aspx?UserName={0}" 
                                 DataTextField="UserName" DataTextFormatString="{0}" HeaderText="用户名" />
                             <asp:BoundField DataField="Email" HeaderText="邮箱" />
                             <asp:TemplateField  HeaderText="激活">
                                 <ItemTemplate>
                                     <asp:CheckBox ID="Confirmchb" runat="server"  
                            commandName="confirm" CommandArgument= '<%#Eval("UserName") %>'   autopostback=true Oncheckedchanged="Confirmbtn_Click" Checked=<%#Eval("Confirm") %>/>
                                 </ItemTemplate>
                             </asp:TemplateField> 
                             <asp:BoundField DataField="CreateTime" HeaderText="创建时间" />
                             <asp:CommandField ShowDeleteButton="True" HeaderText="操作" />
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
            </div>
            <br />
            <div>
                 <div class="yy">
                     <div class="yy">
   <uc1:PageBarControl ID="PageBarControl1" runat="server" TableName="Users"  PageSize="6" />
   </div>
   </div>
                <br />
                <br />
                <br />
            </div>
            <br />
        </div>
    </div>
   
</asp:Content>