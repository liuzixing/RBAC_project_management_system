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


public class CommonClass
{
	public CommonClass()
	{
		
	}
    public string MessageBox(string TxtMessage, string Url)//返回一个对话框在新窗口
    {
        string str;
        str = "<script language=javascript>alert('" + TxtMessage + "');location='" + Url + "';</script>";
        return str;
    }


    public string MessageBox(string TxtMessage)//返回对话框在当前窗口
    {
        string str;
        str = "<script language=javascript>alert('" + TxtMessage + "')</script>";
        return str;
    }
 
    
   
    public string MessageBoxPage(string TxtMessage)//显示对话框在前一个窗口
    {
        string str;
        str = "<script language=javascript>alert('" + TxtMessage + "');location='javascript:history.go(-1)';</script>";
        return str;
    }
 
    
    public string RandomNum(int n) //return n characters  that that every neighbor is not same
    {
        
        string strchar = "0,1,2,3,4,5,6,7,8,9";
       
        string[] VcArray = strchar.Split(',');
        string VNum = "";
                  
        int temp = -1;                       
        
        Random rand = new Random();
        for (int i = 1; i < n + 1; i++)
        {
           
            
            int t = rand.Next(10);//return a random number which less than 10
            if (temp != -1 && temp == t)
            { 
                return RandomNum(n);
            }
            temp = t;
            VNum += VcArray[t];
        }
        return VNum;
    }

    public string VarStr(string sString, int nLeng)//返回从0到"."之后长度为nLeng的字符串
    {
        int index = sString.IndexOf(".");
        if (index == -1 || index + nLeng >= sString.Length)
            return sString;
        else
            return sString.Substring(0, (index + nLeng + 1));
    }
  

}
