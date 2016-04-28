//------------------------------------------------------------------------------
// 创建标识: 
// 创建描述: 
//
// 功能描述: 
//
// 修改标识: 
// 修改描述: 
//------------------------------------------------------------------------------

using System;

namespace DecathlonDataProcessSystem.Model
{
    /// <summary>
    /// 实体类 EmployeeEntity
    /// </summary>
    public class EmployeeEntity
    {
        public EmployeeEntity()
        { }
        #region Model
        private string _employeeid;
        private string _employeename;
        private string _password;
        /// <summary>
        /// 
        /// </summary>
        public string EmployeeId
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        #endregion Model
    }
}
