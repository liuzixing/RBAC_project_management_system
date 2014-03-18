using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;

public partial class oilStation_NoLikeDigital : System.Web.UI.Page
{
    protected string picture;  // 轮转图片的路径
    protected string[] DigitalTitle = new string[3];
    protected string[] DigitalUrl = new string[3];
    protected Digital n = new Digital();
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Web.sitemap"));
      //  picture = ResolveUrl(document.DocumentElement.SelectSingleNode("picture").InnerText);
         


        /*
       * 从XML文件中载入f分类计划的的标题与链接
       */
        classify.DataSource = document.DocumentElement.SelectSingleNode("//siteMapNode[@title='无象数码']").ChildNodes;
        classify.DataBind();


       /*
        XmlNode titleNode = document.DocumentElement.SelectSingleNode("Company").SelectSingleNode("item");
        DigitalTitle[0] = titleNode.InnerText;
        DigitalUrl[0] = ResolveUrl(titleNode.Attributes["url"].Value);
        for (int i = 1; i <= 2; i++)
        {
            titleNode = titleNode.NextSibling;
            DigitalTitle[i] = titleNode.InnerText;
            DigitalUrl[i] = ResolveUrl(titleNode.Attributes["url"].Value);
        }
        * */
        if (Request["DigitalID"] == null)
            n.DigitalID = 1;
        else
        {
            n.DigitalID = Convert.ToInt32(Request["DigitalID"].Trim());
        }
        string sql = "select * from Digital where DigitalID=" + n.DigitalID;
        var r = DBHelper.INST.ExecuteSqlDR(sql);
        if (!r.Read())
        {
            Response.Write("没有上传信息");
            Response.End();
        }


        n.DigitalContent = r["DigitalContent"].ToString();
        r.Close();
    }
}
