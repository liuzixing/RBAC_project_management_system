using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

public partial class Account_Login : System.Web.UI.Page
{
    string ReturnUrl;
    CommonClass ccObj = new CommonClass();
    UserClass ucObj = new UserClass();
    DBClass dbObj = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ErrorMessage.Visible = false;
        this.care.Visible = false;

        if (!IsPostBack)//首次加载
        {
            this.RememberMe.Checked = true;
            Session["CheckCode"] = "1111";
            //Page.RegisterStartupScript("ggg", "<script>SetVisible(1);</script>");
            Random num = new Random();
            int num1 = num.Next();
            this.ImageButton1.ImageUrl = "~/Account/VerifyCode.aspx?" + num1;

            if (Session["UserID"] != null)
            {
                Session["UserID"] = null;
                Response.Redirect("~/Index.aspx");
            }
            
        }
        if (Request.QueryString["register"] == "yes")
        {
            
            care.Text = "您的帐号已被成功创建，正在等待管理员的审核...（未审核的用户无法进行登入）";
            this.care.Visible = true;
        }
        if (Request.Cookies["password"] != null)
        {
            if (this.LoginName.Text.Trim() == "")
                this.LoginName.Text = Request.Cookies["UserName"].Value;
            if (this.Password.Text.Trim() == "")
                this.Password.Attributes.Add("Value", Request.Cookies["password"].Value);
        }
        //Response.Cookies.Get("Loginname").Domain;
    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {

        string str= "";



       
        this.ErrorMessage.Visible = false;

        Session["UserID"] = null;
        Session["UserName"] = null;
        string Loginname;
        string UserPassword;
        Loginname = LoginName.Text.Trim();
        UserPassword = Password.Text.Trim();
        if (this.VerificationCode.Text.ToUpper() != Session["CheckCode"].ToString().ToUpper())
        {
            
            ErrorMessage.Text = "验证码错误！";
            this.ErrorMessage.Visible = true;
        }
        else
        {
            DataTable dsTable = ucObj.UserLogin(Loginname, UserPassword);
            
            if (dsTable.Rows.Count != 0)
            {
                if (this.RememberMe.Checked == true)
                {
                    HttpCookie ckUserInfo1 = new HttpCookie("UserName", Loginname);
                    HttpCookie ckUserInfo2 = new HttpCookie("password", UserPassword);
                    ckUserInfo1.Expires = DateTime.Now.AddMonths(2);
                    ckUserInfo2.Expires = DateTime.Now.AddMonths(2);
                    Response.Cookies.Add(ckUserInfo1);
                    Response.Cookies.Add(ckUserInfo2);
                }
                else if (this.RememberMe.Checked == false)
                {
                    HttpCookie ckUserInfo1 = new HttpCookie("UserName");
                    HttpCookie ckUserInfo2 = new HttpCookie("password");
                    ckUserInfo1.Expires = DateTime.Now.AddDays(-1);
                    ckUserInfo2.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(ckUserInfo1);
                    Response.Cookies.Add(ckUserInfo2);

                }

                if (dsTable.Rows[0][5].ToString().Equals("True"))
                {
                    Session["UserID"] = Convert.ToInt32(dsTable.Rows[0][0].ToString());
                    Session["UserName"] = dsTable.Rows[0][1].ToString();
                    updateStatistics();
                    FormsAuthentication.SetAuthCookie((string)Session["UserName"],true,"~/book.txt");
                    Response.Redirect(ResolveUrl("~/admin/Index.aspx"));
                }
                else
                {
                    
                    ErrorMessage.Text = "用户名未被激活，请耐心等候...";
                    this.ErrorMessage.Visible = true;
                }
            }
            else
            {
                
                ErrorMessage.Text = "您的用户名或密码有误，请核对后再重新登录！";
                this.ErrorMessage.Visible = true;
            }
        }

    }
    private void updateStatistics()
    {
        HttpBrowserCapabilities bc = Request.Browser;
        string browser = bc.Type;
        string os = bc.Platform;
        int userID = (int)Session["UserID"];
        string userName = (string)Session["UserName"];
        string str = "insert into Statistics(Browser, OS, UserName, CreateTime) values('" + browser
            + "','" + os + "','" + userName + "',date())";

        dbObj.ExecNonQuery(dbObj.GetCommandStr(str));
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Random num = new Random();
        int num1 = num.Next();
        this.ImageButton1.ImageUrl = "~/Account/VerifyCode.aspx?" + num1;
    }
    protected void aaaaaa(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("FindPassword.aspx");
    }
}

