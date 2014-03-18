using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;

public partial class admin_OilProject : System.Web.UI.Page
{
    protected string editorContent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["OilProjectID"] == null)
            {
                DataSet ds1 = DBHelper.INST.ExecuteSqlDS("select * from OilProject");
                // 为控件绑定数据源，必须是表 
                DropDownList1.DataSource = ds1.Tables[0];
                // 选择为下拉列表提供数据的字段
                DropDownList1.DataValueField = "OilProjectID";
                DropDownList1.DataTextField = "OilProjectTitle";
                DropDownList1.DataBind();
                if (ds1.Tables[0].Rows.Count != 0)
                editorContent = ds1.Tables[0].Rows[0]["OilProjectContent"].ToString();
                ds1.Dispose();
            }
            else
            {
                DataSet ds1 = DBHelper.INST.ExecuteSqlDS("select * from OilProject where OilProjectID=" + Request["OilProjectID"]);
                // 为控件绑定数据源，必须是表 
                DropDownList1.DataSource = ds1.Tables[0];
                // 选择为下拉列表提供数据的字段
                DropDownList1.DataValueField = "OilProjectID";
                DropDownList1.DataTextField = "OilProjectTitle";
                DropDownList1.DataBind();
                if (ds1.Tables[0].Rows.Count != 0)
                editorContent = ds1.Tables[0].Rows[0]["OilProjectContent"].ToString();
                ds1.Dispose();
            }
        }
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {

        DataSet ds1 = DBHelper.INST.ExecuteSqlDS("select OilProjectContent from OilProject where OilProjectID =" + DropDownList1.SelectedValue);
        editorContent = ds1.Tables[0].Rows[0]["OilProjectContent"].ToString();
        ds1.Dispose();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
            // 修改数据
        String sql = "update  OilProject set  OilProjectTitle ='" + DropDownList1.SelectedItem + "',OilProjectContent ='" + Request["editor1"] + "',NewRecruitsDate= '" + DateTime.Now.ToLocalTime().ToString() + "' Confirm = " + this.cbConfirm.Checked + " where OilProjectID =" + DropDownList1.SelectedValue;
                    DBHelper.INST.ExecuteSql(sql);

                    Response.Write("<script type='text/javascript'>		alert('成功');window.location = 'ProjectNameEditor.aspx';</script>");
    



    }
}