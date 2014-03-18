<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Information.aspx.cs" Inherits="Information" MasterPageFile="~/Master/AdminConsole.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <br />

<div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="912px" 
                    BackColor="Red" ForeColor="White"  ></asp:Label>
            </div>
            <br />
    
    <div  style="font-size: large; float:left; width:50%" > 
        我的账号<br />
        <br />
        <div>
            注册时间：<asp:TextBox ID="tbtime" runat="server" Enabled="False"></asp:TextBox>
            <br />
        </div>
        <table style="width:50%;" align="center">
            </table>
        <br />
        <div>
            邮箱：&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; 
            <asp:TextBox ID="tbemail" runat="server" Enabled="False"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
         
        </div>
        <br />
    
       
        
      
        <div id="passPart" runat="Server">
            新密码：&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbpass" 
                runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
          
            
            密码确认：<asp:TextBox ID="tbconfirm" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
        </div>
       
        <div>
            &nbsp;&nbsp; &nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="savebtn" runat="server" Text="保存" onClick="Save" ForeColor="#FF3300" 
            Height="31px" Width="79px" />
        </div>
       
        
       </div>
       
     
        <div style="float:right;width:49%">
            <p style="font-size: large">我的项目</p><br />
            <asp:DataList ID="projectList" runat="server" BackColor="Cyan" 
                BorderColor="Cyan" BorderStyle="None" BorderWidth="1px" CellPadding="10" 
                CellSpacing="7" DataSourceID="AccessDataSource1" GridLines="Both" 
                RepeatColumns="1" RepeatDirection="Horizontal" 
                style="margin-right: 0px" Width="443px">
                <ItemTemplate>
                    项目名： &nbsp;<%-- ID:
                         <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                         <br />--%><asp:HyperLink ID="HyperLink1" runat="server"  NavigateUrl = '<%#"FileManage.aspx?ProjectName=" + Eval("ProjectName") %>'
                            Text='<%# Eval("ProjectName") %>' />
                    <br />
                        角色名：
                    <asp:Label ID="RoleNameLabel" runat="server" Text='<%# Eval("RoleName") %>' />
                    <br />
                    <br />
                </ItemTemplate>
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                <ItemStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            </asp:DataList>
            <br />
            <br />
        </div>
        <div>
            <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
                DataFile="~/App_Data/DIS.mdb" 
                SelectCommand="SELECT [ProjectName], [RoleName] FROM [UPR] WHERE ([UserName] = ?)">
                <SelectParameters>
                    <asp:SessionParameter Name="UserName" SessionField="UserName" Type="String" />
                </SelectParameters>
            </asp:AccessDataSource>
        </div>
        <br />
        <br />
    </div>

</asp:Content>
