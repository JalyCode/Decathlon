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
using System.Data;
using DecathlonDataProcessSystem.Model;
using System.Collections.Generic;
using System.Text;
using DecathlonDataProcessSystem.DAL;

namespace DecathlonDataProcessSystem.BLL
{
    /// <summary>
    /// 业务逻辑类 EmployeeLoginBLL
    /// </summary>
    public class EmployeeLoginBLL
    {
        private readonly EmployeeLoginDAL dal = new EmployeeLoginDAL();
        public EmployeeLoginBLL()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddEmployeeLogin(EmployeeLoginEntity model)
        {
            return dal.AddEmployeeLogin(model);
        }

        /// <summary>
        /// 增加多条数据
        /// </summary>
        public void AddEmployeeLoginList(List<EmployeeLoginEntity> l)
        {
            foreach (EmployeeLoginEntity model in l)
                dal.AddEmployeeLogin(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateEmployeeLogin(EmployeeLoginEntity model)
        {
            return dal.UpdateEmployeeLogin(model);
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        public void UpdateEmployeeLoginList(List<EmployeeLoginEntity> l)
        {
            foreach (EmployeeLoginEntity model in l)
                dal.UpdateEmployeeLogin(model);
        }
        /// <summary>
        /// 用户登出
        /// </summary>
        public bool EmployeeLogout(EmployeeLoginEntity model)
        {
            return dal.EmployeeLogout(model);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteEmployeeLogin(int LoginId)
        {
            return dal.DeleteEmployeeLogin(LoginId);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        public void DeleteEmployeeLoginList(List<int> l)
        {
            foreach (int LoginId in l)
                dal.DeleteEmployeeLogin(LoginId);
        }

        /// <summary>
        /// 将未登陆的无效信息删除
        /// </summary>
        public bool DeleteEmployeeLogin(string EmployeeId, string strIP)
        {
            return dal.DeleteEmployeeLogin(EmployeeId, strIP);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool EmployeeLoginExists(int LoginId)
        {
            return dal.EmployeeLoginExists(LoginId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EmployeeLoginEntity GetEmployeeLoginByLoginId(int LoginId)
        {
            return dal.GetEmployeeLoginByLoginId(LoginId);
        }

        public DataSet GetEmployeeLoginByEmployeeId(string EmployeeId)
        {
            return dal.GetEmployeeLoginByEmployeeId(EmployeeId);
        }

        public DataSet GetDsEmployeeInfo(string logon_No)
        {
            return dal.GetDsEmployeeInfo(logon_No);
        }
        /// <summary>
        /// 获得泛型数据列表
        /// </summary>
        public DataSet GetAllEmployeeLoginList()
        {
            return dal.GetEmployeeLoginList();
        }

        /// <summary>
        /// 分页获取泛型数据列表,不建议直接使用此方法,请根据业务逻辑重写
        /// </summary>
        public DataSet GetEmployeeLoginPageList(int pageSize, int pageIndex, string fldSort, bool Sort, string strCondition, out int pageCount, out int Counts)
        {
            return dal.GetEmployeeLoginPageList(pageSize, pageIndex, fldSort, Sort, strCondition, out pageCount, out Counts);
        }
        #endregion
    }
}



