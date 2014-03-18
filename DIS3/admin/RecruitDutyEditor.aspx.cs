using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.Xml.Linq;
using System.Xml;

public partial class admin_RecruitDutyEditor : System.Web.UI.Page
{
    protected string editorContent;
    private int NewRecruitsID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["NewRecruitsID"] != null)
        {
            NewRecruitsID = Convert.ToInt32(Request["NewRecruitsID"].Trim());
        }
        else
        {
            NewRecruitsID = -1;
        }

        if (!IsPostBack)
        {
            DataSet ds1 = DBHelper.INST.ExecuteSqlDS("select * from NewRecruitsCategory where IsDelete=0");
            // 为控件绑定数据源，必须是表 
            DropDownList1.DataSource = ds1.Tables[0];
            // 选择为下拉列表提供数据的字段
            DropDownList1.DataValueField = "NewRecruitsCategoryID";
            DropDownList1.DataTextField = "NewRecruitsCategory";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "-请选择职位类别-");
            ds1.Dispose();
            DataSet ds2 = DBHelper.INST.ExecuteSqlDS("select * from OilProjectDirection where IsDelete=0");
            // 为控件绑定数据源，必须是表 
            DropDownList2.DataSource = ds2.Tables[0];
            // 选择为下拉列表提供数据的字段
            DropDownList2.DataValueField = "OilProjectDirectionID";
            DropDownList2.DataTextField = "OilProjectDirectionTitle";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "-请选择所属项目方向-");
            ds2.Dispose();
        }
        if (Request["NewRecruitsID"] != null)
        {
            /* NewRecruits n = new NewRecruits();
               NewRecruitsID = Convert.ToInt32(Request["NewRecruitsID"].Trim());
               string sql = "select * from NewRecruits where NewRecruitsID=" + NewRecruitsID;
               var r = DBHelper.INST.ExecuteSqlDR(sql);
               n.NewRecruitsDuty = r["NewRecruitsDuty"].ToString();
               n.NewRecruitsContent = r["NewRecruitsContent"].ToString();
               NewCategory.Text = n.NewRecruitsDuty;
               editorContent = n.NewRecruitsContent;
               */
            NewRecruitsID = Convert.ToInt32(Request["NewRecruitsID"].Trim());
            string sql = "select * from NewRecruitsCategory where NewRecruitsCategoryID = (select NewRecruitsCategoryID from NewRecruits where NewRecruitsID=" + Request["NewRecruitsID"] + ")";
            var r = DBHelper.INST.ExecuteSqlDR(sql);
            string sql1 = "select * from OilProjectDirection where OilProjectDirectionID = (select OilProjectDirectionID from NewRecruits where NewRecruitsID=" + Request["NewRecruitsID"] + ")";
            var r1 = DBHelper.INST.ExecuteSqlDR(sql1);
            string sql2 = "select * from NewRecruits where NewRecruitsID=" + Request["NewRecruitsID"];
            var r2 = DBHelper.INST.ExecuteSqlDR(sql2);
            r.Read();
            r1.Read();
            r2.Read();
            DropDownList1.SelectedValue = r["NewRecruitsCategoryID"].ToString();
            DropDownList2.SelectedValue = r1["OilProjectDirectionID"].ToString();
            NewCategory.Text = r2["NewRecruitsDuty"].ToString();
            editorContent = r2["NewRecruitsContent"].ToString();
            r.Close();
            r1.Close();
            r2.Close();
        }
        else
            NewRecruitsID = -1;
    }
        /*     String sd = "select * from NewRecruits where NewRecruitsID= " + Convert.ToInt32(Request["NewRecruitsID"]);
             DataSet ds = DBHelper.INST.ExecuteSqlDS(sd);

            // NewCategory.Text = ds.Tables[0].Rows[0]["NewRecruitsDuty"].ToString();
             DropDownList1.SelectedValue = ds1.Tables[0].Rows[0]["NewRecruitsCategoryID"].ToString();
             DropDownList2.SelectedValue = ds2.Tables[0].Rows[0]["OilProjectDirectionID"].ToString();
         }
         else
         {
                
             editorContent = "&lt;p&gt;This is some &lt;strong&gt;sample text&lt;/strong&gt;. ";
         }
         */
   
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (NewRecruitsID == -1)   
      {
        if (DropDownList1.SelectedItem.ToString() == "-请选择职位类别-")
        {
            Response.Write("<script language= javascript> alert('请选择职位类别')</script>");
            Response.Write("<script language=javascript type ='text/javascript'>window.location = 'RecruitDutyEditor.aspx'; </script>");
        }
        else if (DropDownList2.SelectedItem.ToString() == "-请选择项目方向-")
        {
            Response.Write("<script language= javascript> alert('请选择项目方向')</script>");
            Response.Write("<script language=javascript type ='text/javascript'>window.location = 'RecruitDutyEditor.aspx'; </script>");
        }
        else if (NewCategory.Text == string.Empty)
        {
            Response.Write("<script language= javascript> alert('职位不能为空')</script>");
            Response.Write("<script language=javascript type ='text/javascript'>window.location = 'RecruitDutyEditor.aspx'; </script>");
        }
        else
            {
             //   String sql = "insert into NewRecruits (NewRecruitsDuty, NewRecruitsContent, NewRecruitsCategoryID, IsDelete) values('" + BoxProjectName.Text.ToString() + "','" + Request["editor1"].ToString() + "'," + DropDownList1.SelectedValue + "," + 0 + ")";
                String sql = "insert into  NewRecruits (NewRecruitsDuty,NewRecruitsContent,NewRecruitsDate,NewRecruitsCategoryID,OilProjectDirectionID,IsDelete) values('"
                    + NewCategory.Text.ToString() + "','" + Request["editor1"].ToString() + "',#" + DateTime.Now.ToLocalTime().ToString() + "#," + DropDownList1.SelectedValue + "," + DropDownList2.SelectedValue + "," + 0 + ")";
                DBHelper.INST.ExecuteSql(sql);
                refleshDoc();
                
            Response.Write("<script type='text/javascript'>		alert('发布成功');window.location = 'RecruitDutyEditor.aspx';</script>");
            Response.Redirect("RecruitDutyManage.aspx");
        }
        }
    else
    {
         if (DropDownList1.SelectedItem.ToString() == "-请选择职位类别-")
        {
            Response.Write("<script language= javascript> alert('请选择职位类别')</script>");
            Response.Write("<script language=javascript type ='text/javascript'>window.location = 'RecruitDutyEditor.aspx'; </script>");
        }
        else if (DropDownList2.SelectedItem.ToString() == "-请选择项目方向-")
        {
            Response.Write("<script language= javascript> alert('请选择项目方向')</script>");
            Response.Write("<script language=javascript type ='text/javascript'>window.location = 'RecruitDutyEditor.aspx'; </script>");

        }
        else if (NewCategory.Text == string.Empty)
        {
            Response.Write("<script language= javascript> alert('职位不能为空')</script>");
            Response.Write("<script language=javascript type ='text/javascript'>window.location = 'RecruitDutyEditor.aspx'; </script>");
        }
        else
            {
                String sql = "update NewRecruits set NewRecruitsDuty='" + NewCategory.Text.ToString() + "',NewRecruitsContent='"
                    + Request["editor1"].ToString() + "',NewRecruitsDate=#" + DateTime.Now.ToLocalTime().ToString() + "#,NewRecruitsCategoryID=" + DropDownList1.SelectedValue 
                    + ",OilProjectDirectionID=" + DropDownList2.SelectedValue + ",IsDelete=" + 0 + " where NewRecruitsID=" + NewRecruitsID;
                DBHelper.INST.ExecuteSql(sql);
                //  DBHelper.INST.ExecuteSql(sqlstr2);
                //  NewCategory.Text = "";

                refleshDoc();
                Response.Write("<script type='text/javascript'>		alert('发布成功');window.location = 'RecruitDutyEditor.aspx';</script>");
                Response.Redirect("RecruitDutyManage.aspx");
                //   bindData();
                //   Response.Write("<script type='text/javascript'>		alert('发布成功');window.location = 'RecruitDutyEditor.aspx';</script>");
            }
        }
    }

    private void refleshDoc()
    {
        /*刷新web.sitemap
               */
        String sd = "select * from NewRecruits where IsDelete=" + 0 + " order by NewRecruitsCategoryID asc";
        DataSet ds1 = DBHelper.INST.ExecuteSqlDS(sd);
        sd = "select * from NewRecruitsCategory where IsDelete=" + 0;
        DataSet ds2 = DBHelper.INST.ExecuteSqlDS(sd);
        XmlDocument document2 = new XmlDocument();
        document2.Load(Server.MapPath("~/Web.sitemap"));
        XmlNode mNode2 = document2.DocumentElement.FirstChild;
        XmlNode mCurrentNode2 = mNode2.SelectSingleNode("siteMapNode[@title='油站招新']");
        mCurrentNode2.RemoveAll();

        XmlAttribute attr11 = null, attr12 = null, attr13 = null;
        attr11 = document2.CreateAttribute("url");
        attr12 = document2.CreateAttribute("title");
        attr13 = document2.CreateAttribute("description");
        attr11.Value = "~/oilStation/NewRecruitsCenter.aspx";
        attr12.Value = "油站招新";
        attr13.Value = "";
        mCurrentNode2.Attributes.Append(attr11);
        mCurrentNode2.Attributes.Append(attr12);
        mCurrentNode2.Attributes.Append(attr13);
        if (ds2.Tables[0].Rows.Count != 0)
        {
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                XmlElement siteMapNode = document2.CreateElement("siteMapNode");
                siteMapNode.SetAttribute("url", "~/oilStation/NewRecruitsCenter.aspx?NewRecruitsCategoryID=" + ds2.Tables[0].Rows[i]["NewRecruitsCategoryID"].ToString());
                XmlAttribute attr111 = null, attr112 = null;
                attr111 = document2.CreateAttribute("title");
                attr112 = document2.CreateAttribute("description");
                attr111.Value = ds2.Tables[0].Rows[i]["NewRecruitsCategory"].ToString();
                attr112.Value = "";
                siteMapNode.Attributes.Append(attr111);
                siteMapNode.Attributes.Append(attr112);
                mCurrentNode2.AppendChild(siteMapNode);
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        if (ds1.Tables[0].Rows[j]["NewRecruitsCategoryID"].ToString() == ds2.Tables[0].Rows[i]["NewRecruitsCategoryID"].ToString())
                        {
                            XmlElement projectNode = document2.CreateElement("siteMapNode");
                            projectNode.SetAttribute("url", "~/oilStation/NewRecruitsContent.aspx?NewRecruitsID=" + ds1.Tables[0].Rows[j]["NewRecruitsID"].ToString());
                            attr111 = document2.CreateAttribute("title");
                            attr112 = document2.CreateAttribute("description");
                            attr111.Value = ds1.Tables[0].Rows[j]["NewRecruitsDuty"].ToString();
                            attr112.Value = "";
                            projectNode.Attributes.Append(attr111);
                            projectNode.Attributes.Append(attr112);
                            siteMapNode.AppendChild(projectNode);
                        }
                    }
                }

            }
        }
        document2.Save(Server.MapPath("~/Web.sitemap"));
        ds1.Dispose();
        ds2.Dispose();
    }
}