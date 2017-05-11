using WeChat.DomainService.Repository.IRepositories;

namespace WeChat.DomainService.Domain.IService
{
    /// <summary>
    /// 员工部门相关领域服务
    /// dongx
    /// 20160104
    /// </summary>
    public interface IDeptStaffManager : IDomainService
    {
        IInsideStaffRepository InsideStaffRepository { get; }
        IRolePowerRepository RolePowerRepository { get; }
        IInsideDepartRepository InsideDepartRepository { get; }

        /// <summary>
        /// 根据工号查询姓名
        /// </summary>
        /// <param name="staffno"></param>
        /// <returns></returns>
        string GetStaffName(string staffno); 
    }
}
