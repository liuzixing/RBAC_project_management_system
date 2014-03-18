using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewsList : System.Web.UI.Page
{
    int pageSize = 12;
    int pageIndex = 1;
    String condition = null;
    protected PageResult result;

    protected string url = "NewsList.aspx?";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request["p"] != null)
            pageIndex = Convert.ToInt32(Request["p"]);
        else
            pageIndex = 1;

        //根据categoryid，分类显示列表

        if (Request["categoryid"] != null)//&& Request["month"] == "")
        {
            condition = "NewsCategoryID = " + Convert.ToInt32(Request["categoryid"]);
        }

        //      根据年份与月份显示
        if (Request["NewsYear"] != null && Request["month"] == "")
        {
            condition = "NewsYear = " + Convert.ToInt32(Request["NewsYear"]);
        }
        if (Request["NewsYear"] != null && Request["month"] != "")
        {

            condition = "NewsYear = " + Convert.ToInt32(Request["NewsYear"]) + "And NewsMonth= " + Convert.ToInt32(Request["month"]);

        }

        DataPager p = new ExclusiveDataPager();
        result = p.PageData(pageIndex, pageSize, "News", "*", "NewsID", condition, "desc");

        GridView1.DataSource = result.Result;
        GridView1.DataBind();
    }
    protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}