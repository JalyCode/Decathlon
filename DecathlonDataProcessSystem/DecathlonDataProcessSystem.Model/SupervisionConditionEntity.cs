using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecathlonDataProcessSystem.Model
{
    /// <summary>
    /// T_SupervisionCondition:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SupervisionConditionEntity
    {
        public SupervisionConditionEntity( )
        { }
        #region Model
        private int _supervisionconditionid;
        private string _supervisionconditionname;
        private string _supervisionconditionremark;
        /// <summary>
        /// 
        /// </summary>
        public int SupervisionConditionID
        {
            set { _supervisionconditionid=value; }
            get { return _supervisionconditionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SupervisionConditionName
        {
            set { _supervisionconditionname=value; }
            get { return _supervisionconditionname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SupervisionConditionRemark
        {
            set { _supervisionconditionremark=value; }
            get { return _supervisionconditionremark; }
        }
        #endregion Model

    }
}
