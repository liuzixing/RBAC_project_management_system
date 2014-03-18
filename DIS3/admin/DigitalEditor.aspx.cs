using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Xml;

public partial class admin_DigitalEditor : System.Web.UI.Page
{
    protected string editorContent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["DigitalID"] != null)  // 修改
            {
                Digital n = new Digital();
                n.DigitalID = Convert.ToInt32(Request["DigitalID"].Trim());
                

              //  int DigitalID = Convert.ToInt32(Request["DigitalID"].Trim());
                string sql = "select * from Digital where DigitalID=" + n.DigitalID;// +" and NewsState<>'0'";
                var r = DBHelper.INST.ExecuteSqlDR(sql);
                if (!r.Read())
                {
                    Response.Write("内容未通过审核");
                    Response.End();
                }

                n.DigitalTitle = r["DigitalTitle"].ToString();
                n.DigitalContent = r["DigitalContent"].ToString();

                txtTitle.Text = n.DigitalTitle;
                editorContent = n.DigitalContent;
                r.Close();
            }
            else  // 添加
            {
                editorContent = "&lt;p&gt;This is some &lt;strong&gt;sample text&lt;/strong&gt;. ";
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Request["DigitalID"] == null) // 添加无象数码的内容;
        {
            if (txtTitle.Text == string.Empty)
                Response.Write("<script language= javascript> alert('标题不能为空')</script>");
            else
            {
                if (txtTitle.Text.Length > 100) // 标题长度超过100字符;
                    Response.Write("<script language= javascript> alert('关于油站名称长度超过上限')</script>");
                else
                {
                  //  DataSet ds = DBHelper.INST.ExecuteSqlDS(s);
                   // int id = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    // 插入数据;
                   
                    String sql = "insert into  Digital  ( DigitalTitle, DigitalContent, IsDelete) values('"
                    + txtTitle.Text.ToString() + "', '" + Request["editor1"] + "'," + 0 + ")";//尚需修改;
                    
                    
                    //+ txtTitle.Text.ToString() + "', '" + Request["editor1"] + "'," + 0 + ")";
                    DBHelper.INST.ExecuteSql(sql);
                    refleshDoc();
                    Response.Write("<script type='text/javascript'>		alert('发布成功');window.location = 'DigitalEditor.aspx';</script>");
                    Response.Redirect("DigitalMage.aspx");
                }
            }
        }
        else // 编辑无象数码的内容
        {
            int DigitalID = Convert.ToInt32(Request["DigitalID"].Trim());
            if (txtTitle.Text == string.Empty)
                Response.Write("<script language= javascript> alert('标题不能为空')</script>");
            else
            {
                if (txtTitle.Text.Length > 100) // 标题长度超过100字符
                    Response.Write("<script language= javascript> alert('关于油站名称长度超过上限')</script>");
                else
                {
                  //  DataSet ds = DBHelper.INST.ExecuteSqlDS(s);
                   // int id = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    
                    // 修改数据
                   // String sql = "update  Digital set  DigitalTitle ='" + txtTitle.Text.ToString() + "',DigitalContent= '" + Request["editor1"] + "',IsDelete=" + 0 + " where DigitalID =" + DigitalID;
                    String sql = "update  Digital set  DigitalTitle ='" + txtTitle.Text.ToString() + "',DigitalContent= '" + Request["editor1"] + "',IsDelete=" + 0 + " where DigitalID =" + DigitalID;  
                    DBHelper.INST.ExecuteSql(sql);
                    refleshDoc();
                    Response.Write("<script type='text/javascript'>		alert('修改成功');window.location = 'DigitalMage.aspx';</script>");
                    Response.Redirect("DigitalMage.aspx");
                }
            }

        }
    }
    protected void refleshDoc()
    {
        /*
 * 刷新Footer.xml
 */


        String sd = " select DigitalID, DigitalTitle from Digital where IsDelete=0 order by DigitalID desc, DigitalTitle desc";
        DataSet ds = DBHelper.INST.ExecuteSqlDS(sd);

        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Footer.xml"));
        XmlNode mNode = document.DocumentElement.FirstChild;
        XmlNode mCurrentNode = mNode.ParentNode.SelectSingleNode("Company[@value='无象数码']");
        mCurrentNode.RemoveAll();


        XmlAttribute attr1 = null, attr2 = null, attr3 = null;
        attr1 = document.CreateAttribute("value");
        attr3 = document.CreateAttribute("url");
        attr1.Value = "无象数码";
        attr3.Value = "~/oilStation/NoLikeDigital.aspx?DigitalID=" + ds.Tables[0].Rows[0]["DigitalID"].ToString();
        mCurrentNode.Attributes.Append(attr1);
        mCurrentNode.Attributes.Append(attr3);



        int num = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (num < 5)
            {
                XmlElement item = document.CreateElement("item");
                item.SetAttribute("url", "~/oilStation/NoLikeDigital.aspx?DigitalID=" + ds.Tables[0].Rows[i]["DigitalID"].ToString());
                XmlText value = document.CreateTextNode(ds.Tables[0].Rows[i]["DigitalTitle"].ToString());
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

        String sdd = " select DigitalID, DigitalTitle from Digital where IsDelete=0 order by DigitalID desc, DigitalTitle desc";
        DataSet dss = DBHelper.INST.ExecuteSqlDS(sdd);

        XmlDocument document2 = new XmlDocument();
        document2.Load(Server.MapPath("~/Web.sitemap"));
        XmlNode mNode2 = document2.DocumentElement.FirstChild;
        XmlNode mCurrentNode2 = mNode2.SelectSingleNode("siteMapNode[@title='无象数码']");
        mCurrentNode2.RemoveAll();

        XmlAttribute attr11 = null, attr12 = null, attr13 = null;
        attr11 = document2.CreateAttribute("url");
        attr12 = document2.CreateAttribute("title");
        attr13 = document2.CreateAttribute("description");
        attr11.Value = "~/oilStation/NoLikeDigital.aspx";
        attr12.Value = "无象数码";
        attr13.Value = "";
        mCurrentNode2.Attributes.Append(attr11);
        mCurrentNode2.Attributes.Append(attr12);
        mCurrentNode2.Attributes.Append(attr13);





        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            XmlElement siteMapNode = document2.CreateElement("siteMapNode");
            siteMapNode.SetAttribute("url", "~/oilStation/NoLikeDigital.aspx?DigitalID=" + dss.Tables[0].Rows[i]["DigitalID"].ToString());
            XmlAttribute attr111 = null, attr112 = null;
            attr111 = document2.CreateAttribute("title");
            attr112 = document2.CreateAttribute("description");
            attr111.Value = dss.Tables[0].Rows[i]["DigitalTitle"].ToString();
            attr112.Value = "";
            siteMapNode.Attributes.Append(attr111);
            siteMapNode.Attributes.Append(attr112);

            mCurrentNode2.AppendChild(siteMapNode);

        }
        document2.Save(Server.MapPath("~/Web.sitemap"));
        dss.Dispose();
    }
}