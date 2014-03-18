using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_PlanList : System.Web.UI.UserControl
{
    int pageSize = 15;
    int pageIndex = 1;
    String condition = null;
    protected PageResult result;

    protected string url = "PlanList.aspx?";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request["p"] != null)
            pageIndex = Convert.ToInt32(Request["p"]);
        else
            pageIndex = 1;

        //根据categoryid，分类显示列表

        if (Request["PlanClassifyID"] != null)//&& Request["month"] == "")
        {
            condition = "StationPlan.IsDelete = 0 and PlanClassifyID = " + Convert.ToInt32(Request["PlanClassifyID"]);
        }
        
        else
        {
            condition = "StationPlan.IsDelete = 0";
        }
   


        DataPager p = new ExclusiveDataPager();
        result = p.PageData(pageIndex, pageSize, "StationPlan", "*", "StationPlanID", condition, "desc");

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