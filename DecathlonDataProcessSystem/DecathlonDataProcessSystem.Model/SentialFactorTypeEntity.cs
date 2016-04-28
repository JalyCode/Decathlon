using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecathlonDataProcessSystem.Model
{
    /// <summary>
    /// T_SentialFactorType:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SentialFactorTypeEntity
    {
        public SentialFactorTypeEntity( )
        { }
        #region Model
        private int _sentialfactortypeid;
        private string _sentialfactortypename;
        private string _sentialfactortypedescription;
        /// <summary>
        /// 
        /// </summary>
        public int SentialFactorTypeID
        {
            set { _sentialfactortypeid=value; }
            get { return _sentialfactortypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SentialFactorTypeName
        {
            set { _sentialfactortypename=value; }
            get { return _sentialfactortypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SentialFactorTypeDescription
        {
            set { _sentialfactortypedescription=value; }
            get { return _sentialfactortypedescription; }
        }
        #endregion Model

    }
}
