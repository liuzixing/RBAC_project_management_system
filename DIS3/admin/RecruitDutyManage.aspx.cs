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
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Web.Caching;

public partial class admin_RecruitDutyManage : System.Web.UI.Page
{
    int pageSize = 10;
    int pageIndex = 1;
    protected PageResult result; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            ErrorMessage.Text = "请先登录！";
            this.ErrorMessage.Visible = true;

            this.CategoryManage.Visible = false;
            this.AddRecruitDuty.Visible = false;
            this.Button1.Visible = false;
            this.Button5.Visible = false;
            this.Button6.Visible = false;
            return;
        }
        if (Session["UserName"].ToString() != "lin")
        {
            ErrorMessage.Text = "非管理员用户不能浏览当前页面！";
            this.ErrorMessage.Visible = true;

            this.CategoryManage.Visible = false;
            this.AddRecruitDuty.Visible = false;
            this.Button1.Visible = false;
            this.Button5.Visible = false;
            this.Button6.Visible = false;
            return;

            //this.LinkButton1.Enabled = false;
        }
        if (!IsPostBack)
        {
            DataSet ds = DBHelper.INST.ExecuteSqlDS("select * from NewRecruitsCategory where IsDelete = 0");
            // 为控件绑定数据源，必须是表 
            DropDownList1.DataSource = ds.Tables[0];
            // 选择为下拉列表提供数据的字段
            DropDownList1.DataValueField = "NewRecruitsCategoryID";
            DropDownList1.DataTextField = "NewRecruitsCategory";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "-请选择职位类别-");
            if (Request["NewRecruitsID"] != null)
            {

            }

            bindData();
            ds.Dispose();
        }
    }

    void bindData()
    {
        string dsStr = "select * from NewRecruits as A,NewRecruitsCategory as B,OilProjectDirection as C where A.IsDelete=0 and A.NewRecruitsCategoryID=B.NewRecruitsCategoryID "
            + "and A.OilProjectDirectionID=C.OilProjectDirectionID";
        DataSet ds = DBHelper.INST.ExecuteSqlDS(dsStr);

        GridView1.DataSource = ds;//result1.Result;
        GridView1.DataBind();
        InitPage();
        ds.Dispose();
    }
    private void InitPage()
    {
//         CheckBox CheckBox2 = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox2");
//         CheckBox2.Checked = false;
//         for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
//         {
//             CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
//             cbox.Checked = false;
//         }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        String sqlstr1 = "delete from NewRecruits where IsDelete=" + "1";
        DBHelper.INST.ExecuteSql(sqlstr1);
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked == true)
            {

                String sqlstr = "update NewRecruits set IsDelete=" + 1 + " where NewRecruitsID=" + Convert.ToInt32(GridView1.DataKeys[i].Value);
                DBHelper.INST.ExecuteSql(sqlstr);
            }
        }
        refleshDoc();
        bindData();
        Response.Redirect("RecruitDutyManage.aspx");
    }
    protected void DeleteBtn_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.Parent.Parent as GridViewRow;
        CheckBox cbox = (CheckBox)row.FindControl("CheckBox1");
        cbox.Checked = true;
        Button2_Click(sender, e);
    }

    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox CheckBox2 = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox2");
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (CheckBox2.Checked == true)
            {
                cbox.Checked = true;
            }
            else
            {
                cbox.Checked = false;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        CheckBox CheckBox2 = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox2");
        CheckBox2.Checked = false;
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            cbox.Checked = false;
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {

        String sqlstr = "update NewRecruits set IsDelete=" + 0 + " where IsDelete=" + 1;
        DBHelper.INST.ExecuteSql(sqlstr);
        refleshDoc();
        Response.Redirect("RecruitDutyManage.aspx");
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        String sd;
        if (DropDownList1.SelectedIndex == 0)
            sd = "select * from NewRecruits as A,NewRecruitsCategory as B,OilProjectDirection as C where A.IsDelete=0 and A.NewRecruitsCategoryID=B.NewRecruitsCategoryID "
            + "and A.OilProjectDirectionID=C.OilProjectDirectionID";
        else
            sd = "select * from NewRecruits as A,NewRecruitsCategory as B,OilProjectDirection as C where A.IsDelete=0 and A.NewRecruitsCategoryID=B.NewRecruitsCategoryID "
          + " and A.OilProjectDirectionID=C.OilProjectDirectionID " + " and A.NewRecruitsCategoryID= " + DropDownList1.SelectedValue;
        DataSet ds1 = DBHelper.INST.ExecuteSqlDS(sd);
        GridView1.DataSource = ds1;//result1.Result;
        GridView1.DataBind();
        ds1.Dispose();
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindData();
    }
    protected void AddRecruitDuty_Click(object sender, EventArgs e)
    {
        Response.Redirect("RecruitDutyEditor.aspx");
    }
    protected void AddRecruitCategory_Click(object sender, EventArgs e)
    {
        Response.Redirect("RecruitCategoryManage.aspx");
    }
}