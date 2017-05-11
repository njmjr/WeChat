using WeChat.ServiceModel.PrivilegePR;

namespace WeChat.DomainService.Application.IService
{
    /// <summary>
    /// 系统管理相关应用服务接口
    /// </summary>
    public interface IPrivilegeService : IApplicationService
    {     
        /// <summary>
        /// 查询员工信息
        /// </summary>
        /// <param name="request">请求dto</param>
        /// <param name="response">响应dto</param>
        void QueryStaffInfo(StaffConfig request, StaffConfigResponse response);

        /// <summary>
        /// 新增员工信息
        /// </summary>
        /// <param name="request">请求dto</param>
        /// <param name="response">响应dto</param>
        void AddStaffInfo(StaffConfig request, StaffConfigResponse response);

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="request">请求dto</param>
        /// <param name="response">响应dto</param>
        void ModifyStaffInfo(StaffConfig request, StaffConfigResponse response);

        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="request">请求dto</param>
        /// <param name="response">响应dto</param>
        void DeleteStaffInfo(StaffConfig request, StaffConfigResponse response);

        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <param name="request">请求dto</param>
        /// <param name="response">响应dto</param>
        void QueryDepartInfo(DepartConfig request, DepartConfigResponse response);

        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="request">请求dto</param>
        /// <param name="response">响应dto</param>
        void AddDepart(DepartConfig request, DepartConfigResponse response);

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="request">请求dto</param>
        /// <param name="response">响应dto</param>
        void ModifyDepart(DepartConfig request, DepartConfigResponse response);

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="request">请求dto</param>
        /// <param name="response">响应dto</param>
        void DeleteDepart(DepartConfig request, DepartConfigResponse response);

        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="request">请求dto</param>
        /// <param name="response">响应dto</param>
        void QueryRoleInfo(RoleConfig request, RoleConfigResponse response);

        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="request">请求dto</param>
        /// <param name="response">响应dto</param>
        void AddRole(RoleConfig request, RoleConfigResponse response);

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="request">请求dto</param>
        /// <param name="response">响应dto</param>
        void ModifyRole(RoleConfig request, RoleConfigResponse response);

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="request">请求dto</param>
        /// <param name="response">响应dto</param>
        void DeleteRole(RoleConfig request, RoleConfigResponse response);

        /// <summary>
        /// 查询角色权限
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void QueryStaffRoleInfo(StaffRole request, StaffRoleResponse response);

        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void ModifyStaffRole(StaffRole request, StaffRoleResponse response);

        /// <summary>
        /// 查询角色操作权限
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void QueryRolePowerHandle(RolePower request, RolePowerResponse response);

        /// <summary>
        /// 查看角色菜单权限
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void QueryRolePowerMenus(RolePower request, RolePowerResponse response);

        /// <summary>
        /// 修改角色权限
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void ModifyRolePower(RolePower request, RolePowerResponse response);
       
    }
}
