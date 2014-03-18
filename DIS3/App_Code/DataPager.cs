using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
//分页
/// <summary>
///DataPager 
///Author : DQ
/// </summary>
public abstract class DataPager
{
	public DataPager()
	{
	}

	/// <summary>
	/// 分页（只提供结果集，不提供分页状态）。要获得分页状态信息，调用PageState
	/// </summary>
	/// <param name="pageIndex">页码，大于1</param>
	/// <param name="pageSize">页大小</param>
	/// <param name="table">数据库表名</param>
	/// <param name="fields">指定查询字段。如id,title,content</param>
	/// <param name="idField">指定主键字段。必须为自增标识。null表示使用默认名称（表名+ID）</param>
	/// <param name="condition">指定查询语句。如 title like '%keyword%'</param>
	/// <param name="order">指定排序方式。只能是"asc"、"desc"之一</param>
	/// <returns>分页查询结果</returns>
	public abstract PageResult PageData(int pageIndex, int pageSize, string table, string fields, string idField, string condition, string order);

	//not completed
	/// <summary>
	/// 查询分页状态，如总页数、总记录条数。一般由.net进行缓存优化效率。
	/// </summary>
	/// <param name="table">数据库表</param>
	/// <param name="pageSize">页大小</param>
	/// <param name="condition">附加的查询条件</param>
	/// <returns></returns>
	public static PageResult PageState(string table,int pageSize, string condition)
	{
		PageResult pr = new PageResult()
		{
			PageIndex = -1,
			Result = null,
			PageSize = pageSize,
			TotalPage = 0,
			TotalRow = 0
		};
		string sql = "select count(*) from {0} {1}";
		sql = string.Format(sql,
			table,
			condition == null ? "" : "where " + condition);

		DbDataReader r = DBHelper.INST.ExecuteSqlDR(sql);
		r.Read();
		pr.TotalRow = r.GetInt32(0);
		pr.TotalPage = pr.TotalRow % pr.PageSize == 0 ? pr.TotalRow / pr.PageSize : pr.TotalRow / pr.PageSize + 1;

		return pr;
	}

	public static PageResult PageData(int pageIndex, int pageSize, string table) {
		DataPager impl = new ExclusiveDataPager();
		return impl.PageData(pageIndex, pageSize, table, "*", table + "ID", null, "desc");
	}

	public static DataPager CreateInstance
	{
		get { return new ExclusiveDataPager(); }
	}
}

//正常可用
public class ExclusiveDataPager : DataPager {
	public ExclusiveDataPager()
	{
	}

	public override PageResult PageData(int pageIndex, int pageSize, string table, string fields, string idField, string condition, string order)
	{
		PageResult pr = new PageResult()
		{
			PageIndex = pageIndex,
			Result = null,
			PageSize = pageSize,
			TotalPage = 0,
			TotalRow = 0
		};

       

		string sql;
		if (pageIndex != 1)
		{
			bool isAsc = String.Compare(order,"asc",true)==0 ? true : false;
			sql = "select top {0} {1} from {2} where {3} {7} (select {8}({3}) from (select top {9} {3} from {2}  {10} order by {3} {5}) as T1354A ) {6} order by {3} {5}";
			sql = string.Format(sql,
				pageSize,
				fields,
				table,
				idField,
				pageIndex-1,
				order,
				condition == null ? "" : "and " + condition,
				isAsc ? ">" : "<",
				isAsc ? "max" : "min",
                (pageIndex-1)*pageSize,
                condition == null ? "" : "where " + condition);
		}
		else
		{
			sql = "select top {0} {1} from {2} {5} order by {3} {4}";
			sql = string.Format(sql,
				pageSize,
				fields,
				table,
				idField,
				order,
				condition == null ? "" : "where " + condition);
		}

		pr.Result = DBHelper.INST.ExecuteSqlDS(sql);

		//test sentence
		//throw new Exception(sql);

		return pr;
	}
}

//查询结果不正确，12-1
public class NotInDataPager : DataPager {
	public NotInDataPager() { }

	public override PageResult PageData(int pageIndex, int pageSize, string table, string fields, string idField, string condition, string order) 
	{
		PageResult pr = new PageResult()
		{
			PageIndex = pageIndex,
			Result = null,
			PageSize = pageSize,
			TotalPage = 0,
			TotalRow = 0
		};

		string sql = "select top {0} {1} from {2} where ({3} not in (select top ({0}*{4}) {3} from {2})) {6} order by {3} {5}";
		sql = string.Format(sql,
			pageSize,
			fields,
			table,
			idField,
			pageIndex-1,
			order,
			condition == null ? "" : "and " + condition);

		pr.Result = DBHelper.INST.ExecuteSqlDS(sql);

		//test sentence
		//throw new Exception(sql);

		return pr;
	}
}

//no implement
public class ProcessDataPager : DataPager
{
	public ProcessDataPager() { }

	public override PageResult PageData(int pageIndex, int pageSize, string table, string fields, string idField, string condition, string order)
	{
		return null;
	}
}

//page modal
public class PageResult{
	public int PageIndex{get;set;}

	public DataSet Result{get;set;}

	public int PageSize{get;set;}

	public int TotalPage{get;set;}

	public int TotalRow{get;set;}
}