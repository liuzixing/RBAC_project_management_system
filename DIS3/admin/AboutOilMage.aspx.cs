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

public partial class admin_AboutOilMage : System.Web.UI.Page
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
            this.AddProject.Visible = false;
            this.Button1.Visible = false;
            this.Button2.Visible = false;
            this.Button3.Visible = false;
            this.Button4.Visible = false;
            return;
        }
        if (Session["UserName"].ToString() != "lin")
        {
            ErrorMessage.Text = "非管理员用户不能浏览当前页面！";
            this.ErrorMessage.Visible = true;
            this.AddProject.Visible = false;
            this.Button1.Visible = false;
            this.Button2.Visible = false;
            this.Button3.Visible = false;
            this.Button4.Visible = false;
            return;
        }
        if (!IsPostBack)
        {
            bindData();
        }
    }

     void bindData()
    {
        string dsStr = "select * from AboutOil where IsDelete= 0";
        DataSet ds = DBHelper.INST.ExecuteSqlDS(dsStr);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        ds.Dispose();
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



     protected void Button2_Click(object sender, EventArgs e)
     {
         String sqlstr1 = "delete from AboutOil where IsDelete=" + "1";
         DBHelper.INST.ExecuteSql(sqlstr1);
         for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
         {
             CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
             if (cbox.Checked == true)
             {
                 String sqlstr = "update AboutOil set IsDelete=" + 1 + " where AboutOilID=" + Convert.ToInt32(GridView1.DataKeys[i].Value);
                 DBHelper.INST.ExecuteSql(sqlstr);
             }
         }

         bindData();
         refleshDoc();
         Response.Redirect("AboutOilMage.aspx");

         

     }

     protected void DeleteBtn_Click(object sender, EventArgs e)
     {
         LinkButton btn = sender as LinkButton;
         GridViewRow row = btn.Parent.Parent as GridViewRow;
         CheckBox cbox = (CheckBox)row.FindControl("CheckBox1");
         cbox.Checked = true;
         Button2_Click(sender, e);
     }

     protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
         GridView1.PageIndex = e.NewPageIndex;
         bindData();
     }
     protected void Button5_Click(object sender, EventArgs e)
     {
         String sqlstr = "update AboutOil set IsDelete=" + 0 + " where IsDelete=" + 1;
         DBHelper.INST.ExecuteSql(sqlstr);
         bindData();
         refleshDoc();
         Response.Redirect("AboutOilMage.aspx");
     }
     private void refleshDoc()
     {
         /*
           * 刷新Footer.xml
           */

         String sd = " select AboutOilID, OilClassify from AboutOil where IsDelete =0 order by AboutOilID desc, OilClassify desc";
         DataSet ds = DBHelper.INST.ExecuteSqlDS(sd);

         XmlDocument document = new XmlDocument();
         document.Load(Server.MapPath("~/Footer.xml"));
         XmlNode mNode = document.DocumentElement.FirstChild;
         XmlNode mCurrentNode = mNode.SelectSingleNode("catalogue[@id='1']");
         mCurrentNode.RemoveAll();

         XmlAttribute attr1 = null, attr2 = null, attr3 = null;
         attr1 = document.CreateAttribute("value");
         attr2 = document.CreateAttribute("id");

         attr1.Value = "关于油站";
         attr2.Value = "1";
         mCurrentNode.Attributes.Append(attr1);
         mCurrentNode.Attributes.Append(attr2);
         if (ds.Tables[0].Rows.Count > 0)
         {
             attr3 = document.CreateAttribute("url");
             attr3.Value = "~/oilStation/AboutOil.aspx?AboutOilID=" + ds.Tables[0].Rows[0]["AboutOilID"].ToString();
             mCurrentNode.Attributes.Append(attr3);
         }


         XmlElement item = document.CreateElement("item");
         item.SetAttribute("url", "~/oilStation/AboutOil.aspx?AboutOilID=0");
         XmlText value1 = document.CreateTextNode("常见问题");
         item.AppendChild(value1);
         mCurrentNode.AppendChild(item);


         int num = 1;
         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
         {
             if (num < 5)
             {
                 item = document.CreateElement("item");
                 item.SetAttribute("url", "~/oilStation/AboutOil.aspx?AboutOilID=" + ds.Tables[0].Rows[i]["AboutOilID"].ToString());
                 XmlText value = document.CreateTextNode(ds.Tables[0].Rows[i]["OilClassify"].ToString());
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

         String sdd = " select AboutOilID, OilClassify from AboutOil where IsDelete=0 order by AboutOilID desc, OilClassify desc";
         DataSet dss = DBHelper.INST.ExecuteSqlDS(sdd);

         XmlDocument document2 = new XmlDocument();
         document2.Load(Server.MapPath("~/Web.sitemap"));
         XmlNode mNode2 = document2.DocumentElement.FirstChild;
         XmlNode mCurrentNode2 = mNode2.SelectSingleNode("siteMapNode[@title='关于油站']");
         mCurrentNode2.RemoveAll();

         XmlAttribute attr11 = null, attr12 = null, attr13 = null;
         attr11 = document2.CreateAttribute("url");
         attr12 = document2.CreateAttribute("title");
         attr13 = document2.CreateAttribute("description");
         attr11.Value = "~/oilStation/AboutOil.aspx";
         attr12.Value = "关于油站";
         attr13.Value = "aboutOil";
         mCurrentNode2.Attributes.Append(attr11);
         mCurrentNode2.Attributes.Append(attr12);
         mCurrentNode2.Attributes.Append(attr13);


         XmlElement siteMapNode = document2.CreateElement("siteMapNode");
         siteMapNode.SetAttribute("url", "~/oilStation/AboutOil.aspx?AboutOilID=0");
         XmlAttribute attr111 = null, attr112 = null;
         attr111 = document2.CreateAttribute("title");
         attr112 = document2.CreateAttribute("description");
         attr111.Value = "常见问题";
         attr112.Value = "常见问题";
         siteMapNode.Attributes.Append(attr111);
         siteMapNode.Attributes.Append(attr112);
         mCurrentNode2.AppendChild(siteMapNode);


         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
         {

             siteMapNode = document2.CreateElement("siteMapNode");
             siteMapNode.SetAttribute("url", "~/oilStation/AboutOil.aspx?AboutOilID=" + dss.Tables[0].Rows[i]["AboutOilID"].ToString());
             attr111 = document2.CreateAttribute("title");
             attr112 = document2.CreateAttribute("description");
             attr111.Value = dss.Tables[0].Rows[i]["OilClassify"].ToString();
             attr112.Value = "";
             siteMapNode.Attributes.Append(attr111);
             siteMapNode.Attributes.Append(attr112);

             mCurrentNode2.AppendChild(siteMapNode);

         }
         document2.Save(Server.MapPath("~/Web.sitemap"));
         dss.Dispose();
     }

     protected void AddAboutOil_Click(object sender, EventArgs e)
     {
         Response.Redirect("AboutOilEditor.aspx");
     }
     protected void EditFAQ_Click(object sender, EventArgs e)
     {
         Response.Redirect("FAQMage.aspx");
     }
}
