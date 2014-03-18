using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml; 

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected string[] titleName = new string[7];  // 记录目录标题名
    protected string[] titleUrl = new string[7];  // 记录目录标题的路径
    protected string[] buttonImage = new string[13];// 图标的路径
    protected string[] buttonImageUrl = new string[13];// 图标的路径
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
         *  载入 Footer.xml 文件
         *  加载轮转图片与页脚
         */
        if (!IsPostBack)
        {
            if (Session["UserID"] == null)
            {
                this.loginState.Text = "登录";
                this.UserManage.Text = "";
            }
            else
            {
                this.loginState.Text = "退出";
                this.UserManage.Text = "欢迎您 " + Session["UserName"];
            }
        }
        XmlDocument document = new XmlDocument();           
        document.Load(Server.MapPath("~/Footer.xml"));
        Footer.DataSource = document.DocumentElement.SelectSingleNode("footer").ChildNodes;
        Footer.DataBind();

        /*
         * 利用 Footer.xml 文件 更新 titleName 与 titleUrl 
         */
        XmlNode titleNode = document.DocumentElement.SelectSingleNode("footer").SelectSingleNode("catalogue");
        titleName[0] = titleNode.Attributes["value"].Value;
        titleUrl[0] = ResolveUrl(titleNode.Attributes["url"].Value);
        for (int i = 1; i <= 5; i++)
        {
            titleNode = titleNode.NextSibling;
            titleName[i] = titleNode.Attributes["value"].Value;
            titleUrl[i] = ResolveUrl(titleNode.Attributes["url"].Value);
        }

        XmlNode titleNode3 = document.DocumentElement.SelectSingleNode("buttons").SelectSingleNode("//buttonImage[@id='1']").SelectSingleNode("item");
        buttonImage[0] = ResolveUrl(titleNode3.InnerText);
        buttonImageUrl[0] = ResolveUrl(titleNode3.Attributes["url"].Value);
        for (int i = 1; i <= 12; i++)
        {
            titleNode3 = titleNode3.NextSibling;
            buttonImage[i] = ResolveUrl(titleNode3.InnerText);
            buttonImageUrl[i] = ResolveUrl(titleNode3.Attributes["url"].Value);
        }


        // 绑定数据，公司名与链接
        titleNode = titleNode.ParentNode.ParentNode.SelectSingleNode("Company");
        titleName[6] = titleNode.Attributes["value"].Value;
        titleUrl[6] = ResolveUrl(titleNode.Attributes["url"].Value);

        // 绑定公司名页脚下的Repeater控件
        Company.DataSource = document.DocumentElement.SelectSingleNode("Company").ChildNodes;
        Company.DataBind();
    }
}
