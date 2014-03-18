using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///AboutOil 的摘要说明
/// </summary>
public class AboutOil
{
	public AboutOil()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        AboutOilID = 0;
      OilClassify="";
      OilContent = "";
	}
     private int _AboutOilID;
    public int AboutOilID{
    
        get { return _AboutOilID; }
        set { _AboutOilID = value; }
    }

    private string _OilClassify;
    public string OilClassify
    {
        get { return _OilClassify; }
        set { _OilClassify = value; }
    }

    private string _OilContent;
    public string OilContent
    {
        get { return _OilContent; }
        set { _OilContent = value; }
    }      
    
}
