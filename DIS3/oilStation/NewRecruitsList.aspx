<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewRecruitsList.aspx.cs"
    Inherits="oilStation_NewRecruitsList" %>

<%@ Register Src="~/Control/NewRecruitsPageBarControl.ascx" TagName="NewRecruitsPageBarControl"
    TagPrefix="uc1" %>
<%@ Register Src="~/Control/NewRecruitsList.ascx" TagName="NewRecruitsList" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" src="<%=ResolveUrl("~/js/jquery-1.5.1.js")%>"></script>
    <script type="text/javascript" src="../js/NewRecruitsList.js"></script>
    <title></title>
    <link href="../css/NewRecruitsList.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="gridView">
        <uc:NewRecruitsList ID="NewRecruitsList" runat="server" />
    </div>
    <!-- <div class="yy">
   <uc1:NewRecruitsPageBarControl ID="PageBarControl1" runat="server" TableName="NewRecruits" PageSize="7" />
   </div>
   -->
    </form>
</body>
</html>
