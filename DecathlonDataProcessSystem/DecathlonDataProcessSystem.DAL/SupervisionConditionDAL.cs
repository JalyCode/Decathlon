using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DecathlonDataProcessSystem.DBUtility;
using System.Data;
using DecathlonDataProcessSystem.Model;

namespace DecathlonDataProcessSystem.DAL
{
    /// <summary>
    /// 数据访问类:T_SupervisionCondition
    /// </summary>
    public class SupervisionConditionDAL
    {
        public SupervisionConditionDAL( )
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists( int SupervisionConditionID )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) from T_SupervisionCondition" );
            strSql.Append( " where SupervisionConditionID=@SupervisionConditionID" );
            SqlParameter[] parameters = {
					new SqlParameter("@SupervisionConditionID", SqlDbType.Int,4)
			};
            parameters[0].Value = SupervisionConditionID;

            return SqlHelper.Exists(SqlHelper.LocalSqlServer, strSql.ToString( ) , parameters );
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add( SupervisionConditionEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "insert into T_SupervisionCondition(" );
            strSql.Append( "SupervisionConditionName,SupervisionConditionRemark)" );
            strSql.Append( " values (" );
            strSql.Append( "@SupervisionConditionName,@SupervisionConditionRemark)" );
            strSql.Append( ";select @@IDENTITY" );
            SqlParameter[] parameters = {
					new SqlParameter("@SupervisionConditionName", SqlDbType.VarChar,100),
					new SqlParameter("@SupervisionConditionRemark", SqlDbType.VarChar,250)};
            parameters[0].Value = model.SupervisionConditionName;
            parameters[1].Value = model.SupervisionConditionRemark;

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
        public bool Update( SupervisionConditionEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "update T_SupervisionCondition set " );
            strSql.Append( "SupervisionConditionName=@SupervisionConditionName," );
            strSql.Append( "SupervisionConditionRemark=@SupervisionConditionRemark" );
            strSql.Append( " where SupervisionConditionID=@SupervisionConditionID" );
            SqlParameter[] parameters = {
					new SqlParameter("@SupervisionConditionName", SqlDbType.VarChar,100),
					new SqlParameter("@SupervisionConditionRemark", SqlDbType.VarChar,250),
					new SqlParameter("@SupervisionConditionID", SqlDbType.Int,4)};
            parameters[0].Value = model.SupervisionConditionName;
            parameters[1].Value = model.SupervisionConditionRemark;
            parameters[2].Value = model.SupervisionConditionID;

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
        public bool Delete( int SupervisionConditionID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_SupervisionCondition " );
            strSql.Append( " where SupervisionConditionID=@SupervisionConditionID" );
            SqlParameter[] parameters = {
					new SqlParameter("@SupervisionConditionID", SqlDbType.Int,4)
			};
            parameters[0].Value = SupervisionConditionID;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList( string SupervisionConditionIDlist )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_SupervisionCondition " );
            strSql.Append( " where SupervisionConditionID in ("+SupervisionConditionIDlist + ")  " );
            int rows=SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) );
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
        public SupervisionConditionEntity GetModel( int SupervisionConditionID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select  top 1 SupervisionConditionID,SupervisionConditionName,SupervisionConditionRemark from T_SupervisionCondition " );
            strSql.Append( " where SupervisionConditionID=@SupervisionConditionID" );
            SqlParameter[] parameters = {
					new SqlParameter("@SupervisionConditionID", SqlDbType.Int,4)
			};
            parameters[0].Value = SupervisionConditionID;

            SupervisionConditionEntity model=new SupervisionConditionEntity( );
            DataSet ds=SqlHelper.Query(SqlHelper.LocalSqlServer, strSql.ToString( ) , parameters );
            if ( ds.Tables[0].Rows.Count>0 )
            {
                if ( ds.Tables[0].Rows[0]["SupervisionConditionID"]!=null && ds.Tables[0].Rows[0]["SupervisionConditionID"].ToString( )!="" )
                {
                    model.SupervisionConditionID=int.Parse( ds.Tables[0].Rows[0]["SupervisionConditionID"].ToString( ) );
                }
                if ( ds.Tables[0].Rows[0]["SupervisionConditionName"]!=null && ds.Tables[0].Rows[0]["SupervisionConditionName"].ToString( )!="" )
                {
                    model.SupervisionConditionName=ds.Tables[0].Rows[0]["SupervisionConditionName"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["SupervisionConditionRemark"]!=null && ds.Tables[0].Rows[0]["SupervisionConditionRemark"].ToString( )!="" )
                {
                    model.SupervisionConditionRemark=ds.Tables[0].Rows[0]["SupervisionConditionRemark"].ToString( );
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
        public DataSet GetList( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select SupervisionConditionID,SupervisionConditionName,SupervisionConditionRemark " );
            strSql.Append( " FROM T_SupervisionCondition " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query(SqlHelper.LocalSqlServer, strSql.ToString( ) );
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList( int Top , string strWhere , string filedOrder )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select " );
            if ( Top>0 )
            {
                strSql.Append( " top "+Top.ToString( ) );
            }
            strSql.Append( " SupervisionConditionID,SupervisionConditionName,SupervisionConditionRemark " );
            strSql.Append( " FROM T_SupervisionCondition " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            strSql.Append( " order by " + filedOrder );
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) FROM T_SupervisionCondition " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            object obj = SqlHelper.GetSingle( SqlHelper.LocalSqlServer , strSql.ToString( ) );
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage( string strWhere , string orderby , int startIndex , int endIndex )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "SELECT * FROM ( " );
            strSql.Append( " SELECT ROW_NUMBER() OVER (" );
            if ( !string.IsNullOrEmpty( orderby.Trim( ) ) )
            {
                strSql.Append( "order by T." + orderby );
            }
            else
            {
                strSql.Append( "order by T.SupervisionConditionID desc" );
            }
            strSql.Append( ")AS Row, T.*  from T_SupervisionCondition T " );
            if ( !string.IsNullOrEmpty( strWhere.Trim( ) ) )
            {
                strSql.Append( " WHERE " + strWhere );
            }
            strSql.Append( " ) TT" );
            strSql.AppendFormat( " WHERE TT.Row between {0} and {1}" , startIndex , endIndex );
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }

        #endregion  Method
    }
}
