<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminConsole.master" AutoEventWireup="true" CodeFile="button.aspx.cs" Inherits="admin_button" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
         <script>
             $(function () {
                 $("#9").show();
                 $("#09").html("-");
                 $("#92").addClass("present");
                 $("#a9").toggle(function (e) {
                     e.preventDefault();
                     $(this).next().hide();
                     $(this).children(".jia").html("+");
                 }, function (e) {
                     e.preventDefault();
                     $(this).next().show();
                     $(this).children(".jia").html("-");
                 });
             });
     </script>
<link href="../css/HeadPictureMage.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<div class="bg">

<a href="<% =buttonImageUrl[0] %>"><img src="<%= backButtonImage[0]%>" alt="修改此图标" width="180" height="90" /></a>
</div>
<!-- <div  class="bg">
<a href=" <% =buttonImageUrl[1] %>"><img src="<%= backButtonImage[1]%>" alt="修改此图标" width="29" height="29" /></a>
</div>
<div  class="bg">
<a href=" <% =buttonImageUrl[2] %>"><img src="<%= backButtonImage[2]%>" alt="修改此图标" width="29" height="29" /></a>
</div>
<div  class="bg">
<a href=" <% =buttonImageUrl[3] %>"><img src="<%= backButtonImage[3]%>" alt="修改此图标" width="29" height="29" /></a>
</div>
<div  class="bg">
<a href=" <% =buttonImageUrl[4] %>"><img src="<%= backButtonImage[4]%>" alt="修改此图标" width="29" height="29" /></a>
</div>
<div  class="bg">
<a href=" <% =buttonImageUrl[5] %>"><img src="<%= backButtonImage[5]%>" alt="修改此图标" width="29" height="29" /></a>
</div>
<div  class="bg">
<a href=" <% =buttonImageUrl[6] %>"><img src="<%= backButtonImage[6]%>" alt="修改此图标" width="29" height="29" /></a>
</div>
<div  class="bg">
<a href=" <% =buttonImageUrl[7] %>"><img src="<%= backButtonImage[7]%>" alt="修改此图标" width="50" height="30" /></a>
</div>
<div  class="bg">
<a href="<% =buttonImageUrl[8] %>"><img src="<%= backButtonImage[8]%>" alt="修改此图标" width="35" height="22" /></a>
</div>
<div  class="bg">
<a href=" <% =buttonImageUrl[9] %>"><img src="<%= backButtonImage[9]%>" alt="修改此图标" width="35" height="22" /></a>
</div>
<div  class="bg">
<a href=" <% =buttonImageUrl[10] %>"><img src="<%= backButtonImage[10]%>" alt="修改此图标" width="16" height="16" /></a>
</div>
<div  class="bg">
<a href=" <% =buttonImageUrl[11] %>"><img src="<%= backButtonImage[11]%>" alt="修改此图标" width="16" height="16" /></a>
</div>
<div  class="bg">
<a href=" <% =buttonImageUrl[12] %>"><img src="<%= backButtonImage[12]%>" alt="修改此图标" width="16" height="16" /></a>
</div>
 -->
</asp:Content>

