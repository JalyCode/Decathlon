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
    /// 数据访问类:T_ParcelSetUp
    /// </summary>
    public class ParcelSetUpDAL
    {
        public ParcelSetUpDAL( )
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists( int ItemID )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) from T_ParcelSetUp" );
            strSql.Append( " where ItemID=@ItemID" );
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.Int,4)
			};
            parameters[0].Value = ItemID;
            return SqlHelper.Exists(SqlHelper.LocalSqlServer, strSql.ToString( ) , parameters );
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add( ParcelSetUpEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "insert into T_ParcelSetUp(" );
            strSql.Append( "LocalProductName,Flag,MoreParcelNumber,Remark)" );
            strSql.Append( " values (" );
            strSql.Append( "@LocalProductName,@Flag,@MoreParcelNumber,@Remark)" );
            strSql.Append( ";select @@IDENTITY" );
            SqlParameter[] parameters = {
					new SqlParameter("@LocalProductName", SqlDbType.VarChar,50),
					new SqlParameter("@Flag", SqlDbType.Bit,1),
					new SqlParameter("@MoreParcelNumber", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar)};
            parameters[0].Value = model.LocalProductName;
            parameters[1].Value = model.Flag;
            parameters[2].Value = model.MoreParcelNumber;
            parameters[3].Value = model.Remark;

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
        /// 将数据批量插入到数据库中。
        /// </summary>
        /// <param name="dataTable">要批量插入的 <see cref="DataTable"/>。</param>
        /// <param name="batchSize">每批次写入的数据量。</param>
        public void BulkParcelSetUpInsert( DataTable dataTable , int batchSize = 10000 )
        {
            if ( dataTable.Rows.Count == 0 )
            {
                return;
            }
            using ( SqlConnection connection = new SqlConnection( SqlHelper.LocalSqlServer ) )
            {
                try
                {
                    connection.Open( );
                    using ( var bulk = new SqlBulkCopy( connection , SqlBulkCopyOptions.KeepIdentity , null )
                    {
                        DestinationTableName = "T_ParcelSetUp" ,
                        BatchSize = batchSize
                    } )
                    {
                        bulk.ColumnMappings.Add( "中文品名" , "LocalProductName" );
                        bulk.ColumnMappings.Add( "是否大类" , "Flag" );
                        bulk.ColumnMappings.Add( "需多配箱数" , "MoreParcelNumber" );
                        bulk.ColumnMappings.Add( "备注说明" , "Remark" );
                        bulk.WriteToServer( dataTable );
                        bulk.Close( );
                    }
                    dataTable.Dispose( );
                }
                catch ( Exception exp )
                {
                    throw new Exception( exp.Message );
                }
                finally
                {
                    connection.Close( );

                }
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update( ParcelSetUpEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "update T_ParcelSetUp set " );
            strSql.Append( "LocalProductName=@LocalProductName," );
            strSql.Append( "Flag=@Flag," );
            strSql.Append( "MoreParcelNumber=@MoreParcelNumber," );
            strSql.Append( "Remark=@Remark" );
            strSql.Append( " where ItemID=@ItemID" );
            SqlParameter[] parameters = {
					new SqlParameter("@LocalProductName", SqlDbType.VarChar,50),
					new SqlParameter("@Flag", SqlDbType.Bit,1),
					new SqlParameter("@MoreParcelNumber", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar),
					new SqlParameter("@ItemID", SqlDbType.Int,4)};
            parameters[0].Value = model.LocalProductName;
            parameters[1].Value = model.Flag;
            parameters[2].Value = model.MoreParcelNumber;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.ItemID;

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
        /// 删除所有数据
        /// </summary>
        public void DeleteAll( )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_ParcelSetUp; " );
            SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete( int ItemID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_ParcelSetUp " );
            strSql.Append( " where ItemID=@ItemID" );
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.Int,4)
			};
            parameters[0].Value = ItemID;

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
        public bool DeleteList( string ItemIDlist )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_ParcelSetUp " );
            strSql.Append( " where ItemID in ("+ItemIDlist + ")  " );
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
        public ParcelSetUpEntity GetModel( int ItemID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select  top 1 ItemID,LocalProductName,Flag,MoreParcelNumber,Remark from T_ParcelSetUp " );
            strSql.Append( " where ItemID=@ItemID" );
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.Int,4)
			};
            parameters[0].Value = ItemID;

            ParcelSetUpEntity model=new ParcelSetUpEntity( );
            DataSet ds=SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
            if ( ds.Tables[0].Rows.Count>0 )
            {
                if ( ds.Tables[0].Rows[0]["ItemID"]!=null && ds.Tables[0].Rows[0]["ItemID"].ToString( )!="" )
                {
                    model.ItemID=int.Parse( ds.Tables[0].Rows[0]["ItemID"].ToString( ) );
                }
                if ( ds.Tables[0].Rows[0]["LocalProductName"]!=null && ds.Tables[0].Rows[0]["LocalProductName"].ToString( )!="" )
                {
                    model.LocalProductName=ds.Tables[0].Rows[0]["LocalProductName"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["Flag"]!=null && ds.Tables[0].Rows[0]["Flag"].ToString( )!="" )
                {
                    if ( ( ds.Tables[0].Rows[0]["Flag"].ToString( )=="1" )||( ds.Tables[0].Rows[0]["Flag"].ToString( ).ToLower( )=="true" ) )
                    {
                        model.Flag=true;
                    }
                    else
                    {
                        model.Flag=false;
                    }
                }
                if ( ds.Tables[0].Rows[0]["MoreParcelNumber"]!=null && ds.Tables[0].Rows[0]["MoreParcelNumber"].ToString( )!="" )
                {
                    model.MoreParcelNumber=int.Parse( ds.Tables[0].Rows[0]["MoreParcelNumber"].ToString( ) );
                }
                if ( ds.Tables[0].Rows[0]["Remark"]!=null && ds.Tables[0].Rows[0]["Remark"].ToString( )!="" )
                {
                    model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString( );
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
            strSql.Append( "select ItemID,LocalProductName,Flag,MoreParcelNumber,Remark " );
            strSql.Append( " FROM T_ParcelSetUp " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) FROM T_ParcelSetUp " );
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

        #endregion  Method
    }
}
