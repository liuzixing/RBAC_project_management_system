
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_FindPassword : System.Web.UI.Page
{
    private string Code = "admin123";//加密所使用的八位的key
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ErrorMessage.Visible = false;
        if (Request.QueryString["state"] == "successed")
        {
            
            ErrorMessage.Text = "一封验证邮件已经发送到你的邮箱，请登陆你的邮箱查看更改密码!";
            this.ErrorMessage.Visible = true;
            this.LoginName.Enabled = false;
            this.Email.Enabled = false;
        }
    }
    protected void ChangePasswordButton_Click(object sender, EventArgs e)
    {

        if (this.VerificationCode.Text.ToUpper() == Session["CheckCode"].ToString().ToUpper())
        {
            this.ErrorMessage.Visible = false;
            if (Users.isMatch(this.LoginName.Text, this.Email.Text))
            {
                Session["UserName"] = this.LoginName.Text;
                //string url = "http://localhost:3154/RBAC3/Account/NewPasswordeA899288FF3CEF4197989BB69C14D1369E4589A787D4D1D27.aspx";
                //string msg = "<p>请尽快点击<a href=\"" + url + "\">此处</a>更改你的密码，或者点击以下网址:</p><br>" + url;
                //Mail.SendMail(this.Email.Text, "DIS网站:修改密码", msg);
                //Response.Redirect(ResolveUrl("~/Account/FindPassword.aspx?state=successed"));

                string url = HttpContext.Current.Request.Url.Host + "/Account/NewPassword.aspx?e=" + (DES.Encrypt(this.Email.Text, Code));
                string msg = "<p>请点击<a href=\"" + url + "\">此处</a>更改你的密码，或者点击以下网址:</p><br>" + url;
                Mail.SendMail(this.Email.Text, "DIS网站:修改密码", msg);
                Response.Redirect(ResolveUrl("~/Account/FindPassword.aspx?state=successed"));
            }
            else
            {
                
                ErrorMessage.Text = "用户名不存在或邮箱跟用户名不一致";
                this.ErrorMessage.Visible = true;
            }
        }
        else
        {
            
            ErrorMessage.Text = "验证码错误，请重新输入";
            this.ErrorMessage.Visible = true;
        }
    }
}
