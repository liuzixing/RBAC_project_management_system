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
   // int xx = Convert.ToInt32(Request.QueryString["RoleID"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ErrorMessage.Visible = false;
        this.ErrorMessage.Visible = false;
        if (Session["UserID"] == null)
        {

            ErrorMessage.Text = "请先登录！";
            this.ErrorMessage.Visible = true;
            this.Button1.Enabled = false;
            return;


        }

        if (Session["UserName"].ToString() != "lin")
        {
            ErrorMessage.Text = "非管理员用户不能浏览当前页面！";
            this.ErrorMessage.Visible = true;
            this.Button1.Enabled = false;
            return;
        }
      
        if (!Page.IsPostBack)
        {

            string str = "select [RoleName] from RolePermission where ID = " + Request.QueryString["RoleID"].ToString();
            DataTable Rolename = dbObj.GetDataSet(dbObj.GetCommandStr(str), "RolePermission");
            this.RoleName.Text = Rolename.Rows[0][0] + "的权限管理";
            ArrayList myArrayList = new ArrayList();
            myArrayList.Add("查看文件");
            myArrayList.Add("删除文件");
            myArrayList.Add("上传文件");
            myArrayList.Add("下载文件");
            myArrayList.Add("个人信息");

            permCheck.DataSource = myArrayList;
            permCheck.DataBind();
            string str1 = "select[ReadFile],[DeleteFile],[Upload],[DownLoad],[ReadSelf] from  RolePermission where ID= " + Request.QueryString["RoleID"].ToString();
            DataTable fuck = dbObj.GetDataSet(dbObj.GetCommandStr(str1), "RolePermission");


            for (int i = 0; i < 5; i++)
            {


                if (Convert.ToBoolean(fuck.Rows[0][i].ToString().Trim()) == true)
                {


                    permCheck.Items[i].Selected = true;
                }
            }
            if (Session["UserID"] == null)
            {
                
                ErrorMessage.Text = "请先登录！";
                this.ErrorMessage.Visible = true;
                return;
            }
            
        }
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       

        string str1 = "update RolePermission set [ReadFile]="  + permCheck.Items[0].Selected
            +",[DeleteFile]=" + permCheck.Items[1].Selected
            +",[Upload]=" + permCheck.Items[2].Selected
            +",[DownLoad]=" + permCheck.Items[3].Selected
            +",[ReadSelf]="+ permCheck.Items[4].Selected
            + " where ID=" + Request.QueryString["RoleID"].ToString();

        dbObj.ExecScalar(dbObj.GetCommandStr(str1));
        Response.Redirect("~/admin/PermissionManagement.aspx?RoleID=" + Request.QueryString["RoleID"].ToString());
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/RoleManage.aspx");
    }
}