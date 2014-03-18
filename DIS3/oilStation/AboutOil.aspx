<%@ Page Title="关于油站" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true"
    CodeFile="AboutOil.aspx.cs" Inherits="oilStation_aboutUs" %>

<%@ Register Src="~/Control/FAQList.ascx" TagPrefix="uc" TagName="FAQList" %>
<%@ Register Src="~/Control/FAQpageBarControl.ascx" TagName="FAQpageBarControl" TagPrefix="uc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../css/aboutUs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/AboutOil.js"></script>
    <script type="text/javascript">
        $(function () {
            $("a:eq(2)").css({ "color": "#E65D06" });
        });

    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="bannerWrap">
        <div id="bannerWrap_top">
        </div>
        <div id="navigator">
                <img src="../images/1.jpg" alt="Screenshot 1" width="950" height="296" /></div>
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
            <div id="aboutUs-nav">
                <ul>
                    <asp:Repeater ID="classify" runat="server">
                        <ItemTemplate>
                            <li class="item"><a href="<%#ResolveUrl(((System.Xml.XmlNode)Container.DataItem).Attributes["url"].Value) %>">
                                <%#((System.Xml.XmlNode)Container.DataItem).Attributes["title"].Value%></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div id="tempate_right">
            <div id="content_right">
                <% int i = 5;
                   if (Request["AboutOilID"] != null)
                   { i = Convert.ToInt32(Request["AboutOilID"].Trim()); }
                   if (i != 0)
                   { %>
                <%= n.OilContent%>
                <%  }
                   else
                   {%>
                <uc:FAQList ID="FAQList" runat="server" />
                <div id="FAQpageBarControl">
                    <uc1:FAQpageBarControl runat="server" TableName="FAQ" PageSize="7" />
                </div>
                <%  }%>
            </div>
        </div>
        <div class="clear_both">
        </div>
    </div>
</asp:Content>
