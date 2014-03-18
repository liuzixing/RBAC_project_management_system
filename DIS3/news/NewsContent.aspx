<%@ Page Title="新闻正文" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Site.master"
    CodeFile="NewsContent.aspx.cs" Inherits="NewsContent" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../css/newscontent.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("a:eq(3)").css({ "color": "#e65d06" });
        });
        
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="bannerWrap">
        <div id="navigator">
            <a href="../oilStation/NewRecruitsCenter.aspx">
                <img src="../images/1.jpg" alt="Screenshot 1" width="950" height="296" /></a></div>
    </div>
    <div class="tempate_main">
        <form id="form1" runat="server">
        <div id="newslist-content" style="height: auto;">
            <div id="newslist-content-head" style="height: auto;">
                <div class="breadCrumb">
                    <img src="../images/li_icon2.png" height="15" width="15" alt="" />
                    <h5>
                        您当前的位置：
                        <asp:SiteMapPath ID="SiteMapPath1" runat="server">
                        </asp:SiteMapPath>
                    </h5>
                </div>
                <div class="clear_both" style="height: auto;">
                </div>
                <div id="newslist-con" style="height: auto;">
                    <h1 id="news-title">
                        <%=n.Title %>
                    </h1>
                    <h2 id="news-date">
                        <span>发布时间：</span>
                        <%=n.PublishTime %>
                        &nbsp; &nbsp; <span>作者：</span><%=n.Author %>
                    </h2>
                    <div id="visit">
                        访问次数：<%=n.VisitCount %>
                    </div>
                    <hr class="curoff" />
                    <div id="news-cont" style="height: auto;">
                        <%= n.Content %>
                    </div>
                </div>
                <hr class="curoff" />
            </div>
        </div>
        </form>
    </div>
    <div id="flpage">
        <a href="<%=previousNewsPage[1] %>">上一篇：<%=previousNewsPage[0]%></a><br />
        <a href="<%=nextNewsPage[1] %>">下一篇：<%=nextNewsPage[0]%></a><br />
    </div>
    <div class="clear_both" style="height: auto;">
    </div>
</asp:Content>
