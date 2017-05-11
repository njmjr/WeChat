using WeChat.ServiceModel.PrivilegePR;

namespace WeChat.DomainService.Application.IService
{
    /// <summary>
    /// 员工部门相关应用服务接口
    /// dongx
    /// 20160105
    /// </summary>
    public interface IDeptStaffService : IApplicationService
    {
        void GetAssignedStaff(DeptStaff request, DeptStaffResponse response);
        //获取部门和员工集合
        void GetDepartsAndStaffs(DeptStaff request, DeptStaffResponse response);

        void CheckToken(DeptStaff request, DeptStaffResponse rsp);

        void Verify(ForeVerify request, ForeVerifyResponse response);
    }
}
