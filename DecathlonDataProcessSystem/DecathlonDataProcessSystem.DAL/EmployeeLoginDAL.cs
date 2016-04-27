//------------------------------------------------------------------------------
// 创建标识: 
// 创建描述: 
//
// 功能描述: 
//
// 修改标识: 
// 修改描述: 
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DecathlonDataProcessSystem.DBUtility;
using System.Collections;
using System.Collections.Generic;
using DecathlonDataProcessSystem.Model;

namespace DecathlonDataProcessSystem.DAL
{
    /// <summary>
    /// 数据访问类 EmployeeLoginDAL
    /// </summary>
    public class EmployeeLoginDAL
    {
        public EmployeeLoginDAL()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddEmployeeLogin(EmployeeLoginEntity model)
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "insert into T_EmployeeLogin(" );
            strSql.Append( "EmployeeId,IpAddress,MacAddress,LoginTime,Flag)" );
            strSql.Append( " values (" );
            strSql.Append( "@EmployeeId,@IpAddress,@MacAddress,@LoginTime,@Flag)" );
            strSql.Append( ";select @@IDENTITY" );
            SqlParameter[] parameters = {
					new SqlParameter("@EmployeeId", SqlDbType.VarChar,50),
					new SqlParameter("@IpAddress", SqlDbType.VarChar,50),
					new SqlParameter("@MacAddress", SqlDbType.VarChar,50),
					new SqlParameter("@LoginTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Flag", SqlDbType.Bit,1)};
            parameters[0].Value = model.EmployeeId;
            parameters[1].Value = model.IpAddress;
            parameters[2].Value = model.MacAddress;
            parameters[3].Value = model.LoginTime;
            parameters[4].Value = model.Flag;

            object obj = SqlHelper.GetSingle( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
            if ( obj == null )
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32( obj );
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateEmployeeLogin(EmployeeLoginEntity model)
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "update T_EmployeeLogin set " );
            strSql.Append( "Flag=@Flag," );
            strSql.Append( "CurrentTime=@CurrentTime" );
            strSql.Append( " where LoginId=@LoginId" );
            SqlParameter[] parameters = {
					new SqlParameter("@Flag", SqlDbType.Bit,1),
					new SqlParameter("@CurrentTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LoginId", SqlDbType.Int,4)};

            parameters[0].Value = model.Flag;
            parameters[1].Value = model.CurrentTime;
            parameters[2].Value = model.LoginId;

            int rows=SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
            if ( rows > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EmployeeLogout(EmployeeLoginEntity model)
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "update T_EmployeeLogin set " );
            strSql.Append( "LogoutTime=@LogoutTime" );
            strSql.Append( " where LoginId=@LoginId" );
            SqlParameter[] parameters = {
					new SqlParameter("@LogoutTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LoginId", SqlDbType.Int,4)};
            parameters[0].Value = model.LogoutTime;
            parameters[1].Value = model.LoginId;

            int rows=SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
            if ( rows > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteEmployeeLogin(int LoginId)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@LoginId", SqlDbType.Int)};
            parameters[0].Value = LoginId;
            SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "DeleteEmployeeLogin_SP", parameters, out rowsAffected);
            return rowsAffected > 0;
        }
        /// <summary>
        /// 将未登陆的无效信息删除
        /// </summary>
        public bool DeleteEmployeeLogin(string EmployeeId, string IpAddress)
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_EmployeeLogin " );
            strSql.Append( " where EmployeeId=@EmployeeId and IpAddress=@IpAddress;" );
            SqlParameter[] parameters = {
                 new SqlParameter("@EmployeeId", SqlDbType.NChar,20),
                 new SqlParameter("@IpAddress", SqlDbType.NVarChar,100),  
            };
            parameters[0].Value = EmployeeId;
            parameters[1].Value = IpAddress;

            int rows=SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString() , parameters );
            if ( rows > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool EmployeeLoginExists(int LoginId)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@LoginId", SqlDbType.Int)};
            parameters[0].Value = LoginId;
            return SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "EmployeeLoginExists_SP", parameters, out rowsAffected) == 1;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EmployeeLoginEntity GetEmployeeLoginByLoginId(int LoginId)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@LoginId", SqlDbType.Int)};
            parameters[0].Value = LoginId;
            DataSet ds = SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "GetEmployeeLoginByLoginId_SP", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow r = ds.Tables[0].Rows[0];
                return GetModel(r);
            }
            else
            {
                return null;
            }
        }

        public DataSet GetDsEmployeeInfo( string EmployeeId )
        {
            string strSql = "select * from T_Employee where EmployeeId=@EmployeeId";
            SqlParameter[] parameters ={
                    new SqlParameter("@EmployeeId",SqlDbType.NVarChar,20)};
            parameters[0].Value = EmployeeId;
            return SqlHelper.Query(SqlHelper.LocalSqlServer, strSql, parameters);
        }

        public DataSet GetEmployeeLoginByEmployeeId(string EmployeeId)
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select LoginId,EmployeeId,IpAddress,MacAddress,LoginTime,Flag,CurrentTime,LogoutTime " );
            strSql.Append( " FROM T_EmployeeLogin where EmployeeId=@EmployeeId;" );
            SqlParameter[] parameters = {
                    new SqlParameter("@EmployeeId", SqlDbType.VarChar,20)};
            parameters[0].Value = EmployeeId;
            DataSet ds = SqlHelper.Query( SqlHelper.LocalSqlServer ,strSql.ToString() , parameters);
            return ds;
        }

        /// <summary>
        /// 获取泛型数据列表
        /// </summary>
        public DataSet GetEmployeeLoginList()
        {
            SqlParameter[] parameters ={ };
            DataSet ds = SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "GetEmployeeLoginList_SP", parameters, "ds");
            return ds;
        }

        /// <summary>
        /// 分页获取泛型数据列表
        /// </summary>
        public DataSet GetEmployeeLoginPageList(int pageSize, int pageIndex, string fldSort, bool sort, string strCondition, out int pageCount, out int count)
        {
            string strSql;
            DataSet ds = SqlHelper.PageList(SqlHelper.LocalSqlServer, "[T_EmployeeLogin]", null, pageSize, pageIndex, fldSort, sort, strCondition, "LoginId", false, out pageCount, out count, out strSql);
            return ds;
        }
        #endregion

        /// <summary>
        /// 由一行数据得到一个实体
        /// </summary>
        private EmployeeLoginEntity GetModel(DataRow r)
        {
            EmployeeLoginEntity model = new EmployeeLoginEntity();
            model.LoginId = SqlHelper.GetInt(r["LoginId"]);
            model.EmployeeId = SqlHelper.GetString(r["EmployeeId"]);
            model.IpAddress = SqlHelper.GetString(r["IpAddress"]);
            model.MacAddress = SqlHelper.GetString(r["MacAddress"]);
            model.LoginTime = SqlHelper.GetDateTime(r["LoginTime"]);
            model.Flag = SqlHelper.GetBool(r["Flag"]);
            model.CurrentTime = SqlHelper.GetDateTime(r["CurrentTime"]);
            model.LogoutTime = SqlHelper.GetDateTime(r["LogoutTime"]);
            return model;
        }

        /// <summary>
        /// 由数据集得到泛型数据列表
        /// </summary>
        private IList<EmployeeLoginEntity> GetList(DataSet ds)
        {
            List<EmployeeLoginEntity> l = new List<EmployeeLoginEntity>();
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                l.Add(GetModel(r));
            }
            return l;
        }
    }
}

