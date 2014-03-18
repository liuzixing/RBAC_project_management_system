using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using System.Xml;   // ADD

public partial class admin_NewsEditor : System.Web.UI.Page
{
    protected string editorContent;
    private int newsID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["newsid"] != null)
        {
            newsID = Convert.ToInt32(Request["newsid"].Trim());
        }
        else
        {
            newsID = -1;
        }

        if (!IsPostBack)
        {
//             DataSet ds = DBHelper.INST.ExecuteSqlDS("select NewsCategoryName from NewsCategories ");
//             // 为控件绑定数据源，必须是表 
//             DropDownList1.DataSource = ds.Tables[0];
//             // 选择为下拉列表提供数据的字段
//             DropDownList1.DataValueField = "NewsCategoryName";
//             DropDownList1.DataTextField = "NewsCategoryName";
//             DropDownList1.DataBind();

            if (Request["newsid"] != null)
            {
                
                News n = new News();
                string sql = "select * from News where NewsID=" + newsID;// +" and NewsState<>'0'";
                var r = DBHelper.INST.ExecuteSqlDR(sql);
                if (!r.Read())
                {
                    Response.Write("新闻未通过审核");
                    Response.End();
                }
                n.Title = r["NewsTitle"].ToString();
                n.Content = r["Content"].ToString();
                n.Author = r["NewsAuthor"].ToString();
//                 int newsCategoryID = Convert.ToInt32(r["NewsCategoryID"].ToString());
//                 String s = " select NewsCategoryName from NewsCategories where NewsCategoryID=" + newsCategoryID;
//                 DataSet dsNews = DBHelper.INST.ExecuteSqlDS(s);
//                 string newsCategoryName = dsNews.Tables[0].Rows[0][0].ToString();
//                 DropDownList1.SelectedValue = newsCategoryName;
                txtTitle.Text = n.Title;
                txtAuthor.Text = n.Author;
                editorContent = n.Content;
                r.Close();
            }
            else
            {
                
                editorContent = "&lt;p&gt;This is some &lt;strong&gt;sample text&lt;/strong&gt;. ";
            }
        }
       
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (newsID == -1)   //  添加新闻
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
//                     String s = " select NewsCategoryID from NewsCategories where NewsCategoryName='" + DropDownList1.SelectedValue + "'";
//                     DataSet ds = DBHelper.INST.ExecuteSqlDS(s);
//                     int id = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    // 插入数据
                    String sql = "insert into  News  ( NewsTitle, Content, NewsDate, NewsAuthor ,  NewsYear, NewsMonth,VisitCount,IsDelete) values('"
                    + txtTitle.Text.ToString() + "', '" + Request["editor1"] + "', #" + DateTime.Now.ToLocalTime().ToString() + "#, '" + txtAuthor.Text + "'," + DateTime.Now.Year + "," + DateTime.Now.Month + "," + 1 + ","+0+")";//尚需修改
                    DBHelper.INST.ExecuteSql(sql);
                    /*
                      * ADD 写入XML文件
                     */
                    String sd = " select NewsYear, NewsMonth from News order by NewsYear desc, NewsMonth desc";
                    DataSet dsDate = DBHelper.INST.ExecuteSqlDS(sd);

                    XmlDocument document = new XmlDocument();           // 操作Date.xml

                    //                XmlDocument docSiteMap = new XmlDocument();

                    document.Load(Server.MapPath("~/Date.xml"));

                    //                docSiteMap.Load(Server.MapPath("~/Web.sitemap"));

                    XmlElement root = document.DocumentElement;
                    root.RemoveAll();
                    XmlElement year = document.CreateElement("year");
                    year.SetAttribute("value", dsDate.Tables[0].Rows[0]["NewsYear"].ToString());

                    XmlElement month = document.CreateElement("month");
                    XmlText monthValue = document.CreateTextNode(dsDate.Tables[0].Rows[0]["NewsMonth"].ToString());
                    month.AppendChild(monthValue);
                    year.AppendChild(month);
                    root.AppendChild(year);


//5-18
                    XmlDocument docSiteMap = new XmlDocument();
                    docSiteMap.Load(Server.MapPath("~/Web.sitemap"));
                    XmlElement rootSiteMap = docSiteMap.DocumentElement;
                    //// 删除原本的与分类导航有关的siteMapNode节点
                    XmlNodeList dateNodeList = rootSiteMap.SelectNodes("//siteMapNode[@description='date']");
                    for (int j = 0; j < dateNodeList.Count; j++)
                    {
                        dateNodeList.Item(j).ParentNode.RemoveChild(dateNodeList.Item(j));
                    }
//5-18end
                    

                    for (int i = 1; i < dsDate.Tables[0].Rows.Count; i++)
                    {
                        if (dsDate.Tables[0].Rows[i]["NewsYear"].ToString() == dsDate.Tables[0].Rows[i - 1]["NewsYear"].ToString())
                        {
                            if (dsDate.Tables[0].Rows[i]["NewsMonth"].ToString() != dsDate.Tables[0].Rows[i - 1]["NewsMonth"].ToString())
                            {
                                // for the Date.xml
                                month = document.CreateElement("month");
                                monthValue = document.CreateTextNode(dsDate.Tables[0].Rows[i]["NewsMonth"].ToString());
                                month.AppendChild(monthValue);
                                year.AppendChild(month);
                            }
                        }
                        else
                        {
                            // for the Date.xml
                            year = document.CreateElement("year");
                            year.SetAttribute("value", dsDate.Tables[0].Rows[i]["NewsYear"].ToString());
                            root.AppendChild(year);
                            month = document.CreateElement("month");
                            monthValue = document.CreateTextNode(dsDate.Tables[0].Rows[i]["NewsMonth"].ToString());
                            month.AppendChild(monthValue);
                            year.AppendChild(month);
                        }
                    }

                    for (int i = 0; i < dsDate.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            //增加第一条数据
                            XmlNode newsCenter = rootSiteMap.SelectSingleNode("//siteMapNode[@description='newsCenter']");
                            XmlElement siteMapNodeByCategory = docSiteMap.CreateElement("siteMapNode");
                            siteMapNodeByCategory.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=");
                            siteMapNodeByCategory.SetAttribute("title", dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年");
                            siteMapNodeByCategory.SetAttribute("description", "date");
                            newsCenter.AppendChild(siteMapNodeByCategory);
                            XmlElement siteMapNodeByCategory2 = docSiteMap.CreateElement("siteMapNode");
                            siteMapNodeByCategory2.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=" + dsDate.Tables[0].Rows[i]["NewsMonth"].ToString());
                            siteMapNodeByCategory2.SetAttribute("title", dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年" + dsDate.Tables[0].Rows[i]["NewsMonth"].ToString() + "月");
                            siteMapNodeByCategory2.SetAttribute("description", "date");
                            newsCenter.AppendChild(siteMapNodeByCategory2);
                        }
                        else  if (dsDate.Tables[0].Rows[i]["NewsYear"].ToString() == dsDate.Tables[0].Rows[i - 1]["NewsYear"].ToString())
                        {
                            if (dsDate.Tables[0].Rows[i]["NewsMonth"].ToString() != dsDate.Tables[0].Rows[i - 1]["NewsMonth"].ToString())
                            {
                                //// 写入与分类导航有关的siteMapNode节点
                                XmlNode newsCenter = rootSiteMap.SelectSingleNode("//siteMapNode[@description='newsCenter']");
                                XmlElement siteMapNodeByCategory = docSiteMap.CreateElement("siteMapNode");
                                siteMapNodeByCategory.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=" + dsDate.Tables[0].Rows[i]["NewsMonth"].ToString());
                                siteMapNodeByCategory.SetAttribute("title", dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年" + dsDate.Tables[0].Rows[i]["NewsMonth"].ToString() + "月");
                                siteMapNodeByCategory.SetAttribute("description", "date");
                                newsCenter.AppendChild(siteMapNodeByCategory);
                            }
                        }
                        else
                        {
                            //// 写入与分类导航有关的siteMapNode节点
                            XmlNode newsCenter = rootSiteMap.SelectSingleNode("//siteMapNode[@description='newsCenter']");
                            XmlElement siteMapNodeByCategory = docSiteMap.CreateElement("siteMapNode");
                            siteMapNodeByCategory.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=");
                            siteMapNodeByCategory.SetAttribute("title", dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年");
                            siteMapNodeByCategory.SetAttribute("description", "date");
                            newsCenter.AppendChild(siteMapNodeByCategory);
                              XmlElement siteMapNodeByCategory2 = docSiteMap.CreateElement("siteMapNode");
                            siteMapNodeByCategory2.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=" + dsDate.Tables[0].Rows[i]["NewsMonth"].ToString());
                            siteMapNodeByCategory2.SetAttribute("title", dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年" + dsDate.Tables[0].Rows[i]["NewsMonth"].ToString() + "月");
                            siteMapNodeByCategory2.SetAttribute("description", "date");
                            newsCenter.AppendChild(siteMapNodeByCategory2);
                        }
                    }
                    document.Save(Server.MapPath("~/Date.xml"));
                    docSiteMap.Save(Server.MapPath("~/Web.sitemap"));
                   

                    //操作Footer.xml
                    XmlDocument documentfoot = new XmlDocument();
                    documentfoot.Load(Server.MapPath("~/Footer.xml"));
                    XmlNode mNode = documentfoot.DocumentElement.FirstChild;
                    XmlNode mCurrentNode = mNode.SelectSingleNode("catalogue[@id='2']");
                    mCurrentNode.RemoveAll();


                    XmlAttribute attr1 = null, attr2 = null, attr3 = null;
                    attr1 = documentfoot.CreateAttribute("value");
                    attr2 = documentfoot.CreateAttribute("id");

                    attr1.Value = "油站新闻";
                    attr2.Value = "2";
                    mCurrentNode.Attributes.Append(attr1);
                    mCurrentNode.Attributes.Append(attr2);
                    if (dsDate.Tables[0].Rows.Count > 0)
                    {
                        attr3 = documentfoot.CreateAttribute("url");
                        attr3.Value = "~/news/NewsTempate.aspx";
                        mCurrentNode.Attributes.Append(attr3);
                    }
                    XmlElement item = documentfoot.CreateElement("item");
                    int num = 0;
                    for (int i = 0; i < dsDate.Tables[0].Rows.Count && num < 5; i++)
                    {
                        if (i == 0)
                        {
                            //增加第一条数据
                            item = documentfoot.CreateElement("item");
                            item.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=");

                            XmlText value = documentfoot.CreateTextNode(dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年");
                            item.AppendChild(value);
                            mCurrentNode.AppendChild(item);
                            num++;
                        }
                        else if (dsDate.Tables[0].Rows[i]["NewsYear"].ToString() != dsDate.Tables[0].Rows[i - 1]["NewsYear"].ToString())
                        {

                                item = documentfoot.CreateElement("item");
                                item.SetAttribute("url", "~/news/NewsTempate.aspx?NewsYear=" + dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "&month=");

                                XmlText value = documentfoot.CreateTextNode(dsDate.Tables[0].Rows[i]["NewsYear"].ToString() + "年");
                                item.AppendChild(value);
                                mCurrentNode.AppendChild(item);
                                num++;


                        }

                    }
                    documentfoot.Save(Server.MapPath("~/Footer.xml"));
/*                    docSiteMap.Save(Server.MapPath("~/Web.sitemap"));*/

                   


                    //  写入Date.xml文件, 其他实现方式

                    //String sd = " select NewsYear, NewsState from News order by NewsYear desc, NewsState desc";
                    //DataSet dsDate = DBHelper.INST.ExecuteSqlDS(sd);
                    //XmlWriterSettings settings = new XmlWriterSettings();
                    //settings.Indent = true;
                    //settings.NewLineOnAttributes = true;
                    //XmlWriter writer = XmlWriter.Create(Server.MapPath("~/")+"filesystem/Date.xml", settings);
                    //writer.WriteStartDocument();
                    //writer.WriteStartElement("date");
                    //writer.WriteStartElement("year");
                    //writer.WriteAttributeString("value", dsDate.Tables[0].Rows[0]["NewsYear"].ToString());
                    //writer.WriteElementString("month", dsDate.Tables[0].Rows[0]["NewsState"].ToString());
                    //for (int i = 1; i < dsDate.Tables[0].Rows.Count; i++)
                    //{
                    //    if (dsDate.Tables[0].Rows[i]["NewsYear"].ToString() == dsDate.Tables[0].Rows[i - 1]["NewsYear"].ToString())
                    //    {
                    //        if (dsDate.Tables[0].Rows[i]["NewsState"].ToString() != dsDate.Tables[0].Rows[i - 1]["NewsState"].ToString())
                    //        {
                    //            writer.WriteElementString("month", dsDate.Tables[0].Rows[i]["NewsState"].ToString());
                    //        }
                    //    }
                    //    else
                    //    {
                    //        writer.WriteEndElement();
                    //        writer.WriteStartElement("year");
                    //        writer.WriteAttributeString("value", dsDate.Tables[0].Rows[i]["NewsYear"].ToString());
                    //        writer.WriteElementString("month", dsDate.Tables[0].Rows[i]["NewsState"].ToString());
                    //    }
                    //}
                    //writer.WriteEndElement();
                    //writer.WriteEndElement();
                    //writer.WriteEndDocument();
                    //writer.Flush();
                    //writer.Close();

                    // END

                    /* String sql = "insert into  News  ( NewsTitle, NewsText, NewsDate, Month,NewsCategoryID, NewsAuthor, UserID, NewsState) values('"
                    + txtTitle.Text.ToString() + "', '" + Request["editor1"] + "', #" + DateTime.Now.ToLocalTime().ToString() + "#, " + "', #" + DateTime.Now.Month.ToString() + "#, " + id + ", '" + txtAuthor.Text + "'," + 0 + "," + 0 + ")";//尚需修改
                     DBHelper.INST.ExecuteSql(sql);
                 */
                    /* String sql = "insert into  News  ( NewsTitle, NewsText, NewsDate, Month,NewsCategoryID, NewsAuthor, UserID, NewsState)values('"
                         +txtTitle.Text.ToString()+"'"+Request["editor1"]+"'"+DateTime.Now.ToLocalTime().ToString()+"'"+DateTime.Now.Month+"'"+id+"'"+txtAuthor.Text+"'"+0+"'"+0+"')";
                  DBHelper.INST.ExecuteSql(sql);
                     *   */

                    //修改Footer.xml









                    dsDate.Dispose();
                    Response.Write("<script type='text/javascript'>		alert('发布成功');window.location = 'NewsEditor.aspx';</script>");
                }
            }

        }
        else  // 修改新闻
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
//                    String s = " select NewsCategoryID from NewsCategories where NewsCategoryName='" + DropDownList1.SelectedValue + "'";
//                    DataSet ds = DBHelper.INST.ExecuteSqlDS(s);
//                   int id = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
//                     // 修改数据
                    String sql = "update  News set  NewsTitle ='" + txtTitle.Text.ToString() + "',Content= '" + Request["editor1"].ToString() + "',NewsAuthor= '" + txtAuthor.Text + "',NewsDate=#" + DateTime.Now.ToLocalTime().ToString() + "#,NewsYear=" + DateTime.Now.Year + ",NewsMonth=" + DateTime.Now.Month + " where NewsID =" + newsID;
                    DBHelper.INST.ExecuteSql(sql);
                    /*
                      *  写入XML文件
                     */
                    String sd = " select NewsYear, NewsMonth from News order by NewsYear desc, NewsMonth desc";
                    DataSet dsDate = DBHelper.INST.ExecuteSqlDS(sd);
                    XmlDocument document = new XmlDocument();           // 操作Date.xml
                    document.Load(Server.MapPath("~/Date.xml"));
                    XmlElement root = document.DocumentElement;
                    root.RemoveAll();
                    XmlElement year = document.CreateElement("year");
                    year.SetAttribute("value", dsDate.Tables[0].Rows[0]["NewsYear"].ToString());
                    XmlElement month = document.CreateElement("month");
                    XmlText monthValue = document.CreateTextNode(dsDate.Tables[0].Rows[0]["NewsMonth"].ToString());
                    month.AppendChild(monthValue);
                    year.AppendChild(month);
                    root.AppendChild(year);
                    for (int i = 1; i < dsDate.Tables[0].Rows.Count; i++)
                    {
                        if (dsDate.Tables[0].Rows[i]["NewsYear"].ToString() == dsDate.Tables[0].Rows[i - 1]["NewsYear"].ToString())
                        {
                            if (dsDate.Tables[0].Rows[i]["NewsMonth"].ToString() != dsDate.Tables[0].Rows[i - 1]["NewsMonth"].ToString())
                            {
                                // for the Date.xml
                                month = document.CreateElement("month");
                                monthValue = document.CreateTextNode(dsDate.Tables[0].Rows[i]["NewsMonth"].ToString());
                                month.AppendChild(monthValue);
                                year.AppendChild(month);
                            }
                        }
                        else
                        {
                            // for the Date.xml
                            year = document.CreateElement("year");
                            year.SetAttribute("value", dsDate.Tables[0].Rows[i]["NewsYear"].ToString());
                            root.AppendChild(year);
                            month = document.CreateElement("month");
                            monthValue = document.CreateTextNode(dsDate.Tables[0].Rows[i]["NewsMonth"].ToString());
                            month.AppendChild(monthValue);
                            year.AppendChild(month);                            
                        }
                    }
                    document.Save(Server.MapPath("~/Date.xml"));
                    dsDate.Dispose();
                    Response.Write("<script type='text/javascript'>		alert('修改成功');window.location = 'NewsManage.aspx';</script>");

                }
            }

        }
    }
}
