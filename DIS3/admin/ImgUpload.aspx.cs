using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class admin_ImgUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
 //       string path = "image\\news\\content\\";//上传文件存储的相对路径
        string path = "image/news/content/";
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
                    string fileName = System.IO.Path.GetFileName(InserImg.FileName);
                    string savePath = Server.MapPath("~/") + path + fileName;//绝对路径
                    InserImg.PostedFile.SaveAs(savePath);
           //         ImgPath.Text = "..\\" + path + fileName;
                    ImgPath.Text = "../" + path + fileName;     // 修改
                }
                catch (HttpException ex)
                {
                    Response.Write("图片上传失败");
                }
            }

            else Response.Write("<script language=javascript type ='text/javascript'>alert('不可接受的图片类型') </script>");
        }
        //未选择上传文件
        else
        {
            ImgPath.Text = null;
            Response.Write("<script language=javascript type ='text/javascript'>alert('请选择图片') </script>");
        }

        // Response.End();
    }
}
