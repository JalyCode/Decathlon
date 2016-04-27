//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 Socansoft.com 版权所有
// 创建描述: SocanCode代码生成器自动创建于 2010-4-8 15:32:37
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
using System.Collections;
using System.Collections.Generic;
using DecathlonDataProcessSystem.DBUtility;
using DecathlonDataProcessSystem.Model;

namespace DecathlonDataProcessSystem.DAL
{
    /// <summary>
    /// 数据访问类 EmployeeDAL
    /// </summary>
    public class EmployeeDAL
    {
        public EmployeeDAL()
        { }

        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool EmployeeExists(string EmployeeId)
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) from T_Employee" );
            strSql.Append( " where EmployeeId=@EmployeeId " );
            SqlParameter[] parameters = {
					new SqlParameter("@EmployeeId", SqlDbType.VarChar,20)			};
            parameters[0].Value = EmployeeId;
            return SqlHelper.Exists( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ValidateEmployee(string EmployeeId, string password)
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) from T_Employee" );
            strSql.Append( " where EmployeeId=@EmployeeId and Password=@Password; " );
            SqlParameter[] parameters = {
                    new SqlParameter("@EmployeeId", SqlDbType.VarChar,20),new SqlParameter ("@Password",SqlDbType.VarChar,50)};
            parameters[0].Value = EmployeeId;
            parameters[1].Value = password;
            return SqlHelper.Exists( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EmployeeEntity GetEmployeeByEmployeeId(string EmployeeId)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@EmployeeId", SqlDbType.NVarChar,20)};
            parameters[0].Value = EmployeeId;
            DataSet ds = SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "GetEmployeeByEmployeeId_SP", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow r = ds.Tables[0].Rows[0];
                return GetEmployee(r);
            }
            else
            {
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 由一行数据得到一个实体
        /// </summary>
        private EmployeeEntity GetEmployee(DataRow r)
        {
            EmployeeEntity model = new EmployeeEntity();
            model.EmployeeId = SqlHelper.GetString(r["EmployeeId"]);
            model.EmployeeName = SqlHelper.GetString(r["EmployeeName"]);
            model.Password = SqlHelper.GetString(r["Password"]);
            return model;
        }

        /// <summary>
        /// 由数据集得到泛型数据列表
        /// </summary>
        private IList<EmployeeEntity> GetEmployeeList(DataSet ds)
        {
            List<EmployeeEntity> list = new List<EmployeeEntity>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(GetEmployee(row));
            }
            return list;
        }
    }
}
