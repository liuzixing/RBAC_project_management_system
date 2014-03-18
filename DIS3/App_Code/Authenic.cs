using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Data.Common;

/// <summary>
///IAuthenicable 的摘要说明
/// </summary>
public abstract class IAuthenicable
{
	public const string IDKey = "UID";
	public const string NameKey = "USERNAME";
	public const string UserKey = "CURRENTUSER";

	public abstract bool IsAuthenic(HttpSessionState session);

	public abstract Users CurrentUser(HttpSessionState session);

	public abstract bool Login(string username, string password, HttpSessionState session);

	public abstract void Logout(HttpSessionState session);
}

//public class SQLAuthImpl : IAuthenicable
//{
//    #region IAuthenicable 成员

//    public bool IsAuthenic(HttpSessionState session)
//    {
//        throw new NotImplementedException();
//    }

//    public Users CurrentUser(HttpSessionState session)
//    {
//        throw new NotImplementedException();
//    }

//    public bool Login(string username, string password, HttpSessionState session)
//    {
//        throw new NotImplementedException();
//    }

//    public void Logout(HttpSessionState session)
//    {
//        throw new NotImplementedException();
//    }

//    #endregion
//}


public class SessionAuthImpl : IAuthenicable
{
	#region IAuthenicable 成员

	public override bool IsAuthenic(HttpSessionState session)
	{
		return session[IAuthenicable.NameKey] != null;
	}

	public override Users CurrentUser(HttpSessionState session)
	{
		return null;
	}

	public override bool Login(string username, string password, HttpSessionState session)
	{
		string sql = "select * from [User] where UserName = '" + username + 
			"' and [Password] = '" + Utils.Crypto(password) + "'";
		DbDataReader r = DBHelper.INST.ExecuteSqlDR(sql);

		if (!r.Read())
			return false;

		session[IAuthenicable.NameKey] = username;
		session[IAuthenicable.IDKey] = r.GetInt32(0);

		return true;
	}

	public override void Logout(HttpSessionState session)
	{
		session.Clear();
	}

	#endregion
}

