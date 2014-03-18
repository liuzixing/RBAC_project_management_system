using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Plans 的摘要说明
/// </summary>
public class Plans
{
	public Plans()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        StationPlanID = 0;
        PlanName = "";
        PlanClassifyID = 0;
        Content = "";
        PlanAuthor = "";
        PlanDate = DateTime.Now;
        VisitCount = 1;
        PlanClassify = "";
	}
    private int _StationPlanID;
    public int StationPlanID
    {
        get { return _StationPlanID; }
        set { _StationPlanID = value; }
    }

    private int _PlanClassifyID;
    public int PlanClassifyID
    {
        get { return _PlanClassifyID; }
        set { _PlanClassifyID = value; }
    }

    private string _Content;
    public string Content
    {
        get { return _Content; }
        set { _Content = value; }
    }

    private string _PlanName;
    public string PlanName
    {
        get { return _PlanName; }
        set { _PlanName = value; }
    }

    private string _PlanAuthor;
    public string PlanAuthor
    {
        get { return _PlanAuthor; }
        set { _PlanAuthor = value; }
    }

    public DateTime PlanDate
    {
        get;
        set;
    }

    private int _VisitCount;
    public int VisitCount
    {
        get { return _VisitCount; }
        set { _VisitCount = value; }
    }

    private string _PlanClassify;
    public string PlanClassify
    {
        get { return _PlanClassify; }
        set { _PlanClassify = value; }
    }
}