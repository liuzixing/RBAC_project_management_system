<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsList.aspx.cs" Inherits="NewsList" %>

<%@ Register src="~/Control/PageBarControl.ascx" tagname="PageBarControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/NewsList.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
   
   <div id="gridView">
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  
            CellPadding="5" GridLines="Horizontal" Width="710px" BorderWidth="1px" 
         BorderColor="#E7E7FF" 
           onselectedindexchanged="GridView1_SelectedIndexChanged1" >

                        <Columns>
                            <asp:TemplateField HeaderText = "标题">
                                <HeaderStyle   HorizontalAlign= "Left "   />
                                <ItemStyle Width="56%" Height="22px" />
                                <ItemTemplate>
                                    <div class="news_icon"></div>
                                    <a href='NewsContent.aspx?newsid=<%# Eval("NewsID")%>&NewsCategoryName=<%=Request["NewsCategoryName"] %>' target="_blank">                                      
                                        <%# Eval("NewsTitle").ToString().Length > 20 ? Eval("NewsTitle").ToString().Substring(0, 20) + " …… " : Eval("NewsTitle")%></a>
                                    &#8195;&#8195;&nbsp;&nbsp; &nbsp; 
                                </ItemTemplate>
                            </asp:TemplateField >
                            <asp:TemplateField HeaderText = "作者">
                                <HeaderStyle   HorizontalAlign= "Left "   />
                                <ItemStyle Width="22%" Height="22px" />
                                <ItemTemplate>
                                    <%# Eval("NewsAuthor") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText = "时间">
                                <HeaderStyle   HorizontalAlign= "Left "   />
                                <ItemStyle Width="22%" Height="22px" />
                                <ItemTemplate>
                                    <%# Eval("NewsDate") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                         <AlternatingRowStyle BackColor="#d9d8d8" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#d9d8d8" Font-Bold="True" ForeColor="#161617" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle BackColor="#F9F9F9" ForeColor="#161617" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                     
    </asp:GridView>
    </div>
    <div class="yy">
   <uc1:PageBarControl ID="PageBarControl1" runat="server" TableName="News" PageSize="12" />
   </div>
     </form>
</body>
</html>
