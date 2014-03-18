using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Caching;

/// <summary>
///FQA 的摘要说明
/// </summary>
public class FAQ
{
	public FAQ()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        FAQID = 0;
        Question="";
        Answer = "";

	}

    private int _FAQID;
    public int FAQID
    {

        get { return _FAQID; }
        set { _FAQID = value; }
    }

    private string _Question;
    public string Question
    {
        get { return _Question; }
        set { _Question = value; }
    }

    private string _Answer;
    public string Answer
    {
        get { return _Answer; }
        set { _Answer = value; }
    }
    public const string QuestionCategoryKey = "QuestionCategoryDataSet";
    public static DataSet GetQuestionCategory(bool isFlush, Cache cache)
    {
        DataSet ds;
        if (isFlush)
        {
            ds = FlushQuestionCategoryKey();
            cache.Insert(QuestionCategoryKey, ds);
            return ds;
        }

        if (cache.Get(QuestionCategoryKey) == null)
        {
            ds = FlushQuestionCategoryKey();
            cache.Insert(QuestionCategoryKey, ds);
        }
        else
            ds = (DataSet)cache.Get(QuestionCategoryKey);

        return ds;
    }

    protected static DataSet FlushQuestionCategoryKey()
    {
        return DBHelper.INST.ExecuteSqlDS("select * from FAQ");
    }

}
