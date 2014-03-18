<%@ Page Language="C#" MasterPageFile="~/Master/AdminConsole.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="NewsManage.aspx.cs" Inherits="admin_Default" Title="油站新闻管理" %>

<%@ Register Src="~/Control/PageBarControl.ascx" TagPrefix="uc" TagName="PageBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <script type="text/javascript" src="../ckeditor/ckeditor.js"></script>
    <div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="912px" 
                    BackColor="Red" ForeColor="White"></asp:Label>
            </div>
    <h3>油站新闻管理</h3>
   
     <asp:Button ID="AddNews" runat="server"  Text="添加新闻" OnClick="AddNews_Click" />
    <asp:Button ID="Button2" runat="server"  Text="批量删除" OnClick="Button2_Click" OnClientClick="return confirm('确定要批量删除吗？')" />
    <asp:Button ID="Button1" runat="server" Text="取消选择" 
        OnClick="Button1_Click" />
    <asp:Button ID="Button3" runat="server"  Text="撤销删除" OnClick="Button5_Click" />
    <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging"
        Width="100%" AutoGenerateColumns="False" CellPadding="4" AllowPaging="True" PageSize="10" DataKeyNames="NewsID"
        ForeColor="#333333" GridLines="None" style="text-align: center">
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
            <asp:BoundField HeaderText="新闻标题" DataField="NewsTitle" ItemStyle-HorizontalAlign=Left  />
            
            <asp:TemplateField HeaderText="时间">
                <ItemStyle Width="10%" />
                <ItemTemplate>
                     <%#  Eval("NewsDate").ToString().Length <= 17 ? Eval("NewsDate").ToString().Substring(0, 9) : Eval("NewsDate").ToString().Substring(0, 10)%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="作者">
                <ItemStyle Width="15%" />
                <ItemTemplate>
                    <%# Eval("NewsAuthor")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑">
                <ItemStyle Width="10%" />
                <ItemTemplate>
                    <a class="edit" href='NewsEditor.aspx?newsid=<%# Eval("NewsID")%>'>编辑</a>
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
    <uc:PageBar ID="PageBar2" runat="server" TableName="News" PageSize="10" Visible=false/>
    <!--  改
 <div id="newfoot">
    新闻标题<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label1" runat="server" Text="新闻内容" ></asp:Label>
   
    <form target="_self" action="News.aspx" method="post">



      </form>
    <br />
 </div>  -->

</asp:Content>





 