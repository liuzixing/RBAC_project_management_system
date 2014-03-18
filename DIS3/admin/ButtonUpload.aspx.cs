using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
public partial class admin_ButtonUpload : System.Web.UI.Page
{
    protected int a;
    protected string[] buttonImage = new string[13];// 图标的路径
    protected string[] backButtonImage = new string[13];// 图标的路径

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Picture"] != null)
        {
            XmlDocument document = new XmlDocument();
            document.Load(Server.MapPath("~/Footer.xml"));


            
            a = Convert.ToInt32(Request["Picture"]);
            
            
            XmlNode titleNode1;
            
            
                titleNode1 = document.DocumentElement.SelectSingleNode("buttons").SelectSingleNode("//buttonImage[@id='1']").SelectSingleNode("item");
            
                
            if (a == 1)
            {
                buttonImage[a - 1] = titleNode1.InnerText;
                backButtonImage[a - 1] = ResolveUrl( buttonImage[a - 1]);
            }
            else
                for (int i = 1; i <= 12; i++)
                {
                    titleNode1 = titleNode1.NextSibling;
                    if (i == a - 1)
                    {
                        buttonImage[a - 1] = titleNode1.InnerText;
                        backButtonImage[a - 1] =ResolveUrl( buttonImage[a - 1]);

                        break;
                    }
                }


        }
    }
    private static char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    public static string GenerateRandomNumber(int Length)
    {
        System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
        Random rd = new Random();
        for (int i = 0; i < Length; i++)
        { newRandom.Append(constant[rd.Next(62)]); } return newRandom.ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string path = "images/button/";
        if (InserImg.HasFile)
        {
            bool isAllow = false;
            //文件类型
            string fileType = System.IO.Path.GetExtension(InserImg.FileName).ToLower();
            // 定义允许上传的文件类型
            string[] allowFile = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };
            //检查文件类型是否合法
            for (int i = 0; i < allowFile.Length; i++)
            {
                if (fileType == allowFile[i])
                    isAllow = true;
            }
            if (isAllow)
            {
                try
                {

                    string fileName = GenerateRandomNumber(5) + System.IO.Path.GetFileName(InserImg.FileName);
                    string savePath = Server.MapPath("~/") + path + fileName;//绝对路径
                    InserImg.PostedFile.SaveAs(savePath);
                    //         ImgPath.Text = "..\\" + path + fileName;
                    ImgPath.Text = "~/" + path + fileName;     // 修改
                    buttonImage[a - 1] ="~/"+ path + fileName;
                    XmlDocument document = new XmlDocument();
                    document.Load(Server.MapPath("~/Footer.xml"));
                    XmlNode titleNode1;
                   
                        titleNode1 = document.DocumentElement.SelectSingleNode("buttons").SelectSingleNode("//buttonImage[@id='1']").SelectSingleNode("item");
                   
                      
                    if (a == 1)
                    {
                        titleNode1.InnerText = buttonImage[a - 1];
                        document.Save(Server.MapPath("~/Footer.xml"));

                    }
                    else
                        for (int i = 1; i <= 12; i++)
                        {
                            titleNode1 = titleNode1.NextSibling;
                            if (i == a - 1)
                            {
                                titleNode1.InnerText = buttonImage[a - 1];
                                document.Save(Server.MapPath("~/Footer.xml"));
                                break;
                            }
                        }




                    Response.Write("<script type='text/javascript'>	alert('修改图片成功');window.location = 'button.aspx';</script>");
                }
                catch (HttpException ex)
                {
                    Response.Write("图片上传失败");
                }
            }

            else Response.Write("<script language=javascript type ='text/javascript'>alert('不可接受的图片类型'); window.location = 'button.aspx';</script>");
        }
        //未选择上传文件
        else
        {
            ImgPath.Text = null;
            Response.Write("<script language=javascript type ='text/javascript'>alert('请选择图片');window.location = 'button.aspx'; </script>");
        }

        // Response.End();
    }
}