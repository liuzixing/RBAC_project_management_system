<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
	<asp:Label ID="msg" runat="server" Text=""></asp:Label>
    <div>
		<table>
			<tr>
				<td>用户</td>
				<td>
					<asp:TextBox ID="user" runat="server"></asp:TextBox>
					<asp:TextBox ID="prevURL" runat="server" Visible="False"></asp:TextBox>
					</td>
			</tr>
			<tr>
				<td>密码</td>
				<td>
					<asp:TextBox ID="password" TextMode="Password" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td colspan="2">
					<asp:Button ID="submit" runat="server" Text="登录" onclick="submit_Click" /></td>
			</tr>
		</table>
    </div>
    </form>
</body>
</html>
