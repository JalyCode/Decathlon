//------------------------------------------------------------------------------
// ������ʶ: Copyright (C) 2010 Socansoft.com ��Ȩ����
// ��������: SocanCode�����������Զ������� 2010-4-8 15:54:43
//
// ��������: 
//
// �޸ı�ʶ: 
// �޸�����: 
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DecathlonDataProcessSystem.DAL;
using DecathlonDataProcessSystem.Model;

namespace DecathlonDataProcessSystem.BLL
{
    /// <summary>
    /// ҵ���߼��� EmployeeBLL
    /// </summary>
    public class EmployeeBLL
    {
        private readonly EmployeeDAL dal = new EmployeeDAL();
        public EmployeeBLL()
        { }

        #region  ��Ա����
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool EmployeeExists(string EmployeeId)
        {
            return dal.EmployeeExists(EmployeeId);
        }
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool ValidateEmployee(string EmployeeId,string password)
        {
            return dal.ValidateEmployee(EmployeeId, password);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public EmployeeEntity GetEmployeeByEmployeeId(string EmployeeId)
        {
            return dal.GetEmployeeByEmployeeId(EmployeeId);
        }

        #endregion
    }
}
