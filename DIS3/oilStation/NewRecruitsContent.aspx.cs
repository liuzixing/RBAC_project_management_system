using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;

public partial class NewRecruits_Default : System.Web.UI.Page
{
    protected NewRecruits n = new NewRecruits();
    protected string picture;  // 轮转图片的路径
    protected string[] nextPage = new string[2];
    protected string[] previousPage = new string[2];
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Footer.xml"));
      //  picture = ResolveUrl(document.DocumentElement.SelectSingleNode("picture").InnerText);

        if (Request["NewRecruitsID"] == null)
            Response.Redirect("~/oilStation/NewRecruitsCenter.aspx");
        n.NewRecruitsID = Convert.ToInt32(Request["NewRecruitsID"].Trim());
        string sql = "select * from NewRecruits where NewRecruitsID=" + n.NewRecruitsID;// +" and NewsState<>'0'";
        var r = DBHelper.INST.ExecuteSqlDR(sql);
        if (!r.Read())
        {
            Response.Write("计划未通过审核");
            Response.End();
        }

        n.NewRecruitsDuty = r["NewRecruitsDuty"].ToString();
        n.NewRecruitsContent = r["NewRecruitsContent"].ToString();
        n.NewRecruitsDate = Convert.ToDateTime(r["NewRecruitsDate"]);
      //  n.VisitCount = Convert.ToInt32(r["VisitCount"].ToString());
       // n.PlanAuthor = r["PlanAuthor"].ToString();
      //  n.PlanDate = Convert.ToDateTime(r["PlanDate"]);
      //  string sqlUpdate = "update NewRecruits set VisitCount=" + (n.VisitCount + 1) + " where  NewRecruitsID=" + n.NewRecruitsID;
       // DBHelper.INST.ExecuteSql(sqlUpdate);


        string sqlOtherNews = "select top 1 * from NewRecruits where NewRecruitsID > " + n.NewRecruitsID + " order by NewRecruitsID ASC";
        DataSet ds = DBHelper.INST.ExecuteSqlDS(sqlOtherNews);
        if (ds.Tables[0].Rows.Count != 0)
        {
            previousPage[0] = ds.Tables[0].Rows[0]["NewRecruitsDuty"].ToString();
            previousPage[0] = (previousPage[0].Length > 20 ? previousPage[0].Substring(0, 20) + "……" : previousPage[0]);
            previousPage[1] = ResolveUrl("~/oilStation/NewRecruitsContent.aspx") + "?NewRecruitsID=" + ds.Tables[0].Rows[0]["NewRecruitsID"].ToString();
        }
        else
        {
            previousPage[0] = "没有了";
            previousPage[1] = ResolveUrl("~/oilStation/NewRecruitsCenter.aspx");
        }
        ds.Dispose();
        sqlOtherNews = "select top 1 * from NewRecruits where NewRecruitsID < " + n.NewRecruitsID + " order by NewRecruitsID DESC";
        ds = DBHelper.INST.ExecuteSqlDS(sqlOtherNews);
        if (ds.Tables[0].Rows.Count != 0)
        {
            nextPage[0] = ds.Tables[0].Rows[0]["NewRecruitsDuty"].ToString();
            nextPage[0] = (nextPage[0].Length > 20 ? nextPage[0].Substring(0, 20) + "……" : nextPage[0]);
            nextPage[1] = ResolveUrl("~/oilStation/NewRecruitsContent.aspx") + "?NewRecruitsID=" + ds.Tables[0].Rows[0]["NewRecruitsID"].ToString();
        }
        else
        {
            nextPage[0] = "没有了";
            nextPage[1] = ResolveUrl("~/oilStation/NewRecruitsCenter.aspx");
        }
        r.Close();
        ds.Dispose();
    }
}