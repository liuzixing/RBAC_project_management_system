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

public partial class admin_RecruitCategoryManage : System.Web.UI.Page
{
  //  int pageSize = 10;
    int pageIndex = 1;
    protected PageResult result;
  // private int NewRecruitsCategoryID;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
           /* DataSet ds = DBHelper.INST.ExecuteSqlDS("select * from NewRecruitsCategory ");
            if (Request["OilProjectID"] != null)
            {
            }
            */
            if (Request["NewRecruitsCategoryID"] != null)
            {
                String sd = "select * from NewRecruitsCategory where NewRecruitsCategoryID= " + Convert.ToInt32(Request["NewRecruitsCategoryID"]);
                DataSet ds1 = DBHelper.INST.ExecuteSqlDS(sd);

                txtTitle.Text = ds1.Tables[0].Rows[0]["NewRecruitsCategory"].ToString();

             /*   string sql = "select * from NewRecruitsCategory where NewRecruitsCategoryID=" + NewRecruitsCategoryID;// +" and NewsState<>'0'";
                var r = DBHelper.INST.ExecuteSqlDR(sql);
                r.Read();
                //                 int newsCategoryID = Convert.ToInt32(r["NewsCategoryID"].ToString());
                //                 String s = " select NewsCategoryName from NewsCategories where NewsCategoryID=" + newsCategoryID;
                //                 DataSet dsNews = DBHelper.INST.ExecuteSqlDS(s);
                //                 string newsCategoryName = dsNews.Tables[0].Rows[0][0].ToString();
                //                 DropDownList1.SelectedValue = newsCategoryName;
                txtTitle.Text = r["NewRecruitsCategory"].ToString();
            
              */
                ds1.Dispose();
              }
            bindData();
            
        }
    }

    void bindData()
    {
        InitPage();
        if (Request["p"] != null)
            pageIndex = Convert.ToInt32(Request["p"]);
        else
            pageIndex = 1;
        DataPager p = new ExclusiveDataPager();
        string dsStr = "select * from NewRecruitsCategory where IsDelete=0 ";
        DataSet ds = DBHelper.INST.ExecuteSqlDS(dsStr);

        GridView1.DataSource = ds;//result1.Result;
        GridView1.DataBind();
        ds.Dispose();
    }
    //添加职位类别
    protected void AddBtn_Click(object sender, EventArgs e)
    {
        if (txtTitle.Text == string.Empty)
        {
            Response.Write("<script language= javascript> alert('项目不能为空')</script>");
            Response.Write("<script language=javascript type ='text/javascript'>window.location = 'RecruitCategoryManage.aspx'; </script>");
        }
        else
        {
            if (txtTitle.Text.Length > 20)
            {
                Response.Write("<script  language= javascript> alert('项目长度超过上限') </script>");
                Response.Write("<script language=javascript type ='text/javascript'>window.location = 'RecruitCategoryManage.aspx'; </script>");
            }
            else
            {
                String sqlstr;
                sqlstr = "insert into  NewRecruitsCategory (NewRecruitsCategory,NewRecruitsCategoryDate,IsDelete) values('"
                + txtTitle.Text + "','"+ DateTime.Now.ToLocalTime().ToString()+"',"  + 0 + ")";//尚需修改;
                DBHelper.INST.ExecuteSql(sqlstr);
                txtTitle.Text = "";
                bindData();
                refleshDoc();
            }
        }
    }
    //编辑
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        bindData();
    }

    //更新
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string sqlstr = "update NewRecruitsCategory set NewRecruitsCategory='"
            + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim() +  "' where NewRecruitsCategoryID=" + GridView1.DataKeys[e.RowIndex].Value.ToString();
        DBHelper.INST.ExecuteSqlDS(sqlstr);
        GridView1.EditIndex = -1;
        bindData();
        refleshDoc();
    }

    //取消
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        bindData();
    }
    private void InitPage()
    {
       // CheckBox2.Checked = false;
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            cbox.Checked = false;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        String sqlstr1 = "delete from NewRecruitsCategory where IsDelete=" + "1";
        DBHelper.INST.ExecuteSql(sqlstr1);
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked == true)
            {
                string sqlstr = "update NewRecruitsCategory set IsDelete=1 where NewRecruitsCategoryID=" + GridView1.DataKeys[i].Value;
                DBHelper.INST.ExecuteSql(sqlstr);
            }
        }
        bindData();
        refleshDoc();
        Response.Redirect("RecruitCategoryManage.aspx");
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

        String sqlstr = "update NewRecruitsCategory set IsDelete=" + 0 + " where IsDelete=" + 1;
        DBHelper.INST.ExecuteSql(sqlstr);
        refleshDoc();
        Response.Redirect("RecruitCategoryManage.aspx");
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
        /*
         *刷新Footer.xml
         */
        String sdd = " select NewRecruitsCategoryID, NewRecruitsCategory from NewRecruitsCategory where IsDelete=" + 0 + " order by NewRecruitsCategoryID , NewRecruitsCategory";
        DataSet dss = DBHelper.INST.ExecuteSqlDS(sdd);

        XmlDocument document1 = new XmlDocument();
        document1.Load(Server.MapPath("~/Footer.xml"));
        XmlNode mNode = document1.DocumentElement.FirstChild;
        XmlNode mCurrentNode = mNode.SelectSingleNode("catalogue[@id='5']");
        mCurrentNode.RemoveAll();

        XmlAttribute attr1 = null, attr2 = null, attr3 = null;
        attr1 = document1.CreateAttribute("value");
        attr2 = document1.CreateAttribute("id");
        attr3 = document1.CreateAttribute("url");
        attr1.Value = "油站招新";
        attr2.Value = "5";
        int countt = dss.Tables[0].Rows.Count;
        attr3.Value = "~/oilStation/NewRecruitsCenter.aspx";
        mCurrentNode.Attributes.Append(attr1);
        mCurrentNode.Attributes.Append(attr2);
        mCurrentNode.Attributes.Append(attr3);


        int num = 0;
        for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
        {
            if (num < 5)
            {
                XmlElement item = document1.CreateElement("item");
                item.SetAttribute("url", "~/oilStation/NewRecruitsCenter.aspx?NewRecruitsCategoryID=" + dss.Tables[0].Rows[i]["NewRecruitsCategoryID"].ToString());
                XmlText value = document1.CreateTextNode(dss.Tables[0].Rows[i]["NewRecruitsCategory"].ToString());
                item.AppendChild(value);
                mCurrentNode.AppendChild(item);
                num = num + 1;
            }
        }
        document1.Save(Server.MapPath("~/Footer.xml"));
        dss.Dispose();
    }

}