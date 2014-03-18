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

public partial class admin_Default : System.Web.UI.Page
{

    //int pageCount;
    int pageSize = 10;
    int pageIndex = 1;
    // String condition = "";
    protected PageResult result;



    protected void page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            ErrorMessage.Text = "请先登录！";
            this.ErrorMessage.Visible = true;
            this.AddNews.Visible = false;
            this.Button1.Visible = false;
            this.Button2.Visible = false;
            this.Button3.Visible = false;
            return;
        }

        if (Session["UserName"].ToString() != "lin")
        {
            ErrorMessage.Text = "非管理员用户不能浏览当前页面！";
            this.ErrorMessage.Visible = true;
            this.AddNews.Visible = false;
            this.Button1.Visible = false;
            this.Button2.Visible = false;
            this.Button3.Visible = false;
            return;
        }
        if (!IsPostBack)
        {
            bindData();
        }


    }

    void bindData()
    {
        string dsStr = "select * from News where IsDelete= 0";
        DataSet ds = DBHelper.INST.ExecuteSqlDS(dsStr);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        ds.Dispose();
    }


    protected void DeleteBtn_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.Parent.Parent as GridViewRow;
        CheckBox cbox = (CheckBox)row.FindControl("CheckBox1");
        cbox.Checked = true;
        Button2_Click(sender, e);
        refleshDoc();
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        String sqlstr1 = "delete from News where IsDelete=" + "1";
        DBHelper.INST.ExecuteSql(sqlstr1);
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbox.Checked == true)
            {
                String sqlstr = "update News set IsDelete=" + 1 + " where NewsID=" + Convert.ToInt32(GridView1.DataKeys[i].Value);
                DBHelper.INST.ExecuteSql(sqlstr);
            }
        }

        bindData();
        refleshDoc();
        Response.Redirect("NewsManage.aspx");
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindData();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        String sqlstr = "update News set IsDelete=" + 0 + " where IsDelete=" + 1;
        DBHelper.INST.ExecuteSql(sqlstr);
        bindData();
        refleshDoc();
        Response.Redirect("NewsManage.aspx");
    }
    protected void AddNews_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewsEditor.aspx");
    }

    private void refleshDoc()
    {
        /*
         * ADD 写入XML文件
        */
        String sd = " select NewsYear, NewsMonth from News order by NewsYear desc, NewsMonth desc";
        DataSet dsDate = DBHelper.INST.ExecuteSqlDS(sd);

        XmlDocument document = new XmlDocument();           // 操作Date.xml

        //                XmlDocument docSiteMap = new XmlDocument();

        document.Load(Server.MapPath("~/Date.xml"));

        //                docSiteMap.Load(Server.MapPath("~/Web.sitemap"));

        XmlElement root = document.DocumentElement;
        root.RemoveAll();
        XmlElement year = document.CreateElement("year");
        year.SetAttribute("value", dsDate.Tables[0].Rows[0]["NewsYear"].ToString());

        XmlElement month = document.CreateElement("month");
        XmlText monthValue = document.CreateTextNode(dsDate.Tables[0].Rows[0]["NewsMonth"].ToString());
        month.AppendChild(monthValue);
        year.AppendChild(month);
        root.AppendChild(year);

        //               XmlElement rootSiteMap = docSiteMap.DocumentElement;
        //XmlNode dateNode = rootSiteMap.SelectSingleNode("//siteMapNode[@description='date']");
        //while (dateNode != null)
        //{
        //    dateNode.ParentNode.RemoveChild(dateNode);
        //    dateNode = rootSiteMap.SelectSingleNode("//siteMapNode[@description='date']");
        //}

        // 删除原本的与时间归档导航有关的siteMapNode节点
        //                XmlNodeList dateNodeList = rootSiteMap.SelectNodes("//siteMapNode[@description='date']");
        //                for (int i = 0; i < dateNodeList.Count; i++)
        //                {
        //                    dateNodeList.Item(i).ParentNode.RemoveChild(dateNodeList.Item(i));
        //                }
        // 写入与时间归档导航有关的siteMapNode节点
        //                XmlNode newsCenter = rootSiteMap.SelectSingleNode("//siteMapNode[@description='newsCenter']");
        //                XmlElement siteMapNodeByDate = docSiteMap.CreateElement("siteMapNode");
        //                siteMapNodeByDate.SetAttribute("url", "~/NewsList.aspx?NewsYear=" + dsDate.Tables[0].Rows[0]["NewsYear"].ToString() + "&month=");
        //                siteMapNodeByDate.SetAttribute("title", dsDate.Tables[0].Rows[0]["NewsYear"].ToString() + "年");
        //                siteMapNodeByDate.SetAttribute("description", "date");
        //                XmlElement siteMapNodeByDateChild = docSiteMap.CreateElement("siteMapNode");
        //                siteMapNodeByDateChild.SetAttribute("url", "~/NewsList.aspx?NewsYear=" + dsDate.Tables[0].Rows[0]["NewsYear"].ToString() + "&month=" + dsDate.Tables[0].Rows[0]["NewsMonth"].ToString());
        //                siteMapNodeByDateChild.SetAttribute("title", dsDate.Tables[0].Rows[0]["NewsMonth"].ToString() + "月");
        //                siteMapNodeByDateChild.SetAttribute("description", "date");
        //                siteMapNodeByDate.AppendChild(siteMapNodeByDateChild);
        //                newsCenter.AppendChild(siteMapNodeByDate);

        for (int i = 1; i < dsDate.Tables[0].Rows.Count; i++)
        {
            if (dsDate.Tables[0].Rows[i]["NewsYear"].ToString() == dsDate.Tables[0].Rows[i - 1]["NewsYear"].ToString())
            {
                if (dsDate.Tables[0].Rows[i]["NewsMonth"].ToString() != dsDate.Tables[0].Rows[i - 1]["NewsMonth"].ToString())
                {
                    // for the Date.xml
                    month = document.CreateElement("month");
                    monthValue = document.CreateTextNode(dsDate.Tables[0].Rows[i]["NewsMonth"].ToString());
                    month.AppendChild(monthValue);
                    year.AppendChild(month);

                }

            }
            else
            {
                // for the Date.xml
                year = document.CreateElement("year");
                year.SetAttribute("value", dsDate.Tables[0].Rows[i]["NewsYear"].ToString());
                root.AppendChild(year);
                month = document.CreateElement("month");
                monthValue = document.CreateTextNode(dsDate.Tables[0].Rows[i]["NewsMonth"].ToString());
                month.AppendChild(monthValue);
                year.AppendChild(month);

            }
        }

        XmlDocument docSiteMap = new XmlDocument();
        docSiteMap.Load(Server.MapPath("~/Web.sitemap"));
        XmlElement rootSiteMap = docSiteMap.DocumentElement;
        //// 删除原本的与分类导航有关的siteMapNode节点
        XmlNodeList dateNodeList = rootSiteMap.SelectNodes("//siteMapNode[@description='date']");
        for (int j = 0; j < dateNodeList.Count; j++)
        {
            dateNodeList.Item(j).ParentNode.RemoveChild(dateNodeList.Item(j));
        }
        for (int i = 0; i < dsDate.Tables[0].Rows.Count; i++)
        {
            if (i == 0)
            {
                //增加第一条数据
                XmlNode newsCenter = rootSiteMap.SelectSingleNode("//siteMapNode[@description='newsCenter']");
                XmlElement siteMapNodeByCategory = docSiteMap.CreateElement("siteMapNode");
                siteMapNodeByCategory.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=");
                siteMapNodeByCategory.SetAttribute("title", dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年");
                siteMapNodeByCategory.SetAttribute("description", "date");
                newsCenter.AppendChild(siteMapNodeByCategory);
                XmlElement siteMapNodeByCategory2 = docSiteMap.CreateElement("siteMapNode");
                siteMapNodeByCategory2.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=" + dsDate.Tables[0].Rows[i]["NewsMonth"].ToString());
                siteMapNodeByCategory2.SetAttribute("title", dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年" + dsDate.Tables[0].Rows[i]["NewsMonth"].ToString() + "月");
                siteMapNodeByCategory2.SetAttribute("description", "date");
                newsCenter.AppendChild(siteMapNodeByCategory2);
            }
            else if (dsDate.Tables[0].Rows[i]["NewsYear"].ToString() == dsDate.Tables[0].Rows[i - 1]["NewsYear"].ToString())
            {
                if (dsDate.Tables[0].Rows[i]["NewsMonth"].ToString() != dsDate.Tables[0].Rows[i - 1]["NewsMonth"].ToString())
                {
                    //// 写入与分类导航有关的siteMapNode节点
                    XmlNode newsCenter = rootSiteMap.SelectSingleNode("//siteMapNode[@description='newsCenter']");
                    XmlElement siteMapNodeByCategory = docSiteMap.CreateElement("siteMapNode");
                    siteMapNodeByCategory.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=" + dsDate.Tables[0].Rows[i]["NewsMonth"].ToString());
                    siteMapNodeByCategory.SetAttribute("title", dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年" + dsDate.Tables[0].Rows[i]["NewsMonth"].ToString() + "月");
                    siteMapNodeByCategory.SetAttribute("description", "date");
                    newsCenter.AppendChild(siteMapNodeByCategory);
                }
            }
            else
            {
                //// 写入与分类导航有关的siteMapNode节点
                XmlNode newsCenter = rootSiteMap.SelectSingleNode("//siteMapNode[@description='newsCenter']");
                XmlElement siteMapNodeByCategory = docSiteMap.CreateElement("siteMapNode");
                siteMapNodeByCategory.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=");
                siteMapNodeByCategory.SetAttribute("title", dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年");
                siteMapNodeByCategory.SetAttribute("description", "date");
                newsCenter.AppendChild(siteMapNodeByCategory);
                XmlElement siteMapNodeByCategory2 = docSiteMap.CreateElement("siteMapNode");
                siteMapNodeByCategory2.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=" + dsDate.Tables[0].Rows[i]["NewsMonth"].ToString());
                siteMapNodeByCategory2.SetAttribute("title", dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年" + dsDate.Tables[0].Rows[i]["NewsMonth"].ToString() + "月");
                siteMapNodeByCategory2.SetAttribute("description", "date");
                newsCenter.AppendChild(siteMapNodeByCategory2);
            }
        }
        docSiteMap.Save(Server.MapPath("~/Web.sitemap"));
        document.Save(Server.MapPath("~/Date.xml"));
        //                docSiteMap.Save(Server.MapPath("~/Web.sitemap"));

        //操作Footer.xml
        XmlDocument documentfoot = new XmlDocument();
        documentfoot.Load(Server.MapPath("~/Footer.xml"));
        XmlNode mNode = documentfoot.DocumentElement.FirstChild;
        XmlNode mCurrentNode = mNode.SelectSingleNode("catalogue[@id='2']");
        mCurrentNode.RemoveAll();


        XmlAttribute attr1 = null, attr2 = null, attr3 = null;
        attr1 = documentfoot.CreateAttribute("value");
        attr2 = documentfoot.CreateAttribute("id");

        attr1.Value = "油站新闻";
        attr2.Value = "2";
        mCurrentNode.Attributes.Append(attr1);
        mCurrentNode.Attributes.Append(attr2);
        if (dsDate.Tables[0].Rows.Count > 0)
        {
            attr3 = documentfoot.CreateAttribute("url");
            attr3.Value = "~/news/NewsTempate.aspx";
            mCurrentNode.Attributes.Append(attr3);
        }
        XmlElement item = documentfoot.CreateElement("item");
        int num = 0;
        for (int i = 0; i < dsDate.Tables[0].Rows.Count && num < 5; i++)
        {
            if (i == 0)
            {
                //增加第一条数据
                item = documentfoot.CreateElement("item");
                item.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=");

                XmlText value = documentfoot.CreateTextNode(dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年");
                item.AppendChild(value);
                mCurrentNode.AppendChild(item);
                num++;
            }
            else if (dsDate.Tables[0].Rows[i]["NewsYear"].ToString() != dsDate.Tables[0].Rows[i - 1]["NewsYear"].ToString())
            {

                item = documentfoot.CreateElement("item");
                item.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=");

                XmlText value = documentfoot.CreateTextNode(dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年");
                item.AppendChild(value);
                mCurrentNode.AppendChild(item);
                num++;


            }
        }
        documentfoot.Save(Server.MapPath("~/Footer.xml"));
        dsDate.Dispose();
    }
}




