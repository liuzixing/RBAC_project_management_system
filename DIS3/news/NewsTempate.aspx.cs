using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
public partial class NewsTempate : System.Web.UI.Page
{
    protected string picture;  // 轮转图片的路径
    protected string newsFrame = "NewsList.aspx"; // 框架内链接

    int pageSize = 5;
    int pageIndex = 1;
    String condition = "IsDelete = 0";
    protected PageResult result;
    protected string url = "NewsTempate.aspx?";
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Footer.xml"));
    //    picture = ResolveUrl(document.DocumentElement.SelectSingleNode("picture").InnerText);
        if (Request["newsId"] != null)
        {
            newsFrame = "NewsContent.aspx?newsid=" + Request["newsId"];
        }
        else
        {
            newsFrame = "NewsTempate.aspx";
        }
        //新闻列表更新

        if (Request["p"] != null)
            pageIndex = Convert.ToInt32(Request["p"]);
        else
            pageIndex = 1;

        //根据categoryid，分类显示列表

        if (Request["categoryid"] != null)//&& Request["month"] == "")
        {
            condition += " and NewsCategoryID = " + Convert.ToInt32(Request["categoryid"]);
        }

        //      根据年份与月份显示
        if (Request["NewsYear"] != null && Request["month"] == "")
        {
            condition += " and NewsYear = " + Convert.ToInt32(Request["NewsYear"]);
        }
        if (Request["NewsYear"] != null && Request["month"] != "")
        {
            condition += " and NewsYear = " + Convert.ToInt32(Request["NewsYear"]) + "And NewsMonth= " + Convert.ToInt32(Request["month"]);
        }

        DataPager p = new ExclusiveDataPager();
        result = p.PageData(pageIndex, pageSize, "News", "*", "NewsID", condition, "desc");

        GridView1.DataSource = result.Result;
        GridView1.DataBind();
}

    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}