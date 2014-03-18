using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///OilProject 的摘要说明
/// </summary>
public class OilProject
{
	public OilProject()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        OilProjectID=0;
        OilProjectTitle="";
        OilProjectContent = "";
        OilProjectDate = DateTime.Now;
	}

    private int _OilProjectID;
    public int OilProjectID
    {
        get { return _OilProjectID; }
        set { _OilProjectID = value; }
    }

    private string _OilProjectTitle;
    public string OilProjectTitle
    {
        get { return _OilProjectTitle; }
        set { _OilProjectTitle = value; }
    }

    private string _OilProjectContent;
    public string OilProjectContent
    {
        get { return _OilProjectContent; }
        set { _OilProjectContent = value; }
    }
    public DateTime OilProjectDate
    {
        get;
        set;
    }
}