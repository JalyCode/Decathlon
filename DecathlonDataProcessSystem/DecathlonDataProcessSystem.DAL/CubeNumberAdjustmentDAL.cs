using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecathlonDataProcessSystem.Model;
using System.Data.SqlClient;
using System.Data;
using DecathlonDataProcessSystem.DBUtility;

namespace DecathlonDataProcessSystem.DAL
{
    /// <summary>
    /// 数据访问类:CubeNumberAdjustmentDAL
    /// </summary>
    public class CubeNumberAdjustmentDAL
    {
        public CubeNumberAdjustmentDAL( )
        { }
        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(CubeNumberAdjustmentEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "insert into T_CubeNumberAdjustment(" );
            strSql.Append( "ShippingNumber,ConfirmType,TotalParcelNumber,TotalCubeNumber)" );
            strSql.Append( " values (" );
            strSql.Append( "@ShippingNumber,@ConfirmType,@TotalParcelNumber,@TotalCubeNumber)" );
            strSql.Append( ";select @@IDENTITY" );
            SqlParameter[] parameters = {
					new SqlParameter("@ShippingNumber", SqlDbType.VarChar,50),
					new SqlParameter("@ConfirmType", SqlDbType.VarChar,50),
					new SqlParameter("@TotalParcelNumber", SqlDbType.Int,4),
					new SqlParameter("@TotalCubeNumber", SqlDbType.Decimal,9)};
            parameters[0].Value = model.ShippingNumber;
            parameters[1].Value = model.ConfirmType;
            parameters[2].Value = model.TotalParcelNumber;
            parameters[3].Value = model.TotalCubeNumber;

            object obj = SqlHelper.GetSingle(SqlHelper.LocalSqlServer, strSql.ToString( ) , parameters );
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
        public void BulkCubeNumberAdjustmentInsert( DataTable dataTable , int batchSize = 10000 )
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
                        DestinationTableName = "T_CubeNumberAdjustment" ,
                        BatchSize = batchSize
                    } )
                    {
                        bulk.ColumnMappings.Add( "SHIPPING_NUMBER" , "ShippingNumber" );
                        bulk.ColumnMappings.Add( "确认类型" , "ConfirmType" );
                        bulk.ColumnMappings.Add( "总箱数" , "TotalParcelNumber");
                        bulk.ColumnMappings.Add( "总立方数" , "TotalCubeNumber" );
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
        public bool Update( CubeNumberAdjustmentEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "update T_CubeNumberAdjustment set " );
            strSql.Append( "ShippingNumber=@ShippingNumber," );
            strSql.Append( "ConfirmType=@ConfirmType," );
            strSql.Append( "TotalParcelNumber=@TotalParcelNumber," );
            strSql.Append( "TotalCubeNumber=@TotalCubeNumber" );
            strSql.Append( " where ItemID=@ItemID" );
            SqlParameter[] parameters = {
					new SqlParameter("@ShippingNumber", SqlDbType.VarChar,50),
					new SqlParameter("@ConfirmType", SqlDbType.VarChar,50),
					new SqlParameter("@TotalParcelNumber", SqlDbType.Int,4),
					new SqlParameter("@TotalCubeNumber", SqlDbType.Decimal,9),
					new SqlParameter("@ItemID", SqlDbType.Int,4)};
            parameters[0].Value = model.ShippingNumber;
            parameters[1].Value = model.ConfirmType;
            parameters[2].Value = model.TotalParcelNumber;
            parameters[3].Value = model.TotalCubeNumber;
            parameters[4].Value = model.ItemID;

            int rows=SqlHelper.ExecuteSql(SqlHelper.LocalSqlServer, strSql.ToString( ) , parameters );
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
            strSql.Append( "delete from T_CubeNumberAdjustment; " );
            SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete( int ItemID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_CubeNumberAdjustment " );
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
            strSql.Append( "delete from T_CubeNumberAdjustment " );
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
        public CubeNumberAdjustmentEntity GetModel( int ItemID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select  top 1 ItemID,ShippingNumber,ConfirmType,TotalParcelNumber,TotalCubeNumber from T_CubeNumberAdjustment " );
            strSql.Append( " where ItemID=@ItemID" );
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.Int,4)
			};
            parameters[0].Value = ItemID;

            CubeNumberAdjustmentEntity model=new CubeNumberAdjustmentEntity( );
            DataSet ds=SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
            if ( ds.Tables[0].Rows.Count>0 )
            {
                if ( ds.Tables[0].Rows[0]["ItemID"]!=null && ds.Tables[0].Rows[0]["ItemID"].ToString( )!="" )
                {
                    model.ItemID=int.Parse( ds.Tables[0].Rows[0]["ItemID"].ToString( ) );
                }
                if ( ds.Tables[0].Rows[0]["ShippingNumber"]!=null && ds.Tables[0].Rows[0]["ShippingNumber"].ToString( )!="" )
                {
                    model.ShippingNumber=ds.Tables[0].Rows[0]["ShippingNumber"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["ConfirmType"]!=null && ds.Tables[0].Rows[0]["ConfirmType"].ToString( )!="" )
                {
                    model.ConfirmType=ds.Tables[0].Rows[0]["ConfirmType"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["TotalParcelNumber"]!=null && ds.Tables[0].Rows[0]["TotalParcelNumber"].ToString( )!="" )
                {
                    model.TotalParcelNumber=int.Parse( ds.Tables[0].Rows[0]["TotalParcelNumber"].ToString( ) );
                }
                if ( ds.Tables[0].Rows[0]["TotalCubeNumber"]!=null && ds.Tables[0].Rows[0]["TotalCubeNumber"].ToString( )!="" )
                {
                    model.TotalCubeNumber=decimal.Parse( ds.Tables[0].Rows[0]["TotalCubeNumber"].ToString( ) );
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
            strSql.Append( "select ItemID,ShippingNumber,ConfirmType,TotalParcelNumber,TotalCubeNumber " );
            strSql.Append( " FROM T_CubeNumberAdjustment " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query(SqlHelper.LocalSqlServer, strSql.ToString( ) );
        }

        #endregion  Method
    }
}
