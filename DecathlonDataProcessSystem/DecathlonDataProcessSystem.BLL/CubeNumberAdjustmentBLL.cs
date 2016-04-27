using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecathlonDataProcessSystem.DAL;
using DecathlonDataProcessSystem.Model;
using System.Data;

namespace DecathlonDataProcessSystem.BLL
{
    /// <summary>
    /// CubeNumberAdjustment
    /// </summary>
    public class CubeNumberAdjustmentBLL
    {
        private readonly CubeNumberAdjustmentDAL dal=new CubeNumberAdjustmentDAL( );
        public CubeNumberAdjustmentBLL( )
        { }
        #region  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add( CubeNumberAdjustmentEntity model )
        {
            return dal.Add( model );
        }
        public void BulkCubeNumberAdjustmentInsert( DataTable dataTable , int batchSize = 10000 )
        {
            dal.DeleteAll( );
            dal.BulkCubeNumberAdjustmentInsert( dataTable , batchSize );
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update( CubeNumberAdjustmentEntity model )
        {
            return dal.Update( model );
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete( int ItemID )
        {

            return dal.Delete( ItemID );
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList( string ItemIDlist )
        {
            return dal.DeleteList( ItemIDlist );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CubeNumberAdjustmentEntity GetModel( int ItemID )
        {

            return dal.GetModel( ItemID );
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList( string strWhere )
        {
            return dal.GetList( strWhere );
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<CubeNumberAdjustmentEntity> GetModelList( string strWhere )
        {
            DataSet ds = dal.GetList( strWhere );
            return DataTableToList( ds.Tables[0] );
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<CubeNumberAdjustmentEntity> DataTableToList( DataTable dt )
        {
            List<CubeNumberAdjustmentEntity> modelList = new List<CubeNumberAdjustmentEntity>( );
            int rowsCount = dt.Rows.Count;
            if ( rowsCount > 0 )
            {
                CubeNumberAdjustmentEntity model;
                for ( int n = 0 ; n < rowsCount ; n++ )
                {
                    model = new CubeNumberAdjustmentEntity( );
                    if ( dt.Rows[n]["ItemID"]!=null && dt.Rows[n]["ItemID"].ToString( )!="" )
                    {
                        model.ItemID=int.Parse( dt.Rows[n]["ItemID"].ToString( ) );
                    }
                    if ( dt.Rows[n]["ShippingNumber"]!=null && dt.Rows[n]["ShippingNumber"].ToString( )!="" )
                    {
                        model.ShippingNumber=dt.Rows[n]["ShippingNumber"].ToString( );
                    }
                    if ( dt.Rows[n]["ConfirmType"]!=null && dt.Rows[n]["ConfirmType"].ToString( )!="" )
                    {
                        model.ConfirmType=dt.Rows[n]["ConfirmType"].ToString( );
                    }
                    if ( dt.Rows[n]["TotalParcelNumber"]!=null && dt.Rows[n]["TotalParcelNumber"].ToString( )!="" )
                    {
                        model.TotalParcelNumber=int.Parse( dt.Rows[n]["TotalParcelNumber"].ToString( ) );
                    }
                    if ( dt.Rows[n]["TotalCubeNumber"]!=null && dt.Rows[n]["TotalCubeNumber"].ToString( )!="" )
                    {
                        model.TotalCubeNumber=decimal.Parse( dt.Rows[n]["TotalCubeNumber"].ToString( ) );
                    }
                    modelList.Add( model );
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList( )
        {
            return GetList( "" );
        }
        #endregion  Method
    }
}
