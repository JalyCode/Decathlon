//------------------------------------------------------------------------------
// ������ʶ: 
// ��������: 
//
// ��������: 
//
// �޸ı�ʶ: 
// �޸�����: 
//------------------------------------------------------------------------------

using System;

namespace DecathlonDataProcessSystem.Model
{
    /// <summary>
    /// ʵ���� EmployeeEntity
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
