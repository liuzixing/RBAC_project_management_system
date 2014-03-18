using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Xml;
/// <summary>
/// Mail 的摘要说明
/// </summary>
public class Mail
{
    public Mail()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static void SendMail(string sendMailBox, string title, string message)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(System.Web.HttpContext.Current.Server.MapPath("~/Mail.xml"));
        XmlNode root = doc.SelectSingleNode("Mail");
        string mailbox = root.ChildNodes[0].InnerText;
        string smtp = root.ChildNodes[1].InnerText;
        int port = Convert.ToInt32(root.ChildNodes[2].InnerText);
        string user = root.ChildNodes[3].InnerText;
        string password = root.ChildNodes[4].InnerText;
        bool ssl = Convert.ToBoolean(root.ChildNodes[5].InnerText);
        SmtpClient client = new SmtpClient(smtp, port);
        MailMessage msg = new MailMessage(mailbox, sendMailBox, title, message);
        client.UseDefaultCredentials = false;
        msg.IsBodyHtml = true;
        msg.BodyEncoding = System.Text.Encoding.UTF8;
        System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(user, password);
        client.Credentials = basicAuthenticationInfo;
        client.EnableSsl = ssl;
        client.Send(msg);
    }
    public static void setMail(string mailbox, string smtp, int port, string user, string password, bool ssl)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(System.Web.HttpContext.Current.Server.MapPath("~/Mail.xml"));
        XmlNode root = doc.SelectSingleNode("Mail");
        root.ChildNodes[0].InnerText=mailbox;
        root.ChildNodes[1].InnerText=smtp;
        root.ChildNodes[2].InnerText=Convert.ToString(port);
        root.ChildNodes[3].InnerText=user;
        root.ChildNodes[4].InnerText=password;
        root.ChildNodes[5].InnerText=Convert.ToString(ssl);
        doc.Save(System.Web.HttpContext.Current.Server.MapPath("~/Mail.xml"));
    }
}