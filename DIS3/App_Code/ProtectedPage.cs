using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///ProtectedPage 的摘要说明
/// </summary>
public class ProtectedPage : System.Web.UI.Page
{
	public ProtectedPage()
	{
		this.Load += onPageLoad;
	}

	private void onPageLoad(object sender, EventArgs e)
	{
		if (!hasRight())
			Response.Redirect("~/Login.aspx?from=" + Server.HtmlEncode(Request.RawUrl));
	}

	protected virtual bool hasRight()
	{
		return true;
	}
}
