<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminConsole.master" AutoEventWireup="true" CodeFile="HeadPictureMage.aspx.cs" Inherits="admin_HeadPictureMage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
          <script>
              $(function () {
                  $("#9").show();
                  $("#09").html("-");
                  $("#91").addClass("present");
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
<div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="912px" 
                    BackColor="Red" ForeColor="White"></asp:Label>
            </div>

<div class="bg">
<a href="<% =pictureUrl[0] %>"><img src="<%= picture[0]%>" alt="修改此图片" width="700" height="190" /></a>
<a href="<% =pictureUrl1[0] %>"><img src="<%= picture1[0]%>" alt="修改此侧边条" width="50" height="190" /></a>
</div>
<div  class="bg">
<a href=" <% =pictureUrl[1] %>"><img src="<%= picture[1]%>" alt="修改此图片" width="700" height="190" /></a>
<a   href="<% =pictureUrl1[1] %>"><img src="<%= picture1[1]%>" alt="修改此侧边条" width="50" height="190" /></a>
</div>
<div  class="bg">
<a href="<% =pictureUrl[2] %>"><img src="<%= picture[2]%>" alt="修改此图片" width="700" height="190" /></a>
<a  href="<% =pictureUrl1[2] %>"><img src="<%= picture1[2]%>" alt="修改此侧边条" width="50" height="190" /></a>
</div>

</asp:Content>

