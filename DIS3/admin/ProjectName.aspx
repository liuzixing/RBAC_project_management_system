<%@ Page Title="油站方向管理" Language="C#" MasterPageFile="~/Master/AdminConsole.master" AutoEventWireup="true" CodeFile="ProjectName.aspx.cs" Inherits="admin_NewRecruitsCategoryProjectNameEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <script>
     $(function () {

         $("#4").show();
         $("#04").html("-");
         $("#41").addClass("present");
         $("#a4").toggle(function (e) {
             e.preventDefault();
             $(this).next().hide();
             $(this).children(".jia").html("+");
         }, function (e) {
             e.preventDefault();
             $(this).next().show();
             $(this).children(".jia").html("-");
         });

         if (window.location.search == "") {
             $('#ContentPlaceHolder_NEWC').val('添加');
             $('#direction').text("请选择方向名称: ");
             $('#project').text("请填写项目名称: ");
         }

     });

    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <h3>项目方向管理</h3>
     <asp:Label ID="Label1" runat="server" Text="添加项目方向："></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="addBtn" runat="server" onclick="addBtn_Click" Text="添加" />
    <br />
    &nbsp;<asp:Button ID="Button5" runat="server"  Text="批量删除" OnClick="Button2_Click" OnClientClick="return confirm('确定要删除吗?')"/>
    <asp:Button ID="Button1" runat="server"  Text="取消选择" OnClick="Button1_Click" />
    <asp:Button ID="Button6" runat="server"  Text="撤销删除" OnClick="Button5_Click" OnClientClick="return confirm('确定要撤销删除吗?')" />
    <asp:GridView ID="GridView1" runat="server"  CellPadding="4" ForeColor="#333333" GridLines="None"  Width="100%"
        AutoGenerateColumns="False" AllowPaging="True" PageSize="8"  OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting"
       DataKeyNames="OilProjectDirectionID" style="text-align: center">
        <Columns>
            <asp:TemplateField>
                <ItemStyle Width="5%" />
                <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Size="9pt" 
                        OnCheckedChanged="CheckBox2_CheckedChanged" Text="" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField HeaderText="项目方向名称" DataField="OilProjectDirectionTitle" ItemStyle-HorizontalAlign=Left  />
       
             <asp:CommandField ShowDeleteButton="false" ShowEditButton="True" HeaderText="编辑" />
             <asp:TemplateField HeaderText="删除">
                <ItemStyle Width="10%" />
                <ItemTemplate>
                
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
 
    <asp:Button ID="Button3" runat="server" Text="上一页" OnClick="Back_Click" 
        Enabled="False" Visible="False"/>
    &nbsp;<asp:Label ID="pagesL" runat="server" Text="1" Visible="False"></asp:Label>
&nbsp;<asp:Button ID="Button4" runat="server" Text="下一页" OnClick="Next_Click" 
        Enabled="False" Visible="False" />        
   
    <br />
</asp:Content>

