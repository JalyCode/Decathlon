using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DecathlonDataProcessSystem.DBUtility;
using System.Data.SqlClient;

namespace DecathlonDataProcessSystem.DAL
{
    public class ParcelTakeOutDAL
    {
        public ParcelTakeOutDAL( )
        { }
        /// <summary>
        /// 将数据批量插入到数据库中。
        /// </summary>
        /// <param name="dataTable">要批量插入的 <see cref="DataTable"/>。</param>
        /// <param name="batchSize">每批次写入的数据量。</param>
        public void BulkParcelTakeOutInsert( DataTable dataTable , int batchSize = 10000 )
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
                        DestinationTableName = "T_ParcelTakeOut" ,
                        BatchSize = batchSize
                    } )
                    {
                        bulk.ColumnMappings.Add( "Model_code" , "ModelCode" );
                        bulk.ColumnMappings.Add( "中文品名" , "LocalProductName" );
                        bulk.ColumnMappings.Add( "备注" , "Remark" );
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
            strSql.Append( "delete from T_ParcelTakeOut; " );
            SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }
        public DataSet GetList( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select Distinct ModelCode " );
            strSql.Append( " FROM T_ParcelTakeOut " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }

        public DataSet GetParcelTakeOutList( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select ModelCode AS Model_code,LocalProductName AS 中文品名,Remark AS 备注 " );
            strSql.Append( " FROM T_ParcelTakeOut " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }
    }
}
