using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;   

public partial class admin_PlanEditor : System.Web.UI.Page
{
    protected string editorContent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 为控件绑定数据源
            DataSet ds = DBHelper.INST.ExecuteSqlDS("select distinct PlanClassify from StationPlan ");
            DropDownList1.DataSource = ds.Tables[0];
            // 选择为下拉列表提供数据的字段
            DropDownList1.DataValueField = "PlanClassify";
            DropDownList1.DataTextField = "PlanClassify";
            DropDownList1.DataBind();
            ds.Dispose();

            if (Request["StationPlanID"] != null)  // 修改计划
            {
                Plans n = new Plans();
                n.StationPlanID = Convert.ToInt32(Request["StationPlanID"].Trim());
                string sql = "select * from StationPlan where StationPlanID=" + n.StationPlanID;// +" and NewsState<>'0'";
                var r = DBHelper.INST.ExecuteSqlDR(sql);
                if (!r.Read())
                {
                    Response.Write("计划未通过审核");
                    Response.End();
                }
                n.PlanName = r["PlanName"].ToString();
                n.Content = r["Content"].ToString();
                n.PlanAuthor = r["PlanAuthor"].ToString();
                n.PlanClassify = r["PlanClassify"].ToString();
                DropDownList1.SelectedValue = n.PlanClassify;
                txtTitle.Text = n.PlanName;
                txtAuthor.Text = n.PlanAuthor;
                editorContent = n.Content;
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
        if (Request["StationPlanID"] == null)  // 添加计划
        {
            if (txtTitle.Text == string.Empty)
                Response.Write("<script language= javascript> alert('标题不能为空')</script>");
            else if (txtAuthor.Text == string.Empty)
                Response.Write("<script language= javascript> alert('作者不能为空')</script>");
            else
            {
                if (txtTitle.Text.Length > 100) // 标题长度超过100字符
                    Response.Write("<script language= javascript> alert('标题栏长度超过上限')</script>");
                else if (txtAuthor.Text.Length > 50) // 作者长度超过50字符
                    Response.Write("<script  language= javascript> alert('作者栏长度超过上限')</script>");
                else
                {
                    String s = " select PlanClassifyID from StationPlan where PlanClassify='" + DropDownList1.SelectedValue + "'";
                    DataSet ds = DBHelper.INST.ExecuteSqlDS(s);
                    int id = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    ds.Dispose();
                    // 插入数据
                    String sql = "insert into  StationPlan  ( PlanName, Content, PlanDate, PlanClassifyID, PlanClassify, PlanAuthor , VisitCount,IsDelete) values('"
                    + txtTitle.Text.ToString() + "', '" + Request["editor1"] + "', #" + DateTime.Now.ToLocalTime().ToString() + "#, " + id + ",  '" + DropDownList1.SelectedValue + "','" + txtAuthor.Text + "'," + 1 + "," +0 + ")";//尚需修改
                    DBHelper.INST.ExecuteSql(sql);
                    Response.Write("<script type='text/javascript'>		alert('发布成功');window.location = 'PlanEditor.aspx';</script>");
          
                    Response.Redirect("PlanMage.aspx");
                }
            }
        }
        else  // 修改计划
        {
            int StationPlanID = Convert.ToInt32(Request["StationPlanID"].Trim());
            if (txtTitle.Text == string.Empty)
                Response.Write("<script language= javascript> alert('标题不能为空')</script>");
            else if (txtAuthor.Text == string.Empty)
                Response.Write("<script language= javascript> alert('作者不能为空')</script>");
            else
            {
                if (txtTitle.Text.Length > 100) // 标题长度超过100字符
                    Response.Write("<script language= javascript> alert('标题栏长度超过上限')</script>");
                else if (txtAuthor.Text.Length > 50) // 作者长度超过50字符
                    Response.Write("<script  language= javascript> alert('作者栏长度超过上限')</script>");
                else
                {
                    String s = " select PlanClassifyID from StationPlan where PlanClassify='" + DropDownList1.SelectedValue + "'";
                    DataSet ds = DBHelper.INST.ExecuteSqlDS(s);
                    int id = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    ds.Dispose();
                    // 修改数据
                    String sql = "update  StationPlan set  PlanName ='" + txtTitle.Text.ToString() + "',Content= '" + Request["editor1"] + "',PlanDate=#" + DateTime.Now.ToLocalTime().ToString() + "#, PlanClassifyID=" + id + ",PlanClassify='" + DropDownList1.SelectedValue + "',PlanAuthor= '" + txtAuthor.Text + "',IsDelete=" + 0 + " where StationPlanID =" + StationPlanID;                   
                    DBHelper.INST.ExecuteSql(sql);
                    Response.Write("<script type='text/javascript'>		alert('修改成功');window.location = 'PlanMage.aspx';</script>");
                    Response.Redirect("PlanMage.aspx"); 
                }
            }
        }


        /*
         * 刷新Footer.xml
         */
        /*
        String sdd = " select PlanClassifyID, PlanClassify from StationPlan order by PlanClassifyID asc ";
        DataSet dss = DBHelper.INST.ExecuteSqlDS(sdd);

        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Footer.xml"));
        XmlNode mNode = document.DocumentElement.FirstChild;
        XmlNode mCurrentNode = mNode.SelectSingleNode("catalogue[@id='4']");
        mCurrentNode.RemoveAll();


        XmlAttribute attr1 = null, attr2 = null, attr3 = null;
        attr1 = document.CreateAttribute("value");
        attr2 = document.CreateAttribute("id");
        attr3 = document.CreateAttribute("url");
        attr1.Value = "油站计划";
        attr2.Value = "4";
        attr3.Value = "~/oilStation/stationPlans.aspx";
        mCurrentNode.Attributes.Append(attr1);
        mCurrentNode.Attributes.Append(attr2);
        mCurrentNode.Attributes.Append(attr3);

        int num = 0;
        for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
        {
            if (num < 5)
            {
                XmlElement item = document.CreateElement("item");
                item.SetAttribute("url", "~/oilStation/stationPlans.aspx?PlanClassifyID=" + dss.Tables[0].Rows[i]["PlanClassifyID"].ToString());
                XmlText value = document.CreateTextNode(dss.Tables[0].Rows[i]["PlanClassify"].ToString());
                item.AppendChild(value);
                mCurrentNode.AppendChild(item);
                num++;
            }
        }
        document.Save(Server.MapPath("~/Footer.xml"));

        */
    }
}