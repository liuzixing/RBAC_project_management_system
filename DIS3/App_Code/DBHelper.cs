using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
///DBHelper
///Author : DQ
/// </summary>
public abstract class DBHelper
{
     public static  DBHelper INST 
    { 
         get{return new AccessDBHelper();}
     }

	public DBHelper()
	{
	}

	/// <summary>
	/// 获得数据库连接。可能有些操作需要直接使用此底层方法。连接使用完毕后应使用CloseConnection关闭
	/// </summary>
	/// <returns></returns>
	public abstract DbConnection GetConnection();
	/// <summary>
	/// 关闭GetConnection的连接
	/// </summary>
	/// <param name="cnt"></param>
	public abstract void CloseConnection(DbConnection cnt);
	/// <summary>
	/// 使用adpater把查询结果填充到DataSet
	/// </summary>
	/// <param name="cmdText">sql语句</param>
	/// <returns></returns>
	public abstract DataSet ExecuteSqlDS(string cmdText);

	/// <summary>
	/// 仅作某些sql操作而不关心查询结果
	/// </summary>
	/// <param name="cmdText">sql语句</param>
	public virtual void ExecuteSql(string cmdText)
	{
		DbConnection cnt = this.GetConnection();
		DbCommand cmd = cnt.CreateCommand();
		cmd.CommandText = cmdText;
		cmd.ExecuteNonQuery();
		this.CloseConnection(cnt);
	}

	/// <summary>
	/// 查询数据库获得结果集
	/// </summary>
	/// <param name="cmdText">sql语句</param>
	/// <returns></returns>
	public virtual DbDataReader ExecuteSqlDR(string cmdText)
	{
		DbConnection cnt = this.GetConnection();
		cnt.Open();
		DbCommand cmd = cnt.CreateCommand();
		cmd.CommandText = cmdText;
		DbDataReader reader = cmd.ExecuteReader();
		//this.CloseConnection(cnt);
		return reader;
	}

	/// <summary>
	/// 使用优化的代码批量执行数据库操作，通常是插入、更新。不带参数设置list为null
	/// </summary>
	/// <param name="cmdText">sql语句，形如insert into table value(@id,@title)</param>
	/// <param name="list">hash参数数组，每个元素有多个key-value对。key形如@id,@title</param>
	public virtual void ExecuteBatch(string cmdText, Hashtable[] list)
	{
		DbConnection cnt = this.GetConnection();
		DbCommand cmd = cnt.CreateCommand();
		cmd.CommandText = cmdText;
		cmd.Prepare();

		DbParameter p;

		for (int i=0;i<list.Length;i++)
		{
			Hashtable h = list[i];
			if (h != null)
			{
				foreach (var key in h.Keys)
				{
					p = cmd.CreateParameter();
					p.ParameterName = key.ToString();
					p.Value = h[key];
					cmd.Parameters.Add(p);
				}
			}
			cmd.ExecuteNonQuery();
		}

		this.CloseConnection(cnt);
	}

	/// <summary>
	/// 执行存储过程。不带参数设置list为null
	/// </summary>
	/// <param name="cmdText">sql语句，形如insert into table value(@id,@title)</param>
	/// <param name="list">sql语句的hash参数数组，每个元素有多个key-value对。key形如@id,@title</param>
	public virtual void ExecuteProcedure(string proc, Hashtable[] list)
	{
		DbConnection cnt = this.GetConnection();
		DbCommand cmd = cnt.CreateCommand();
		cmd.CommandText = proc;
		cmd.CommandType = CommandType.StoredProcedure;

		DbParameter p;

		foreach (var h in list)
		{
			foreach (var key in h.Keys)
			{
				p = cmd.CreateParameter();
				p.ParameterName = key.ToString();
				p.Value = h[key];
				cmd.Parameters.Add(p);
			}
			cmd.ExecuteNonQuery();
		}

		this.CloseConnection(cnt);
	}


   

}
