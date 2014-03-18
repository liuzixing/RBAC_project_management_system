using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    
    private string userName = "administrator"; // 临时
    private string userPwd = "20101010"; // 临时
    protected void Page_Load(object sender, EventArgs e)
    {
        msg.Text = "";
  
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        if (user.Text == null || password.Text == null)
        {
            return;
        }
        if ((user.Text.ToLower() != userName) || (password.Text.ToLower() != userPwd) )
        {
            msg.Text = "用户名密码不正确";
            return;
        }

        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket("administrator", true, 30); // "administrator" 临时
        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        Response.Cookies.Add(authCookie);

         if (String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
         {
             // Response.Redirect(FormsAuthentication.DefaultUrl);
              Response.Redirect("~/admin/Index.aspx");
         }
         else
         {
             //string url = Request.QueryString["ReturnUrl"]; // 测试
             //if (url.Substring(0, 4) == "/DIS")
             //{
             //    url = "~" + url.Substring(4, (url.Length - 4));
             //}
             //Response.Redirect(url);
             Response.Redirect(Request.QueryString["ReturnUrl"]);
         }
    }
    //{
    //    if (user.Text == null || password.Text == null)
    //    {
    //        return;
    //    }
    //    Users u = Users.Login(user.Text, password.Text);
    //    if (u == null)
    //    {
    //        msg.Text = "用户名密码不正确";
    //        return;
    //    }

    //    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(u.UserName, true, 30);
    //    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
    //    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
    //    Response.Cookies.Add(authCookie);

    //    if (String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
    //    {
    //        Response.Redirect(FormsAuthentication.DefaultUrl);
    //    }
    //    else
    //    {
    //        Response.Redirect(Request.QueryString["ReturnUrl"]);
    //    }
    //}
}
