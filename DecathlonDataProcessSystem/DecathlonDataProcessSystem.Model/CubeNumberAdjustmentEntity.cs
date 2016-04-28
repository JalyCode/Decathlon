using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecathlonDataProcessSystem.Model
{
    /// <summary>
    /// CubeNumberAdjustmentEntity:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class CubeNumberAdjustmentEntity
    {
        public CubeNumberAdjustmentEntity( )
        { }
        #region Model
        private int _itemid;
        private string _shippingnumber;
        private string _confirmtype;
        private int _totalparcelnumber;
        private decimal _totalcubenumber;
        /// <summary>
        /// 
        /// </summary>
        public int ItemID
        {
            set { _itemid=value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShippingNumber
        {
            set { _shippingnumber=value; }
            get { return _shippingnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ConfirmType
        {
            set { _confirmtype=value; }
            get { return _confirmtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TotalParcelNumber
        {
            set { _totalparcelnumber=value; }
            get { return _totalparcelnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalCubeNumber
        {
            set { _totalcubenumber=value; }
            get { return _totalcubenumber; }
        }
        #endregion Model

    }
}
