using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Web.Caching;

public partial class admin_PlanMage : System.Web.UI.Page
{
    //int pageCount;
    int pageSize = 10;
    int pageIndex = 1;
    // String condition = "";
    protected PageResult result;



    protected void page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            ErrorMessage.Text = "请先登录！";
            this.ErrorMessage.Visible = true;
            this.AddPlan.Visible = false;
            this.Button3.Visible = false;
            this.Button5.Visible = false;
            this.Button6.Visible = false;
            return;
        }

        if (Session["UserName"].ToString() != "lin")
        {
            ErrorMessage.Text = "非管理员用户不能浏览当前页面！";
            this.ErrorMessage.Visible = true;
            this.AddPlan.Visible = false;
            this.Button3.Visible = false;
            this.Button5.Visible = false;
            this.Button6.Visible = false;
            return;
        }

        if (!IsPostBack)
        {
           /* DataSet ds = DBHelper.INST.ExecuteSqlDS("select * from StationPlan");
            // 为控件绑定数据源，必须是表 
            DropDownList1.DataSource = ds.Tables[0];
            // 选择为下拉列表提供数据的字段
            DropDownList1.DataValueField = "PlanClassifyID";
            DropDownList1.DataTextField = "PlanClassify";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "-请选择计划类别-");
            */ 
            bindData();
        }
    }

    void bindData()
    {
        InitPage();
        if (Request["p"] != null)
            pageIndex = Convert.ToInt32(Request["p"]);
        else
            pageIndex = 1;

        DataPager p = new ExclusiveDataPager();
        result = p.PageData(pageIndex, pageSize, "StationPlan", "*", "StationPlanID", null, "asc");
        string dsStr = "select * from StationPlan where IsDelete=" + 0;
        DataSet ds = DBHelper.INST.ExecuteSqlDS(dsStr);

        GridView1.DataSource = ds;//result1.Result;
        GridView1.DataBind();
        ds.Dispose();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindData();
    }

    private void InitPage()
    {
        //CheckBox CheckBox2 = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox2");
        //CheckBox2.Checked = false;
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            cbox.Checked = false;
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        String sqlstr1 = "delete from StationPlan where IsDelete=" + "1";
        DBHelper.INST.ExecuteSql(sqlstr1);
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked == true)
            {
                string sqlstr2 = "update StationPlan set IsDelete=" + 1 + " where StationPlanID=" + Convert.ToInt32(GridView1.DataKeys[i].Value);
                DBHelper.INST.ExecuteSql(sqlstr2);
            }
        }
        //   refleshDoc();
        Response.Redirect("PlanMage.aspx");
    }

    protected void Button5_Click(object sender, EventArgs e)
    {

        String sqlstr = "update StationPlan set IsDelete=" + 0 + " where IsDelete=" + 1;
        DBHelper.INST.ExecuteSql(sqlstr);
        //   refleshDoc();
        Response.Redirect("PlanMage.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.Parent.Parent as GridViewRow;
        CheckBox cbox = (CheckBox)row.FindControl("CheckBox1");
        cbox.Checked = true;
        Button2_Click(sender, e);
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox CheckBox2 = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox2");
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (CheckBox2.Checked == true)
            {
                cbox.Checked = true;
            }
            else
            {
                cbox.Checked = false;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        CheckBox CheckBox2 = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox2");
        CheckBox2.Checked = false;
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            cbox.Checked = false;
        }
    }

  /*  protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue != "-请选择计划类别-")
        {
            String sd = "select * from StationPlan where distinct PlanClassifyID= " + DropDownList1.SelectedValue + " and IsDelete=" + 0;
            DataSet ds1 = DBHelper.INST.ExecuteSqlDS(sd);
            GridView1.DataSource = ds1;//result1.Result;
            GridView1.DataBind();
        }
        else
        {
            string dsStr = "select * from StationPlan where IsDelete=" + 0;
            DataSet ds = DBHelper.INST.ExecuteSqlDS(dsStr);

            GridView1.DataSource = ds;//result1.Result;
            GridView1.DataBind();
        }
    }
   */
 
    protected void AddPlan_Click(object sender, EventArgs e)
    {
        Response.Redirect("PlanEditor.aspx");
    }
}
