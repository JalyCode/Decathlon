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
    /// T_SentialFactorType
    /// </summary>
    public class SentialFactorTypeBLL
    {
        private readonly SentialFactorTypeDAL dal=new SentialFactorTypeDAL( );
        public SentialFactorTypeBLL( )
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists( int SentialFactorTypeID )
        {
            return dal.Exists( SentialFactorTypeID );
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add( SentialFactorTypeEntity model )
        {
            return dal.Add( model );
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update( SentialFactorTypeEntity model )
        {
            return dal.Update( model );
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete( int SentialFactorTypeID )
        {

            return dal.Delete( SentialFactorTypeID );
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SentialFactorTypeEntity GetModel( int SentialFactorTypeID )
        {

            return dal.GetModel( SentialFactorTypeID );
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
                    if ( dt.Rows[n]["SentialFactorTypeName"]!=null && dt.Rows[n]["SentialFactorTypeName"].ToString( )!="" )
                    {
                        modelList.Add( dt.Rows[n]["SentialFactorTypeName"].ToString( ) );
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

        #endregion  Method
    }
}
