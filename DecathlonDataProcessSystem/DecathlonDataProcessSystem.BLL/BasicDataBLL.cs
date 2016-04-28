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
    /// T_BasicData
    /// </summary>
    public class BasicDataBLL
    {
        private readonly BasicDataDAL dal=new BasicDataDAL( );
        public BasicDataBLL( )
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId( )
        {
            return dal.GetMaxId( );
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists( int ModeCodeID )
        {
            return dal.Exists( ModeCodeID );
        }
        public bool ModelCodeExists( string ModeCode )
        {
            return dal.ModelCodeExists( ModeCode );
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add( BasicDataEntity model )
        {
            return dal.Add( model );
        }
        public void BulkBasicDataInsert( DataTable dataTable , int batchSize = 10000 )
        {
            dal.DeleteAll( );
            dal.BulkBasicDataInsert( dataTable , batchSize );
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update( BasicDataEntity model )
        {
            return dal.Update( model );
        }
        /// <summary>
        /// 根据Size更新成人或者儿童
        /// </summary>
        //public bool UpdateSizeFlagBySize( )
        //{
        //    return dal.UpdateSizeFlagBySize( );
        //}

        /// <summary>
        /// 清除所有数据
        /// </summary>
        public void DeleteAll( )
        {
            dal.DeleteAll();
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList( string ModeCodeIDlist )
        {
            return dal.DeleteList( ModeCodeIDlist );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BasicDataEntity GetModel( int ModeCodeID )
        {

            return dal.GetModel( ModeCodeID );
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        //public BasicDataEntity GetModelByCache( int ModeCodeID )
        //{

        //    string CacheKey = "T_BasicDataModel-" + ModeCodeID;
        //    object objModel = Maticsoft.Common.DataCache.GetCache( CacheKey );
        //    if ( objModel == null )
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel( ModeCodeID );
        //            if ( objModel != null )
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt( "ModelCache" );
        //                Maticsoft.Common.DataCache.SetCache( CacheKey , objModel , DateTime.Now.AddMinutes( ModelCache ) , TimeSpan.Zero );
        //            }
        //        }
        //        catch { }
        //    }
        //    return (Maticsoft.Model.T_BasicData)objModel;
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList( string strWhere )
        {
            return dal.GetList( strWhere );
        }
        public DataSet GetDistinctModeCodeList( )
        {
            return dal.GetDistinctModeCodeList( );
        }
        public DataSet GetRepetitiveModeCodeList( )
        {
            return dal.GetRepetitiveModeCodeList( );
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
        public List<BasicDataEntity> GetModelList( string strWhere )
        {
            DataSet ds = dal.GetList( strWhere );
            return DataTableToList( ds.Tables[0] );
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<BasicDataEntity> DataTableToList( DataTable dt )
        {
            List<BasicDataEntity> modelList = new List<BasicDataEntity>( );
            int rowsCount = dt.Rows.Count;
            if ( rowsCount > 0 )
            {
                BasicDataEntity model;
                for ( int n = 0 ; n < rowsCount ; n++ )
                {
                    model = new BasicDataEntity( );
                    if ( dt.Rows[n]["ModelCodeID"]!=null && dt.Rows[n]["ModelCodeID"].ToString( )!="" )
                    {
                        model.ModelCodeID=int.Parse( dt.Rows[n]["ModelCodeID"].ToString( ) );
                    }
                    if ( dt.Rows[n]["ModelCode"]!=null && dt.Rows[n]["ModelCode"].ToString( )!="" )
                    {
                        model.ModelCode=dt.Rows[n]["ModelCode"].ToString( );
                    }
                    if ( dt.Rows[n]["HSCodeInCat"]!=null && dt.Rows[n]["HSCodeInCat"].ToString( )!="" )
                    {
                        model.HSCodeInCat=dt.Rows[n]["HSCodeInCat"].ToString( );
                    }
                    //if ( dt.Rows[n]["ModelCodeDescription"]!=null && dt.Rows[n]["ModelCodeDescription"].ToString( )!="" )
                    //{
                    //    model.ModelCodeDescription=dt.Rows[n]["ModelCodeDescription"].ToString( );
                    //}
                    if ( dt.Rows[n]["EnglishProductName"]!=null && dt.Rows[n]["EnglishProductName"].ToString( )!="" )
                    {
                        model.EnglishProductName=dt.Rows[n]["EnglishProductName"].ToString( );
                    }
                    if ( dt.Rows[n]["LocalProductName"]!=null && dt.Rows[n]["LocalProductName"].ToString( )!="" )
                    {
                        model.LocalProductName=dt.Rows[n]["LocalProductName"].ToString( );
                    }
                    if ( dt.Rows[n]["QuantityUnit"]!=null && dt.Rows[n]["QuantityUnit"].ToString( )!="" )
                    {
                        model.QuantityUnit=dt.Rows[n]["QuantityUnit"].ToString( );
                    }
                    if ( dt.Rows[n]["SentialFactor"]!=null && dt.Rows[n]["SentialFactor"].ToString( )!="" )
                    {
                        model.SentialFactor=dt.Rows[n]["SentialFactor"].ToString( );
                    }
                    if ( dt.Rows[n]["LocalComposition"]!=null && dt.Rows[n]["LocalComposition"].ToString( )!="" )
                    {
                        model.LocalComposition=dt.Rows[n]["LocalComposition"].ToString( );
                    }
                    if ( dt.Rows[n]["SupervisionCondition"]!=null && dt.Rows[n]["SupervisionCondition"].ToString( )!="" )
                    {
                        model.SupervisionCondition=dt.Rows[n]["SupervisionCondition"].ToString( );
                    }
                    if ( dt.Rows[n]["DoubleOrSet"]!=null && dt.Rows[n]["DoubleOrSet"].ToString( )!="" )
                    {
                        model.DoubleOrSet=dt.Rows[n]["DoubleOrSet"].ToString( );
                    }
                    if ( dt.Rows[n]["Description"]!=null && dt.Rows[n]["Description"].ToString( )!="" )
                    {
                        model.Description=dt.Rows[n]["Description"].ToString( );
                    }
                    if ( dt.Rows[n]["ExaminingReport"]!=null && dt.Rows[n]["ExaminingReport"].ToString( )!="" )
                    {
                        model.ExaminingReport=dt.Rows[n]["ExaminingReport"].ToString( );
                    }
                    if ( dt.Rows[n]["Size"]!=null && dt.Rows[n]["Size"].ToString( )!="" )
                    {
                        model.Size=dt.Rows[n]["Size"].ToString( );
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
