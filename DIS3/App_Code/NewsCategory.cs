using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Caching;

/// <summary>
///NewsCategory 的摘要说明
/// </summary>
public class NewsCategory
{
	public NewsCategory()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

	public int NewsCategoryID { get; set; }
	public string NewsCategoryName { get; set; }

	public const string NewsCategoryKey = "NewsCategoryDataSet";
	public static DataSet GetNewsCategory(bool isFlush,Cache cache)
	{
		DataSet ds;
		if (isFlush)
		{
			ds = FlushNewsCategory();
			cache.Insert(NewsCategoryKey, ds);
			return ds;
		}

		if (cache.Get(NewsCategoryKey) == null)
		{
			ds = FlushNewsCategory();
			cache.Insert(NewsCategoryKey, ds);
		}
		else
			ds = (DataSet)cache.Get(NewsCategoryKey);

		return ds;
	}

	protected static DataSet FlushNewsCategory()
	{
        return DBHelper.INST.ExecuteSqlDS("select * from NewsCategories");
	}
}
