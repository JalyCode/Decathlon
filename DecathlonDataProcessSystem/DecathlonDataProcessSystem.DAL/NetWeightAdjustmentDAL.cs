using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DecathlonDataProcessSystem.DBUtility;
using DecathlonDataProcessSystem.Model;
using System.Data;

namespace DecathlonDataProcessSystem.DAL
{
    /// <summary>
    /// 数据访问类:T_NetWeightAdjustment
    /// </summary>
    public class NetWeightAdjustmentDAL
    {
        public NetWeightAdjustmentDAL( )
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists( int ItemID )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) from T_NetWeightAdjustment" );
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
        public int Add( NetWeightAdjustmentEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "insert into T_NetWeightAdjustment(" );
            strSql.Append( "LocalProductName,AdjustRatio)" );
            strSql.Append( " values (" );
            strSql.Append( "@LocalProductName,@AdjustRatio)" );
            strSql.Append( ";select @@IDENTITY" );
            SqlParameter[] parameters = {
					new SqlParameter("@LocalProductName", SqlDbType.VarChar,50),
					new SqlParameter("@AdjustRatio", SqlDbType.Decimal,9)};
            parameters[0].Value = model.LocalProductName;
            parameters[1].Value = model.AdjustRatio;

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
        public void BulkNetWeightAdjustInsert( DataTable dataTable , int batchSize = 10000 )
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
                        DestinationTableName = "T_NetWeightAdjustment" ,
                        BatchSize = batchSize
                    } )
                    {
                        bulk.ColumnMappings.Add( "中文品名" , "LocalProductName" );
                        bulk.ColumnMappings.Add( "调整系数" , "AdjustRatio" );
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
        public bool Update( NetWeightAdjustmentEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "update T_NetWeightAdjustment set " );
            strSql.Append( "LocalProductName=@LocalProductName," );
            strSql.Append( "AdjustRatio=@AdjustRatio" );
            strSql.Append( " where ItemID=@ItemID" );
            SqlParameter[] parameters = {
					new SqlParameter("@LocalProductName", SqlDbType.VarChar,50),
					new SqlParameter("@AdjustRatio", SqlDbType.Decimal,9),
					new SqlParameter("@ItemID", SqlDbType.Int,4)};
            parameters[0].Value = model.LocalProductName;
            parameters[1].Value = model.AdjustRatio;
            parameters[2].Value = model.ItemID;

            int rows=SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer, strSql.ToString( ) , parameters );
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
            strSql.Append( "delete from T_NetWeightAdjustment; " );
            SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete( int ItemID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_NetWeightAdjustment " );
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
            strSql.Append( "delete from T_NetWeightAdjustment " );
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
        public NetWeightAdjustmentEntity GetModel( int ItemID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select  top 1 ItemID,LocalProductName,AdjustRatio from T_NetWeightAdjustment " );
            strSql.Append( " where ItemID=@ItemID" );
            SqlParameter[] parameters = {
					new SqlParameter("@ItemID", SqlDbType.Int,4)
			};
            parameters[0].Value = ItemID;

            NetWeightAdjustmentEntity model=new NetWeightAdjustmentEntity( );
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
                if ( ds.Tables[0].Rows[0]["AdjustRatio"]!=null && ds.Tables[0].Rows[0]["AdjustRatio"].ToString( )!="" )
                {
                    model.AdjustRatio=decimal.Parse( ds.Tables[0].Rows[0]["AdjustRatio"].ToString( ) );
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
            strSql.Append( "select ItemID,LocalProductName,AdjustRatio " );
            strSql.Append( " FROM T_NetWeightAdjustment " );
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
            strSql.Append( "select count(1) FROM T_NetWeightAdjustment " );
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
