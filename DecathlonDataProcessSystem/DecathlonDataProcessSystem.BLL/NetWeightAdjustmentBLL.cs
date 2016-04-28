using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecathlonDataProcessSystem.DAL;
using DecathlonDataProcessSystem.Model;
using System.Data;

namespace DecathlonDataProcessSystem.BLL
{

    public class NetWeightAdjustmentBLL
    {
        private readonly NetWeightAdjustmentDAL dal=new NetWeightAdjustmentDAL( );
        public NetWeightAdjustmentBLL( )
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists( int ItemID )
        {
            return dal.Exists( ItemID );
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add( NetWeightAdjustmentEntity model )
        {
            return dal.Add( model );
        }
        public void BulkNetWeightAdjustInsert( DataTable dataTable , int batchSize = 10000 )
        {
            dal.DeleteAll( );
            dal.BulkNetWeightAdjustInsert( dataTable , batchSize );
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update( NetWeightAdjustmentEntity model )
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
        public NetWeightAdjustmentEntity GetModel( int ItemID )
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
        public List<NetWeightAdjustmentEntity> GetModelList( string strWhere )
        {
            DataSet ds = dal.GetList( strWhere );
            return DataTableToList( ds.Tables[0] );
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<NetWeightAdjustmentEntity> DataTableToList( DataTable dt )
        {
            List<NetWeightAdjustmentEntity> modelList = new List<NetWeightAdjustmentEntity>( );
            int rowsCount = dt.Rows.Count;
            if ( rowsCount > 0 )
            {
                NetWeightAdjustmentEntity model;
                for ( int n = 0 ; n < rowsCount ; n++ )
                {
                    model = new NetWeightAdjustmentEntity( );
                    if ( dt.Rows[n]["ItemID"]!=null && dt.Rows[n]["ItemID"].ToString( )!="" )
                    {
                        model.ItemID=int.Parse( dt.Rows[n]["ItemID"].ToString( ) );
                    }
                    if ( dt.Rows[n]["LocalProductName"]!=null && dt.Rows[n]["LocalProductName"].ToString( )!="" )
                    {
                        model.LocalProductName=dt.Rows[n]["LocalProductName"].ToString( );
                    }
                    if ( dt.Rows[n]["AdjustRatio"]!=null && dt.Rows[n]["AdjustRatio"].ToString( )!="" )
                    {
                        model.AdjustRatio=decimal.Parse( dt.Rows[n]["AdjustRatio"].ToString( ) );
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

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount( string strWhere )
        {
            return dal.GetRecordCount( strWhere );
        }

        #endregion  Method
    }
}
