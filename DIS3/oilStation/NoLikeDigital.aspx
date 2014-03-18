<%@ Page Title="无象数码" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true"
    CodeFile="NoLikeDigital.aspx.cs" Inherits="oilStation_NoLikeDigital" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../css/NoLikeDigital.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("a:eq(9)").css({ "color": "#E65D06" });
        });
    </script>
    <script src="../js/AboutOil.js" type="text/javascript"></script>
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

            <div id="NoLikeDigital-nav">
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
                <%=n.DigitalContent%>
            </div>
        </div>
    </div>
    <div class="clear_both">
    </div>
</asp:Content>
