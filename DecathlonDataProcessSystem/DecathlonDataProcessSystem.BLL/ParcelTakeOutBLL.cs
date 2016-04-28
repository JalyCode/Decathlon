using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecathlonDataProcessSystem.DAL;
using System.Data;

namespace DecathlonDataProcessSystem.BLL
{
    public class ParcelTakeOutBLL
    {
        private readonly ParcelTakeOutDAL dal=new ParcelTakeOutDAL( );
        public ParcelTakeOutBLL( )
        { }
        public void BulkParcelTakeOutInsert( DataTable dataTable , int batchSize = 10000 )
        {
            dal.DeleteAll( );
            dal.BulkParcelTakeOutInsert( dataTable , batchSize );
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetParcelTakeOutList( string strWhere )
        {
            return dal.GetParcelTakeOutList( strWhere );
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
        public List<string> GetModelList( string strWhere )
        {
            DataSet ds = dal.GetList( strWhere );
            return DataTableToList( ds.Tables[0] );
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<string> DataTableToList( DataTable dt )
        {
            List<string> modelList = new List<string>( );
            int rowsCount = dt.Rows.Count;
            if ( rowsCount > 0 )
            {
                for ( int n = 0 ; n < rowsCount ; n++ )
                {
                    if ( dt.Rows[n]["ModelCode"]!=null && dt.Rows[n]["ModelCode"].ToString( )!="" )
                    {
                        modelList.Add( dt.Rows[n]["ModelCode"].ToString( ) );
                    }

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
    }
}
