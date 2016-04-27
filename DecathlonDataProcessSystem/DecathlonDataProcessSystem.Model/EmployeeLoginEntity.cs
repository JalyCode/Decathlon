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
    /// 实体类 T_EmployeeLogin
    /// </summary>
    public class EmployeeLoginEntity
    {
        public EmployeeLoginEntity()
        { }
        #region Model
        private int _loginid;
        private string _employeeid;
        private string _ipaddress;
        private string _macaddress;
        private DateTime _logintime;
        private bool _flag;
        private DateTime _currenttime;
        private DateTime _logouttime;
        /// <summary>
        /// 
        /// </summary>
        public int LoginId
        {
            set { _loginid = value; }
            get { return _loginid; }
        }
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
        public string IpAddress
        {
            set { _ipaddress = value; }
            get { return _ipaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MacAddress
        {
            set { _macaddress = value; }
            get { return _macaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LoginTime
        {
            set { _logintime = value; }
            get { return _logintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Flag
        {
            set { _flag = value; }
            get { return _flag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CurrentTime
        {
            set { _currenttime = value; }
            get { return _currenttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LogoutTime
        {
            set { _logouttime = value; }
            get { return _logouttime; }
        }
        #endregion Model
    }
}

