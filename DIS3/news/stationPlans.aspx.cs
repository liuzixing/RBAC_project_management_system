using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;

public partial class stationPlans : System.Web.UI.Page
{

    protected string picture;  // 轮转图片的路径
    protected string[] planName = new string[3];
    protected string[] planUrl = new string[3];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["p"] != null)
        {
            SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(this.ModifyPath);
        }

        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Footer.xml"));
      //  picture = ResolveUrl(document.DocumentElement.SelectSingleNode("picture").InnerText);



        /*
       * 从XML文件中载入分类计划的的标题与链接
       */
        XmlNode titleNode = document.DocumentElement.SelectSingleNode("footer").SelectSingleNode("//catalogue[@id='4']").SelectSingleNode("item");
        planName[0] = titleNode.InnerText;
        planUrl[0] = ResolveUrl(titleNode.Attributes["url"].Value);
        for (int i = 1; i <= 2; i++)
        {
            titleNode = titleNode.NextSibling;
            planName[i] = titleNode.InnerText;
            planUrl[i] = ResolveUrl(titleNode.Attributes["url"].Value);
        }

    }
    private SiteMapNode ModifyPath(object sender, SiteMapResolveEventArgs e)
    {
        // 克隆当前结点和父节点
        SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
        // 创建新节点
        int id = Convert.ToInt32(Request["PlanClassifyID"].Trim());
        string nodeName;
        switch (id)
        {
            case 1: nodeName = "创意征集计划";
                break;
            case 2: nodeName = "立项计划";
                break;
            case 3: nodeName = "市场化计划";
                break;
            default: nodeName = "创意征集计划";
                break;
        }
        SiteMapNode newNode = new SiteMapNode(e.Provider, nodeName, "OthrePage.aspx", nodeName);
        newNode.ParentNode = currentNode;
        currentNode = newNode;
        return currentNode;
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        SiteMap.SiteMapResolve -= new SiteMapResolveEventHandler(ModifyPath);
    }
}