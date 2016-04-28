using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecathlonDataProcessSystem.DAL;
using System.Data;

namespace DecathlonDataProcessSystem.BLL
{

    /// <summary>
    /// T_SetBreakUp
    /// </summary>
    public class SetBreakUpBLL
    {
        private readonly SetBreakUpDAL dal=new SetBreakUpDAL( );
        public SetBreakUpBLL( )
        { }
        public void BulkSETBreakUpInsert( DataTable dataTable , int batchSize = 10000 )
        {
            dal.DeleteAll( );
            dal.BulkSETBreakUpInsert( dataTable , batchSize );
        }
        #region  Method

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList( string strWhere )
        {
            return dal.GetList( strWhere );
        }
        public DataSet GetSetBreakUpList( string strWhere )
        {
            return dal.GetSetBreakUpList( strWhere );
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
