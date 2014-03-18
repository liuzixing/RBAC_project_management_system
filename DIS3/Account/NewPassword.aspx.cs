using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_NewPassword : System.Web.UI.Page
{
    CommonClass ccObj = new CommonClass();
    UserClass ucObj = new UserClass();
    DBClass dbObj = new DBClass();
    private string em, state;
    private string Code = "admin123";//解密所使用的八位的key，必须跟加密的一样

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ErrorMessage.Visible = false;
        em = Request.QueryString["e"];
        state = (string)Request.QueryString["state"];
        if (state == "successed")
        {
            
            ErrorMessage.Text = "您的密码已更改,请返回登录页登录";
            this.ErrorMessage.Visible = true;
            this.LoginName.Enabled = false;
        }
        else if (em == null)
        {
            
            ErrorMessage.Text = "页面失效，请重新从邮箱打开相应的链接";
            this.ErrorMessage.Visible = true;
            this.LoginName.Enabled = false;
        }
        else
        {
            string email = DES.Decrypt(em, Code);
            this.Email.Text = email;
            this.Email.Enabled = false;
        }
    }
    protected void OK_Button_Click(object sender, EventArgs e)
    {
        this.ErrorMessage.Visible = false;
        if (Users.isMatch(this.LoginName.Text, this.Email.Text))
        {
            string UserLoginname;
            string UserPassword;
            UserLoginname = Session["UserName"].ToString();
            UserPassword = this.NewPassword.Text.Trim();
            if (ucObj.updatePassword(UserLoginname, UserPassword))
            {
                Response.Redirect(ResolveUrl("~/Account/NewPassword.aspx?state=successed"));
                Session["UserName"] = null;
            }
            else
            {
                
                ErrorMessage.Text = "输入的信息不正确，请检查后再输入！";
                this.ErrorMessage.Visible = true;
            }
        }
        else
        {
            
            ErrorMessage.Text = "用户名与注册邮箱不一致";
            this.ErrorMessage.Visible = true;
        }
    }
    
}