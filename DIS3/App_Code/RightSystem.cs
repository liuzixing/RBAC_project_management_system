using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///RightSystem管理权限系统的配置信息，预读数据库的信息到内存中以减少数据库访问。
///
/// </summary>
public class RightSystem
{

	public RightSystem()
	{

	}

	//获得所有已声明的Role
	public static Role[] DeclaredRoles
    { 
        get  { return _role.ToArray();} 
    }


    private static List<Role> _role;

   

	//通过RoleID查询Role
	public static Role findRole(int rid)
	{
        
        foreach (var v in _role)
        {
            if (v.RoleID == rid)
            {
                return v;
            }
        }

        return null; 

	}

	

	//新增一个Role，写入数据库并更新内存缓存
	public static Role addRole(string rNam, string desc, int rg)
	{ 
        Role r = new Role();

        string sql = String.Format(
            "insert into [Role]([RoleName],[CreateTime],[Description],[Right]) values('{0}','{1}','{2}',{3})",
            rNam,
            Utils.DateTime2String(DateTime.Now),
            desc,
            rg
        );

        //throw new Exception(sql);
        
        var con = DBHelper.INST.GetConnection();
		con.Open();
		var cmd = con.CreateCommand();
		cmd.CommandText = sql;
		if (cmd.ExecuteNonQuery() != 1)
			throw new Exception("不能操作数据库");

		cmd.CommandText = "SELECT @@IDENTITY";
		r.RoleID = Convert.ToInt32(cmd.ExecuteScalar());
        r.RoleName = rNam;
        r.Description = desc;
        r.RoleRight = rg;
        r.CreateTime = DateTime.Now;

        con.Close();

        _role.Add(r);

        return r;

	}

	

	//删除一个Role，写入数据库并更新内存缓存
	public static Role deleteRole(int rid)
	{
        Role r = new Role();
        string sql = "delete * from Role where ID=" + rid;
        DBHelper.INST.ExecuteSql(sql);

        r = findRole(rid);
        _role.Remove(r);

        return r;

	}

	//同步数据库信息
	public static void reload()
	{
        _role = new List<Role>();
        string sql = "select * from Role";
        var v = DBHelper.INST.ExecuteSqlDR(sql);

        while (v.Read())
        {
            Role r = new Role();
            r.RoleID = Convert.ToInt32(v["RoleID"]);
            r.RoleName = Convert.ToString(v["RoleName"]);
            r.CreateTime = Convert.ToDateTime(v["CreateTime"]);
            r.Description = Convert.ToString(v["Description"]);
            r.RoleRight = Convert.ToInt32(v["Right"]);
            _role.Add(r);
        }

        v.Close();
		
	}

	//预读数据库
	public static void init()
	{
		reload();
	}
}
