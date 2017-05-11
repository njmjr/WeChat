using ServiceStack;
using WeChat.DomainService.Application.IService;
using WeChat.ServiceModel.PrivilegePR;

namespace WeChat.ServiceInterface
{
    /// <summary>
    /// 系统管理
    /// </summary>
    public class PrivilegeService : Service
    {
        private readonly IPrivilegeService _privilegeService;

        public PrivilegeService(IPrivilegeService privilegeService)
        {
            _privilegeService = privilegeService;
        }

        public override void Dispose()
        {
            base.Dispose();
            _privilegeService.Dispose();
        }

        /// <summary>
        /// 员工维护
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Post(StaffConfig request)
        {
            StaffConfigResponse rsp = new StaffConfigResponse();
            if (request.RequestType == (short)StaffConfigRequestType.QueryStaffInfo)
            {
                _privilegeService.QueryStaffInfo(request, rsp);
            }
            if (request.RequestType == (short)StaffConfigRequestType.AddStaffInfo)
            {
                _privilegeService.AddStaffInfo(request, rsp);
            }
            if (request.RequestType == (short)StaffConfigRequestType.ModifyStaffInfo)
            {
                _privilegeService.ModifyStaffInfo(request, rsp);
            }
            if (request.RequestType == (short)StaffConfigRequestType.DeleteStaffInfo)
            {
                _privilegeService.DeleteStaffInfo(request, rsp);
            }
            return rsp;
        }

        /// <summary>
        /// 角色维护
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Post(RoleConfig request)
        {
            RoleConfigResponse rsp = new RoleConfigResponse();
            if (request.RequestType == (short)RoleConfigRequestType.QueryRole)
            {
                _privilegeService.QueryRoleInfo(request, rsp);
            }
            if (request.RequestType == (short)RoleConfigRequestType.AddRole)
            {
                _privilegeService.AddRole(request, rsp);
            }
            if (request.RequestType == (short)RoleConfigRequestType.ModifyRole)
            {
                _privilegeService.ModifyRole(request, rsp);
            }
            if (request.RequestType == (short)RoleConfigRequestType.DeleteRole)
            {
                _privilegeService.DeleteRole(request, rsp);
            }
            return rsp;
        }

        /// <summary>
        /// 部门维护
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Post(DepartConfig request)
        {
            DepartConfigResponse rsp = new DepartConfigResponse();
            if (request.RequestType == (short)DepartConfigRequestType.QueryDepart)
            {
                _privilegeService.QueryDepartInfo(request, rsp);
            }
            if (request.RequestType == (short)DepartConfigRequestType.AddDepart)
            {
                _privilegeService.AddDepart(request, rsp);
            }
            if (request.RequestType == (short)DepartConfigRequestType.ModifyDepart)
            {
                _privilegeService.ModifyDepart(request, rsp);
            }
            if (request.RequestType == (short)DepartConfigRequestType.DeleteDepart)
            {
                _privilegeService.DeleteDepart(request, rsp);
            }
            return rsp;
        }

        /// <summary>
        /// 员工角色维护
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Post(StaffRole request)
        {
            StaffRoleResponse rsp = new StaffRoleResponse();
            if (request.RequestType == (short)StaffRoleRequestType.QueryStaffRole)
            {
                _privilegeService.QueryStaffRoleInfo(request, rsp);
            }
            if (request.RequestType == (short)StaffRoleRequestType.ModifyStaffRole)
            {
                _privilegeService.ModifyStaffRole(request, rsp);
            }
            return rsp;
        }

        public object Post(RolePower request)
        {
            RolePowerResponse rsp = new RolePowerResponse();
            if (request.RequestType == (short)RolePowerRequestType.Menu)
            {
                _privilegeService.QueryRolePowerMenus(request, rsp);
            }
            else if (request.RequestType == (short)RolePowerRequestType.Handle)
            {
                _privilegeService.QueryRolePowerHandle(request, rsp);
            }
            else if (request.RequestType == (short)RolePowerRequestType.Modify)
            {
                _privilegeService.ModifyRolePower(request, rsp);
            }
            return rsp;
        }
    }
     
}
