using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;

public partial class oilStation_Project : System.Web.UI.Page
{

    int pageSize =15;
    int pageIndex = 1;
    String condition = null;
    String condition_direction = "IsDelete = 0"; 
    protected PageResult result_direction;
    protected PageResult result1;
    protected int i;

    protected void Page_Load(object sender, EventArgs e)
    {
        // FAQList.DataSource = FAQ.GetQuestionCategory(false, Cache);



        //   FAQList.DataBind();
        if (Request["p"] != null)
        {
            pageIndex = Convert.ToInt32(Request["p"]);
        }
        else
        {
            pageIndex = 1;

        }
        if (Request["OilProjectDirectionID"] != null)
        {
            condition = "OilProject.IsDelete = 0 and OilProject.Confirm = True and OilProject.OilProjectDirectionID = OilProjectDirection.OilProjectDirectionID and OilProjectDirection.OilProjectDirectionID=" + Convert.ToInt32(Request["OilProjectDirectionID"]);
        }
        else
        {
            condition = "OilProject.IsDelete = 0 and OilProject.Confirm = True and OilProject.OilProjectDirectionID = OilProjectDirection.OilProjectDirectionID ";
        }
        DataPager p = new ExclusiveDataPager();

        result_direction = p.PageData(1, 100, "OilProjectDirection", "*", "OilProjectDirectionID", condition_direction, null);
        result1 = p.PageData(pageIndex, pageSize, "OilProject,OilProjectDirection", "*", "OilProjectID", condition, "desc");
        ProjectList.DataSource = result_direction.Result;
        ProjectList.DataBind();

        GridView1.DataSource = result1.Result;
        GridView1.DataBind();
    }
}
