<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageBarControl.ascx.cs"
    Inherits="Control_PageBarControl" %>
<%@ OutputCache Duration="5" VaryByParam="p" %>
<div class="pagecontrol">
    <div class="pagebar">
        <!-- <%= DataSource.TotalRow %>条数据， -->共<%= DataSource.TotalPage %>页
        <%if (DataSource.TotalRow > 0)
          { %>
        <% if (PageIndex != 1)
           {
               int a = PageIndex - 1;%>
        <a href="<%= BaseURL + "p=1" %>" class="pagebar-first">首页</a> 
        <a href="<%= BaseURL + "p=" + a %>">上一页</a>
        <%} else{%>
           <span class="disabled">首页</span>
           <span class="disabled">上一页</span>
        <% } %>
        <% for (int i = PageIndex - 3; i < PageIndex; i++)
           {
               if (i > 0)
               {%>
        <a href="<%= BaseURL + "p=" + i %>" class="pagebar-prev">
            <%= i%></a>
        <%}
    }%>
        <a href="<%= BaseURL + "p=" + PageIndex %>" class="pagebar-current">
            <%= PageIndex%></a>
        <% for (int j = PageIndex + 1; j < PageIndex + 4 && j <= DataSource.TotalPage; j++)
           {%>
        <a href="<%= BaseURL + "p=" + j %>" class="pagebar-next">
            <%= j%></a>
        <%} %>
        
        <%if (PageIndex != DataSource.TotalPage)
          {
              int a = PageIndex + 1;%>
        <a href="<%= BaseURL + "p=" + a %>">下一页</a> 
        <a href="<%= BaseURL + "p=" + DataSource.TotalPage %>" class="pagebar-last">尾页</a>
        <%}
          else
          {%>
          <span  class="disabled">下一页</span> 
          <span  class="disabled">尾页</span> 
        <%} %>
        <%} %>
    </div>
</div>
