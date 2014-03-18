using System;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

/// <summary>
///Utils 的摘要说明
/// </summary>
public class Utils
{
	public Utils()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

	public static string Crypto(string str)
	{
		return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
	}

	public static string DateTime2String(DateTime d)
	{
		return d.ToString("yyyy-MM-dd HH:mm:ss");
	}

	public static string RenderControl(Control c)
	{
		StringBuilder sb = new StringBuilder();
		StringWriter tw = new StringWriter(sb);
		HtmlTextWriter hw = new HtmlTextWriter(tw);
		HtmlForm form = new FakeHtmlForm();
		Page page = new Page();
		page.EnableViewState = false;
		
		form.Controls.Add(c);
		page.Controls.Add(form);
		page.RenderControl(hw);
		return sb.ToString();
	}
}

public class FakeHtmlForm : HtmlForm
{
	//protected override void RenderEndTag(HtmlTextWriter writer)
	//{
	//    //base.RenderEndTag(writer);
	//}

	//protected override void RenderBeginTag(HtmlTextWriter writer)
	//{
	//    //base.RenderBeginTag(writer);
	//}
}
