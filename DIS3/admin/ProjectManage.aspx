<%@ Page Title="油站项目管理" Language="C#" MasterPageFile="~/Master/AdminConsole.master" AutoEventWireup="true" CodeFile="ProjectManage.aspx.cs" Inherits="admin_ProjectEditor" %>
<%@ Register Src="~/Control/PageBarControl.ascx" TagPrefix="uc" TagName="PageBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="912px" 
                    BackColor="Red" ForeColor="White"></asp:Label>
            </div>
<h3>油站项目管理</h3>
    请选择方向<asp:DropDownList ID="DropDownList1" runat="server" 
        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" 
        AutoPostBack="True"></asp:DropDownList> 
        <asp:Button ID="Button2" runat="server"  Text="项目方向管理" OnClick="ProjectNameEdit_Click" />
    <br />
    <asp:Button ID="AddProject" runat="server"  Text="添加项目" 
        onclick="AddProject_Click" />
    <asp:Button ID="Button5" runat="server"  Text="批量删除" OnClick="Button2_Click" OnClientClick="return confirm('确定要删除吗?')"/>
    <asp:Button ID="Button1" runat="server"  Text="取消选择" OnClick="Button1_Click" />
    <asp:Button ID="Button6" runat="server"  Text="撤销删除" OnClick="Button5_Click" OnClientClick="return confirm('确定要撤销删除吗?')" />

    <asp:GridView ID="GridView1" runat="server" DataKeyNames="OilProjectID" CellPadding="4" 
        ForeColor="#333333" GridLines="None"  Width="100%" 
        OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="True" PageSize="10" 
        AutoGenerateColumns="False" style="text-align: center">
        <Columns>
            <asp:TemplateField>
                <ItemStyle Width="5%" />
                <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Size="9pt" 
                        OnCheckedChanged="CheckBox2_CheckedChanged"  />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField HeaderText="项目名称" DataField="OilProjectTitle" ItemStyle-HorizontalAlign=Left  />
  
            <asp:TemplateField HeaderText="编辑">
                <ItemStyle Width="10%"/>
                <ItemTemplate>

                    <a class="edit" href='ProjectEditor.aspx?OilProjectID=<%# Eval("OilProjectID")%>'>编辑 </a>

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

