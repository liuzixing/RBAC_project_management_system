using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Configuration;
using System.Web.Security;


public partial class Information : System.Web.UI.Page
{
    private DataSet myDS = new DataSet();
    UserClass usObj = new UserClass();
    DBClass dbObj = new DBClass();
    CommonClass ccObj = new CommonClass();
    string pass = null;
    string email = null;
    DateTime createTime = new DateTime();

    

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ErrorMessage.Visible = false;

        if (!Page.IsPostBack)
        {

            if (Session["UserID"] == null)
            {
                
                ErrorMessage.Text = "请先登录！";
                this.ErrorMessage.Visible = true;
                return;
            }
            Data_Load();
        }
    }

    private void Data_Load()
    {
       
        DataTable user = usObj.GetUserInfo((int)Session["UserID"]);
        pass = user.Rows[0][2].ToString();
        email = user.Rows[0][3].ToString();
        createTime = Convert.ToDateTime(user.Rows[0][4].ToString());
       
        this.tbpass.Text = pass;
        
        this.tbemail.Text = email;

        this.tbtime.Text = createTime.ToString();
    
        
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
 
    protected void Save(object sender, EventArgs e)
    {
        this.ErrorMessage.Visible = false;
        if (Session["UserID"] == null)
        {
            
            ErrorMessage.Text = "请重新登录！";
            this.ErrorMessage.Visible = true;
            
            return;
        }

        if (this.tbemail.Text.Trim() == "" || this.tbpass.Text.Trim() == ""
            || this.tbconfirm.Text.Trim() == "")
        {
            
            ErrorMessage.Text = "请输入必要信息！";
            this.ErrorMessage.Visible = true;

            
        }
        else if (this.tbpass.Text.Trim() != this.tbconfirm.Text.Trim())
        {
            
            ErrorMessage.Text = "密码前后不一致！";
            this.ErrorMessage.Visible = true;

           
        }

        else if (this.tbpass.Text.Trim().Length < 6 || this.tbpass.Text.Trim().Length > 16)
        {
            
            ErrorMessage.Text = "密码长度不符合要求，请输入6-16位的字符串！";
            this.ErrorMessage.Visible = true;

            
        }
        else if (!EmailDetecton(this.tbemail.Text.Trim()))
        {
            
            ErrorMessage.Text = "邮箱格式不正确！";
            this.ErrorMessage.Visible = true;

            
        }
        else
        {
            usObj.ModifyUser((int)Session["ID"], (string)Session["UserName"], pass, email, createTime);
        }
       
    }
    //protected void modifybtn_Click(object sender, EventArgs e)
    //{
    //    this.passPart.Visible = true;
    //    this.tbpass.Enabled = true;
    //    this.tbemail.Enabled = true;

    //}
}