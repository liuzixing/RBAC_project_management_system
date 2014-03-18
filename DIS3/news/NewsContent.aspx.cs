using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;
public partial class NewsContent : System.Web.UI.Page
{
    protected News n = new News();
    protected string picture;  // 轮转图片的路径
    protected string[] nextNewsPage = new string[2];
    protected string[] previousNewsPage = new string[2];
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Footer.xml"));
     
        

     if (Request["newsid"] == null)
         Response.Redirect("~/NewsTempate.aspx");
        n.NewsID = Convert.ToInt32(Request["newsid"].Trim());
        string sql = "select * from News where NewsID=" + n.NewsID;// +" and NewsState<>'0'";
        var r = DBHelper.INST.ExecuteSqlDR(sql);
        if (!r.Read())
        {
            Response.Write("新闻未通过审核");
            Response.End();
        }

        n.Title = r["NewsTitle"].ToString();
        n.Content = r["Content"].ToString();
        n.Author = r["NewsAuthor"].ToString();
        n.IsVerified = true;
        n.PublishTime = Convert.ToDateTime(r["NewsDate"]);
        n.VisitCount = Convert.ToInt32(r["VisitCount"].ToString());

        string sqlUpdate = "update News set VisitCount=" + (n.VisitCount + 1) + " where  NewsID=" + n.NewsID;
        DBHelper.INST.ExecuteSql(sqlUpdate);

        string sqlOtherNews = "select top 1 * from News where NewsID > " + n.NewsID +" and IsDelete="+0+ " order by NewsID ASC";
        DataSet ds = DBHelper.INST.ExecuteSqlDS(sqlOtherNews);
        if (ds.Tables[0].Rows.Count != 0)
        {
            previousNewsPage[0] = ds.Tables[0].Rows[0]["NewsTitle"].ToString();
            previousNewsPage[0] = (previousNewsPage[0].Length > 20? previousNewsPage[0].Substring(0,20)+"……":previousNewsPage[0]);
            previousNewsPage[1] = ResolveUrl("~/news/NewsContent.aspx") + "?newsid=" + ds.Tables[0].Rows[0]["NewsID"].ToString();
        }
        else
        {
            previousNewsPage[0] = "没有了";
            previousNewsPage[1] = ResolveUrl("~/news/NewsTempate.aspx");
        }
        ds.Dispose();
        sqlOtherNews = "select top 1 * from News where NewsID < " + n.NewsID +" and IsDelete="+0+ " order by NewsID DESC";
        ds = DBHelper.INST.ExecuteSqlDS(sqlOtherNews);
        if (ds.Tables[0].Rows.Count != 0)
        {
            nextNewsPage[0] = ds.Tables[0].Rows[0]["NewsTitle"].ToString();
            nextNewsPage[0] = (nextNewsPage[0].Length > 20 ? nextNewsPage[0].Substring(0, 20) + "……" : nextNewsPage[0]);
            nextNewsPage[1] = ResolveUrl("~/news/NewsContent.aspx") + "?newsid=" + ds.Tables[0].Rows[0]["NewsID"].ToString();
        }
        else
        {
            nextNewsPage[0] = "没有了";
            nextNewsPage[1] = ResolveUrl("~/news/NewsTempate.aspx");
        }
        r.Close();
        ds.Dispose();
    }
}