using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecathlonDataProcessSystem.Model
{
    /// <summary>
    /// T_ParcelTakeOutCondition:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ParcelTakeOutConditionEntity
    {
        public ParcelTakeOutConditionEntity( )
        { }
        #region Model
        private int _itemid;
        private string _itemcode;
        private string _modelcode;
        private string _localproductname;
        private int _minexportqty;
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
        public string ItemCode
        {
            set { _itemcode=value; }
            get { return _itemcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModelCode
        {
            set { _modelcode=value; }
            get { return _modelcode; }
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
        public int MinExportQTY
        {
            set { _minexportqty=value; }
            get { return _minexportqty; }
        }
        #endregion Model

    }
}
