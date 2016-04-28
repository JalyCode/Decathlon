//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 Socansoft.com 版权所有
// 创建描述: SocanCode代码生成器自动创建于 2010-4-8 15:54:43
//
// 功能描述: 
//
// 修改标识: 
// 修改描述: 
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
    /// 业务逻辑类 EmployeeBLL
    /// </summary>
    public class EmployeeBLL
    {
        private readonly EmployeeDAL dal = new EmployeeDAL();
        public EmployeeBLL()
        { }

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool EmployeeExists(string EmployeeId)
        {
            return dal.EmployeeExists(EmployeeId);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ValidateEmployee(string EmployeeId,string password)
        {
            return dal.ValidateEmployee(EmployeeId, password);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EmployeeEntity GetEmployeeByEmployeeId(string EmployeeId)
        {
            return dal.GetEmployeeByEmployeeId(EmployeeId);
        }

        #endregion
    }
}
