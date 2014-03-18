using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Enterprises 的摘要说明
/// </summary>
public class Enterprises
{
	public Enterprises()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        EnterprisesID = 0;
        EnterprisesName = "";       
        Content = "";                  
        
	}
    private int _EnterprisesID;
    public int EnterprisesID
    {
        get { return _EnterprisesID; }
        set { _EnterprisesID = value; }
    }

    private string _Content;
    public string Content
    {
        get { return _Content; }
        set { _Content = value; }
    }

    private string _EnterprisesName;
    public string EnterprisesName
    {
        get { return _EnterprisesName; }
        set { _EnterprisesName = value; }
    }      
    
}