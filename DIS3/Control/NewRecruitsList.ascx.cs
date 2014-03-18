using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_NewRecruitsList : System.Web.UI.UserControl
{
    int pageSize = 7;
    int pageIndex = 1;
    String condition = null;
    protected PageResult result;
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
        if (Request["ProjectID"] != null)//&& Request["month"] == "")
        {
            condition = "ProjectID = " + Convert.ToInt32(Request["ProjectID"]);
        }
        DataPager p = new ExclusiveDataPager();
        result = p.PageData(pageIndex, pageSize, "NewRecruits", "*", "NewRecruitsID", condition, "desc");

        NewRecruitsList.DataSource = result.Result;
        NewRecruitsList.DataBind();
    }

}