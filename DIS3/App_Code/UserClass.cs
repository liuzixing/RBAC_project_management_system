using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OleDb;




public class UserClass
{
    DBClass dbObj = new DBClass();
	public UserClass()
	{
		
	}
    //***************************************登录界面************************************************************
    public DataTable UserLogin(string strName,string strPwd)
    {
        string str = "select * from Users where UserName = '" + strName + "' and [Password] = '" + strPwd +"'";
        OleDbCommand myCmd = dbObj.GetCommandStr(str);
        string exists = dbObj.ExecScalar(dbObj.GetCommandStr(str));
       
        DataTable dsTable = dbObj.GetDataSet(myCmd, "Users");
        return dsTable;
        
      
       
    }
    //***************************************注册界面************************************************************


    public int AddUsers(string UserName, string Password, string Email, int Confirm)
    {
        string str = "select * from Users where UserName = '" + UserName + "'";
        string exists = dbObj.ExecScalar(dbObj.GetCommandStr(str));

        if (exists == "")
        {
            string add = "insert into Users(UserName,[Password],Email, Confirm, CreateTime) values ('" + UserName + "','" +
                Password + "','" + Email  + "'," + Confirm + ",Date())";
            dbObj.ExecNonQuery(dbObj.GetCommandStr(add));
            return 100;
        }
        else
        {
            return -100;
        }
    }
  
    //***************************************修改界面************************************************************
  
    
    public DataTable GetUserInfo(int IntMemberID)
    {
        string str = "select * from Users where ID = " + IntMemberID;
        OleDbCommand myCmd = dbObj.GetCommandStr(str);
        string exists = dbObj.ExecScalar(myCmd);
        DataTable dsTable = dbObj.GetDataSet(myCmd, "Users");
        return dsTable;
    }


    public DataTable GetUserInfo(string name)
    {
        string str = "select * from Users where UserName = '" + name + "'";
        OleDbCommand myCmd = dbObj.GetCommandStr(str);
        string exists = dbObj.ExecScalar(myCmd);
        DataTable dsTable = dbObj.GetDataSet(myCmd, "Users");
        return dsTable;
    }

    public void ModifyUser(int id,string strName, string password, string email,DateTime date)
    {
        string str = "delete from Users where ID = " + id;
        string str1 = "insert into Users values(" + id + ",'" + strName + "','" + password + "','" + email + "',' " + date + "')";

        dbObj.ExecNonQuery(dbObj.GetCommandStr(str));
        dbObj.ExecNonQuery(dbObj.GetCommandStr(str1));
        
        
    }

    public bool updatePassword(string strName, string strPwd)
    {
        string str = "UPDATE Users SET [Password] = '" + strPwd + "' WHERE UserName = '" + strName + "';";
        return dbObj.UpdateDB(dbObj.GetCommandStr(str));

    }

}
