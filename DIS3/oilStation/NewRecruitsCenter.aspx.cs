using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;

public partial class oilStation_NewRecruitsCenter : System.Web.UI.Page
{
    protected string picture;  // 轮转图片的路径
    int pageSize = 10;
    int pageIndex = 1;
    String condition = null;
  //  String condition_direction = "IsDelete = 0";
    String condition_category = "IsDelete = 0";
  //  protected PageResult result_direction;
    protected PageResult result_category;
    protected PageResult result1;

    protected void Page_Load(object sender, EventArgs e)
    {
        //  XmlDocument document = new XmlDocument();
        //  document.Load(Server.MapPath("~/Footer.xml"));
        //   picture = ResolveUrl(document.DocumentElement.SelectSingleNode("picture").InnerText);
        if (Request["p"] != null)
        {
            pageIndex = Convert.ToInt32(Request["p"]);
        }
        else
        {
            pageIndex = 1;

        }
       if (Request["NewRecruitsCategoryID"] != null)
        {
            condition = "NewRecruits.IsDelete = 0 and NewRecruits.NewRecruitsCategoryID = NewRecruitsCategory.NewRecruitsCategoryID  and NewRecruits.OilProjectDirectionID = OilProjectDirection.OilProjectDirectionID and NewRecruitsCategory.NewRecruitsCategoryID=" + Convert.ToInt32(Request["NewRecruitsCategoryID"]);
        }
       else if (Request["OilProjectDirectionID"] != null)
        {
            condition = "NewRecruits.IsDelete = 0 and NewRecruits.NewRecruitsCategoryID = NewRecruitsCategory.NewRecruitsCategoryID  and NewRecruits.OilProjectDirectionID = OilProjectDirection.OilProjectDirectionID and NewRecruits.OilProjectDirectionID=" + Convert.ToInt32(Request["OilProjectDirectionID"]);
        }else
        {
            condition = "NewRecruits.IsDelete = 0 and NewRecruits.NewRecruitsCategoryID = NewRecruitsCategory.NewRecruitsCategoryID  and NewRecruits.OilProjectDirectionID = OilProjectDirection.OilProjectDirectionID";
        }
   
        DataPager p = new ExclusiveDataPager();

        result_category = p.PageData(1, 100, "NewRecruitsCategory", "*", "NewRecruitsCategoryID", condition_category, "asc");
     //   result_direction = p.PageData(1, 10, "OilProjectDirectionTitle", "*", "OilProjectDirectionID", condition_direction, "asc");
        result1 = p.PageData(pageIndex, pageSize, "NewRecruits,NewRecruitsCategory,OilProjectDirection", "*", "NewRecruitsID", condition, "asc");
        NewRecruitsList.DataSource = result_category.Result;
     //  OilProjectList.DataSource = result_direction.Result;

        NewRecruitsList.DataBind();
     // OilProjectList.DataBind();

        GridView1.DataSource = result1.Result;
        GridView1.DataBind();
    }
}