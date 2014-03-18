using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Xml;

public partial class _index : System.Web.UI.Page
{
   protected string[] picture = new string[3];// 轮转图片的路径
    protected string[] pictureUrl = new string[3];// 轮转图片的路径
    protected string[] picture1 = new string[3];// 轮转图片的路径
    protected string[] pictureUrl1 = new string[3];// 轮转图片的路径
    protected string[] buttonImage = new string[13];// 图标的路径
    protected string[] buttonImageUrl = new string[13];// 图标的路径
    
    protected string[] titleName = new string[3];
    protected string[] titleUrl = new string[3];
    protected void Page_Load(object sender, EventArgs e)
    {
       
    
        if (!IsPostBack)
        {
            DataPager p = new ExclusiveDataPager();
            String condition = "IsDelete = 0";
            News.DataSource = (p.PageData(1, 5, "News", "*", "NewsID", condition, "desc")).Result;
            News.DataBind();

            DataPager pp = new ExclusiveDataPager();
            Plans.DataSource = (pp.PageData(1, 5, "StationPlan", "*", "StationPlanID", condition, "desc")).Result;
            Plans.DataBind();

            DataPager ppp = new ExclusiveDataPager();
            string project_condition = "IsDelete = 0 and Confirm = True";
            OilProject.DataSource = (ppp.PageData(1, 5, "OilProject", "*", "OilProjectID", project_condition, "desc")).Result;
            OilProject.DataBind();
        }
   
        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Footer.xml"));
        XmlNode titleNode1 = document.DocumentElement.SelectSingleNode("pictures").SelectSingleNode("//picture[@id='1']").SelectSingleNode("item");
        picture[0] = ResolveUrl(titleNode1.InnerText);
        pictureUrl[0] = ResolveUrl(titleNode1.Attributes["url"].Value);
        for (int i = 1; i <= 2; i++)
       {
           titleNode1 = titleNode1.NextSibling;
           picture[i] = ResolveUrl(titleNode1.InnerText);
            pictureUrl[i] = ResolveUrl(titleNode1.Attributes["url"].Value);
       }
        XmlNode titleNode2 = document.DocumentElement.SelectSingleNode("pictures").SelectSingleNode("//picture[@id='2']").SelectSingleNode("item");
        picture1[0] =ResolveUrl( titleNode2.InnerText);
        pictureUrl1[0] = ResolveUrl(titleNode2.Attributes["url"].Value);
        for (int j = 1; j <= 2; j++)
        {
            titleNode2 = titleNode2.NextSibling;
            picture1[j] = ResolveUrl(titleNode2.InnerText);
            pictureUrl1[j] = ResolveUrl(titleNode2.Attributes["url"].Value);
        }


        XmlNode titleNode3 = document.DocumentElement.SelectSingleNode("buttons").SelectSingleNode("//buttonImage[@id='1']").SelectSingleNode("item");
        buttonImage[0] = ResolveUrl(titleNode3.InnerText);
        buttonImageUrl[0] = ResolveUrl(titleNode3.Attributes["url"].Value);
        for (int i = 1; i <= 12; i++)
        {
            titleNode3 = titleNode3.NextSibling;
            buttonImage[i] = ResolveUrl(titleNode3.InnerText);
            buttonImageUrl[i] = ResolveUrl(titleNode3.Attributes["url"].Value);
        }

        /*
         * 从XML文件中载入主页中间的标题与链接
         */ 
        XmlNode titleNode = document.DocumentElement.SelectSingleNode("footer");
        titleName[0] = titleNode.SelectSingleNode("//catalogue[@id='2']").Attributes["value"].Value;
        titleUrl[0] = ResolveUrl(titleNode.SelectSingleNode("//catalogue[@id='2']").Attributes["url"].Value);
        titleName[1] = titleNode.SelectSingleNode("//catalogue[@id='4']").Attributes["value"].Value;
        titleUrl[1] = ResolveUrl(titleNode.SelectSingleNode("//catalogue[@id='4']").Attributes["url"].Value);
        titleName[2] = titleNode.SelectSingleNode("//catalogue[@id='3']").Attributes["value"].Value;
        titleUrl[2] = ResolveUrl(titleNode.SelectSingleNode("//catalogue[@id='3']").Attributes["url"].Value);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/Index.aspx");
    }
}
