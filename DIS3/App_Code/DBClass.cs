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




public class DBClass
{
	public DBClass()
	{
		
	}

    public OleDbConnection GetConnection()//connect sql
    {
        string myStr = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            //HttpContext.Current.Request.MapPath("~")+
      // ConfigurationManager.AppSettings["dbPath"].ToString();//connection string
        OleDbConnection myConn = new OleDbConnection(myStr);
        return myConn;
    }
    
    
    public void  ExecNonQuery(OleDbCommand myCmd)//执行，除select以外的sql语句，不返回任何行，只改变数据库，不需利用dataset
    {
        try
        {
            if (myCmd.Connection.State != ConnectionState.Open)
            {
                myCmd.Connection.Open(); 
            }
           
            myCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            if (myCmd.Connection.State == ConnectionState.Open)
            {
                myCmd.Connection.Close(); 
            } 
        }
    }
    public bool UpdateDB(OleDbCommand myCmd)//执行，除select以外的sql语句，不返回任何行，只改变数据库，不需利用dataset
    {
        try
        {
            if (myCmd.Connection.State != ConnectionState.Open)
            {
                myCmd.Connection.Open();
            }

            myCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
            return false;
        }
        finally
        {
            if (myCmd.Connection.State == ConnectionState.Open)
            {
                myCmd.Connection.Close();
                
            }
        }
        return true;
    }
    
   public string ExecScalar(OleDbCommand myCmd)//返回一个单值，一般用select操作
    {
        string strSql;
        try
        {
            if (myCmd.Connection.State != ConnectionState.Open)
            {
                myCmd.Connection.Open(); 
            }
            

            strSql=Convert.ToString(myCmd.ExecuteScalar());
            return strSql ;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            if (myCmd.Connection.State == ConnectionState.Open)
            {
                myCmd.Connection.Close();
            }
        }    
    }
    
    
    
    public DataTable GetDataSet(OleDbCommand myCmd, string TableName)
    {
        OleDbDataAdapter adapt;
        DataSet ds = new DataSet();
        try
        {
            if (myCmd.Connection.State != ConnectionState.Open)
            {
                myCmd.Connection.Open();
            }
            adapt = new OleDbDataAdapter(myCmd);
            adapt.Fill(ds,TableName);
            return ds.Tables[TableName];

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);

        }
        finally
        {
            if (myCmd.Connection.State == ConnectionState.Open)
            {
                myCmd.Connection.Close();

            }
        }

    }
    
   public OleDbCommand GetCommandProc(string strProcName)//存储过程，返回一个数据库命令对象
    {
        OleDbConnection myConn = GetConnection();
        OleDbCommand myCmd = new OleDbCommand();
        myCmd.Connection = myConn;
        myCmd.CommandText = strProcName;
        myCmd.CommandType = CommandType.StoredProcedure;
        return myCmd;
    }
    
   public OleDbCommand GetCommandStr(string strSql)
    {
        OleDbConnection myConn = GetConnection();
        OleDbCommand myCmd = new OleDbCommand();
        myCmd.Connection = myConn;
        myCmd.CommandText = strSql;
        myCmd.CommandType = CommandType.Text;
        return myCmd;
    }
  
    public DataTable GetDataSetStr(string sqlStr, string TableName)//返回名为TableName的datatable
    {
        OleDbConnection myConn = GetConnection();
        myConn.Open();
        DataSet ds = new DataSet();
        OleDbDataAdapter adapt = new OleDbDataAdapter(sqlStr, myConn);
        adapt.Fill(ds, TableName);
        myConn.Close();
        return ds.Tables[TableName];
    }

  



}
