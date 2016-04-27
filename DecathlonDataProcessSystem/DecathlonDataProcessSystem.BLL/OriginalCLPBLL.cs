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
    /// T_OriginalCLP
    /// </summary>
    public class OriginalCLPBLL
    {
        private readonly OriginalCLPDAL dal=new OriginalCLPDAL( );
        public OriginalCLPBLL( )
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
        public bool Exists( int CLPID )
        {
            return dal.Exists( CLPID );
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add( OriginalCLPEntity model )
        {
            return dal.Add( model );
        }
        public void BulkOriginalCLPInsert( DataTable dataTable , int batchSize = 10000 )
        {
            dal.DeleteByFileName( dataTable.TableName );
            dal.BulkOriginalCLPInsert( dataTable , batchSize );
        }
        public DataTable GetDiffrenceOriginalCLPRecord( string filename )
        {
            return dal.GetDiffrenceOriginalCLPRecord( filename );
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update( OriginalCLPEntity model )
        {
            return dal.Update( model );
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete( int CLPID )
        {

            return dal.Delete( CLPID );
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList( string CLPIDlist )
        {
            return dal.DeleteList( CLPIDlist );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OriginalCLPEntity GetModel( int CLPID )
        {

            return dal.GetModel( CLPID );
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList( string strWhere )
        {
            return dal.GetList( strWhere );
        }
        public DataSet GetDeleteColumnCLPList( string filename , string userid )
        {
            return dal.GetDeleteColumnCLPList( filename , userid );
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
        public List<OriginalCLPEntity> GetModelList( string strWhere )
        {
            DataSet ds = dal.GetList( strWhere );
            return DataTableToList( ds.Tables[0] );
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<OriginalCLPEntity> DataTableToList( DataTable dt )
        {
            List<OriginalCLPEntity> modelList = new List<OriginalCLPEntity>( );
            int rowsCount = dt.Rows.Count;
            if ( rowsCount > 0 )
            {
                OriginalCLPEntity model;
                for ( int n = 0 ; n < rowsCount ; n++ )
                {
                    model = new OriginalCLPEntity( );
                    if ( dt.Rows[n]["CLPID"]!=null && dt.Rows[n]["CLPID"].ToString( )!="" )
                    {
                        model.CLPID=int.Parse( dt.Rows[n]["CLPID"].ToString( ) );
                    }
                    if ( dt.Rows[n]["ItemCode"]!=null && dt.Rows[n]["ItemCode"].ToString( )!="" )
                    {
                        model.ItemCode=dt.Rows[n]["ItemCode"].ToString( );
                    }
                    if ( dt.Rows[n]["ShippingNumber"]!=null && dt.Rows[n]["ShippingNumber"].ToString( )!="" )
                    {
                        model.ShippingNumber=dt.Rows[n]["ShippingNumber"].ToString( );
                    }
                    if ( dt.Rows[n]["OrderNumber"]!=null && dt.Rows[n]["OrderNumber"].ToString( )!="" )
                    {
                        model.OrderNumber=dt.Rows[n]["OrderNumber"].ToString( );
                    }
                    if ( dt.Rows[n]["OriginalPONumber"]!=null && dt.Rows[n]["OriginalPONumber"].ToString( )!="" )
                    {
                        model.OriginalPONumber=dt.Rows[n]["OriginalPONumber"].ToString( );
                    }
                    if ( dt.Rows[n]["PalletNumber"]!=null && dt.Rows[n]["PalletNumber"].ToString( )!="" )
                    {
                        model.PalletNumber=dt.Rows[n]["PalletNumber"].ToString( );
                    }
                    if ( dt.Rows[n]["ParcelNumber"]!=null && dt.Rows[n]["ParcelNumber"].ToString( )!="" )
                    {
                        model.ParcelNumber=dt.Rows[n]["ParcelNumber"].ToString( );
                    }
                    if ( dt.Rows[n]["ModelCode"]!=null && dt.Rows[n]["ModelCode"].ToString( )!="" )
                    {
                        model.ModelCode=dt.Rows[n]["ModelCode"].ToString( );
                    }
                    if ( dt.Rows[n]["Origin"]!=null && dt.Rows[n]["Origin"].ToString( )!="" )
                    {
                        model.Origin=dt.Rows[n]["Origin"].ToString( );
                    }
                    if ( dt.Rows[n]["Quantity"]!=null && dt.Rows[n]["Quantity"].ToString( )!="" )
                    {
                        model.Quantity=dt.Rows[n]["Quantity"].ToString( );
                    }
                    if ( dt.Rows[n]["QuantityUnit"]!=null && dt.Rows[n]["QuantityUnit"].ToString( )!="" )
                    {
                        model.QuantityUnit=dt.Rows[n]["QuantityUnit"].ToString( );
                    }
                    if ( dt.Rows[n]["DispatchingKey"]!=null && dt.Rows[n]["DispatchingKey"].ToString( )!="" )
                    {
                        model.DispatchingKey=dt.Rows[n]["DispatchingKey"].ToString( );
                    }
                    if ( dt.Rows[n]["EnglishComposition"]!=null && dt.Rows[n]["EnglishComposition"].ToString( )!="" )
                    {
                        model.EnglishComposition=dt.Rows[n]["EnglishComposition"].ToString( );
                    }
                    if ( dt.Rows[n]["LocalComposition"]!=null && dt.Rows[n]["LocalComposition"].ToString( )!="" )
                    {
                        model.LocalComposition=dt.Rows[n]["LocalComposition"].ToString( );
                    }
                    if ( dt.Rows[n]["Size"]!=null && dt.Rows[n]["Size"].ToString( )!="" )
                    {
                        model.Size=dt.Rows[n]["Size"].ToString( );
                    }
                    if ( dt.Rows[n]["EnglishDescription"]!=null && dt.Rows[n]["EnglishDescription"].ToString( )!="" )
                    {
                        model.EnglishDescription=dt.Rows[n]["EnglishDescription"].ToString( );
                    }
                    if ( dt.Rows[n]["LocalDescription"]!=null && dt.Rows[n]["LocalDescription"].ToString( )!="" )
                    {
                        model.LocalDescription=dt.Rows[n]["LocalDescription"].ToString( );
                    }
                    if ( dt.Rows[n]["Brand"]!=null && dt.Rows[n]["Brand"].ToString( )!="" )
                    {
                        model.Brand=dt.Rows[n]["Brand"].ToString( );
                    }
                    if ( dt.Rows[n]["TypeOfGoods"]!=null && dt.Rows[n]["TypeOfGoods"].ToString( )!="" )
                    {
                        model.TypeOfGoods=dt.Rows[n]["TypeOfGoods"].ToString( );
                    }
                    if ( dt.Rows[n]["Price"]!=null && dt.Rows[n]["Price"].ToString( )!="" )
                    {
                        model.Price=dt.Rows[n]["Price"].ToString( );
                    }
                    if ( dt.Rows[n]["Currency"]!=null && dt.Rows[n]["Currency"].ToString( )!="" )
                    {
                        model.Currency=dt.Rows[n]["Currency"].ToString( );
                    }
                    if ( dt.Rows[n]["HSCode"]!=null && dt.Rows[n]["HSCode"].ToString( )!="" )
                    {
                        model.HSCode=dt.Rows[n]["HSCode"].ToString( );
                    }
                    if ( dt.Rows[n]["TotalValue"]!=null && dt.Rows[n]["TotalValue"].ToString( )!="" )
                    {
                        model.TotalValue=dt.Rows[n]["TotalValue"].ToString( );
                    }
                    if ( dt.Rows[n]["Unit"]!=null && dt.Rows[n]["Unit"].ToString( )!="" )
                    {
                        model.Unit=dt.Rows[n]["Unit"].ToString( );
                    }
                    if ( dt.Rows[n]["NETWeight"]!=null && dt.Rows[n]["NETWeight"].ToString( )!="" )
                    {
                        model.NETWeight=dt.Rows[n]["NETWeight"].ToString( );
                    }
                    if ( dt.Rows[n]["GrossWeight"]!=null && dt.Rows[n]["GrossWeight"].ToString( )!="" )
                    {
                        model.GrossWeight=dt.Rows[n]["GrossWeight"].ToString( );
                    }
                    if ( dt.Rows[n]["CommercialInvoiceNO"]!=null && dt.Rows[n]["CommercialInvoiceNO"].ToString( )!="" )
                    {
                        model.CommercialInvoiceNO=dt.Rows[n]["CommercialInvoiceNO"].ToString( );
                    }
                    if ( dt.Rows[n]["StoreNO"]!=null && dt.Rows[n]["StoreNO"].ToString( )!="" )
                    {
                        model.StoreNO=dt.Rows[n]["StoreNO"].ToString( );
                    }
                    if ( dt.Rows[n]["StoreName"]!=null && dt.Rows[n]["StoreName"].ToString( )!="" )
                    {
                        model.StoreName=dt.Rows[n]["StoreName"].ToString( );
                    }
                    if ( dt.Rows[n]["FileName"]!=null && dt.Rows[n]["FileName"].ToString( )!="" )
                    {
                        model.FileName=dt.Rows[n]["FileName"].ToString( );
                    }
                    if ( dt.Rows[n]["CreateTime"]!=null && dt.Rows[n]["CreateTime"].ToString( )!="" )
                    {
                        model.CreateTime=DateTime.Parse( dt.Rows[n]["CreateTime"].ToString( ) );
                    }
                    if ( dt.Rows[n]["Creator"]!=null && dt.Rows[n]["Creator"].ToString( )!="" )
                    {
                        model.Creator=dt.Rows[n]["Creator"].ToString( );
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
