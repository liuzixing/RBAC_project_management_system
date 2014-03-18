using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;   

public partial class admin_AboutOilEditor : System.Web.UI.Page
{
    protected string editorContent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["AboutOilID"] != null)  // 修改计划
            {
                int AboutOilID = Convert.ToInt32(Request["AboutOilID"].Trim());
                string sql = "select * from AboutOil where AboutOilID=" + AboutOilID;// +" and NewsState<>'0'";
                var r = DBHelper.INST.ExecuteSqlDR(sql);
                if (!r.Read())
                {
                    Response.Write("内容未通过审核");
                    Response.End();
                }
                txtTitle.Text = r["OilClassify"].ToString();
                editorContent = r["OilContent"].ToString();
                r.Close();
            }
            else  // 添加计划
            {
                editorContent = "&lt;p&gt;This is some &lt;strong&gt;sample text&lt;/strong&gt;. ";
            }
        }
       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Request["AboutOilID"] == null) // 添加关于油站的内容
        {
            if (txtTitle.Text == string.Empty)
                Response.Write("<script language= javascript> alert('标题不能为空')</script>");
            else
            {
                if (txtTitle.Text.Length > 100) // 标题长度超过100字符
                    Response.Write("<script language= javascript> alert('关于油站名称长度超过上限')</script>");
                else
                {
                    // 插入数据
                    String sql = "insert into  AboutOil ( OilClassify, oilContent,IsDelete) values('"
                    + txtTitle.Text.ToString() + "', '" + Request["editor1"] + "'," + 0 + ")";
                    DBHelper.INST.ExecuteSql(sql);

                    Response.Write("<script type='text/javascript'>		alert('发布成功');window.location = 'AboutOilEditor.aspx';</script>");
                }
            }
        }
        else // 编辑关于油站的内容
        {
            int AboutOilID = Convert.ToInt32(Request["AboutOilID"].Trim());
            if (txtTitle.Text == string.Empty)
                Response.Write("<script language= javascript> alert('标题不能为空')</script>");
            else
            {
                if (txtTitle.Text.Length > 100) // 标题长度超过100字符
                    Response.Write("<script language= javascript> alert('关于油站名称长度超过上限')</script>");
                else
                {
                    // 修改数据
                    String sql = "update  AboutOil set  OilClassify ='" + txtTitle.Text.ToString() + "',oilContent= '" + Request["editor1"] + "' where AboutOilID =" + AboutOilID;
                    DBHelper.INST.ExecuteSql(sql);

                    Response.Write("<script type='text/javascript'>		alert('修改成功');window.location = 'AboutOilMage.aspx';</script>");
                }
            }
          
        }
        refreshDoc();
    }


        private void refreshDoc()
        {
            
        String sd = " select AboutOilID, OilClassify from AboutOil where IsDelete=0 order by AboutOilID desc, OilClassify desc";
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
        XmlText value1 = document.CreateTextNode("FAQ");
        item.AppendChild(value1 );
        mCurrentNode.AppendChild(item);

        /*
        item = document.CreateElement("item");
        item.SetAttribute("url", "~/oilStation/AboutOil.aspx?AboutOilID=" + ds.Tables[0].Rows[0]["AboutOilID"].ToString());
        XmlText value = document.CreateTextNode(ds.Tables[0].Rows[0]["OilClassify"].ToString());
        item.AppendChild(value);
        mCurrentNode.AppendChild(item);
        
        */
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
        attr13=document2.CreateAttribute("description");
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
        attr111.Value = "FAQ";
        attr112.Value = "FAQ";
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
}