<%@ Page Title="职位类别管理" Language="C#" MasterPageFile="~/Master/AdminConsole.master" AutoEventWireup="true" CodeFile="RecruitCategoryManage.aspx.cs" Inherits="admin_RecruitCategoryManage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
       <script>
           $(function () {

               $("#3").show();
               $("#03").html("-");
               $("#32").addClass("present");
               $("#a3").toggle(function (e) {
                   e.preventDefault();
                   $(this).next().hide();
                   $(this).children(".jia").html("+");
               }, function (e) {
                   e.preventDefault();
                   $(this).next().show();
                   $(this).children(".jia").html("-");
               });
           });
    </script>
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <h3>职位类别管理</h3>
    <asp:Label ID="Label1" runat="server" Text="添加职位类别："></asp:Label>
    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
    <asp:Button ID="AddBtn" runat="server" onclick="AddBtn_Click" Text="添加" />
    <br />
    <asp:Button ID="Button2" runat="server"  Text="批量删除"  
        OnClientClick="return confirm('确定要批量删除吗？')" onclick="Button2_Click"/>
    <asp:Button ID="Button1" runat="server"  Text="取消选择" 
        OnClick="Button1_Click" />
    <asp:Button ID="Button6" runat="server"  Text="撤销删除" OnClick="Button5_Click" OnClientClick="return confirm('确定要撤销删除吗?')"/>

    <asp:GridView ID="GridView1" runat="server" DataKeyNames="NewRecruitsCategoryID" CellPadding="4" 
        ForeColor="#333333" GridLines="None"  Width="100%"
        AutoGenerateColumns="False" style="text-align: center" 
        OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit">
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
            <asp:BoundField DataField="NewRecruitsCategory" HeaderText="职位类别" />
            <asp:CommandField HeaderText="编辑" ShowEditButton="True" />
            <asp:TemplateField HeaderText="删除">
                <ItemStyle Width="10%" />
                <ItemTemplate>
                <a class="delete"></a>
                    <asp:LinkButton ID="LinkButton1" OnClick="DeleteBtn_Click" OnClientClick="return confirm('确定要删除吗？')" runat="server">删除</asp:LinkButton>
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

