<%@ Page Title="油站项目" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true"
    CodeFile="Project.aspx.cs" Inherits="oilStation_Project" %>

<%@ Register Src="~/Control/ProjectPageBarControl.ascx" TagName="ProjectPageBarControl"
    TagPrefix="uc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../css/Project.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("a:eq(4)").css({ "color": "#E65D06" });
        });
        
    </script>
    <script src="../js/AboutOil.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="bannerWrap">
        <div id="bannerWrap_top">
        </div>
        <div id="navigator">
            <a href="NewRecruitsCenter.aspx">
                <img src="../images/1.jpg" alt="Screenshot 1" width="950" height="296" /></a></div>
    </div>
    <div id="tempate_main">
        <div class="breadCrumb">
            <img src="../images/li_icon2.png" height="15" width="15" alt="" />
            <h5>
                您当前的位置：
                <asp:SiteMapPath ID="SiteMapPath1" runat="server">
                </asp:SiteMapPath>
            </h5>
        </div>
        <div id="tempate_left">
            <div id="Project-nav">
                <ul>
                    <asp:Repeater ID="ProjectList" runat="server">
                        <ItemTemplate>
                            <li class="item"><a href="Project.aspx?OilProjectDirectionID=<%#Eval("OilProjectDirectionID")%>">
                                <%#Eval("OilProjectDirectionTitle")%></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div id="tempate_right">
            <div id="content_right">
                <form id="form1" runat="server">
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                    AutoGenerateColumns="False" Style="text-align: center" Width="710px">
                    <Columns>
                        <asp:TemplateField HeaderText="具体项目" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left " />
                            <ItemStyle Width="10%" />
                            <ItemTemplate>
                                <li><a class="edit" href='OilProjectContent.aspx?OilProjectID=<%# Eval("OilProjectID")%>'>
                                    <%#Eval("OilProjectTitle")%>
                                </a></li>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所属方向" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left " />
                            <ItemStyle Width="10%" />
                            <ItemTemplate>
                                <%#Eval("OilProjectDirectionTitle")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目发布时间" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left " />
                            <ItemStyle Width="10%" />
                            <ItemTemplate>
                                <%# Eval("OilProjectDate")%>
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
                 <div class="yy">
                <uc1:ProjectPageBarControl ID="PageBarControl1" runat="server" TableName="OilProject,OilProjectDirection"
                    PageSize="15" />
            </div>
            </div>
           
            </form>
        </div>
    </div>
    <div class="clear_both">
    </div>
</asp:Content>
