using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Digital 的摘要说明
/// </summary>
public class Digital
{
	public Digital()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        DigitalID = 0;
        DigitalTitle="";
        DigitalContent = "";
	}

    private int _DigitalID;
    public int DigitalID
    {
        get { return _DigitalID; }
        set { _DigitalID = value; }
    }

    private string _DigitalContent;
    public string DigitalContent
    {
        get { return _DigitalContent; }
        set { _DigitalContent = value; }
    }

    private string _DigitalTitle;
    public string DigitalTitle
    {
        get { return _DigitalTitle; }
        set { _DigitalTitle = value; }
    }

}