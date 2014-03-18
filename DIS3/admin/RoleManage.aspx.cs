using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class RoleManage : System.Web.UI.Page
{
    DBClass dbObj = new DBClass();
    CommonClass ccObj = new CommonClass();
    protected void Page_Load(object sender, EventArgs e)
    {

        this.ErrorMessage.Visible = false;
        if (Session["UserID"] == null)
        {

            ErrorMessage.Text = "请先登录！";
            this.ErrorMessage.Visible = true;
            this.createRole.Enabled = false;
            this.create.Enabled = false;
            this.GridView1.Visible = false;
            return;


        }

        if (Session["UserName"].ToString() != "lin")
        {
            ErrorMessage.Text = "非管理员用户不能浏览当前页面！";
            this.ErrorMessage.Visible = true;
            this.createRole.Enabled = false;
            this.create.Enabled = false;
            this.GridView1.Visible = false;
            return;
        }

        if (!Page.IsPostBack)
        {
            if (Session["UserID"] == null)
            {
                
                ErrorMessage.Text = "请先登录！";
                this.ErrorMessage.Visible = true;
                //return;
            }
            ArrayList myArrayList = new ArrayList();
            myArrayList.Add("查看文件");
            myArrayList.Add("删除文件");
            myArrayList.Add("上传文件");
            myArrayList.Add("下载文件");
            myArrayList.Add("个人信息");
            
            permCheck.DataSource = myArrayList;
            this.createField.Visible = false;
            permCheck.DataBind();

           // Data_Load();
        }
       // this.createField.Visible = false;
    }

    
    protected void createRole_Click(object sender, EventArgs e)
    {
        this.createField.Visible = true;
    }
    protected void create_Click(object sender, EventArgs e)
    {
        if (this.roleName.Text == "")
        {
            ErrorMessage.Text = "请输入角色名！";
            this.ErrorMessage.Visible = true;
        }
        else
        {
            string str1 = "insert into RolePermission([RoleName], [ReadFile],[DeleteFile],[Upload],[DownLoad],[ReadSelf]) values ('" + this.roleName.Text
                              + "'," + permCheck.Items[0].Selected + "," + permCheck.Items[1].Selected + "," + permCheck.Items[2].Selected
                              + "," + permCheck.Items[3].Selected + "," + permCheck.Items[4].Selected + ")";

            dbObj.ExecScalar(dbObj.GetCommandStr(str1));
            Response.Redirect("~/admin/RoleManage.aspx");
        }
    }
}