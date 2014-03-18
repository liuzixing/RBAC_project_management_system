using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;   

public partial class admin_EnterprisesEditor : System.Web.UI.Page
{
    protected string editorContent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["EnterprisesID"] != null)  // 修改计划
            {
                int EnterprisesID = Convert.ToInt32(Request["EnterprisesID"].Trim());
                string sql = "select * from CooperationEnterprises where EnterprisesID=" + EnterprisesID;// +" and NewsState<>'0'";
                var r = DBHelper.INST.ExecuteSqlDR(sql);
                if (!r.Read())
                {
                    Response.Write("内容未通过审核");
                    Response.End();
                }
                txtTitle.Text = r["EnterprisesName"].ToString();
                editorContent = r["Content"].ToString();
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
        if (Request["EnterprisesID"] == null)  // 添加合作企业
        {
            if (txtTitle.Text == string.Empty)
                Response.Write("<script language= javascript> alert('标题不能为空')</script>");
            else
            {
                if (txtTitle.Text.Length > 100) // 标题长度超过100字符
                    Response.Write("<script language= javascript> alert('企业名称长度超过上限')</script>");
                else
                {
                    // 插入数据
                    String sql = "insert into  CooperationEnterprises  ( EnterprisesName, Content,IsDelete) values('"
                    + txtTitle.Text.ToString() + "', '" + Request["editor1"] + "'," + 0 + ")";
                    DBHelper.INST.ExecuteSql(sql);
                    refleshDoc();
                    Response.Write("<script type='text/javascript'>		alert('发布成功');window.location = 'EnterprisesEditor.aspx';</script>");
                    Response.Redirect("EnterprisesMage.aspx");
            
                }
            }
        }
        else  // 编辑合作企业
        {
            int EnterprisesID = Convert.ToInt32(Request["EnterprisesID"].Trim());
            if (txtTitle.Text == string.Empty)
                Response.Write("<script language= javascript> alert('标题不能为空')</script>");
            else
            {
                if (txtTitle.Text.Length > 100) // 标题长度超过100字符
                    Response.Write("<script language= javascript> alert('企业名称长度超过上限')</script>");
                else
                {
                    // 修改数据
                    String sql = "update  CooperationEnterprises set  EnterprisesName ='" + txtTitle.Text.ToString() + "',Content= '" + Request["editor1"] + "',IsDelete=" + 0 + " where EnterprisesID =" + EnterprisesID;    
                    DBHelper.INST.ExecuteSql(sql);
                    refleshDoc();
                    Response.Write("<script type='text/javascript'>		alert('修改成功');window.location = 'EnterprisesMage.aspx';</script>");
                    Response.Redirect("EnterprisesMage.aspx");
                }
            }
        }

  

   
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