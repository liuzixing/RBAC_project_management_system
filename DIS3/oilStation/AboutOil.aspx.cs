using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;

public partial class oilStation_aboutUs : System.Web.UI.Page
{


    protected AboutOil n = new AboutOil();

    protected string[] OilClassifyname = new string[4];
    protected string[] OilClassifyUrl = new string[4];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["p"] != null)
        {
            SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(this.ModifyPath);
        }

        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Web.sitemap"));
        // picture = ResolveUrl(document.DocumentElement.SelectSingleNode("picture").InnerText);
        /*
       * 从XML文件中载入f分类计划的的标题与链接
       */
        classify.DataSource = document.DocumentElement.SelectSingleNode("siteMapNode[@url='~/index.aspx']").SelectSingleNode("//siteMapNode[@url='~/oilStation/AboutOil.aspx']").ChildNodes;
        classify.DataBind();
        /*
        XmlNode titleNode = document.DocumentElement.SelectSingleNode("footer").SelectSingleNode("//catalogue[@id='1']").SelectSingleNode("item");
        OilClassifyname[0] = titleNode.InnerText;
        OilClassifyUrl[0] = ResolveUrl(titleNode.Attributes["url"].Value);
        for (int i = 1; i <= 3; i++)
        {
            titleNode = titleNode.NextSibling;
            OilClassifyname[i] = titleNode.InnerText;
            OilClassifyUrl[i] = ResolveUrl(titleNode.Attributes["url"].Value);
        }
        * */
        if (Request["AboutOilID"] == null)
            n.AboutOilID = 1;
        else
        {
            n.AboutOilID = Convert.ToInt32(Request["AboutOilID"].Trim());
        }

        if (n.AboutOilID != 0)
        {
            string sql = "select * from AboutOil where AboutOilID=" + n.AboutOilID;// +" and NewsState<>'0'";
            var r = DBHelper.INST.ExecuteSqlDR(sql);

            if (!r.Read())
            {
                Response.Write("没有上传信息");
                Response.End();
            }
            n.OilContent = r["OilContent"].ToString();
            r.Close();
        }
    }
    private SiteMapNode ModifyPath(object sender, SiteMapResolveEventArgs e)
    {
        // 克隆当前结点和父节点
        SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
        // 创建新节点
        SiteMapNode newNode = new SiteMapNode(e.Provider, "FAQ", "OthrePage.aspx", "FAQ");
        newNode.ParentNode = currentNode;
        currentNode = newNode;
        return currentNode;
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        SiteMap.SiteMapResolve -= new SiteMapResolveEventHandler(ModifyPath);
    }
}

