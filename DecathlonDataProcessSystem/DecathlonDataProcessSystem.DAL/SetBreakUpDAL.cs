using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DecathlonDataProcessSystem.DBUtility;

namespace DecathlonDataProcessSystem.DAL
{
    /// <summary>
    /// 数据访问类:T_SetBreakUp
    /// </summary>
    public class SetBreakUpDAL
    {
        public SetBreakUpDAL( )
        { }
        /// <summary>
        /// 将数据批量插入到数据库中。
        /// </summary>
        /// <param name="dataTable">要批量插入的 <see cref="DataTable"/>。</param>
        /// <param name="batchSize">每批次写入的数据量。</param>
        public void BulkSETBreakUpInsert( DataTable dataTable , int batchSize = 10000 )
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
                        DestinationTableName = "T_SetBreakUp" ,
                        BatchSize = batchSize
                    } )
                    {
                        bulk.ColumnMappings.Add( "MODEL_CODE" , "ModelCode" );
                        bulk.ColumnMappings.Add( "中文品名" , "LocalProductName" );
                        bulk.ColumnMappings.Add( "金额" , "TotalValue" );
                        bulk.ColumnMappings.Add( "净重" , "NetWeight" );
                        bulk.ColumnMappings.Add( "毛重" , "GrossWeight" );
                        bulk.ColumnMappings.Add( "分组号" , "GroupID" );
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
            strSql.Append( "delete from T_SetBreakUp; " );
            SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }
        #region  Method
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "SELECT s.ItemID,s.ModelCode,s.LocalProductName,cast(round(s.TotalValue/g.TotalValueSUM,2) as numeric(6,3)) as TotalValueRatio, " );
            strSql.Append( " cast(round(s.NetWeight/g.NetWeightSUM,2) as numeric(6,3)) as NetWeightRatio,cast(round(s.GrossWeight/g.GrossWeightSUM,2) as numeric(6,3)) as GrossWeightRatio,s.GroupID " );
            strSql.Append( " FROM dbo.T_SetBreakUp s INNER JOIN (select GroupID ,SUM(TotalValue) AS TotalValueSUM,SUM(NetWeight) AS NetWeightSUM ,SUM(GrossWeight)AS GrossWeightSUM " );
            strSql.Append( "  FROM dbo.T_SetBreakUp GROUP BY GroupID ) g on s.GroupID=g.GroupID" );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query(SqlHelper.LocalSqlServer, strSql.ToString( ) );
        }
        public DataSet GetSetBreakUpList( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "SELECT s.ModelCode AS Model_Code, s.LocalProductName AS 中文品名,cast(round(s.TotalValue/g.TotalValueSUM,2) as numeric(6,3)) as 金额, " );
            strSql.Append( " cast(round(s.NetWeight/g.NetWeightSUM,2) as numeric(6,3)) as 净重,cast(round(s.GrossWeight/g.GrossWeightSUM,2) as numeric(6,3)) as 毛重,s.GroupID AS 分组号" );
            strSql.Append( " FROM dbo.T_SetBreakUp s INNER JOIN (select GroupID ,SUM(TotalValue) AS TotalValueSUM,SUM(NetWeight) AS NetWeightSUM ,SUM(GrossWeight)AS GrossWeightSUM " );
            strSql.Append( "  FROM dbo.T_SetBreakUp GROUP BY GroupID ) g on s.GroupID=g.GroupID" );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }
        #endregion  Method
    }
}
