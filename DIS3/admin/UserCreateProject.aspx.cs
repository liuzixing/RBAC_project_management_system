using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_UserProject : System.Web.UI.Page
{
    DBClass dbObj = new DBClass();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        if (!IsPostBack)
        {
            Data_Load();

            ListItem li = new ListItem("--请选择--", "");
            this.ddlProject.Items.Insert(0, li);
            this.ddlRole.Items.Insert(0, li);

        }
    }

    protected void Data_Load()
    {
        string str = "select OilProjectTitle from OilProject";
        DataTable ds =  dbObj.GetDataSet(dbObj.GetCommandStr(str), "OilProject");
        ddlProject.DataSource = ds;
        ddlProject.DataTextField = "OilProjectTitle";
    
        ddlProject.DataBind();

        string str1 = "select RoleName from RolePermission";
        DataTable ds1 = dbObj.GetDataSet(dbObj.GetCommandStr(str1), "RolePermission");
        ddlRole.DataSource = ds1;
        ddlRole.DataTextField = "RoleName";
        ddlRole.DataBind();

    }

    protected void addProject_Click(object sender, EventArgs e)
    {
        string str = "insert into UPR (ProjectName, UserName, RoleName) values ('" + this.ddlProject.SelectedValue + "','" +
            Request.QueryString["UserName"] + "','" + this.ddlRole.SelectedValue + "')";

        dbObj.ExecNonQuery(dbObj.GetCommandStr(str));
        GridView1.DataBind();
    }

    protected void DataBound_click(object sender, GridViewRowEventArgs e)
    {
      
    }
}