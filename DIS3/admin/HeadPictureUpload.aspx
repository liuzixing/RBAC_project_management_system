<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminConsole.master" AutoEventWireup="true" CodeFile="HeadPictureUpload.aspx.cs" Inherits="admin_HeadPictureUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <%if (Request["Picture"] != null)
      { %>
    <img src="<%=picture[a-1]%>" alt="修改此图片"style=" max-height:500px; max-width:800px;" />
      <asp:FileUpload ID="InserImg" runat="server" Width="191px" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click"  Text="修改" />
    <div style="display:none">
     <asp:TextBox ID="ImgPath" runat="server"  ></asp:TextBox>
    </div>
    <%}
      else
      { %>
    <p>未选择该修改的图片</p>

   <%} %>
  
 
</asp:Content>

