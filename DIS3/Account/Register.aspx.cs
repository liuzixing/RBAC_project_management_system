using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Configuration;
using System.Data;
public partial class Account_Register : System.Web.UI.Page
{
    private string Code;
    UserClass ucObj = new UserClass();
    DBClass dbObj = new DBClass();
    CommonClass ccObj = new CommonClass();
    string Loginname = "";
    string UserEmail = "";
    string UserPassword = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ErrorMessage.Visible = false;
        HttpContext context = HttpContext.Current;
       if(!IsPostBack)
            Data_Load();
    }
    private bool EmailDetecton(string str)
    {
        string pattern = @"^[0-9a-zA-Z\.\-_]+@([a-zA-Z\.\-_]+\.)+[a-zA-Z]{2,4}$";
        RegexStringValidator rsv = new RegexStringValidator(pattern);
        try
        {
            rsv.Validate(str);
            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void CreateUserButton_Click(object sender, EventArgs e)
    {
        this.ErrorMessage.Visible = false;

        string Loginname;
        string UserEmail;
        string UserPassword;
        Loginname = LoginName.Text.Trim();
        UserEmail = Email.Text.Trim();
        UserPassword = Password.Text.Trim();
        if (Loginname == "" || UserPassword == ""
            || UserEmail == "")
        {
            
            ErrorMessage.Text = "请输入必要信息！";
            this.ErrorMessage.Visible = true;
        }

        else if (UserPassword.Length < 6 || UserPassword.Length > 16)
        {
            
            ErrorMessage.Text = "密码长度不符合要求，请输入6-16位的字符串";
            this.ErrorMessage.Visible = true;
        }
        else if (!EmailDetecton(UserEmail))
        {
            
            ErrorMessage.Text = "邮箱格式不正确！";
            this.ErrorMessage.Visible = true;
        }
        else if (EmaiRegistered(UserEmail))
        {
            ErrorMessage.Text = "邮箱已被注册！";
            this.ErrorMessage.Visible = true;
        }
        else if (this.VerificationCode.Text.ToUpper() == Session["CheckCode"].ToString().ToUpper())
        {
         
            int intReturn = ucObj.AddUsers(Loginname,UserPassword,
                UserEmail, 0);

            if (intReturn == 100)
            {
                Response.Redirect(ResolveUrl("~/Account/Login.aspx?register=yes"));
            }
            else
            {
                
                ErrorMessage.Text = "注册失败，该姓名已存在！";
                this.ErrorMessage.Visible = true;
            }

        }
        else
        {
            
            ErrorMessage.Text = "验证码错误，请重新输入";
            this.ErrorMessage.Visible = true;
        }

    }
    protected void LoginName_TextChanged(object sender, EventArgs e)
    {

    }
 
    protected void Password_TextChanged1(object sender, EventArgs e)
    { 
        UserEmail = this.Email.Text.Trim();
        Loginname = this.LoginName.Text.Trim();
        UserPassword = this.Password.Text.Trim();
        if (UserPassword.Length < 6 || UserPassword.Length > 16)
        {
            this.ErrorMessage.CssClass = "failureNotification";
            ErrorMessage.Text = "密码长度不符合要求，请输入6-16位的字符串";
            this.ErrorMessage.Visible = true;

        }
       
        Data_Load();
    }

    private bool EmaiRegistered(string str)
    {
        string str1 = "select * from Users where Email = '" + str + "'";
        DataTable ds = dbObj.GetDataSet(dbObj.GetCommandStr(str1), "Users");
        if (ds.Rows.Count == 0)
        {
            return false;
        }
        return true;
    }

    private void Data_Load()
    {
        LoginName.Text = Loginname;
        Email.Text = UserEmail;
        Password.Attributes.Add("value", UserPassword);
    }
}
