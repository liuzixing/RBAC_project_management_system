using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;
public partial class oilStation_coEnterprise : System.Web.UI.Page
{
    protected string picture;  // 轮转图片的路径
    protected Enterprises n = new Enterprises();
    //protected string[] enterprisesName = new string[3];
    //protected string[] enterprisesUrl = new string[3];
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Web.sitemap"));
      //  picture = ResolveUrl(document.DocumentElement.SelectSingleNode("picture").InnerText);
        /*
       * 从XML文件中载入f分类计划的的标题与链接
       */
        classify.DataSource = document.DocumentElement.SelectSingleNode("//siteMapNode[@title='合作企业']").ChildNodes;
        classify.DataBind();
        //XmlNode titleNode = document.DocumentElement.SelectSingleNode("footer").SelectSingleNode("//catalogue[@id='6']").SelectSingleNode("item");
        //enterprisesName[0] = titleNode.InnerText;
        //enterprisesUrl[0] = ResolveUrl(titleNode.Attributes["url"].Value);
        //for (int i = 1; i <= 2; i++) 
        //{
        //    titleNode = titleNode.NextSibling;
        //    enterprisesName[i] = titleNode.InnerText;
        //    enterprisesUrl[i] = ResolveUrl(titleNode.Attributes["url"].Value);
        //}
        string sql;
      
        if (Request["EnterprisesID"] == null)
        {
            sql = "select * from CooperationEnterprises where IsDelete=0";
           var  r = DBHelper.INST.ExecuteSqlDR(sql);
           if (!r.Read())
           {
               Response.Write("没有上传信息");
               Response.End();
           }
           r.Close();
        }
        else
        {
            n.EnterprisesID = Convert.ToInt32(Request["EnterprisesID"].Trim());
            sql = "select * from CooperationEnterprises where EnterprisesID=" + n.EnterprisesID;
           var r = DBHelper.INST.ExecuteSqlDR(sql);
           if (!r.Read())
           {
               Response.Write("没有上传信息");
               Response.End();
           }
       
            n.Content = r["Content"].ToString();
            r.Close();
        }
    }
}
