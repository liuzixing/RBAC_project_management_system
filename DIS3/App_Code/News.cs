using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///News 的摘要说明
/// </summary>
public class News
{
	public News()
	{
		Title = "";
		Author = "";
		NewsID = 0;
		PublishTime = DateTime.Now;
		Content = "";
        NewsYear = DateTime.Today.Year;
        NewsMonth = DateTime.Today.Month;
        VisitCount = 1;
		IsVerified = false;
	}

	private string _Title;

	public string Title
	{
		get { return _Title; }
		set { _Title = value; }
	}
	private string _Content;

	public string Content
	{
		get { return _Content; }
		set { _Content = value; }
	}
	private string _Author;

	public string Author
	{
		get { return _Author; }
		set { _Author = value; }
	}
	private int _NewsID;

	public int NewsID
	{
		get { return _NewsID; }
		set { _NewsID = value; }
	}
	public DateTime PublishTime
	{
		get;
		set;
	}
    private int _NewsYear;
    public int NewsYear
    {
        get { return _NewsYear; }
        set { _NewsYear = value; }
    }
    private int _NewsMonth;
    public int NewsMonth
    {
        get { return _NewsMonth; }
        set { _NewsMonth = value; }
    }
    private int _VisitCount;
    public int VisitCount
    {
        get { return _VisitCount; }
        set { _VisitCount = value; }
    }
	public bool IsVerified
	{
		get;
		set;
	}
}
