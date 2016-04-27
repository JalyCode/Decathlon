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
    /// T_ParcelTakeOutCondition
    /// </summary>
    public class ParcelTakeOutConditionBLL
    {
        private readonly ParcelTakeOutConditionDAL dal=new ParcelTakeOutConditionDAL( );
        public ParcelTakeOutConditionBLL( )
        { }
        public void BulkShippingInsert( DataTable dataTable , int batchSize = 10000 )
        {
            dal.DeleteAll( );
            dal.BulkShippingInsert( dataTable , batchSize );
        }
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
        public int Add( ParcelTakeOutConditionEntity model )
        {
            return dal.Add( model );
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update( ParcelTakeOutConditionEntity model )
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
        public ParcelTakeOutConditionEntity GetModel( int ItemID )
        {

            return dal.GetModel( ItemID );
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetShippingList( string strWhere )
        {
            return dal.GetShippingList( strWhere );
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
        public List<ParcelTakeOutConditionEntity> GetModelList( string strWhere )
        {
            DataSet ds = dal.GetList( strWhere );
            return DataTableToList( ds.Tables[0] );
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ParcelTakeOutConditionEntity> DataTableToList( DataTable dt )
        {
            List<ParcelTakeOutConditionEntity> modelList = new List<ParcelTakeOutConditionEntity>( );
            int rowsCount = dt.Rows.Count;
            if ( rowsCount > 0 )
            {
                ParcelTakeOutConditionEntity model;
                for ( int n = 0 ; n < rowsCount ; n++ )
                {
                    model = new ParcelTakeOutConditionEntity( );
                    if ( dt.Rows[n]["ItemID"]!=null && dt.Rows[n]["ItemID"].ToString( )!="" )
                    {
                        model.ItemID=int.Parse( dt.Rows[n]["ItemID"].ToString( ) );
                    }
                    if ( dt.Rows[n]["ItemCode"]!=null && dt.Rows[n]["ItemCode"].ToString( )!="" )
                    {
                        model.ItemCode=dt.Rows[n]["ItemCode"].ToString( );
                    }
                    if ( dt.Rows[n]["ModelCode"]!=null && dt.Rows[n]["ModelCode"].ToString( )!="" )
                    {
                        model.ModelCode=dt.Rows[n]["ModelCode"].ToString( );
                    }
                    if ( dt.Rows[n]["LocalProductName"]!=null && dt.Rows[n]["LocalProductName"].ToString( )!="" )
                    {
                        model.LocalProductName=dt.Rows[n]["LocalProductName"].ToString( );
                    }
                    if ( dt.Rows[n]["MinExportQTY"]!=null && dt.Rows[n]["MinExportQTY"].ToString( )!="" )
                    {
                        model.MinExportQTY=int.Parse( dt.Rows[n]["MinExportQTY"].ToString( ) );
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
