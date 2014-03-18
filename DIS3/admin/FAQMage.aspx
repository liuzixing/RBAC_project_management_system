<%@ Page Title="常用问题管理" Language="C#" MasterPageFile="~/Master/AdminConsole.master"
    AutoEventWireup="true" CodeFile="FAQMage.aspx.cs" Inherits="admin_FAQMage" %>

<%@ Register Src="~/Control/PlanPageBarControl.ascx" TagPrefix="uc" TagName="PageBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script>
        $(function () {
            $("#5").show();
            $("#05").html("-");
            $("#54").addClass("present");
            $("#a5").toggle(function (e) {
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
<h3>常用问题管理</h3>
     <asp:Button ID="AddFAQ" runat="server"  Text="添加问题" OnClick="AddFAQ_Click" />
    <asp:Button ID="Button3" runat="server"  Text="批量删除" OnClick="Button2_Click" />
    <asp:Button ID="Button4" runat="server"  Text="撤销删除" OnClick="Button2_Click" />
    <asp:GridView ID="GridView1" runat="server" Height="219px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
        Width="100%" CellPadding="4" 
        ForeColor="#333333"  GridLines="None"   AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField>
            <ItemStyle Width="5%" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FAQ问题">
                <ItemStyle Width="40%" />
                <ItemTemplate>
                    <a href='FAQEditor.aspx?FAQID=<%# Eval("FAQID")%>'>
                        <%# Eval("Question").ToString().Length > 20 ? Eval("Question").ToString().Substring(0, 20) + " …… " : Eval("Question")%></a>
                    &#8195;&#8195;&nbsp;&nbsp; &nbsp;
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FAQ答案">
                <ItemStyle Width="35%" />
                <ItemTemplate>
                    <%# Eval("Answer").ToString().Length > 20 ? Eval("Answer").ToString().Substring(0, 20) + " …… " : Eval("Answer")%>
                </ItemTemplate>
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="编辑">
                <ItemStyle Width="10%" />
                <ItemTemplate>
                    <a class="edit" href='FAQEditor.aspx?FAQID=<%# Eval("FAQID")%>'>编辑</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="删除">
                <ItemStyle Width="10%" />
                <ItemTemplate>
                    <a class="delete"></a>
                    <asp:LinkButton ID="LinkButton1" OnClick="DeleteBtn_Click" runat="server" OnClientClick="return confirm('确定要删除吗？')">删除</asp:LinkButton>
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
    <uc:PageBar ID="PageBar2" runat="server" TableName="FAQ" PageSize="10" />
    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Size="9pt" OnCheckedChanged="CheckBox2_CheckedChanged"
        Text="全选" />
    <asp:Button ID="Button1" runat="server" Font-Size="9pt" Text="取消" OnClick="Button1_Click" />
    <asp:Button ID="Button2" runat="server" Font-Size="9pt" Text="删除" OnClick="Button2_Click" />
</asp:Content>
