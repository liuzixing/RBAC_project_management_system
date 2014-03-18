using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;

public partial class oilStation_Default : System.Web.UI.Page
{
    protected Plans n = new Plans();
    protected string picture;  // 轮转图片的路径
    protected string[] nextPage = new string[2];
    protected string[] previousPage = new string[2];
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Footer.xml"));
      //  picture = ResolveUrl(document.DocumentElement.SelectSingleNode("picture").InnerText);

        if (Request["StationPlanID"] == null)
            Response.Redirect("~/oilStation/stationPlans.aspx");
        n.StationPlanID = Convert.ToInt32(Request["StationPlanID"].Trim());
        string sql = "select * from StationPlan where StationPlanID=" + n.StationPlanID;// +" and NewsState<>'0'";
        var r = DBHelper.INST.ExecuteSqlDR(sql);
        if (!r.Read())
        {
            Response.Write("计划未通过审核");
            Response.End();
        }

        n.PlanName = r["PlanName"].ToString();
        n.Content = r["Content"].ToString();
        n.VisitCount = Convert.ToInt32(r["VisitCount"].ToString());
        n.PlanAuthor = r["PlanAuthor"].ToString();
        n.PlanDate = Convert.ToDateTime(r["PlanDate"]);
        string sqlUpdate = "update StationPlan set VisitCount=" + (n.VisitCount + 1) + " where  StationPlanID=" + n.StationPlanID;
        DBHelper.INST.ExecuteSql(sqlUpdate);


        string sqlOtherNews = "select top 1 * from StationPlan where StationPlanID > " + n.StationPlanID + " order by StationPlanID ASC";
        DataSet ds = DBHelper.INST.ExecuteSqlDS(sqlOtherNews);
        if (ds.Tables[0].Rows.Count != 0)
        {
            previousPage[0] = ds.Tables[0].Rows[0]["PlanName"].ToString();
            previousPage[0] = (previousPage[0].Length > 20 ? previousPage[0].Substring(0, 20) + "……" : previousPage[0]);
            previousPage[1] = ResolveUrl("~/oilStation/planContent.aspx") + "?StationPlanID=" + ds.Tables[0].Rows[0]["StationPlanID"].ToString();
        }
        else
        {
            previousPage[0] = "没有了";
            previousPage[1] = ResolveUrl("~/oilStation/stationPlans.aspx");
        }
        ds.Dispose();
        sqlOtherNews = "select top 1 * from StationPlan where StationPlanID < " + n.StationPlanID + " order by StationPlanID DESC";
        ds = DBHelper.INST.ExecuteSqlDS(sqlOtherNews);
        if (ds.Tables[0].Rows.Count != 0)
        {
            nextPage[0] = ds.Tables[0].Rows[0]["PlanName"].ToString();
            nextPage[0] = (nextPage[0].Length > 20 ? nextPage[0].Substring(0, 20) + "……" : nextPage[0]);
            nextPage[1] = ResolveUrl("~/oilStation/planContent.aspx") + "?StationPlanID=" + ds.Tables[0].Rows[0]["StationPlanID"].ToString();
        }
        else
        {
            nextPage[0] = "没有了";
            nextPage[1] = ResolveUrl("~/oilStation/stationPlans.aspx");
        }
        r.Close();
        ds.Dispose();
    }
}