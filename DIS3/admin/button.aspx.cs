using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class admin_button : System.Web.UI.Page
{
    protected string[] buttonImage = new string[13];// 图标的路径
    protected string[] backButtonImage = new string[13];// 备份图标的路径
    protected string[] buttonImageUrl = new string[13];// 图标的路径
    protected void Page_Load(object sender, EventArgs e)
    {

        XmlDocument document = new XmlDocument();
        document.Load(Server.MapPath("~/Footer.xml"));
        XmlNode titleNode1 = document.DocumentElement.SelectSingleNode("buttons").SelectSingleNode("//buttonImage[@id='1']").SelectSingleNode("item");
        buttonImage[0] = titleNode1.InnerText;
        backButtonImage[0] = ResolveUrl( buttonImage[0]);
        buttonImageUrl[0] = ResolveUrl(titleNode1.Attributes["url"].Value);
        for (int i = 1; i <= 12; i++)
        {
            titleNode1 = titleNode1.NextSibling;
            buttonImage[i] = titleNode1.InnerText;
            backButtonImage[i] = ResolveUrl(buttonImage[i]);
            buttonImageUrl[i] = ResolveUrl(titleNode1.Attributes["url"].Value);
        }
       /* XmlNode titleNode2 = document.DocumentElement.SelectSingleNode("buttons").SelectSingleNode("//buttonImage[@id='2']").SelectSingleNode("item");
        buttonImage[0] = titleNode2.InnerText;
        backButtonImage[0] = "../" + buttonImage[0];
        buttonImageUrl[0] = ResolveUrl(titleNode2.Attributes["url"].Value);
        for (int j = 1; j <= 12; j++)
        {
            titleNode2 = titleNode2.NextSibling;
            buttonImage[j] = titleNode2.InnerText;
            backButtonImage[j] = "../" + buttonImage[j];
            buttonImageUrl[j] = ResolveUrl(titleNode2.Attributes["url"].Value);
        }*/




    }
}