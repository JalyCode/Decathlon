using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecathlonDataProcessSystem.Model
{
    /// <summary>
    /// 用户信息实体类
    /// </summary>
    [Serializable]
    public class SysUserInfoEntity
    {
        public SysUserInfoEntity()
        { }
        #region Model
        private int _id;
        private string _jobno;
        private string _jobpwd;
        private string _name;
        private string _createtime;
        private string _modifypwdtime;
        private int _online;
        private string _rolename;
        /// <summary>
        /// 人员表中Id
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 工号
        /// </summary>
        public string JobNo
        {
            set { _jobno = value; }
            get { return _jobno; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string JobPwd
        {
            set { _jobpwd = value; }
            get { return _jobpwd; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string ModifyPwdTime
        {
            set { _modifypwdtime = value; }
            get { return _modifypwdtime; }
        }
        public int OnLine
        {
            set { _online = value; }
            get { return _online; }
        }
        /// <summary>
        /// 所属角色
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        #endregion Model

    }
}
