using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
public partial class admin_HeadPictureMage : System.Web.UI.Page
{
    protected string[] picture = new string[3];// 轮转图片的路径

    protected string[] pictureUrl = new string[3];// 轮转图片的路径
    protected string[] picture1 = new string[3];// 轮转图片的路径

    protected string[] pictureUrl1 = new string[3];// 轮转图片的路径
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            ErrorMessage.Text = "请先登录！";
            this.ErrorMessage.Visible = true;
            return;
        }

        if (Session["UserName"].ToString() != "lin")
        {
            ErrorMessage.Text = "非管理员用户不能浏览当前页面！";
            this.ErrorMessage.Visible = true;
            return;
        }

        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Footer.xml"));
        XmlNode titleNode1 = document.DocumentElement.SelectSingleNode("pictures").SelectSingleNode("//picture[@id='1']").SelectSingleNode("item");
        picture[0] = ResolveUrl(titleNode1.InnerText);
      
        pictureUrl[0] = ResolveUrl(titleNode1.Attributes["url"].Value);
        for (int i = 1; i <= 2; i++)
        {
            titleNode1 = titleNode1.NextSibling;
            picture[i] = ResolveUrl(titleNode1.InnerText);
          
            pictureUrl[i] = ResolveUrl(titleNode1.Attributes["url"].Value);
        }
        XmlNode titleNode2 = document.DocumentElement.SelectSingleNode("pictures").SelectSingleNode("//picture[@id='2']").SelectSingleNode("item");
        picture1[0] = ResolveUrl(titleNode2.InnerText);
     
        pictureUrl1[0] = ResolveUrl(titleNode2.Attributes["url"].Value);
        for (int j = 1; j <= 2; j++)
        {
            titleNode2 = titleNode2.NextSibling;
            picture1[j] =ResolveUrl( titleNode2.InnerText);
        
            pictureUrl1[j] = ResolveUrl(titleNode2.Attributes["url"].Value);
        }

       


    }
}