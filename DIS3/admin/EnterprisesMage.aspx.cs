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

public partial class admin_EnterprisesMage : System.Web.UI.Page
{
    //int pageCount;
    int pageSize = 10;
    int pageIndex = 1;
    // String condition = "";
    protected PageResult result;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            ErrorMessage.Text = "请先登录！";
            this.ErrorMessage.Visible = true;
            this.AddEnterprises.Visible = false;
            this.Button3.Visible = false;
            this.Button5.Visible = false;
            this.Button6.Visible = false;
            return;
        }

        if (Session["UserName"].ToString() != "lin")
        {
            ErrorMessage.Text = "非管理员用户不能浏览当前页面！";
            this.ErrorMessage.Visible = true;
            this.AddEnterprises.Visible = false;
            this.Button3.Visible = false;
            this.Button5.Visible = false;
            this.Button6.Visible = false;
            return;
        }
        if (!IsPostBack)
        {
            bindData();
        }
    }
     void bindData()
    {

        if (Request["p"] != null)
            pageIndex = Convert.ToInt32(Request["p"]);
        else
            pageIndex = 1;

        DataPager p = new ExclusiveDataPager();
        result = p.PageData(pageIndex, pageSize, "CooperationEnterprises", "*", "EnterprisesID", null, "asc");
        string dsStr = "select * from CooperationEnterprises where IsDelete=" + 0;
        DataSet ds = DBHelper.INST.ExecuteSqlDS(dsStr);
        GridView1.DataSource = ds;//result1.Result;
        GridView1.DataBind();
        ds.Dispose(); 
       /*  
         GridView1.DataSource = result.Result;
        GridView1.DataKeyNames = new string[] { "EnterprisesID" };
        GridView1.DataBind();
        */ 

    }

     protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
         GridView1.PageIndex = e.NewPageIndex;
         bindData();
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
        String sqlstr1 = "delete from CooperationEnterprises where IsDelete=" + "1";
        DBHelper.INST.ExecuteSql(sqlstr1);
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked == true)
            {
                string sqlstr2 = "update CooperationEnterprises set IsDelete=" + 1 + " where EnterprisesID =" + Convert.ToInt32(GridView1.DataKeys[i].Value);
                DBHelper.INST.ExecuteSql(sqlstr2);
            }
        }
           refleshDoc();
        Response.Redirect("EnterprisesMage.aspx");



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

        String sqlstr = "update CooperationEnterprises set IsDelete=" + 0 + " where IsDelete=" + 1;
        DBHelper.INST.ExecuteSql(sqlstr);
           refleshDoc();
        Response.Redirect("EnterprisesMage.aspx");
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
    protected void AddEnterprises_Click(object sender, EventArgs e)
    {
        Response.Redirect("EnterprisesEditor.aspx");
    }
    protected void refleshDoc()
    {
        /*
 * 刷新Footer.xml
 */


        String sd = " select EnterprisesID, EnterprisesName from CooperationEnterprises where IsDelete=0 order by EnterprisesID desc, EnterprisesName desc";
        DataSet ds = DBHelper.INST.ExecuteSqlDS(sd);

        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Footer.xml"));
        XmlNode mNode = document.DocumentElement.FirstChild;
        XmlNode mCurrentNode = mNode.SelectSingleNode("catalogue[@id='6']");
        mCurrentNode.RemoveAll();


        XmlAttribute attr1 = null, attr2 = null, attr3 = null;
        attr1 = document.CreateAttribute("value");
        attr2 = document.CreateAttribute("id");
        attr3 = document.CreateAttribute("url");
        attr1.Value = "合作企业";
        attr2.Value = "6";
        attr3.Value = "~/oilStation/coEnterprise.aspx?EnterprisesID=" + ds.Tables[0].Rows[0]["EnterprisesID"].ToString();
        mCurrentNode.Attributes.Append(attr1);
        mCurrentNode.Attributes.Append(attr2);
        mCurrentNode.Attributes.Append(attr3);



        int num = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (num < 5)
            {
                XmlElement item = document.CreateElement("item");
                item.SetAttribute("url", "~/oilStation/coEnterprise.aspx?EnterprisesID=" + ds.Tables[0].Rows[i]["EnterprisesID"].ToString());
                XmlText value = document.CreateTextNode(ds.Tables[0].Rows[i]["EnterprisesName"].ToString());
                item.AppendChild(value);
                mCurrentNode.AppendChild(item);
                num = num + 1;
            }
        }
        document.Save(Server.MapPath("~/Footer.xml"));
        ds.Dispose();

        /*
         * 刷新Web.sitemap
         */

        String sdd = " select EnterprisesID, EnterprisesName from CooperationEnterprises where IsDelete=0 order by EnterprisesID desc, EnterprisesName desc";
        DataSet dss = DBHelper.INST.ExecuteSqlDS(sdd);

        XmlDocument document2 = new XmlDocument();
        document2.Load(Server.MapPath("~/Web.sitemap"));
        XmlNode mNode2 = document2.DocumentElement.FirstChild;
        XmlNode mCurrentNode2 = mNode2.SelectSingleNode("siteMapNode[@title='合作企业']");
        mCurrentNode2.RemoveAll();

        XmlAttribute attr11 = null, attr12 = null, attr13 = null;
        attr11 = document2.CreateAttribute("url");
        attr12 = document2.CreateAttribute("title");
        attr13 = document2.CreateAttribute("description");
        attr11.Value = "~/oilStation/coEnterprise.aspx";
        attr12.Value = "合作企业";
        attr13.Value = "";
        mCurrentNode2.Attributes.Append(attr11);
        mCurrentNode2.Attributes.Append(attr12);
        mCurrentNode2.Attributes.Append(attr13);





        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            XmlElement siteMapNode = document2.CreateElement("siteMapNode");
            siteMapNode.SetAttribute("url", "~/oilStation/coEnterprise.aspx?EnterprisesID=" + dss.Tables[0].Rows[i]["EnterprisesID"].ToString());
            XmlAttribute attr111 = null, attr112 = null;
            attr111 = document2.CreateAttribute("title");
            attr112 = document2.CreateAttribute("description");
            attr111.Value = dss.Tables[0].Rows[i]["EnterprisesName"].ToString();
            attr112.Value = "";
            siteMapNode.Attributes.Append(attr111);
            siteMapNode.Attributes.Append(attr112);

            mCurrentNode2.AppendChild(siteMapNode);

        }
        document2.Save(Server.MapPath("~/Web.sitemap"));
        dss.Dispose();
    }
}
