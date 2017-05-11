using System.Collections.Generic;
using WeChat.Models;

namespace WeChat.DomainService.Repository.IRepositories
{
    public interface IRoleRepository : IRepository<Role, string>
    {
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="roleNo"></param>
        /// <returns></returns>
        List<dynamic> QueryRoleInfo(string roleNo = null);

        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="roleNo"></param>
        /// <param name="roleName"></param>
        /// <param name="curOper"></param>
        /// <param name="remark"></param>
        void InsertRole(string roleNo, string roleName, string curOper, string remark);

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="roleNo"></param>
        /// <param name="roleName"></param>
        /// <param name="curOper"></param>
        /// <param name="remark"></param>
        void UpdateRoleInfo(string roleNo, string roleName, string curOper, string remark);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleNo"></param>
        void DeleteRole(string roleNo);

        /// <summary>
        /// 查询角色权限
        /// </summary>
        /// <param name="stffNo"></param>
        /// <returns></returns>
        List<dynamic> QueryStaffRoleInfo(string stffNo);

        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="stffNo"></param>
        /// <param name="roles"></param>
        /// <param name="curOper"></param>
        void UpdateStaffRoleInfo(string stffNo, IEnumerable<string> roles, string curOper);

        /// <summary>
        /// 删除所有权限
        /// </summary>
        /// <param name="stffNo"></param>
        void DeleteStaffRoleInfo(string stffNo);

        /// <summary>
        /// 查询角色操作权限
        /// </summary>
        /// <param name="roleNo"></param>
        /// <returns></returns>
        List<dynamic> QueryRolePowerHandle(string roleNo = null);

        /// <summary>
        /// 查询角色菜单权限
        /// </summary>
        /// <param name="roleNo"></param>
        /// <returns></returns>
        IEnumerable<MenuPri> QueryRolePowerMenus(string roleNo = null);

        /// <summary>
        /// 删除所有权限
        /// </summary>
        /// <param name="roleNo"></param>
        void DeleteAllRolePower(string roleNo);

        /// <summary>
        /// 新增角色菜单权限
        /// </summary>
        /// <param name="roleNo"></param>
        /// <param name="menus"></param>
        void InsertRoleMenus(string roleNo, IEnumerable<string> menus);

        /// <summary>
        /// 新增角色操作权限
        /// </summary>
        /// <param name="roleNo"></param>
        /// <param name="handles"></param>
        void InsertRoleHandles(string roleNo, IEnumerable<string> handles);

        /// <summary>
        /// 新增角色菜单父节点权限
        /// </summary>
        /// <param name="roleNo"></param>
        void InsertRoleMenuFather(string roleNo);

    }
}
