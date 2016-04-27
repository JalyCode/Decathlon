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
    /// 数据访问类:T_ParcelTakeOutCondition
    /// </summary>
    public class ParcelTakeOutConditionDAL
    {
        public ParcelTakeOutConditionDAL( )
        { }

        /// <summary>
        /// 将数据批量插入到数据库中。
        /// </summary>
        /// <param name="dataTable">要批量插入的 <see cref="DataTable"/>。</param>
        /// <param name="batchSize">每批次写入的数据量。</param>
        public void BulkShippingInsert( DataTable dataTable , int batchSize = 10000 )
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
                        DestinationTableName = "T_ParcelTakeOutCondition" ,
                        BatchSize = batchSize
                    } )
                    {
                        bulk.ColumnMappings.Add( "Item_code" , "ItemCode" );
                        bulk.ColumnMappings.Add( "Model_code" , "ModelCode" );
                        bulk.ColumnMappings.Add( "中文品名" , "LocalProductName" );
                        bulk.ColumnMappings.Add( "Minimun_Export_QTY" , "MinExportQTY" );
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
        /// 删除所有数据
        /// </summary>
        public void DeleteAll( )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_ParcelTakeOutCondition; " );
            SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists( int ItemID )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) from T_ParcelTakeOutCondition" );
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
        public int Add( ParcelTakeOutConditionEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "insert into T_ParcelTakeOutCondition(" );
            strSql.Append( "ItemCode,ModelCode,LocalProductName,MinExportQTY)" );
            strSql.Append( " values (" );
            strSql.Append( "@ItemCode,@ModelCode,@LocalProductName,@MinExportQTY)" );
            strSql.Append( ";select @@IDENTITY" );
            SqlParameter[] parameters = {
					new SqlParameter("@ItemCode", SqlDbType.VarChar,50),
					new SqlParameter("@ModelCode", SqlDbType.VarChar,50),
					new SqlParameter("@LocalProductName", SqlDbType.VarChar,50),
					new SqlParameter("@MinExportQTY", SqlDbType.Int,4)};
            parameters[0].Value = model.ItemCode;
            parameters[1].Value = model.ModelCode;
            parameters[2].Value = model.LocalProductName;
            parameters[3].Value = model.MinExportQTY;

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
        public bool Update( ParcelTakeOutConditionEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "update T_ParcelTakeOutCondition set " );
            strSql.Append( "ItemCode=@ItemCode," );
            strSql.Append( "ModelCode=@ModelCode," );
            strSql.Append( "LocalProductName=@LocalProductName," );
            strSql.Append( "MinExportQTY=@MinExportQTY" );
            strSql.Append( " where ItemID=@ItemID" );
            SqlParameter[] parameters = {
					new SqlParameter("@ItemCode", SqlDbType.VarChar,50),
					new SqlParameter("@ModelCode", SqlDbType.VarChar,50),
					new SqlParameter("@LocalProductName", SqlDbType.VarChar,50),
					new SqlParameter("@MinExportQTY", SqlDbType.Int,4),
					new SqlParameter("@ItemID", SqlDbType.Int,4)};
            parameters[0].Value = model.ItemCode;
            parameters[1].Value = model.ModelCode;
            parameters[2].Value = model.LocalProductName;
            parameters[3].Value = model.MinExportQTY;
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
        /// 删除一条数据
        /// </summary>
        public bool Delete( int ItemID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_ParcelTakeOutCondition " );
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
            strSql.Append( "delete from T_ParcelTakeOutCondition " );
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
        public ParcelTakeOutConditionEntity GetModel( int ItemID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select  top 1 ItemID,ItemCode,ModelCode,LocalProductName,MinExportQTY from T_ParcelTakeOutCondition " );
            strSql.Append( " where ItemID=@ItemID" );
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.Int,4)
			};
            parameters[0].Value = ItemID;

            ParcelTakeOutConditionEntity model=new ParcelTakeOutConditionEntity( );
            DataSet ds=SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
            if ( ds.Tables[0].Rows.Count>0 )
            {
                if ( ds.Tables[0].Rows[0]["ItemID"]!=null && ds.Tables[0].Rows[0]["ItemID"].ToString( )!="" )
                {
                    model.ItemID=int.Parse( ds.Tables[0].Rows[0]["ItemID"].ToString( ) );
                }
                if ( ds.Tables[0].Rows[0]["ItemCode"]!=null && ds.Tables[0].Rows[0]["ItemCode"].ToString( )!="" )
                {
                    model.ItemCode=ds.Tables[0].Rows[0]["ItemCode"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["ModelCode"]!=null && ds.Tables[0].Rows[0]["ModelCode"].ToString( )!="" )
                {
                    model.ModelCode=ds.Tables[0].Rows[0]["ModelCode"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["LocalProductName"]!=null && ds.Tables[0].Rows[0]["LocalProductName"].ToString( )!="" )
                {
                    model.LocalProductName=ds.Tables[0].Rows[0]["LocalProductName"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["MinExportQTY"]!=null && ds.Tables[0].Rows[0]["MinExportQTY"].ToString( )!="" )
                {
                    model.MinExportQTY=int.Parse( ds.Tables[0].Rows[0]["MinExportQTY"].ToString( ) );
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        public DataSet GetShippingList( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select ItemCode as Item_code,ModelCode AS Model_code,LocalProductName AS 中文品名,MinExportQTY AS Minimun_Export_QTY " );
            strSql.Append( " FROM T_ParcelTakeOutCondition " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select ItemID,ItemCode,ModelCode,LocalProductName,MinExportQTY " );
            strSql.Append( " FROM T_ParcelTakeOutCondition " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }

        #endregion  Method
    }
}
