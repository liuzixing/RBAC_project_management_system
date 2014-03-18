<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewRecruitsList.ascx.cs" Inherits="Control_NewRecruitsList" %>

<asp:Repeater ID="NewRecruitsList" runat="server">

 <ItemTemplate>
 <ul class="NewRecruitsList">
<li>
<a class="NewRecruitsDuty" href="#"><span class="jia">+</span><%# Eval("NewRecruitsDuty")%></a>
</li>

<li class="NewRecruitsContent">
<%# Eval("NewRecruitsContent")%>
</li>
</ul>
 </ItemTemplate>
</asp:Repeater>

