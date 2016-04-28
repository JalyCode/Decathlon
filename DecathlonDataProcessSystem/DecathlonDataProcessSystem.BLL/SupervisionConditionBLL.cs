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
    /// T_SupervisionCondition
    /// </summary>
    public class SupervisionConditionBLL
    {
        private readonly SupervisionConditionDAL dal=new SupervisionConditionDAL( );
        public SupervisionConditionBLL( )
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists( int SupervisionConditionID )
        {
            return dal.Exists( SupervisionConditionID );
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add( SupervisionConditionEntity model )
        {
            return dal.Add( model );
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update( SupervisionConditionEntity model )
        {
            return dal.Update( model );
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete( int SupervisionConditionID )
        {

            return dal.Delete( SupervisionConditionID );
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList( string SupervisionConditionIDlist )
        {
            return dal.DeleteList( SupervisionConditionIDlist );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SupervisionConditionEntity GetModel( int SupervisionConditionID )
        {

            return dal.GetModel( SupervisionConditionID );
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList( string strWhere )
        {
            return dal.GetList( strWhere );
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList( int Top , string strWhere , string filedOrder )
        {
            return dal.GetList( Top , strWhere , filedOrder );
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SupervisionConditionEntity> GetModelList( string strWhere )
        {
            DataSet ds = dal.GetList( strWhere );
            return DataTableToList( ds.Tables[0] );
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SupervisionConditionEntity> DataTableToList( DataTable dt )
        {
            List<SupervisionConditionEntity> modelList = new List<SupervisionConditionEntity>( );
            int rowsCount = dt.Rows.Count;
            if ( rowsCount > 0 )
            {
                SupervisionConditionEntity model;
                for ( int n = 0 ; n < rowsCount ; n++ )
                {
                    model = new SupervisionConditionEntity( );
                    if ( dt.Rows[n]["SupervisionConditionID"]!=null && dt.Rows[n]["SupervisionConditionID"].ToString( )!="" )
                    {
                        model.SupervisionConditionID=int.Parse( dt.Rows[n]["SupervisionConditionID"].ToString( ) );
                    }
                    if ( dt.Rows[n]["SupervisionConditionName"]!=null && dt.Rows[n]["SupervisionConditionName"].ToString( )!="" )
                    {
                        model.SupervisionConditionName=dt.Rows[n]["SupervisionConditionName"].ToString( );
                    }
                    if ( dt.Rows[n]["SupervisionConditionRemark"]!=null && dt.Rows[n]["SupervisionConditionRemark"].ToString( )!="" )
                    {
                        model.SupervisionConditionRemark=dt.Rows[n]["SupervisionConditionRemark"].ToString( );
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
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage( string strWhere , string orderby , int startIndex , int endIndex )
        {
            return dal.GetListByPage( strWhere , orderby , startIndex , endIndex );
        }


        #endregion  Method
    }
}
