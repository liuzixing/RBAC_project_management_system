using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Data;
/// <summary>
///NewRecruits 的摘要说明
/// </summary>
public class NewRecruits
{
    public NewRecruits()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
        NewRecruitsDuty = "";
        NewRecruitsID = 0;
        NewRecruitsDate = DateTime.Now;
        NewRecruitsContent = "";
        //NewsYear = DateTime.Today.Year;
        //NewsMonth = DateTime.Today.Month;
        OilProjectDirectionID = 0;
        NewRecruitsCategoryID = 0;
    }
    private string _NewRecruitsDuty;

    public string NewRecruitsDuty
    {
        get { return _NewRecruitsDuty; }
        set { _NewRecruitsDuty = value; }
    }
    private string _NewRecruitsContent;

    public string NewRecruitsContent
    {
        get { return _NewRecruitsContent; }
        set { _NewRecruitsContent = value; }
    }
    private int _OilProjectDirectionID;

    public int OilProjectDirectionID
    {
        get { return _OilProjectDirectionID; }
        set { _OilProjectDirectionID = value; }
    }
    private int _NewRecruitsID;

    public int NewRecruitsID
    {
        get { return _NewRecruitsID; }
        set { _NewRecruitsID = value; }
    }
    public DateTime NewRecruitsDate
    {
        get;
        set;
    }
    private string _NewRecruitsNumber;
    public string NewRecruitsNumber
    {
        get { return NewRecruitsNumber; }
        set { NewRecruitsNumber = value; }
    }
    private int _NewRecruitsCategoryID;
    public int NewRecruitsCategoryID
    {
        get { return _NewRecruitsCategoryID; }
        set { _NewRecruitsCategoryID = value; }
    }


}