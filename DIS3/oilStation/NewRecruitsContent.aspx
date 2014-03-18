<%@ Page Title="项目介绍" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true"
    CodeFile="NewRecruitsContent.aspx.cs" Inherits="NewRecruits_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../css/newscontent.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("a:eq(6)").css({ "color": "#E65D06" });
        });
        
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="bannerWrap">
        <div id="bannerWrap_top">
        </div>
        <div id="navigator">
            <a href="#">
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
                        <%=n.NewRecruitsDuty %>
                    </h1>
                    <h2 id="news-date">
                        <span>发布时间：</span>
                        <%=n.NewRecruitsDate %>
                    </h2>
                    <div>
                    </div>
                    <hr class="curoff" />
                    <div id="news-cont" style="height: auto;">
                        <%= n.NewRecruitsContent%>
                    </div>
                </div>
                <hr class="curoff" />
            </div>
        </div>
        </form>
    </div>
    <div id="flpage">
        <a href="<%=previousPage[1] %>">上一篇：<%=previousPage[0]%></a><br />
        <a href="<%=nextPage[1] %>">下一篇：<%=nextPage[0]%></a><br />
    </div>
    <div class="clear_both" style="height: auto;">
    </div>
</asp:Content>
