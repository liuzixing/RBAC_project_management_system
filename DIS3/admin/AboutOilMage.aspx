<%@ Page Title="关于油站管理" Language="C#" MasterPageFile="~/Master/AdminConsole.master"
    AutoEventWireup="true" CodeFile="AboutOilMage.aspx.cs" Inherits="admin_AboutOilMage" %>

<%@ Register Src="~/Control/PlanPageBarControl.ascx" TagPrefix="uc" TagName="PageBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script>


   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
<div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="912px" 
                    BackColor="Red" ForeColor="White"></asp:Label>
            </div>
    <h3>关于油站管理</h3>
    
    <asp:Button ID="AddProject" runat="server"  Text="添加关于" 
        onclick="AddAboutOil_Click" />
    <asp:Button ID="Button1" runat="server"  Text="取消选择" OnClick="Button1_Click" />
    <asp:Button ID="Button3" runat="server"  Text="批量删除" OnClick="Button2_Click" />
    <asp:Button ID="Button4" runat="server"  Text="撤销删除" OnClick="Button5_Click" />
    
    <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
        Width="100%" AutoGenerateColumns="False" CellPadding="4"  DataKeyNames="AboutOilID"
        OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="True" PageSize="10" 
        ForeColor="#333333"  GridLines="None"  style="text-align: center">
        <Columns>
            <asp:TemplateField>
            <ItemStyle Width="5%" />
                <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Size="9pt" 
                        OnCheckedChanged="CheckBox2_CheckedChanged" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="标题" ItemStyle-HorizontalAlign=Left>
                <ItemStyle Width="50%" />
                <ItemTemplate>
                    <a href='AboutOilEditor.aspx?AboutOilID=<%# Eval("AboutOilID")%>'>
                        <%# Eval("OilClassify").ToString().Length > 20 ? Eval("OilClassify").ToString().Substring(0, 20) + " …… " : Eval("OilClassify")%></a>
                    &#8195;&#8195;&nbsp;&nbsp; &nbsp;
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑">
                <ItemStyle Width="10%" />
                <ItemTemplate>
                    <a class="edit" href='AboutOilEditor.aspx?AboutOilID=<%# Eval("AboutOilID")%>'>编辑</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="删除">
                <ItemStyle Width="10%" />
                <ItemTemplate>
                    <a class="delete"></a>
                    <asp:LinkButton ID="LinkButton1" OnClick="DeleteBtn_Click" runat="server" OnClientClick="return confirm('确定要删除吗?')">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#366B6B" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#366B6B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#366B6B" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="Small" Height="10" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#366B6B" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#366B6B" />
    </asp:GridView>
    <asp:Button ID="Button2" runat="server"  Text="常见问题管理" OnClick="EditFAQ_Click" />
    </asp:Content>
