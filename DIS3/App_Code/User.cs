using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Collections;
using System.Data.SqlClient;
using System.Data.OleDb;


//增加字段LoginName
//修改Register函数
//修改User数据库字段，增加了[Email],[Activated]
/*********************
    *************
 * 移植自fortune  此Users类所关联使用的文件如下：
 * Role.cs  RightSystem.cs Authenic.cs
*/

/// <summary>
///Users 实现了IPrincipal接口，因此可以注入HttpContext.User对象
/// </summary>
public class Users : System.Security.Principal.IPrincipal
{
    //旧版本
    public Users()
    {
        roles = new int[3];
    }

    public string UserName { get; set; }
    public string LoginName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int UserType { get; set; }
    public int UserID { get; set; }
    public UsersRole Role { get; set; }

    protected static Type _Auth = null;
    public static IAuthenicable AuthInst
    {
        get
        {
            if (_Auth == null)
                _Auth = typeof(SessionAuthImpl);
            return (IAuthenicable)_Auth.GetConstructor(new Type[0]).Invoke(null);
        }
    }
    //end 旧版本

    //以下是新的版本

    //将一个Users实例写入Session
    public static void SetUserSession(Users u, HttpSessionState Session)
    {
        Session["UserState"] = u;
    }
    //从Session中读取一个Users实例
    public static Users GetUserSession(HttpSessionState Session)
    {
        Users u = (Users)Session["UserState"];
        return u;
    }
    //登录，查询数据库并返回user对象，包括用户信息，权限（组别权限、个人信息）信息
    public static Users Login(string loginname, string password)
    {
        Users u = new Users();
        string sql = String.Format("select * from [User] where [LoginName] = '{0}' and [Password] = '{1}'", loginname, password);
        var r = DBHelper.INST.ExecuteSqlDR(sql);

        if (r.Read())
        {
            u.Password = password;
            u.LoginName = loginname;
            u.UserType = Convert.ToInt32(r["UserType"]);
            u.UserID = Convert.ToInt32(r["UserID"]);
            //u.myright = int.MaxValue;

            r.Close();
            //sql = "select [RoleID] from [UserRole] where [UserID] = " + u.UserID;
            //r = DBHelper.INST.ExecuteSqlDR(sql);

            //ArrayList l = new ArrayList();
            //while (r.Read())
            //    l.Add(r.GetValue(0));
            //u.roles = new int[l.Count];
            //for (int i = 0; i < l.Count; i++)
            //    u.roles[i] = Convert.ToInt32(l[i]);
            return u;
        }

        if (!r.IsClosed)
            r.Close();

        return null;
    }
    public int getUserType(string username)
    {

        int userType = 1;
        string sql = "select [UserType] from [User] where [UserName] = '" + username + "'";
        var r = DBHelper.INST.ExecuteSqlDR(sql);
        if (r.Read())
        {
            userType = (int)r["UserType"];
            return userType;
        }
        return userType;
    }


    private int[] roles;
    private int myright;

    //注销，清除Session
    public static void Logout()
    {

    }

    //读取用户数据
    public static Users load(string name)
    {
        Users u = new Users();
        string sql = String.Format("select * from [User] where [LoginName] = '{0}'", name);
        var r = DBHelper.INST.ExecuteSqlDR(sql);

        if (r.Read())
        {
            u.UserName = Convert.ToString(r["UserName"]);
            u.UserID = Convert.ToInt32(r["UserID"]);
            u.Email = Convert.ToString(r["Email"]);
            u.UserType = Convert.ToInt32(r["UserType"]);
            u.Password = Convert.ToString(r["Password"]);
            r.Close();

            //sql = "select [RoleID] from [UserRole] where [UserID] = " + u.UserID;
            //r = DBHelper.INST.ExecuteSqlDR(sql);

            //ArrayList l = new ArrayList();
            //while (r.Read())
            //    l.Add(r.GetValue(0));
            //u.roles = new int[l.Count];
            //for (int i = 0; i < l.Count; i++)
            //    u.roles[i] = Convert.ToInt32(l[i]);

            return u;
        }

        if (!r.IsClosed)
            r.Close();

        return null;
    }

    //用户注册
    public static int Register(string password, string loginName, string email, int userType = 1)
    {
        Users u = new Users();
        string sql = String.Format(
            "insert into [Users]([LoginName],[Password],[CreateTime],[LastLoginTime],[Email],[UserType]) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
            loginName,
            password,
            Utils.DateTime2String(DateTime.Now),
            Utils.DateTime2String(DateTime.Now),
            email,
            userType
        );
        //用于判断是否有重复的用户名或邮箱
        string query = String.Format("select * from [User] where ( LoginName='{0}' or Email='{1}' )",
            loginName,
            email);
        var r = DBHelper.INST.ExecuteSqlDR(query);
        if (r.Read())
        {
            if (loginName == r["LoginName"].ToString() && email == r["Email"].ToString())
            {
                return 0;
            }
            if (loginName == r["LoginName"].ToString())
            {
                return -1;
            }
            if (email == r["Email"].ToString())
            {
                return -2;
            }
        }
        r.Close();
        var con = DBHelper.INST.GetConnection();
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = sql;
        if (cmd.ExecuteNonQuery() != 1)
            throw new Exception("不能操作数据库");

        cmd.CommandText = "SELECT @@IDENTITY";
        u.UserID = Convert.ToInt32(cmd.ExecuteScalar());
        //u.UserName = username;
        //u.Password = password;
        //u.myright = int.MaxValue;

        con.Close();

        return 1;
    }
    public static void UpdateLastLoginDate(string username)
    {
        string sql = String.Format("update [User] set [LastLoginTime]='{0}' where  [LoginName] ='{1}'",
        Utils.DateTime2String(DateTime.Now),
        username);
        var con = DBHelper.INST.GetConnection();
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = sql;
        if (cmd.ExecuteNonQuery() != 1)
            throw new Exception("不能操作数据库");
        con.Close();
    }
    public static int UpdateUser(string username, string password, string loginName, string email, int userType)
    {
        Users u = new Users();
        string sql = String.Format("update [User] set [Password]='{0}',[UserName]='{1}',[Email]='{2}',[UserType]='{3}' where  [LoginName] ='{4}'",
      password,
      username,
      email,
      userType,
      loginName
  );
        var con = DBHelper.INST.GetConnection();
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = sql;
        if (cmd.ExecuteNonQuery() != 1)
            throw new Exception("不能操作数据库");

        cmd.CommandText = "SELECT @@IDENTITY";
        u.UserID = Convert.ToInt32(cmd.ExecuteScalar());
        //u.UserName = username;
        //u.Password = password;
        //u.myright = int.MaxValue;

        con.Close();

        return 1;
    }
    //获得当前用户，如果未登录则返回null
    public static Users CurrentUser
    {
        get
        {
            HttpContext context = HttpContext.Current;
            object u = context.Session[SessionKey];
            if (u != null)
            {
                if (u is Users)
                    return (Users)u;
            }
            else if (context.User != null)
            {
                if (context.User is Users)
                    return (Users)context.User;
                Users user = load(context.User.Identity.Name);
                if (user != null)
                {
                    context.Session[SessionKey] = user;
                    return user;
                }
            }
            return null;

        }
    }

    //通过用户名和密码从数据库同步信息，如果把用户信息序列化到用户Cookies可能需要此方法支持，若用户信息保存在Session中则不需
    public void reloadSelf()
    {

    }

    //保存在Session中的键
    public const string SessionKey = "UsersSessionKey";

    //经过权限组合后用户最终的权限码
    //long与int的&操作有误，将此属性改为long，修改者GPQ
    public int UserRight
    {
        get
        {
            int right = myright;

            foreach (var r in Roles)
                right = r.RoleRight & right;

            return right;
        }
    }

    //用户所属的组别
    public Role[] Roles
    {
        get
        {
            Role r = null;
            List<Role> list = new List<Role>();
            for (int i = 0; i < roles.Length; i++)
            {
                r = RightSystem.findRole(roles[i]);
                if (r != null)
                    list.Add(r);
            }
            return list.ToArray();
        }
    }
    //添加用户到组
    public void addToRole(int rid)
    {

    }

    //修改用户个人信息，更新到数据库
    public bool modifyInformation(/*some params*/)
    {
        return false;
    }

    //把user加入到组中
    public static bool addToRoles(int uid, int[] roleids)
    {

        bool b = false;
        string str = "select * from UserRole where UserID=" + uid;
        var v = DBHelper.INST.ExecuteSqlDR(str);
        if (v.HasRows)
        {
            b = true;
        }
        if (b)
        {
            for (int i = 0; i < roleids.Length; i++)
            {
                str = String.Format(
                "insert into UserRole(UserID,RoleID) values ({0},{1})",
                uid,
                roleids[i]
                );
                DBHelper.INST.ExecuteSql(str);
            }

        }

        return b;
    }


    public static bool addToRoles(Users user, Role[] roles)
    {
        bool b = true;
        int size = roles.Length + user.roles.Length;
        int[] rs = new int[size];

        user.roles.CopyTo(rs, 0);

        foreach (var rid in user.roles)
        {
            foreach (var role in roles)
            {
                if (rid == role.RoleID)
                {
                    b = false;
                }

            }
        }

        if (b)
        {

            for (int i = 0; i < roles.Length; i++)
            {
                rs[user.roles.Length + i] = roles[i].RoleID;
                string str = String.Format(
                "insert into UserRole(UserID,RoleID) values ({0},{1})",
                user.UserID,
                roles[i].RoleID
                );
                DBHelper.INST.ExecuteSql(str);
            }

            user.roles = rs;

        }
        return b;

    }

    //从组中移除user
    public static bool removeFromRoles(int uid, int[] roleids)
    {

        for (int i = 0; i < roleids.Length; i++)
        {
            string str =
            String.Format(
                 "delete from UserRole where UserID={0} and RoleID={1}",
                 uid,
                 roleids[i]
                );

            DBHelper.INST.ExecuteSql(str);
        }


        return true;
    }
    public static bool removeFromRoles(Users user, Role[] roles)
    {
        List<int> arr = new List<int>(user.roles);

        int[] roleids = new int[roles.Length];
        for (int i = 0; i < roles.Length; i++)
        {
            roleids[i] = roles[i].RoleID;
            arr.RemoveAt(i);
        }
        user.roles = arr.ToArray();

        removeFromRoles(user.UserID, roleids);

        return false;
    }
    //改变用户状态
    public static void udUserkind(int kind, int uid)
    {
        string sql = "update [User] set UserType = " + kind + " where UserID = " + uid;
        DBHelper.INST.ExecuteSql(sql);

    }
    //删除User
    public static bool removeFromUser(int uid)
    {

        string str = "delete from [User] where UserID=" + uid;
        DBHelper.INST.ExecuteSql(str);
        return true;
    }

    //测试更改密码的用户跟邮箱是否一致
    public static bool isMatch(string loginName, string email)
    {
        string query = String.Format("select * from [Users] where ( UserName='{0}' and Email='{1}' )",
            loginName,
            email);
        DBClass dbObj = new DBClass();
        string exists = dbObj.ExecScalar(dbObj.GetCommandStr(query));
        if (exists != null)
        {
            return true;
        }
        else return false;
    }
    //更改密码，修改到数据库
    public static bool changePassword(string email, string pass)
    {
        string str = "update [User] set [Password] = '" + pass + "'" + " where Email= '" + email + "'";

        DBHelper.INST.ExecuteSql(str);
        return true;
    }
    //更改用户本身权限
    public static bool grantRight(Users user, int right)
    {
        string str = String.Format(
            "update [User] set [Right]={0} where [ID]={1}",
            right,
            user.UserID
            );
        DBHelper.INST.ExecuteSql(str);

        return false;
    }
    public static bool revokeRight(Users user, int right)
    {
        string str = String.Format(
            "update [User] set [Right]={0} where [ID]={1}",
            right,
            user.UserID
            );
        DBHelper.INST.ExecuteSql(str);

        return false;
    }

    #region IPrincipal 成员

    public System.Security.Principal.IIdentity Identity
    {
        get
        {
            return null;
            // throw new NotImplementedException();
        }
    }

    public bool IsInRole(string role)
    {
        return false;
        //throw new NotImplementedException();
    }

    #endregion
}

public enum UsersRole
{
    Admin,
    NonActivated,
    Activated,
    Locked
}
