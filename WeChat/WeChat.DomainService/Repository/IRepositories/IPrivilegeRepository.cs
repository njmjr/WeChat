using System;
using System.Collections.Generic;
using WeChat.Models;

namespace WeChat.DomainService.Repository.IRepositories
{
    public interface IPrivilegeRepository : IRepository<InsideStaff, string>
    {
        /// <summary>
        /// 查询员工信息
        /// </summary>
        /// <param name="departNo"></param>
        /// <param name="staffNo"></param>
        /// <returns></returns>
        List<dynamic> QueryStaffInfo(string departNo = null, string staffNo = null);

        /// <summary>
        /// 新增员工信息
        /// </summary>
        /// <param name="staffNo"></param>
        /// <param name="staffName"></param>
        /// <param name="operCardPwd"></param>
        /// <param name="departNo"></param>
        /// <param name="dimissionTag"></param>
        /// <param name="curOper"></param>
        /// <param name="updateTime"></param>
        void InsertStaffInfo(string staffNo, string staffName, string operCardPwd, string departNo, string dimissionTag, string curOper, DateTime updateTime);

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="staffNo"></param>
        /// <param name="staffName"></param>
        /// <param name="departNo"></param>
        /// <param name="dimissionTag"></param>
        /// <param name="curOper"></param>
        /// <param name="updateTime"></param>
        void UpdateStaffInfo(string staffNo, string staffName, string departNo, string dimissionTag, string curOper, DateTime updateTime);

        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="staffNo"></param>
        void DeleteStaffInfo(string staffNo);
    }
}
