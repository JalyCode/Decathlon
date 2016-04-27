using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecathlonDataProcessSystem.DBUtility;

namespace DecathlonDataProcessSystem.DAL
{
    public class CommonDAL
    {
        /// <summary>
        /// 返回指定日期格式
        /// </summary>
        /// <param name="strFormat"></param>
        /// <returns></returns>
        public static string GetDate( string strFormat )
        {
            string strSql = "SELECT getdate()";
            return ( Convert.ToDateTime( SqlHelper.GetSingle( SqlHelper.LocalSqlServer , strSql ) ) ).ToString( strFormat );
        }
        public DateTime GetDateTime( )
        {
            string strSql = "SELECT getdate()";
            return ( Convert.ToDateTime( SqlHelper.GetSingle( SqlHelper.LocalSqlServer , strSql ) ) );
        }
    }
}
