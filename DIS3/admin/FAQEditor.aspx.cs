using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_FAQ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["FAQID"] != null)  // 修改计划
            {
                int FAQID = Convert.ToInt32(Request["FAQID"].Trim());
                string sql = "select * from FAQ where FAQID=" + FAQID;// +" and NewsState<>'0'";
                var r = DBHelper.INST.ExecuteSqlDR(sql);
                if (!r.Read())
                {
                    Response.Write("内容未通过审核");
                    Response.End();
                }
                Question.Text = r["Question"].ToString();
                Answer.Text = r["Answer"].ToString();
                r.Close();
            }
            else  // 添加计划
            {

            }
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Request["FAQID"] == null)
        {
            if ((Question.Text == string.Empty) | (Answer.Text == string.Empty))
                Response.Write("<script language= javascript> alert('问题或答案不能为空')</script>");
            else
            {
                if (Question.Text.Length > 400 | Answer.Text.Length > 400)//标题长度超过100字符
                    Response.Write("<script language= javascript> alert('问题长度超过上限')</script>");
                else
                {
                    //插入数据
                    String sql = "insert into  FAQ ( Question, Answer) values('"
                    + Question.Text.ToString() + "', '" + Answer.Text.ToString() + "')";
                    DBHelper.INST.ExecuteSql(sql);

                    Response.Write("<script type='text/javascript'>		alert('发布成功');window.location = 'FAQEditor.aspx';</script>");
                }
            }
        }
        else
        {
            int FAQID = Convert.ToInt32(Request["FAQID"].Trim());
            if ((Question.Text == string.Empty) | (Answer.Text == string.Empty))
                Response.Write("<script language= javascript> alert('问题或答案不能为空')</script>");
            else
            {
                if (Question.Text.Length > 400 | Answer.Text.Length > 400)//标题长度超过100字符
                    Response.Write("<script language= javascript> alert('问题长度超过上限')</script>");
                else
                {
                    // 修改数据
                    String sql = "update  FAQ set  Question ='" + Question.Text.ToString() + "',Answer= '" + Answer.Text.ToString() + "' where FAQID =" + FAQID;   
                    DBHelper.INST.ExecuteSql(sql);

                    Response.Write("<script type='text/javascript'>		alert('修改成功');window.location = 'FAQMage.aspx';</script>");
                }
            }
        }

    }
}