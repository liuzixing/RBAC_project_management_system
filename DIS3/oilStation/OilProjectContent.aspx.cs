using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;

public partial class OilProject_Default : System.Web.UI.Page
{
    protected OilProject n = new OilProject();
    protected string picture;  // 轮转图片的路径
    protected string[] nextPage = new string[2];
    protected string[] previousPage = new string[2];
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Footer.xml"));
      //  picture = ResolveUrl(document.DocumentElement.SelectSingleNode("picture").InnerText);

        if (Request["OilProjectID"] == null)
            Response.Redirect("~/oilStation/Project.aspx");
        n.OilProjectID = Convert.ToInt32(Request["OilProjectID"].Trim());
        string sql = "select * from OilProject where OilProjectID=" + n.OilProjectID;// +" and NewsState<>'0'";
        var r = DBHelper.INST.ExecuteSqlDR(sql);
        if (!r.Read())
        {
            Response.Write("计划未通过审核");
            Response.End();
        }

        n.OilProjectTitle = r["OilProjectTitle"].ToString();
        n.OilProjectContent = r["OilProjectContent"].ToString();
        n.OilProjectDate = Convert.ToDateTime(r["OilProjectDate"]);
      //  n.VisitCount = Convert.ToInt32(r["VisitCount"].ToString());
       // n.PlanAuthor = r["PlanAuthor"].ToString();
      //  n.PlanDate = Convert.ToDateTime(r["PlanDate"]);
      //  string sqlUpdate = "update OilProject set VisitCount=" + (n.VisitCount + 1) + " where  OilProjectID=" + n.OilProjectID;
       // DBHelper.INST.ExecuteSql(sqlUpdate);


        string sqlOtherNews = "select top 1 * from OilProject where OilProjectID > " + n.OilProjectID + " and IsDelete=0 order by OilProjectID ASC";
        DataSet ds = DBHelper.INST.ExecuteSqlDS(sqlOtherNews);
        if (ds.Tables[0].Rows.Count != 0)
        {
            previousPage[0] = ds.Tables[0].Rows[0]["OilProjectTitle"].ToString();
            previousPage[0] = (previousPage[0].Length > 20 ? previousPage[0].Substring(0, 20) + "……" : previousPage[0]);
            previousPage[1] = ResolveUrl("~/oilStation/OilProjectContent.aspx") + "?OilProjectID=" + ds.Tables[0].Rows[0]["OilProjectID"].ToString();
        }
        else
        {
            previousPage[0] = "没有了";
            previousPage[1] = ResolveUrl("~/oilStation/Project.aspx");
        }
        ds.Dispose();
        sqlOtherNews = "select top 1 * from OilProject where OilProjectID < " + n.OilProjectID + " and IsDelete=0 order by OilProjectID DESC";
        ds = DBHelper.INST.ExecuteSqlDS(sqlOtherNews);
        if (ds.Tables[0].Rows.Count != 0)
        {
            nextPage[0] = ds.Tables[0].Rows[0]["OilProjectTitle"].ToString();
            nextPage[0] = (nextPage[0].Length > 20 ? nextPage[0].Substring(0, 20) + "……" : nextPage[0]);
            nextPage[1] = ResolveUrl("~/oilStation/OilProjectContent.aspx") + "?OilProjectID=" + ds.Tables[0].Rows[0]["OilProjectID"].ToString();
        }
        else
        {
            nextPage[0] = "没有了";
            nextPage[1] = ResolveUrl("~/oilStation/Project.aspx");
        }
        r.Close();
        ds.Dispose();
    }
}