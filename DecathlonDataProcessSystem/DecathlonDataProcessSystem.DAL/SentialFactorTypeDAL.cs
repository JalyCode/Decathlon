using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DecathlonDataProcessSystem.DBUtility;
using DecathlonDataProcessSystem.Model;

namespace DecathlonDataProcessSystem.DAL
{
    /// <summary>
    /// 数据访问类:T_SentialFactorType
    /// </summary>
    public class SentialFactorTypeDAL
    {
        public SentialFactorTypeDAL( )
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists( int SentialFactorTypeID )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) from T_SentialFactorType" );
            strSql.Append( " where SentialFactorTypeID=@SentialFactorTypeID" );
            SqlParameter[] parameters = {
					new SqlParameter("@SentialFactorTypeID", SqlDbType.Int,4)
			};
            parameters[0].Value = SentialFactorTypeID;

            return SqlHelper.Exists(SqlHelper.LocalSqlServer, strSql.ToString( ) , parameters );
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add( SentialFactorTypeEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "insert into T_SentialFactorType(" );
            strSql.Append( "SentialFactorTypeName,SentialFactorTypeDescription)" );
            strSql.Append( " values (" );
            strSql.Append( "@SentialFactorTypeName,@SentialFactorTypeDescription)" );
            strSql.Append( ";select @@IDENTITY" );
            SqlParameter[] parameters = {
					new SqlParameter("@SentialFactorTypeName", SqlDbType.VarChar,50),
					new SqlParameter("@SentialFactorTypeDescription", SqlDbType.VarChar,250)};
            parameters[0].Value = model.SentialFactorTypeName;
            parameters[1].Value = model.SentialFactorTypeDescription;

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
        public bool Update( SentialFactorTypeEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "update T_SentialFactorType set " );
            strSql.Append( "SentialFactorTypeName=@SentialFactorTypeName," );
            strSql.Append( "SentialFactorTypeDescription=@SentialFactorTypeDescription" );
            strSql.Append( " where SentialFactorTypeID=@SentialFactorTypeID" );
            SqlParameter[] parameters = {
					new SqlParameter("@SentialFactorTypeName", SqlDbType.VarChar,50),
					new SqlParameter("@SentialFactorTypeDescription", SqlDbType.VarChar,250),
					new SqlParameter("@SentialFactorTypeID", SqlDbType.Int,4)};
            parameters[0].Value = model.SentialFactorTypeName;
            parameters[1].Value = model.SentialFactorTypeDescription;
            parameters[2].Value = model.SentialFactorTypeID;

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
        public bool Delete( int SentialFactorTypeID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_SentialFactorType " );
            strSql.Append( " where SentialFactorTypeID=@SentialFactorTypeID" );
            SqlParameter[] parameters = {
					new SqlParameter("@SentialFactorTypeID", SqlDbType.Int,4)
			};
            parameters[0].Value = SentialFactorTypeID;

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
        /// 得到一个对象实体
        /// </summary>
        public SentialFactorTypeEntity GetModel( int SentialFactorTypeID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select  top 1 SentialFactorTypeID,SentialFactorTypeName,SentialFactorTypeDescription from T_SentialFactorType " );
            strSql.Append( " where SentialFactorTypeID=@SentialFactorTypeID" );
            SqlParameter[] parameters = {
					new SqlParameter("@SentialFactorTypeID", SqlDbType.Int,4)
			};
            parameters[0].Value = SentialFactorTypeID;

            SentialFactorTypeEntity model=new SentialFactorTypeEntity( );
            DataSet ds=SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
            if ( ds.Tables[0].Rows.Count>0 )
            {
                if ( ds.Tables[0].Rows[0]["SentialFactorTypeID"]!=null && ds.Tables[0].Rows[0]["SentialFactorTypeID"].ToString( )!="" )
                {
                    model.SentialFactorTypeID=int.Parse( ds.Tables[0].Rows[0]["SentialFactorTypeID"].ToString( ) );
                }
                if ( ds.Tables[0].Rows[0]["SentialFactorTypeName"]!=null && ds.Tables[0].Rows[0]["SentialFactorTypeName"].ToString( )!="" )
                {
                    model.SentialFactorTypeName=ds.Tables[0].Rows[0]["SentialFactorTypeName"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["SentialFactorTypeDescription"]!=null && ds.Tables[0].Rows[0]["SentialFactorTypeDescription"].ToString( )!="" )
                {
                    model.SentialFactorTypeDescription=ds.Tables[0].Rows[0]["SentialFactorTypeDescription"].ToString( );
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList( string strWhere )
        //{
        //    StringBuilder strSql=new StringBuilder( );
        //    strSql.Append( "select SentialFactorTypeID,SentialFactorTypeName,SentialFactorTypeDescription " );
        //    strSql.Append( " FROM T_SentialFactorType " );
        //    if ( strWhere.Trim( )!="" )
        //    {
        //        strSql.Append( " where "+strWhere );
        //    }
        //    return DbHelperSQL.Query( strSql.ToString( ) );
        //}
        public DataSet GetList( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select Distinct SentialFactorTypeName " );
            strSql.Append( " FROM T_SentialFactorType " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query(SqlHelper.LocalSqlServer, strSql.ToString( ));
        }

        #endregion  Method
    }
}
