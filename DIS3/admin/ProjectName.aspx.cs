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
using System.Data.SqlClient;
public partial class admin_NewRecruitsCategoryProjectNameEditor : System.Web.UI.Page
{
    public static int pageIndex = 1;
    int pageSize = 25;
    String condition = null;
    protected PageResult result1;
    public String w;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindData();
        }
    }
    private void InitPage()
    {
        //CheckBox CheckBox2 = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox2");
        //CheckBox2.Checked = false;
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            cbox.Checked = false;
        }

    }
    void bindData()
    {
        InitPage();

        DataPager p = new ExclusiveDataPager();

        //result1 = p.PageData(pageIndex, pageSize, "NewRecruits", "distinct ProjectName", "OilProjectDirectionID", condition, "asc");
        string dsStr = "select * from OilProjectDirection where IsDelete=" + 0;
        DataSet ds = DBHelper.INST.ExecuteSqlDS(dsStr);

        GridView1.DataSource = ds;//result1.Result;
        GridView1.DataBind();
        ds.Dispose();


    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write("<script language=javascript type ='text/javascript'>window.location = 'ProjectNameEditor.aspx'; </script>");
        InitPage();
        Response.Redirect("ProjectName.aspx");

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        String sqlstr1 = "delete from OilProjectDirection where IsDelete=" + "1";
        DBHelper.INST.ExecuteSql(sqlstr1);
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked == true)
            {
                String sqlstr = "update OilProjectDirection set IsDelete=" + 1 + " where OilProjectDirectionID=" + Convert.ToInt32(GridView1.DataKeys[i].Value);
                DBHelper.INST.ExecuteSql(sqlstr);
                string sqlstr2 = "update OilProject set IsDelete=" + 1 + " where OilProjectDirectionID=" + Convert.ToInt32(GridView1.DataKeys[i].Value);
                DBHelper.INST.ExecuteSql(sqlstr2);
                string sqlstr3 = "update NewRecruits set IsDelete=" + 1 + " where OilProjectDirectionID=" + Convert.ToInt32(GridView1.DataKeys[i].Value);
                DBHelper.INST.ExecuteSql(sqlstr3);
            }
        }
        refleshDoc();
        Response.Redirect("ProjectName.aspx");

    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox CheckBox2 = (CheckBox)GridView1.HeaderRow.FindControl("CheckBox2");
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            cbox.Checked = CheckBox2.Checked;
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {

        String sqlstr = "update OilProjectDirection set IsDelete=" + 0 + " where IsDelete=" + 1;
        DBHelper.INST.ExecuteSql(sqlstr);
        string sqlstr2 = "update OilProject set IsDelete=" + 0 + " where IsDelete=" + 1;
        DBHelper.INST.ExecuteSql(sqlstr2);
        string sqlstr3 = "update NewRecruits set IsDelete=" + 0 + " where IsDelete=" + 1;
        DBHelper.INST.ExecuteSql(sqlstr3);
        refleshDoc();
        Response.Redirect("ProjectName.aspx");
    }

    protected void Back_Click(object sender, EventArgs e)
    {
        if (pageIndex > 1)
        {
            pageIndex--;
            bindData();
        }
        else
        {
        }
        pagesL.Text = Convert.ToString(pageIndex);
    }
    protected void Next_Click(object sender, EventArgs e)
    {
        pageIndex++;
        bindData();
        pagesL.Text = Convert.ToString(pageIndex);
    }

    protected void DeleteBtn_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.Parent.Parent as GridViewRow;
        CheckBox cbox = (CheckBox)row.FindControl("CheckBox1");
        cbox.Checked = true;
        Button2_Click(sender, e);
    }

    private void refleshDoc()
    {

        /*
* 刷新Web.sitemap
*/

        String sd = "select * from OilProject where IsDelete=" + 0 + " order by OilProjectDirectionID";
        DataSet ds1 = DBHelper.INST.ExecuteSqlDS(sd);
        sd = "select * from OilProjectDirection where IsDelete=" + 0;
        DataSet ds2 = DBHelper.INST.ExecuteSqlDS(sd);
        XmlDocument document2 = new XmlDocument();
        document2.Load(Server.MapPath("~/Web.sitemap"));
        XmlNode mNode2 = document2.DocumentElement.FirstChild;
        XmlNode mCurrentNode2 = mNode2.SelectSingleNode("siteMapNode[@title='油站项目']");
        mCurrentNode2.RemoveAll();

        XmlAttribute attr11 = null, attr12 = null, attr13 = null;
        attr11 = document2.CreateAttribute("url");
        attr12 = document2.CreateAttribute("title");
        attr13 = document2.CreateAttribute("description");
        attr11.Value = "~/oilStation/Project.aspx";
        attr12.Value = "油站项目";
        attr13.Value = "";
        mCurrentNode2.Attributes.Append(attr11);
        mCurrentNode2.Attributes.Append(attr12);
        mCurrentNode2.Attributes.Append(attr13);
        if (ds2.Tables[0].Rows.Count != 0)
        {
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                XmlElement siteMapNode = document2.CreateElement("siteMapNode");
                siteMapNode.SetAttribute("url", "~/oilStation/Project.aspx?OilProjectDirectionID=" + ds2.Tables[0].Rows[i]["OilProjectDirectionID"].ToString());
                XmlAttribute attr111 = null, attr112 = null;
                attr111 = document2.CreateAttribute("title");
                attr112 = document2.CreateAttribute("description");
                attr111.Value = ds2.Tables[0].Rows[i]["OilProjectDirectionTitle"].ToString();
                attr112.Value = "";
                siteMapNode.Attributes.Append(attr111);
                siteMapNode.Attributes.Append(attr112);
                mCurrentNode2.AppendChild(siteMapNode);
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        if (ds1.Tables[0].Rows[j]["OilProjectDirectionID"].ToString() == ds2.Tables[0].Rows[i]["OilProjectDirectionID"].ToString())
                        {
                            XmlElement projectNode = document2.CreateElement("siteMapNode");
                            projectNode.SetAttribute("url", "~/oilStation/OilProjectContent.aspx?OilProjectID=" + ds1.Tables[0].Rows[j]["OilProjectID"].ToString());
                            attr111 = document2.CreateAttribute("title");
                            attr112 = document2.CreateAttribute("description");
                            attr111.Value = ds1.Tables[0].Rows[j]["OilProjectTitle"].ToString();
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
    private void GridViewBind()
    {


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        bindData();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        bindData();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string sqlstr = "update OilProjectDirection set OilProjectDirectionTitle='"
           + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim() + "'where OilProjectDirectionID="
           + GridView1.DataKeys[e.RowIndex].Value.ToString();
        DBHelper.INST.ExecuteSql(sqlstr);
        GridView1.EditIndex = -1;
        bindData();
        refleshDoc();
        Response.Redirect("ProjectName.aspx");

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteBtn_Click(sender, e);
    }
    protected void addBtn_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text == string.Empty)
        {
            Response.Write("<script language= javascript> alert('项目不能为空')</script>");
            Response.Write("<script language=javascript type ='text/javascript'>window.location = 'ProjectName.aspx'; </script>");
        }
        else
        {
            if (TextBox1.Text.Length > 20)
            {
                Response.Write("<script  language= javascript> alert('项目长度超过上限') </script>");
                Response.Write("<script language=javascript type ='text/javascript'>window.location = 'ProjectName.aspx'; </script>");
            }
            else
            {
                String sqlstr;
                sqlstr = "insert into  OilProjectDirection (OilProjectDirectionTitle,IsDelete) values('"
                + TextBox1.Text + "'," + 0 + ")";//尚需修改;
                DBHelper.INST.ExecuteSql(sqlstr);
                TextBox1.Text = "";
                refleshDoc();
                Response.Redirect("ProjectName.aspx");
            }
        }
    }

     protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
         GridView1.PageIndex = e.NewPageIndex;
         bindData();
     }
}


   

