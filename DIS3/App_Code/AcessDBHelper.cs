using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OleDb;
using System.Text;
using System.Data.Common;
/// <summary>
///MyBase 的摘要说明
/// </summary>
/// 
 
    public class AccessDBHelper:DBHelper 
    {
        #region "Fields of base class"
        protected static string cntString = ConfigurationManager.ConnectionStrings["DISCONNECTION"].ConnectionString;
        //定义用于连接数据库的字符串，它是从配置文件的strConnettion而来
        #endregion

        public override  DbConnection GetConnection()
        {
          DbConnection  oleconn=new OleDbConnection(cntString);
            return  oleconn;
        }
        public override  void   CloseConnection(DbConnection cnt)
        {
            cnt.Close();
        }
        

        //ExecuteSql 代表了一类没有返回值的数据库语句。如插入，删除等，只须调用这个函数
        public override void ExecuteSql(string cmdText)
        {
          //  OleDbConnection cnt = new OleDbConnection(cntString);     
            OleDbConnection cnt = (OleDbConnection)this.GetConnection();
            OleDbCommand myCommand = new OleDbCommand(cmdText, cnt);
            try
            {
                cnt.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                cnt.Close();
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                cnt.Close();
            }
        }
         
        //ExecuteSqlDR()封装了返回值为OleDbDataReader类型的操作，  如果以后有需要执行返回值OleDbDataReader的操作时，只需要调用这个函数即可。
        public   OleDbDataReader ExecuteSqlDR(string cmdText)
        {
           // OleDbConnection cnt = new OleDbConnection(cntString);
            OleDbConnection cnt = (OleDbConnection)this.GetConnection();
            OleDbCommand myCommand = new OleDbCommand(cmdText, cnt);
            OleDbDataReader da = null;
            try
            {
                cnt.Open();
                da = myCommand.ExecuteReader(CommandBehavior.CloseConnection);        
            }
            catch (OleDbException ex)
            {
                cnt.Open();
                throw new Exception(ex.Message, ex);
            }
             finally
           {
            cnt.Close();
           }
            da.Dispose();
            return da;
            
        }
         

        //ExecuteSqlDS()封装了返回值为DataSet类型的操作，这样如果以后有需要执行返回值DataSet的操作时，只需要调用这个函数即可。
        public override   DataSet ExecuteSqlDS(string cmdText)
        {
            //OleDbConnection cnt = new OleDbConnection(cntString);
            OleDbConnection cnt = (OleDbConnection)this.GetConnection();
            try
            {
                cnt.Open();
                OleDbDataAdapter sda = new OleDbDataAdapter(cmdText, cnt);
                DataSet ds = new DataSet("ds");
                sda.Fill(ds, "aa");//调用Fill方法，为DataSet填充数据
                sda.Dispose();
                ds.Dispose();
                return ds;//返回得到的DataSet对象，它保存了从数据库查询到的数据
                
            }
            catch (OleDbException ex)
            {
                cnt.Close();
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                cnt.Close();
            }
        }

    }
 
