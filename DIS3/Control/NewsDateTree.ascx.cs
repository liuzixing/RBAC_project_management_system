﻿using System;
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
using System.Xml;

public partial class Control_NewsDateTree : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument dom = new XmlDocument();
        dom.Load(Server.MapPath("~/Date.xml"));
        //       XmlNodeList date = dom.SelectNodes("//date");
        dateTree.DataSource = dom.DocumentElement.ChildNodes;
        dateTree.DataBind();
    }
}
