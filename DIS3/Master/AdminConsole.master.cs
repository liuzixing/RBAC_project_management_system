using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_AdminConsole : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int k = 0;
        if (Session["UserName"] != null)
        {
            this.Information.Text = "欢迎您 ：" + Session["UserName"].ToString() + "  ";   
        }
    }
}
