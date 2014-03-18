using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Role 的摘要说明
/// </summary>
public class Role
{
	public Role()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

	
	public int RoleID { get; set; }
	public string RoleName { get; set; }
	public string Description { get; set; }
	public DateTime CreateTime { get; set; }
	//访问权限
	public int RoleRight { get; set; }
}
