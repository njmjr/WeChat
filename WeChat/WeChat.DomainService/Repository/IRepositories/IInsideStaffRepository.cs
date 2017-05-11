using System.Collections.Generic;
using WeChat.Models;

namespace WeChat.DomainService.Repository.IRepositories
{
    public interface IInsideStaffRepository :IRepository<InsideStaff,string>
    {
        /// <summary>
        /// 验证审核员工
        /// </summary>
        /// <param name="operCardNo"></param>
        InsideStaff CheckOperStaff(string operCardNo);

        /// <summary>
        /// 获取有领卡权限的员工列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<InsideStaff> GetAssignedStaff();

        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="departNo"></param>
        /// <param name="staffNo"></param>
        /// <returns></returns>
        IEnumerable<InsideStaff> GetStaffList(string departNo = "", string staffNo = "");

        /// <summary>
        /// 获取代理营业厅员工
        /// </summary>
        /// <param name="dbalUnitNo"></param>
        /// <returns></returns>
        IEnumerable<InsideStaff> GetStaffListForDeptBalunit(string dbalUnitNo);

        /// <summary>
        /// 根据TOKEN获取卡
        /// </summary>
        /// <param name="staffNo"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        InsideStaff GetStaffByToken(string staffNo, string token);
    }
}
