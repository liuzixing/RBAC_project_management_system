using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_FAQList : System.Web.UI.UserControl
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
            i = (Convert.ToInt32(Request["p"]) - 1) * 7 + 1;
        }
        else
        {
            pageIndex = 1;
            i = 1;
        }
        DataPager p = new ExclusiveDataPager();
        result = p.PageData(pageIndex, pageSize, "FAQ", "*", "FAQID", condition, "desc");

        FAQList.DataSource = result.Result;
        FAQList.DataBind();
    }

}