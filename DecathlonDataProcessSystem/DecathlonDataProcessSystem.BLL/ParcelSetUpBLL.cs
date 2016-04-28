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
    /// T_ParcelSetUp
    /// </summary>
    public class ParcelSetUpBLL
    {
        private readonly ParcelSetUpDAL dal=new ParcelSetUpDAL( );
        public ParcelSetUpBLL( )
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
        public int Add( ParcelSetUpEntity model )
        {
            return dal.Add( model );
        }
        public void BulkParcelSetUpInsert( DataTable dataTable , int batchSize = 10000 )
        {
            dal.DeleteAll( );
            dal.BulkParcelSetUpInsert( dataTable , batchSize );
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update( ParcelSetUpEntity model )
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
        public ParcelSetUpEntity GetModel( int ItemID )
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
        public List<ParcelSetUpEntity> GetModelList( string strWhere )
        {
            DataSet ds = dal.GetList( strWhere );
            return DataTableToList( ds.Tables[0] );
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ParcelSetUpEntity> DataTableToList( DataTable dt )
        {
            List<ParcelSetUpEntity> modelList = new List<ParcelSetUpEntity>( );
            int rowsCount = dt.Rows.Count;
            if ( rowsCount > 0 )
            {
                ParcelSetUpEntity model;
                for ( int n = 0 ; n < rowsCount ; n++ )
                {
                    model = new ParcelSetUpEntity( );
                    if ( dt.Rows[n]["ItemID"]!=null && dt.Rows[n]["ItemID"].ToString( )!="" )
                    {
                        model.ItemID=int.Parse( dt.Rows[n]["ItemID"].ToString( ) );
                    }
                    if ( dt.Rows[n]["LocalProductName"]!=null && dt.Rows[n]["LocalProductName"].ToString( )!="" )
                    {
                        model.LocalProductName=dt.Rows[n]["LocalProductName"].ToString( );
                    }
                    if ( dt.Rows[n]["Flag"]!=null && dt.Rows[n]["Flag"].ToString( )!="" )
                    {
                        if ( ( dt.Rows[n]["Flag"].ToString( )=="1" )||( dt.Rows[n]["Flag"].ToString( ).ToLower( )=="true" ) )
                        {
                            model.Flag=true;
                        }
                        else
                        {
                            model.Flag=false;
                        }
                    }
                    if ( dt.Rows[n]["MoreParcelNumber"]!=null && dt.Rows[n]["MoreParcelNumber"].ToString( )!="" )
                    {
                        model.MoreParcelNumber=int.Parse( dt.Rows[n]["MoreParcelNumber"].ToString( ) );
                    }
                    if ( dt.Rows[n]["Remark"]!=null && dt.Rows[n]["Remark"].ToString( )!="" )
                    {
                        model.Remark=dt.Rows[n]["Remark"].ToString( );
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
