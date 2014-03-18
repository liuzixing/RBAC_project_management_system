<%@ Page Title="油站新闻" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Site.master"
    CodeFile="NewsTempate.aspx.cs" Inherits="NewsTempate" %>

<%@ Register Src="~/Control/PageBarControl.ascx" TagName="PageBarControl" TagPrefix="uc1" %>
<%@ Register Src="~/Control/NewsDateTree.ascx" TagName="NewsDateTree" TagPrefix="uc" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../css/NewsTempate.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/newlist.js"></script>
    <script type="text/javascript">
        $(function () {
            $("a:eq(3)").css({ "color": "#e65d06" });
        });
        
    </script>
    <!-- <script type="text/javascript" src="../js/newlist.js"></script>  -->
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="bannerWrap">
    <div id="bannerWrap_top">
        </div>
        <div id="navigator">
            <a href="../oilStation/NewRecruitsCenter.aspx">
                <img src="../images/1.jpg" alt="Screenshot 1" width="950" height="296" /></a></div>
    </div>
    <div class="tempate_main">
        <div class="breadCrumb">
            <img src="../images/li_icon2.png" height="15" width="15" alt="" />
            <h5>
                您当前的位置：
                <asp:SiteMapPath ID="SiteMapPath1" runat="server">
                </asp:SiteMapPath>
            </h5>
        </div>
        <div id="newslist-container">
            <div id="newslist-nav-content">
                <uc:NewsDateTree ID="NewsDateTree" runat="server" />
            </div>
        </div>
        <div id="content_right">
            <!--  <iframe src="<%=newsFrame %>" name="news" id="cwin" class="Tempate_right"  style="background-color:#F9F9F9 ;" frameborder="0" scrolling ="no" ></iframe>
    -->
            <div class="Tempate_right">
                <form id="form1" runat="server">
                <div id="gridView">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="5"
                        GridLines="none" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1" Width="710px">
                        <Columns>
                            <asp:TemplateField HeaderText="标题">
                                <HeaderStyle HorizontalAlign="Left " />
                                <ItemStyle Height="22px" Width="66%" />
                                <ItemTemplate>
                                    <div class="news_icon">
                                    </div>
                                    <a href='NewsContent.aspx?newsid=<%# Eval("NewsID")%>'
                                        target="_blank">
                                        <%# Eval("NewsTitle").ToString().Length > 30 ? Eval("NewsTitle").ToString().Substring(0, 30) + " …… " : Eval("NewsTitle")%>
                                    </a>&nbsp;&nbsp; &nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="作者">
                                <HeaderStyle HorizontalAlign="Left " />
                                <ItemStyle Height="22px" Width="22%" />
                                <ItemTemplate>
                                    <%# Eval("NewsAuthor") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="时间">
                                <HeaderStyle HorizontalAlign="Left " />
                                <ItemStyle Height="22px" Width="12%" />
                                <ItemTemplate>
                                    <%#  Eval("NewsDate").ToString().Length <= 17 ? Eval("NewsDate").ToString().Substring(0, 9) : Eval("NewsDate").ToString().Substring(0, 10)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#EDEDED" />
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
                    <uc1:PageBarControl ID="PageBarControl1" runat="server" TableName="News" PageSize="15" />
                </div>
                </form>
            </div>
        </div>
    </div>
    <div class="clear_both">
    </div>
</asp:Content>
