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

public partial class admin_ProjectEditor : System.Web.UI.Page
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
            this.AddProject.Enabled = false;
            this.Button1.Enabled = false;
            this.Button2.Enabled = false;
            this.Button5.Enabled = false;
            this.Button6.Enabled = false;
            return;
        }
        if (Session["UserName"].ToString() != "lin")
        {
            ErrorMessage.Text = "非管理员用户只能浏览项目，无权进行修改！";
            this.ErrorMessage.Visible = true;
            this.AddProject.Enabled = false;
            this.Button1.Enabled = false;
            this.Button2.Enabled = false;
            this.Button5.Enabled = false;
            this.Button6.Enabled = false;

            //this.LinkButton1.Enabled = false;
        }
        if (!IsPostBack)
        {
            DataSet ds = DBHelper.INST.ExecuteSqlDS("select * from OilProjectDirection where IsDelete = 0 ");
            // 为控件绑定数据源，必须是表 
            DropDownList1.DataSource = ds.Tables[0];
            // 选择为下拉列表提供数据的字段
            DropDownList1.DataValueField = "OilProjectDirectionID";
            DropDownList1.DataTextField = "OilProjectDirectionTitle";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "-请选择方向-");
            bindData();
            ds.Dispose();
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
        result = p.PageData(pageIndex, pageSize, "OilProject", "*", "OilProjectID", null, "asc");
        string dsStr = "select * from OilProject where IsDelete=" + 0;
        DataSet ds = DBHelper.INST.ExecuteSqlDS(dsStr);

        GridView1.DataSource = ds;//result1.Result;
        GridView1.DataBind();
        ds.Dispose();
   
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        String sqlstr1 = "delete from OilProject where IsDelete=" + "1";
        DBHelper.INST.ExecuteSql(sqlstr1);
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked == true)
            {
                string sqlstr2 = "update OilProject set IsDelete=" + 1 + " where OilProjectID=" + Convert.ToInt32(GridView1.DataKeys[i].Value);
                DBHelper.INST.ExecuteSql(sqlstr2);
            }
        }
        refleshDoc();
        Response.Redirect("ProjectManage.aspx");
    }

    protected void Button5_Click(object sender, EventArgs e)
    {

        String sqlstr = "update OilProject set IsDelete=" + 0 + " where IsDelete=" + 1;
        DBHelper.INST.ExecuteSql(sqlstr);
        refleshDoc();
        Response.Redirect("ProjectManage.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != "lin")
        {
            ErrorMessage.Text = "非管理员无权进行删除！！";
            this.ErrorMessage.Visible = true;
            return;
        }
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue != "-请选择方向-")
        {
            String sd = "select * from OilProject where OilProjectDirectionID= " + DropDownList1.SelectedValue + " and IsDelete=" + 0 ;
            DataSet ds1 = DBHelper.INST.ExecuteSqlDS(sd);
            GridView1.DataSource = ds1;//result1.Result;
            GridView1.DataBind();
            ds1.Dispose();
        }
        else
        {
            string dsStr = "select * from OilProject where IsDelete=" + 0;
            DataSet ds = DBHelper.INST.ExecuteSqlDS(dsStr);
            GridView1.DataSource = ds;//result1.Result;
            GridView1.DataBind();
            ds.Dispose();
        }

    }
    private void refleshDoc()
    {

        /*
 *刷新Footer.xml
 */
        String sdd = " select OilProjectID, OilProjectTitle from OilProject where IsDelete=" + 0 + " order by OilProjectID , OilProjectTitle";
        DataSet dss = DBHelper.INST.ExecuteSqlDS(sdd);

        XmlDocument document1 = new XmlDocument();
        document1.Load(Server.MapPath("~/Footer.xml"));
        XmlNode mNode = document1.DocumentElement.FirstChild;
        XmlNode mCurrentNode = mNode.SelectSingleNode("catalogue[@id='3']");
        mCurrentNode.RemoveAll();
        XmlAttribute attr1 = null, attr2 = null, attr3 = null;
        attr1 = document1.CreateAttribute("value");
        attr2 = document1.CreateAttribute("id");
        attr3 = document1.CreateAttribute("url");
        attr1.Value = "油站项目";
        attr2.Value = "3";
        attr3.Value = "~/oilStation/Project.aspx";
        mCurrentNode.Attributes.Append(attr1);
        mCurrentNode.Attributes.Append(attr2);
        mCurrentNode.Attributes.Append(attr3);


        int num = 0;
        for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
        {
            if (num < 5)
            {
                XmlElement item = document1.CreateElement("item");
                item.SetAttribute("url", "~/oilStation/OilProjectContent.aspx?OilProjectID=" + dss.Tables[0].Rows[i]["OilProjectID"].ToString());
                XmlText value = document1.CreateTextNode(dss.Tables[0].Rows[i]["OilProjectTitle"].ToString());
                item.AppendChild(value);
                mCurrentNode.AppendChild(item);
                num = num + 1;
            }
        }
        document1.Save(Server.MapPath("~/Footer.xml"));
        dss.Dispose();
        /*
* 刷新Web.sitemap
*/

        String sd = "select * from OilProject where IsDelete=" + 0 + " order by OilProjectDirectionID asc";
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
        Response.Write("<script language=javascript type ='text/javascript'>window.location = 'ProjectNameEditor.aspx'; </script>");

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindData();
    }


    protected void AddProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectEditor.aspx");
    }
    protected void ProjectNameEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectName.aspx");
    }
}