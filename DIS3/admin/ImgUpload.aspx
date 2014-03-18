<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImgUpload.aspx.cs" Inherits="admin_ImgUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="imgUpload" runat="server" >
    
    <asp:FileUpload ID="InserImg" runat="server" Width="191px" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="确定" />
    <div style="display:none">
     <asp:TextBox ID="ImgPath" runat="server"  ></asp:TextBox>
    </div>
    </form>
</body>
</html>
