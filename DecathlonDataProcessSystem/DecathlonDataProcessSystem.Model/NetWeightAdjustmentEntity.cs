using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecathlonDataProcessSystem.Model
{
    /// <summary>
    /// T_NetWeightAdjustment:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class NetWeightAdjustmentEntity
    {
        public NetWeightAdjustmentEntity( )
        { }
        #region Model
        private int _itemid;
        private string _localproductname;
        private decimal _adjustratio;
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
        public string LocalProductName
        {
            set { _localproductname=value; }
            get { return _localproductname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal AdjustRatio
        {
            set { _adjustratio=value; }
            get { return _adjustratio; }
        }
        #endregion Model

    }
}
