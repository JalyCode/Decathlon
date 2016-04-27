//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2010 Socansoft.com ��Ȩ����
// ��������: SocanCode�����������Զ������� 2010-4-8 15:32:37
//
// ��������: 
//
// �޸ı�ʶ: 
// �޸�����: 
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
    /// ���ݷ����� EmployeeDAL
    /// </summary>
    public class EmployeeDAL
    {
        public EmployeeDAL()
        { }

        #region  ��Ա����

        /// <summary>
        /// �Ƿ���ڸü�¼
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
        /// �Ƿ���ڸü�¼
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
        /// �õ�һ������ʵ��
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
        /// ��һ�����ݵõ�һ��ʵ��
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
        /// �����ݼ��õ����������б�
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
