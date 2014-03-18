using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
 //       Users.AuthInst.Logout(Session);
        Response.Cookies[FormsAuthentication.FormsCookieName].Value = "";
        Response.Redirect("~/index.aspx");
        Session["UserID"] = null;
        Session["UserName"] = null;
        Response.End();
    }
}
