using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecathlonDataProcessSystem.Model
{

    /// <summary>
    /// T_ParcelSetUp:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ParcelSetUpEntity
    {
        public ParcelSetUpEntity( )
        { }
        #region Model
        private int _itemid;
        private string _localproductname;
        private bool _flag;
        private int? _moreparcelnumber;
        private string _remark;
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
        public bool Flag
        {
            set { _flag=value; }
            get { return _flag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MoreParcelNumber
        {
            set { _moreparcelnumber=value; }
            get { return _moreparcelnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark=value; }
            get { return _remark; }
        }
        #endregion Model

    }
}
