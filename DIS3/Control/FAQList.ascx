<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FAQList.ascx.cs" Inherits="Control_FAQList" %>

<ul class="FAQ">
<asp:Repeater ID="FAQList" runat="server">

 <ItemTemplate>
<li class="question">
 <%=i%><h5>.   问题:</h5><%# Eval("Question") %>

</li>
<li class="answer">
 <h5>&nbsp;&nbsp;&nbsp;&nbsp;答案:</h5><%# Eval("Answer") %>
</li>
 <%i++; %>
 </ItemTemplate>
</asp:Repeater>
</ul>