<%@ Page Title="编辑计划" Language="C#" MasterPageFile="~/Master/AdminConsole.master"
    AutoEventWireup="true" CodeFile="PlanMage.aspx.cs" Inherits="admin_PlanMage" %>
<%@ Register Src="~/Control/PageBarControl.ascx" TagPrefix="uc" TagName="PageBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
<div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="912px" 
                    BackColor="Red" ForeColor="White"></asp:Label>
            </div>
    <h3>
        油站计划管理</h3>
    <asp:Button ID="AddPlan" runat="server" Text="添加计划" OnClick="AddPlan_Click" />
    <asp:Button ID="Button5" runat="server" Text="批量删除" OnClick="Button2_Click" OnClientClick="return confirm('确定要删除吗?')" />
    <asp:Button ID="Button3" runat="server" Text="取消选择" OnClick="Button1_Click" />
    <asp:Button ID="Button6" runat="server" Text="撤销删除" OnClick="Button5_Click" OnClientClick="return confirm('确定要撤销删除吗?')" />
    <asp:GridView ID="GridView1" runat="server" DataKeyNames="StationPlanID" CellPadding="4"
        ForeColor="#333333" GridLines="None" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
        AllowPaging="True" PageSize="10" AutoGenerateColumns="False" Style="text-align: center">
        <Columns>
            <asp:TemplateField>
                <ItemStyle Width="5%" />
                <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Size="9pt" OnCheckedChanged="CheckBox2_CheckedChanged" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计划标题" ItemStyle-HorizontalAlign="Left">
                <ItemStyle />
                <ItemTemplate>
                    <a href='PlanEditor.aspx?StationPlanID=<%# Eval("StationPlanID")%>'>
                        <%# Eval("PlanName").ToString().Length > 20 ? Eval("PlanName").ToString().Substring(0, 20) + " …… " : Eval("PlanName")%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PlanClassify" HeaderText="所属计划类别" />
            <asp:BoundField DataField="PlanDate" HeaderText="计划时间" />
            <asp:TemplateField HeaderText="编辑">
                <ItemStyle Width="10%" />
                <ItemTemplate>
                    <a class="edit" href='PlanEditor.aspx?StationPlanID=<%# Eval("StationPlanID")%>'>编辑
                        <a />
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
</asp:Content>
