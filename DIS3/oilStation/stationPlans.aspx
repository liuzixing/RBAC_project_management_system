<%@ Page Title="油站计划" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true"
    CodeFile="stationPlans.aspx.cs" Inherits="stationPlans" %>

<%@ Register Src="~/Control/PlanList.ascx" TagPrefix="uc" TagName="PlanList" %>
<%@ Register Src="~/Control/PlanPageBarControl.ascx" TagName="PlanPageBarControl"
    TagPrefix="uc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../css/stationPlans.css" rel="stylesheet" type="text/css" />
    <link href="../css/PlanList.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/AboutOil.js"></script>
    <script type="text/javascript">
        $(function () {
            $("a:eq(5)").css({ "color": "#E65D06" });
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
            <div id="stationPlan-nav">
                <ul>
                    <li class="item"><a href="<%=planUrl[0] %>">
                        <%=planName[0]%></a></li>
                    <li class="item"><a href="<%=planUrl[1] %>">
                        <%=planName[1]%></a></li>
                    <li class="item"><a href="<%=planUrl[2] %>">
                        <%=planName[2]%></a></li>
                </ul>
            </div>
        </div>
        <div id="tempate_right">
            <div id="content_right" style="background-color: #F9F9F9;">
                <uc:PlanList ID="PlanList" runat="server" />
                <div class="yy">
                    <uc1:PlanPageBarControl ID="PageBarControl1" runat="server" TableName="StationPlan"
                        PageSize="15" />
                </div>
            </div>
        </div>
    </div>
    <div class="clear_both">
    </div>
</asp:Content>
