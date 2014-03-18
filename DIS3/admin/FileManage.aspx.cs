using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data;
using System.IO;

public partial class FileManage : System.Web.UI.Page
{
    DBClass dbObj = new DBClass();
    CommonClass ccObj = new CommonClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ErrorMessage.Visible = false;
        this.care.Visible = false;
        if (!IsPostBack)
        {
            //Load_Data();
           // this.searchPart.Visible = false;
            if (Session["UserName"] == null)
            {
                
                ErrorMessage.Text = "请先登录！";
                this.ErrorMessage.Visible = true;
                return;
            }
            func();
            string str = "select UserName from Users ";
            DataTable ds = dbObj.GetDataSet(dbObj.GetCommandStr(str),"Users");

            this.clistUser.DataSource = ds;
            this.clistUser.DataTextField = "UserName";
            this.clistUser.DataBind();
           
        }
        
    }

    private void func()
    {
        this.ErrorMessage.Visible = false;

        string str = "select ProjectName from UPR where UserName = '" + Session["UserName"] + "'";
        DataTable proName = dbObj.GetDataSet(dbObj.GetCommandStr(str),"UPR");
        int i;
        for ( i = 0; i < proName.Rows.Count; i++)
        {
            if (proName.Rows[i][0].ToString() == Request.QueryString["ProjectName"].ToString())
                break;
        }

       
        if (i == proName.Rows.Count)
        {
            
            ErrorMessage.Text = "不是你所属的项目，因此无法打开！";
            this.ErrorMessage.Visible = true;
            return;
        }
        
        string str1 = "select RoleName from UPR where UserName = '" + Session["UserName"] + "' and ProjectName = '" + Request.QueryString["ProjectName"]
            + "'";
        DataTable role = dbObj.GetDataSet(dbObj.GetCommandStr(str1),"UPR");
        for(int j = 0; j <　role.Rows.Count; j++)
        {
            string str3 = "select ReadFile from RolePermission where RoleName = '" + role.Rows[0][j] + "'";
            if (dbObj.ExecScalar(dbObj.GetCommandStr(str3)) == "True")
            {
                return;
            }
        }
        
        ErrorMessage.Text = "你没有查看文件的权限！";
        this.ErrorMessage.Visible = true;

       
    }

    public void Load_Data()
    {
        int userID = (int)Session["UserID"];
        string mystr = "select * from File where UserID = " + userID;
        DataTable dsTable = dbObj.GetDataSetStr(mystr , "File");
        this.myFile.DataSource = dsTable.DefaultView;
        this.myFile.DataKeyNames = new string[] { "ID" };
        this.myFile.DataBind();

        string newstr = "select top 4 * from File where datediff('d', CreateTime, date()) < 7";
        DataTable newfTable = dbObj.GetDataSetStr(newstr, "File");
        //this.newFile.DataSource = dsTable.DefaultView;
        //this.newFile.DataKeyNames = new string[] { "ID" };
        //this.newFile.DataBind();
    }


    protected void myFile_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void myFileDelete(object sender, GridViewDeleteEventArgs  e)
    {
        this.ErrorMessage.Visible = false;

        string str1 = "select RoleName from UPR where UserName = '" + Session["UserName"] + "' and ProjectName = '" + Request.QueryString["ProjectName"]
           + "'";
        DataTable role = dbObj.GetDataSet(dbObj.GetCommandStr(str1), "UPR");
        int j;
        for (j = 0; j < role.Rows.Count; j++)
        {
            string str3 = "select DeleteFile from RolePermission where RoleName = '" + role.Rows[0][j] + "'";
            if (dbObj.ExecScalar(dbObj.GetCommandStr(str3)) == "True")
            {
                break;
            }
        }

        if (j == role.Rows.Count)
        {
            
            ErrorMessage.Text = "你没有删除文件的权限！";
            this.ErrorMessage.Visible = true;

            return;
        }
        string str = "delete from File where ID = " + Convert.ToInt32(myFile.DataKeys[e.RowIndex].Value.ToString());
        dbObj.ExecNonQuery(dbObj.GetCommandStr(str));
        //Load_Data();
    }

    protected void downLoad(object sender, GridViewCommandEventArgs e)
    {
        this.ErrorMessage.Visible = false;

        string str1 = "select RoleName from UPR where UserName = '" + Session["UserName"] + "' and ProjectName = '" + Request.QueryString["ProjectName"]
           + "'";
        DataTable role = dbObj.GetDataSet(dbObj.GetCommandStr(str1), "UPR");
        int j;
        for ( j = 0; j < role.Rows.Count; j++)
        {
            string str3 = "select DownLoad from RolePermission where RoleName = '" + role.Rows[0][j] + "'";
            if (dbObj.ExecScalar(dbObj.GetCommandStr(str3)) == "True")
            {
                break;
            }
        }

        if (j == role.Rows.Count)
        {
            
            ErrorMessage.Text = "你没有下载文件的权限！";
            this.ErrorMessage.Visible = true;
            return;
        }
        
        if (e.CommandName == "dlCommand")
        {
            string FullFileName = e.CommandArgument.ToString();
            FileInfo info = new FileInfo(FullFileName);
            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = false;
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(info.Name, System.Text.Encoding.UTF8).Replace("+", "%20"));
            Response.AppendHeader("Content-Length", info.Length.ToString());
            Response.WriteFile(FullFileName);
            Response.Flush();
            Response.End();
            //Response.Redirect("Default.aspx");
        }
    }

    protected void uploadClk_Click(object sender, EventArgs e)
    {
        this.ErrorMessage.Visible = false;

        string str1 = "select RoleName from UPR where UserName = '" + Session["UserName"] + "' and ProjectName = '" + Request.QueryString["ProjectName"]
           + "'";
        DataTable role = dbObj.GetDataSet(dbObj.GetCommandStr(str1), "UPR");
        int j;
        for (j = 0; j < role.Rows.Count; j++)
        {
            string str3 = "select UpLoad from RolePermission where RoleName = '" + role.Rows[0][j] + "'";
            if (dbObj.ExecScalar(dbObj.GetCommandStr(str3)) == "True")
            {
                break;
            }
        }

        if (j == role.Rows.Count)
        {
            
            ErrorMessage.Text = "你没有上传文件的权限！";
            this.ErrorMessage.Visible = true;

            return;
        }
        string savePath="~/admin/File";
        if (FileUpload1.HasFile)
        {
            string filename = FileUpload1.FileName;
            savePath += filename;
            FileUpload1.SaveAs(savePath);

            this.care.CssClass = "failureNotification";
            care.Text = "文件类型:" + FileUpload1.PostedFile.ContentType + FileUpload1.PostedFile.ContentLength;
            this.care.Visible = true;
            // Response.Write(FileUpload1.PostedFile.ContentType + FileUpload1.PostedFile.ContentLength + "<br>");

            string str = "insert into File(FileName, FileURL, ProjectName, UserName, Description, CreateTime) values ( '" + filename + "','" + savePath + "','"
                + Request.QueryString["ProjectName"] + "','" + Session["UserName"] + "','a new file', date())";
            dbObj.ExecNonQuery(dbObj.GetCommandStr(str));
            Response.Redirect("FileManage.aspx?ProjectName=" + Request.QueryString["ProjectName"]);
        }
        else
        {
            
            ErrorMessage.Text = "请选择文件！";
            this.ErrorMessage.Visible = true;
        }

    }


   
    protected void tbUserSearch_TextChanged(object sender, EventArgs e)
    {
            string key = this.tbUserSearch.Text.Trim();
            string str = "select UserName from Users where UserName like '%" + key + "%'";
            DataTable ds = dbObj.GetDataSet(dbObj.GetCommandStr(str), "Users");
            this.clistUser.DataSource = ds;
            clistUser.DataBind();
    }

    protected void addMember_Click(object sender, EventArgs e)
    {
        for(int i = 0; i < clistUser.Items.Count; i++)
            for (int j = 0; j < cblistRole.Items.Count; j++)
            {
                if (clistUser.Items[i].Selected && cblistRole.Items[j].Selected)
                {
                    string str = "insert into UPR(ProjectName, UserName, RoleName)values('" + Request.QueryString["ProjectName"]
                        + "','" + clistUser.Items[i].Text + "','" + cblistRole.Items[j].Text + "')";
                    dbObj.ExecNonQuery(dbObj.GetCommandStr(str));
                }
            }
        GridView1.DataBind();
       
    }

    protected void btnFileSearch_Click(object sender, EventArgs e)
    {
        string key = this.tbFileSearch.Text.Trim();
        string str = "select * from File where ProjectName = '" + Request.QueryString["ProjectName"] + "' and FileName like '%" + key + "%'";
        DataTable ds = dbObj.GetDataSet(dbObj.GetCommandStr(str), "Files");
        this.GridView2.DataSource = ds;
        GridView2.DataBind();
        
    }

    protected void RowDelete_Click(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["UserName"].ToString() != "lin")
        {
            this.ErrorMessage.Text = "你没有这个权限！";
            this.ErrorMessage.Visible = true;
            return;
        }

        string str = "Delete from UPR where ID = " + GridView1.DataKeys[e.RowIndex].Value.ToString();
        dbObj.ExecNonQuery(dbObj.GetCommandStr(str));
        this.care.Text = "删除成功！";
        GridView1.DataBind();
        care.Visible = true;
    }
}