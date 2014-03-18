using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_FileDetail : System.Web.UI.Page
{
  
    DBClass dbObj = new DBClass();
    public string fileUrl = null;
    public string fileName = null;
    public string projectName = null;
    public string userName = null;
    public string description = null;
    public string createTime = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                ErrorMessage.Text = "你还没有登录！";
                ErrorMessage.Visible = true;
                return;
            }
            if (Session["UserName"].ToString() != "lin")
            {
                btnSubmit.Enabled = false;
            }
            Data_Load();
          
        }
    }

    private void Data_Load()
    {
        fileName = Request.QueryString["FileName"];
        string str = "select * from File where FileName = '" + fileName + "'";
        DataTable ds = dbObj.GetDataSet(dbObj.GetCommandStr(str), "File");
        fileUrl = ds.Rows[0][2].ToString();
        projectName = ds.Rows[0][3].ToString();
        userName = ds.Rows[0][6].ToString();
        description = ds.Rows[0][4].ToString();
        createTime = ds.Rows[0][5].ToString();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string str = "update File set Description = '" + Request["editor1"].ToString() + "' where FileName = '" +　Request.QueryString["FileName"] + "'";
        dbObj.ExecNonQuery(dbObj.GetCommandStr(str));
        TipMessage.Text = "修改成功!";
        TipMessage.Visible = true;
        Data_Load();
    }
}