using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.Xml;


public partial class admin_ProjectEditor : System.Web.UI.Page
{
    protected string editorContent;
    private int OilProjectID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != "lin")
        {
            ErrorMessage.Text = "非管理员无权进行修改！";
            this.ErrorMessage.Visible = true;
            this.Button2.Enabled = false;
        }

        if (!IsPostBack)
        {
            DataSet ds1 = DBHelper.INST.ExecuteSqlDS("select * from OilProjectDirection where IsDelete=" + 0);
            // 为控件绑定数据源，必须是表 
            DropDownList1.DataSource = ds1.Tables[0];
            // 选择为下拉列表提供数据的字段
            DropDownList1.DataValueField = "OilProjectDirectionID";
            DropDownList1.DataTextField = "OilProjectDirectionTitle";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "-请选择方向-");

            if (Request["OilProjectID"] != null)  // 修改计划
            {
                OilProject n = new OilProject();
                n.OilProjectID = Convert.ToInt32(Request["OilProjectID"].Trim());
                string sql = "select * from OilProject where OilProjectID=" + n.OilProjectID;// +" and NewsState<>'0'";
                var r = DBHelper.INST.ExecuteSqlDR(sql);
                if (!r.Read())
                {
                    Response.Write("项目介绍未通过审核");
                    Response.End();
                }
                n.OilProjectTitle = r["OilProjectTitle"].ToString();
                n.OilProjectContent = r["OilProjectContent"].ToString();
               // n.PlanAuthor = r["PlanAuthor"].ToString();
               // n.PlanClassify = r["PlanClassify"].ToString();
               // DropDownList1.SelectedValue = n.OilProjectDirection;
               // txtTitle.Text = n.PlanName;
               // txtAuthor.Text = n.PlanAuthor;
                editorContent = n.OilProjectContent;
                BoxProjectName.Text = n.OilProjectTitle;
                r.Close();
            }
            else  // 添加计划
            {
                editorContent = "&lt;p&gt;This is some &lt;strong&gt;sample text&lt;/strong&gt;. ";
            }


        }
            if (Request["OilProjectID"] != null)
            {
                OilProjectID = Convert.ToInt32(Request["OilProjectID"].Trim());
                string sql = "select * from OilProjectDirection where OilProjectDirectionID = (select OilProjectDirectionID from OilProject where OilProjectID=" + Request["OilProjectID"] + ")";
                var r = DBHelper.INST.ExecuteSqlDR(sql);
                string sql1 = "select * from OilProject where OilProjectID=" + Request["OilProjectID"];
                var r1 = DBHelper.INST.ExecuteSqlDR(sql1);
                r.Read();
                r1.Read();
                DropDownList1.SelectedValue = r["OilProjectDirectionID"].ToString();
                BoxProjectName.Text = r1["OilProjectTitle"].ToString();
                editorContent = r1["OilProjectContent"].ToString();
                r.Close();
                r1.Close();
            }
            else OilProjectID = -1;
        
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (OilProjectID == -1)   //  添加新闻
        {
            if (BoxProjectName.Text == string.Empty)
                Response.Write("<script language= javascript> alert('项目名称不能为空')</script>");
            else if (BoxProjectName.Text.Length > 100) // 标题长度超过100字符
                Response.Write("<script language= javascript> alert('项目名称长度超过上限')</script>");
            else if(DropDownList1.SelectedValue=="-请选择方向-")
                Response.Write("<script language= javascript> alert('请选择项目方向')</script>");
            else
            {
                String sql = "insert into OilProject (OilProjectTitle, OilProjectContent,OilProjectDate, OilProjectDirectionID, IsDelete,Confirm) values('" + BoxProjectName.Text.ToString() + "','" + Request["editor1"].ToString() + "',#" + DateTime.Now.ToLocalTime().ToString() + "#," + DropDownList1.SelectedValue + "," + 0 + "," + this.cbConfirm.Checked +")";
                DBHelper.INST.ExecuteSql(sql);
                refleshDoc();
                Response.Write("<script type='text/javascript'>		alert('发布成功');</script>");
                Response.Redirect("ProjectManage.aspx");
            }
        }
        else
        {
            if (BoxProjectName.Text == string.Empty)
                Response.Write("<script language= javascript> alert('项目名称不能为空')</script>");
            else if (BoxProjectName.Text.Length > 100) // 标题长度超过100字符
                Response.Write("<script language= javascript> alert('项目名称超过上限')</script>");
            else if (DropDownList1.SelectedValue == "-请选择方向-")
                Response.Write("<script language= javascript> alert('请选择项目方向')</script>");
            else
            {
                String t = BoxProjectName.Text.ToString();
                String sql = "update OilProject set OilProjectTitle='" + t + "',OilProjectContent='"
                    + Request["editor1"].ToString() + "',oilProjectDate=#" + DateTime.Now.ToLocalTime().ToString() + "#,OilProjectDirectionID=" + DropDownList1.SelectedValue + ",IsDelete=" + 0 + ",Confirm = " + cbConfirm.Checked + " where OilProjectID=" + OilProjectID;
                DBHelper.INST.ExecuteSql(sql);
                refleshDoc();
                Response.Write("<script type='text/javascript'>	    alert('修改成功');window.location = 'ProjectManage.aspx';</script>");
            //    Response.Redirect("ProjectManage.aspx");
            }
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
        //Response.Write("<script language=javascript type ='text/javascript'>window.location = 'ProjectNameEditor.aspx'; </script>");
    }
}