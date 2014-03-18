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

public partial class admin_ProjectNameEditor : System.Web.UI.Page
{
    protected string editorContent;
    private int OilProjectDirectionID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["OilProjectDirectionID"] != null)
        {
            OilProjectDirectionID = Convert.ToInt32(Request["OilProjectDirectionID"].Trim());
        }
        else
        {
            OilProjectDirectionID = -1;
        }

        if (!IsPostBack)
        {
            //             DataSet ds = DBHelper.INST.ExecuteSqlDS("select NewsCategoryName from NewsCategories ");
            //             // 为控件绑定数据源，必须是表 
            //             DropDownList1.DataSource = ds.Tables[0];
            //             // 选择为下拉列表提供数据的字段
            //             DropDownList1.DataValueField = "NewsCategoryName";
            //             DropDownList1.DataTextField = "NewsCategoryName";
            //             DropDownList1.DataBind();

            if (Request["OilProjectDirectionID"] != null)
            {
                
                string sql = "select * from OilProjectDirection where OilProjectDirectionID=" + OilProjectDirectionID;// +" and NewsState<>'0'";
                var r = DBHelper.INST.ExecuteSqlDR(sql);
                r.Read();
                //                 int newsCategoryID = Convert.ToInt32(r["NewsCategoryID"].ToString());
                //                 String s = " select NewsCategoryName from NewsCategories where NewsCategoryID=" + newsCategoryID;
                //                 DataSet dsNews = DBHelper.INST.ExecuteSqlDS(s);
                //                 string newsCategoryName = dsNews.Tables[0].Rows[0][0].ToString();
                //                 DropDownList1.SelectedValue = newsCategoryName;
                txtTitle.Text = r["OilProjectDirectionTitle"].ToString();
                editorContent = r["OilProjectDirectionContent"].ToString();
                r.Close();
            }
            else
            {
                editorContent = "&lt;p&gt;This is some &lt;strong&gt;sample text&lt;/strong&gt;. ";
            }
        }
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtTitle.Text == string.Empty)
        {
            Response.Write("<script language= javascript> alert('项目不能为空')</script>");
            Response.Write("<script language=javascript type ='text/javascript'>window.location = 'ProjectNameEditor.aspx'; </script>");
        }
        else
        {
            if (txtTitle.Text.Length > 20)
            {
                Response.Write("<script  language= javascript> alert('项目长度超过上限') </script>");
                Response.Write("<script language=javascript type ='text/javascript'>window.location = 'ProjectNameEditor.aspx'; </script>");
            }
            else
            {
                String sqlstr;
                sqlstr = "insert into  OilProjectDirection (OilProjectDirectionTitle,OilProjectDirectionContent,IsDelete) values('"
                + txtTitle.Text + "','" + Request["editor1"] + "'," + 0 + ")";//尚需修改;
                DBHelper.INST.ExecuteSql(sqlstr);
                txtTitle.Text = "";
                refleshDoc();

            }
        }
    }
    private void refleshDoc()
    {
        /* 刷新OilProjectDirection.xml*/
        String sd = "select * from OilProject where IsDelete=" + 0 + " order by OilProjectDirectionID asc";
        DataSet ds1 = DBHelper.INST.ExecuteSqlDS(sd);
        sd = "select * from OilProjectDirection where IsDelete=" + 0;
        DataSet ds2 = DBHelper.INST.ExecuteSqlDS(sd);
        XmlDocument document = new XmlDocument();           // 操作OilProjectDirection.xml
        document.Load(Server.MapPath("~/OilProjectDirection.xml"));
        XmlElement root = document.DocumentElement;
        root.RemoveAll();
        XmlElement OilProjectDirection = document.CreateElement("OilProjectDirection");

        int j = 0;
        XmlElement ProjectName;
        XmlText ProjectNameValue;
        if (ds1.Tables[0].Rows.Count != 0)
        {
            int f = j;
            for (; f < ds2.Tables[0].Rows.Count; f++)
            {

                OilProjectDirection.SetAttribute("value", ds2.Tables[0].Rows[0]["OilProjectDirectionTitle"].ToString());
                if (ds2.Tables[0].Rows[f]["OilProjectDirectionID"].ToString() == ds1.Tables[0].Rows[0]["OilProjectDirectionID"].ToString())
                {
                    OilProjectDirection = document.CreateElement("OilProjectDirection");
                    OilProjectDirection.SetAttribute("value", ds2.Tables[0].Rows[j]["OilProjectDirectionTitle"].ToString());
                    root.AppendChild(OilProjectDirection);

                    ProjectName = document.CreateElement("ProjectName");
                    ProjectName.SetAttribute("id", ds1.Tables[0].Rows[0]["OilProjectID"].ToString());
                    ProjectNameValue = document.CreateTextNode(ds1.Tables[0].Rows[0]["OilProjectTitle"].ToString());
                    ProjectName.AppendChild(ProjectNameValue);
                    OilProjectDirection.AppendChild(ProjectName);
                    j = f;
                    j++;
                    break;
                }
                else
                {
                    OilProjectDirection = document.CreateElement("OilProjectDirection");
                    OilProjectDirection.SetAttribute("value", ds2.Tables[0].Rows[f]["OilProjectDirectionTitle"].ToString());
                    root.AppendChild(OilProjectDirection);
                    j++;
                }


            }
        }

        for (int i = 1; i < ds1.Tables[0].Rows.Count; i++)
        {

            if (ds1.Tables[0].Rows[i]["OilProjectID"].ToString() != ds1.Tables[0].Rows[i - 1]["OilProjectID"].ToString() && (ds1.Tables[0].Rows[i]["OilProjectDirectionID"].ToString() == ds1.Tables[0].Rows[i - 1]["OilProjectDirectionID"].ToString()))
            {
                ProjectName = document.CreateElement("ProjectName");

                ProjectName.SetAttribute("id", ds1.Tables[0].Rows[i]["OilProjectID"].ToString());
                ProjectNameValue = document.CreateTextNode(ds1.Tables[0].Rows[i]["OilProjectTitle"].ToString());
                ProjectName.AppendChild(ProjectNameValue);
                OilProjectDirection.AppendChild(ProjectName);

            }

            if (ds1.Tables[0].Rows[i]["OilProjectDirectionID"].ToString() != ds1.Tables[0].Rows[i - 1]["OilProjectDirectionID"].ToString())
            {
                int f = j;
                for (; f < ds2.Tables[0].Rows.Count; f++)
                {

                    if (ds2.Tables[0].Rows[f]["OilProjectDirectionID"].ToString() == ds1.Tables[0].Rows[i]["OilProjectDirectionID"].ToString())
                    {
                        OilProjectDirection = document.CreateElement("OilProjectDirection");
                        OilProjectDirection.SetAttribute("value", ds2.Tables[0].Rows[j]["OilProjectDirectionTitle"].ToString());
                        root.AppendChild(OilProjectDirection);

                        ProjectName = document.CreateElement("ProjectName");
                        ProjectName.SetAttribute("id", ds1.Tables[0].Rows[i]["OilProjectID"].ToString());

                        ProjectNameValue = document.CreateTextNode(ds1.Tables[0].Rows[i]["OilProjectTitle"].ToString());
                        ProjectName.AppendChild(ProjectNameValue);
                        OilProjectDirection.AppendChild(ProjectName);
                        j = f;
                        j++;
                        break;
                    }
                    else
                    {
                        OilProjectDirection = document.CreateElement("OilProjectDirection");
                        OilProjectDirection.SetAttribute("value", ds2.Tables[0].Rows[f]["OilProjectDirectionTitle"].ToString());
                        root.AppendChild(OilProjectDirection);
                        j++;
                    }
                }

            }

        }
        for (; j < ds2.Tables[0].Rows.Count; j++)
        {
            OilProjectDirection = document.CreateElement("OilProjectDirection");
            OilProjectDirection.SetAttribute("value", ds2.Tables[0].Rows[j]["OilProjectDirectionTitle"].ToString());
            root.AppendChild(OilProjectDirection);
        }
        Response.Write("<script language=javascript type ='text/javascript'>window.location = 'ProjectNameEditor.aspx'; </script>");
        document.Save(Server.MapPath("~/OilProjectDirection.xml"));
        ds1.Dispose();
        ds2.Dispose();

        /*
         *刷新Footer.xml
         */
        String sdd = " select OilProjectDirectionID, OilProjectDirectionTitle from OilProjectDirection where IsDelete=" + 0 + " order by OilProjectDirectionID desc, OilProjectDirectionTitle desc";
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
        attr3.Value = "~/oilStation/Project.aspx?OilProjectDirectionID=" + dss.Tables[0].Rows[0]["OilProjectDirectionID"].ToString();
        mCurrentNode.Attributes.Append(attr1);
        mCurrentNode.Attributes.Append(attr2);
        mCurrentNode.Attributes.Append(attr3);


        int num = 0;
        for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
        {
            if (num < 5)
            {
                XmlElement item = document1.CreateElement("item");
                item.SetAttribute("url", "~/oilStation/Project.aspx?OilProjectDirectionID=" + dss.Tables[0].Rows[i]["OilProjectDirectionID"].ToString());
                XmlText value = document1.CreateTextNode(dss.Tables[0].Rows[i]["OilProjectDirectionTitle"].ToString());
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

        String sddd = "select OilProjectDirectionID,  OilProjectDirectionTitle from OilProjectDirection where IsDelete=" + 0 + " order by OilProjectDirectionID desc, OilProjectDirectionTitle desc";
        DataSet dsss = DBHelper.INST.ExecuteSqlDS(sddd);

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


        for (int i = 0; i < dsss.Tables[0].Rows.Count; i++)
        {
            XmlElement siteMapNode = document2.CreateElement("siteMapNode");
            siteMapNode.SetAttribute("url", "~/oilStation/Project.aspx?OilProjectDirectionID=" + dsss.Tables[0].Rows[i]["OilProjectDirectionID"].ToString());
            XmlAttribute attr111 = null, attr112 = null;
            attr111 = document2.CreateAttribute("title");
            attr112 = document2.CreateAttribute("description");
            attr111.Value = dss.Tables[0].Rows[i]["OilProjectDirectionTitle"].ToString();
            attr112.Value = "";
            siteMapNode.Attributes.Append(attr111);
            siteMapNode.Attributes.Append(attr112);

            mCurrentNode2.AppendChild(siteMapNode);

        }
        document2.Save(Server.MapPath("~/Web.sitemap"));
        dsss.Dispose();
    }
}