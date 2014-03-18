using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class UserManage : System.Web.UI.Page
{
     CommonClass ccObj = new CommonClass();
    UserClass ucObj = new UserClass();
    DBClass dbObj = new DBClass();
    int pageSize = 6;
    int pageIndex = 1;
    String condition = null;
    protected PageResult result;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.createPart.Visible = false;
        this.ErrorMessage.Visible = false;
        this.care.Visible = false;


        if (Session["UserID"] == null)
        {
            
            ErrorMessage.Text = "请先登录！";
            this.ErrorMessage.Visible = true;
            this.btnSearch.Enabled = false;
            this.Button1.Enabled = false;
            return;
           
            
        }

        if (Session["UserName"].ToString() != "lin")
        {
            ErrorMessage.Text = "非管理员用户不能浏览当前页面！";
            this.ErrorMessage.Visible = true;
            this.btnSearch.Enabled = false;
            this.Button1.Enabled = false;
            return;
        }

        if (!IsPostBack)
        {
           
            Data_Load2();
        }

    }

   
    protected void Data_Load2()
    {
       

        if (Request["p"] != null)
            pageIndex = Convert.ToInt32(Request["p"]);
        else
            pageIndex = 1;
        if (Session["condition"] != null && Session["dropdownValue"] != null)
        {
            condition = Session["condition"].ToString();
            this.DropDownList1.SelectedValue = Session["dropdownValue"].ToString();
            
        }
        DataPager p = new ExclusiveDataPager();
        result = p.PageData(pageIndex, pageSize, "Users", "*", "ID", condition, "desc");

        GridView2.DataSource = result.Result;
        GridView2.DataBind();
        
        
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

    protected void registerclk_Click(object sender, EventArgs e)
    {
        this.createPart.Visible = true;
        if (this.userName.Text.Trim() == "" || this.password.Text.Trim() == ""
            || this.email.Text.Trim() == "")
        {
            
            ErrorMessage.Text = "请输入必要信息！";
            this.ErrorMessage.Visible = true;
        }
        
        else if(this.password.Text.Trim().Length < 6 || this.password.Text.Trim().Length > 16)
        {
            
            ErrorMessage.Text = "密码长度不符合要求，请输入6-16位的字符串";
            this.ErrorMessage.Visible = true;

        }
        else if (!EmailDetecton(this.email.Text.Trim()))
        {
            
            ErrorMessage.Text = "邮箱格式不正确！";
            this.ErrorMessage.Visible = true;

            
        }
        else
        {
           
            int intReturn = ucObj.AddUsers(userName.Text.Trim(), password.Text.Trim(),
                email.Text.Trim(), 0);

            if (intReturn == 100)
            {
                
                care.Text = "该用户已成功创建，请通知该用户前往邮箱查收！";
                this.care.Visible = true;
                string str = "<p>用户名  :" + userName.Text.Trim() + "</p><p>密码   :" + password.Text.Trim() +
                    "</p><p>电子邮箱:" + email.Text.Trim() + "</p>";

                Mail.SendMail(this.email.Text.Trim(), "DIS网站:新用户信息", str);
                this.createPart.Visible = false;
            }
            else
            {
                
                ErrorMessage.Text = "创建失败失败，该姓名已存在！";
                this.ErrorMessage.Visible = true;
            }
           
            Data_Load2();

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.createPart.Visible = true;

    }

    protected void UserEdit(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        Data_Load2();
    }

    protected void UserDelete(object sender, GridViewDeleteEventArgs e)
    {
        string str = "delete from Users where UserName='" + e.Values[0].ToString() + "'";
        dbObj.ExecNonQuery(dbObj.GetCommandStr(str));
        Data_Load2();
        this.care.CssClass = "failureNotification";
        care.Text = "用户 " + e.Values[0].ToString()+" 被成功删除！";
        this.care.Visible = true;
    }
    protected void UserDelete2(object sender, GridViewDeleteEventArgs e)
    {
        string str = "delete from Users where UserName='" + e.Values[0].ToString() + "'";
        dbObj.ExecNonQuery(dbObj.GetCommandStr(str));
        Data_Load2();
        this.care.CssClass = "failureNotification";
        care.Text = "用户 " + e.Values[0].ToString() + " 被成功删除！";
        this.care.Visible = true;
    }
    protected void UserUpdate(object sender, GridViewUpdateEventArgs e)
    {
        string k = e.NewValues[1].ToString();

        string str = "update Users set UserName = '" + e.NewValues[1].ToString()
            + "', Email='" + e.NewValues[2].ToString() +
            "',Confirm=" + e.NewValues[3].ToString()
            + " where ID = " + e.NewValues[0];
        dbObj.ExecNonQuery(dbObj.GetCommandStr(str));
        Data_Load2();
    }

    protected void Confirmbtn_Click(object sender, EventArgs e)
    {
      
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                CheckBox c = (CheckBox)GridView2.Rows[i].FindControl("Confirmchb");

                string sql = "update Users set Confirm = " + c.Checked + " where UserName = '"
                    + GridView2.Rows[i].Cells[0].Text.ToString() + "'";
                dbObj.ExecNonQuery(dbObj.GetCommandStr(sql));


            }
        }
    }
    protected void Confirmbtn_Click2(object sender, EventArgs e)
    {

        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                CheckBox c = (CheckBox)GridView2.Rows[i].FindControl("Confirmchb2");

                string sql = "update Users set Confirm = " + c.Checked + " where UserName = '"
                    + GridView2.Rows[i].Cells[0].Text.ToString() + "'";
                dbObj.ExecNonQuery(dbObj.GetCommandStr(sql));


            }
        }
    }
    protected void UserCancel(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        Data_Load2();
    }

    //protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    GridView1.EditIndex = e.NewEditIndex;
    //    bind();
    //}
    //protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    string sqlstr = "delete from 飞狐工作室 where 身份证号码='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
    //    sqlcon = new SqlConnection(strCon);
    //    sqlcom = new SqlCommand(sqlstr, sqlcon);
    //    sqlcon.Open();
    //    sqlcom.ExecuteNonQuery();
    //    sqlcon.Close();
    //    bind();
    //}
    //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    sqlcon = new SqlConnection(strCon);
    //    string sqlstr = "update 飞狐工作室 set 姓名='"
    //        + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim() + "',家庭住址='"
    //        + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim() + "' where 身份证号码='"
    //        + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
    //    sqlcom = new SqlCommand(sqlstr, sqlcon);
    //    sqlcon.Open();
    //    sqlcom.ExecuteNonQuery();
    //    sqlcon.Close();
    //    GridView1.EditIndex = -1;
    //    bind();
    //}
    //protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    GridView1.EditIndex = -1;
    //    bind();
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string key = this.tbSearch.Text.Trim();

        if (this.DropDownList1.SelectedValue == "全部用户")
            condition = " UserName like '%" + key + "%'";
        else if (this.DropDownList1.SelectedValue == "未激活用户")
            condition = " UserName like '%" + key + "%' and Confirm = False";
        else
            condition = " UserName like '%" + key + "%' and Confirm = True";
        //DataTable ds = dbObj.GetDataSet(dbObj.GetCommandStr(str), "Users");
        //this.GridView2.DataSource = ds;
        //GridView2.DataBind();
        Session["condition"] = condition;
        Session["dropdownValue"] = this.DropDownList1.SelectedValue;
        Data_Load2();
       
        
        //Data_Load2();
       

        //for (int i = 0; i < ds.Rows.Count; i++)
        //{
        //    if (ds.Rows[4][i].ToString() == "False") { 
        //        this.GridView2.DataSource.
        //    }
        //}

    }
}