using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_PlanPageBarControl : System.Web.UI.UserControl
{
    private PageResult _DataSource;
    protected PageResult DataSource
    {
        get { return _DataSource; }
    }

    private string _BaseURL;
    public string BaseURL
    {
        get { return _BaseURL; }
    }

    public string TableName { get; set; }
    //public string Condition { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

    string condition = null;



    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["p"] != null)
            PageIndex = Convert.ToInt32(Request["p"]);
        else
            PageIndex = 1;

        //根据categoryid，分类显示列表

        if (Request["PlanClassifyID"] != null) // && Request["month"] == "")
        {
            condition = "PlanClassifyID = " + Convert.ToInt32(Request["PlanClassifyID"]);
            // url += "categoryid=" + Request["categoryid"] + '&';
        }
        


        object cache = Cache[TableName + condition];
        if (cache == null)
        {
            _DataSource = DataPager.PageState(TableName, PageSize, condition);
            Cache.Add(TableName + condition, _DataSource, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 5), System.Web.Caching.CacheItemPriority.Default, null);
        }
        else
        {
            _DataSource = (PageResult)cache;
        }

        _BaseURL = Request.Url.AbsolutePath + '?';
        foreach (var p in Request.QueryString.AllKeys)
        {
            if (p != "p")
                _BaseURL += p + "=" + Request.QueryString[p] + '&';
        }
    }
}