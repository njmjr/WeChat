using System;
using System.Collections.Generic;
using System.Linq;
using WeChat.DomainService.Application.IService;
using WeChat.DomainService.Interceptors;
using WeChat.DomainService.Repository.IRepositories;
using WeChat.Models;
using WeChat.ServiceModel.PrivilegePR;
using WeChat.ServiceModel.Validatores;
using WeChat.Utility.Secutiry;
using RolePower = WeChat.ServiceModel.PrivilegePR.RolePower;

namespace WeChat.DomainService.Application.Service
{
    /// <summary>
    /// 系统管理相关服务
    /// </summary> 
    public class PrivilegeService : ApplicationService, IPrivilegeService
    {
        private readonly IPrivilegeRepository _privilegeRepository;
        private readonly IInsideDepartRepository _insideDepartRepository;
        private readonly IRoleRepository _roleRepository;

        public PrivilegeService(IPrivilegeRepository privilegeRepository, IInsideDepartRepository insideDepartRepository, IRoleRepository roleRepository)
        {
            _privilegeRepository = privilegeRepository;
            _insideDepartRepository = insideDepartRepository;
            _roleRepository = roleRepository;

            TransientDependencies.AddRange(new List<ITransientDependency>
            {
                _privilegeRepository,
                _insideDepartRepository,
                _roleRepository

            });
        }

        /// <summary>
        /// 查询员工信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public void QueryStaffInfo(StaffConfig request, StaffConfigResponse response)
        {
            var list = _privilegeRepository.QueryStaffInfo(request.DepartNo, request.StaffNo);
            response.QueryData = new Report { total = list.Count, rows = list };
            response.ResponseStatus.ErrorCode = "OK";
        }

        /// <summary>
        /// 新增员工信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [Transaction]
        public void AddStaffInfo(StaffConfig request, StaffConfigResponse response)
        {
            ValidRequest(request, new StaffConfigVaildator(), ruleSet: "Add");
            //验证员工编号是否重复
            var staffConfigs = _privilegeRepository.QueryStaffInfo();
            if (staffConfigs.Any(i => i.STAFFNO == request.StaffNo))
            {
                throw new Utility.WeChatException("STAFF_ADD_ERROR", "员工号在库中存在重复记录");
            }

            DateTime updateTime = DateTime.Now;
            _privilegeRepository.InsertStaffInfo(request.StaffNo, request.StaffName,
                DecryptPwdHelper.EncodePwd("123456"),
                request.DepartNo, request.DimissionTag, request.CurrOper, updateTime);

            response.ResponseStatus.ErrorCode = "OK";
        }

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [Transaction]
        public void ModifyStaffInfo(StaffConfig request, StaffConfigResponse response)
        {
            ValidRequest(request, new StaffConfigVaildator(), ruleSet: "Edit");
            //验证员工编号是否存在
            var staffConfigs = _privilegeRepository.QueryStaffInfo();
            if (!(staffConfigs.Any(i => i.STAFFNO == request.StaffNo)))
            {
                throw new Utility.WeChatException("STAFF_EDIT_ERROR", "当前员工编号在库中不存在，无法编辑");
            }
            DateTime updateTime = DateTime.Now;
            _privilegeRepository.UpdateStaffInfo(request.StaffNo, request.StaffName,
                request.DepartNo, request.DimissionTag, request.CurrOper, updateTime);

            response.ResponseStatus.ErrorCode = "OK";
        }

        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [Transaction]
        public void DeleteStaffInfo(StaffConfig request, StaffConfigResponse response)
        {
            ValidRequest(request, new StaffConfigVaildator(), ruleSet: "Delete");
            //验证员工编号是否存在
            var staffConfigs = _privilegeRepository.QueryStaffInfo();
            if (!(staffConfigs.Any(i => i.STAFFNO == request.StaffNo)))
            {
                throw new Utility.WeChatException("STAFF_DELETE_ERROR", "当前员工编号在库中不存在，无法删除");
            }
            _privilegeRepository.DeleteStaffInfo(request.StaffNo);

            response.ResponseStatus.ErrorCode = "OK";
        }

        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public void QueryDepartInfo(DepartConfig request, DepartConfigResponse response)
        {
            var list = _insideDepartRepository.QueryDepartInfo(request.DepartNo);
            response.QueryData = new Report { total = list.Count, rows = list };
            response.ResponseStatus.ErrorCode = "OK";
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [Transaction]
        public void AddDepart(DepartConfig request, DepartConfigResponse response)
        {
            ValidRequest(request, new DepartConfigVaildator(), ruleSet: "Add");
            //验证部门编号是否重复
            var departConfigs = _insideDepartRepository.QueryDepartInfo();
            if (departConfigs.Any(i => i.DEPARTNO == request.DepartNo))
            {
                throw new Utility.WeChatException("DEPART_ADD_ERROR", "部门编号已存在");
            }
            _insideDepartRepository.InsertDepart(request.DepartNo, request.DepartName, request.CurrOper, request.Remark);
            response.ResponseStatus.ErrorCode = "OK";
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [Transaction]
        public void ModifyDepart(DepartConfig request, DepartConfigResponse response)
        {
            ValidRequest(request, new DepartConfigVaildator(), ruleSet: "Edit");
            //验证部门编号是否存在
            var departConfigs = _insideDepartRepository.QueryDepartInfo();
            if (!(departConfigs.Any(i => i.DEPARTNO == request.DepartNo)))
            {
                throw new Utility.WeChatException("DEPART_EDIT_ERROR", "当前部门编号不存在");
            }
            _insideDepartRepository.UpdateDepartInfo(request.DepartNo, request.DepartName, request.CurrOper, request.Remark);
            response.ResponseStatus.ErrorCode = "OK";
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [Transaction]
        public void DeleteDepart(DepartConfig request, DepartConfigResponse response)
        {
            ValidRequest(request, new DepartConfigVaildator(), ruleSet: "Delete");
            //验证部门编号是否存在
            var departConfigs = _insideDepartRepository.QueryDepartInfo();
            if (!(departConfigs.Any(i => i.DEPARTNO == request.DepartNo)))
            {
                throw new Utility.WeChatException("DEPART_DELETE_ERROR", "当前部门编号不存在");
            }
            _insideDepartRepository.DeleteDepart(request.DepartNo);
            response.ResponseStatus.ErrorCode = "OK";
        }

        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public void QueryRoleInfo(RoleConfig request, RoleConfigResponse response)
        {
            var list = _roleRepository.QueryRoleInfo(request.RoleNo);
            response.QueryData = new Report { total = list.Count, rows = list };
            response.ResponseStatus.ErrorCode = "OK";
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [Transaction]

        public void AddRole(RoleConfig request, RoleConfigResponse response)
        {
            ValidRequest(request, new RoleConfigVaildator(), ruleSet: "Add");
            //验证角色编号是否重复
            var rolsConfigs = _roleRepository.QueryRoleInfo();
            if (rolsConfigs.Any(i => i.ROLENO == request.RoleNo))
            {
                throw new Utility.WeChatException("ROLE_ADD_ERROR", "角色编号已存在");
            }
            _roleRepository.InsertRole(request.RoleNo, request.RoleName, request.CurrOper, request.Remark);
            response.ResponseStatus.ErrorCode = "OK";
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [Transaction]
        public void ModifyRole(RoleConfig request, RoleConfigResponse response)
        {
            ValidRequest(request, new RoleConfigVaildator(), ruleSet: "Edit");
            //验证部门编号是否存在
            var rolsConfigs = _roleRepository.QueryRoleInfo();
            if (!(rolsConfigs.Any(i => i.ROLENO == request.RoleNo)))
            {
                throw new Utility.WeChatException("ROLE_EDIT_ERROR", "当前角色编号不存在");
            }
            _roleRepository.UpdateRoleInfo(request.RoleNo, request.RoleName, request.CurrOper, request.Remark);
            response.ResponseStatus.ErrorCode = "OK";
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [Transaction]
        public void DeleteRole(RoleConfig request, RoleConfigResponse response)
        {
            ValidRequest(request, new RoleConfigVaildator(), ruleSet: "Delete");
            //验证角色编号是否存在
            var rolsConfigs = _roleRepository.QueryRoleInfo();
            if (!(rolsConfigs.Any(i => i.ROLENO == request.RoleNo)))
            {
                throw new Utility.WeChatException("ROLE_DELETE_ERROR", "当前角色编号不存在");
            }
            _roleRepository.DeleteRole(request.RoleNo);
            response.ResponseStatus.ErrorCode = "OK";
        }

        public void QueryStaffRoleInfo(StaffRole request, StaffRoleResponse response)
        {
            ValidRequest(request, new StaffRoleVaildator());
            var list = _roleRepository.QueryStaffRoleInfo(request.StaffNo);
            response.QueryData = new Report { total = list.Count, rows = list };
            response.ResponseStatus.ErrorCode = "OK";
        }

        [Transaction]
        public void ModifyStaffRole(StaffRole request, StaffRoleResponse response)
        {
            ValidRequest(request, new StaffRoleVaildator());
            _roleRepository.DeleteStaffRoleInfo(request.StaffNo);
            _roleRepository.UpdateStaffRoleInfo(request.StaffNo, request.Roles.ToList(), request.CurrOper);
            response.ResponseStatus.ErrorCode = "OK";
        }

        public void QueryRolePowerHandle(RolePower request, RolePowerResponse response)
        {
            ValidRequest(request, new RolePowerVaildator(), ruleSet: "Query");
            var list = _roleRepository.QueryRolePowerHandle(request.RoleNo);
            response.QueryData = new Report { total = list.Count, rows = list };
            response.ResponseStatus.ErrorCode = "OK";

        }

        [Transaction]
        public void QueryRolePowerMenus(RolePower request, RolePowerResponse response)
        {
            var menuPris = _roleRepository.QueryRolePowerMenus(request.RoleNo).ToList();
            List<MenuPri> menuPristmp = new List<MenuPri>();

            foreach (var tmp in menuPris)
            {
                tmp.attributes = new Attributes
                {
                    Pid = tmp.Pid
                };
                menuPristmp.Add(tmp);
            }
            //构造菜单 
            Func<MenuPri, IEnumerable<MenuPri>, MenuPriView> getMenuPriTree = null;
            getMenuPriTree = (menu, source) =>
            {
                MenuPriView view = new MenuPriView(menu);
                var enumerable = source as MenuPri[] ?? source.ToArray();
                List<MenuPri> children = enumerable.Where(m => m.attributes.Pid == menu.id).OrderBy(m => m.id).ToList();
                foreach (MenuPri child in children)
                {
                    MenuPriView childView = getMenuPriTree(child, enumerable);
                    view.children.Add(childView);
                }
                return view;
            };
            List<MenuPri> roots = menuPristmp.Where(m => m.attributes.Pid == "000000").OrderBy(m => m.id).ToList();
            List<MenuPriView> datas = (from root in roots
                                       let source = menuPristmp.Where(m => m.attributes.Pid.Equals(root.id)).ToList()
                                       select getMenuPriTree(root, source)).ToList();

            MenuPriView mp = new MenuPriView(new MenuPri {id = "000000", text = "页面根目录",state="open"}) {children = datas};
            List<MenuPriView> datatmp = new List<MenuPriView> {mp};
            response.Menus = datatmp;
            response.ResponseStatus.ErrorCode = "OK";
        }

        [Transaction]
        public void ModifyRolePower(RolePower request, RolePowerResponse response)
        {
            ValidRequest(request, new RolePowerVaildator(), ruleSet: "Modify");
            _roleRepository.DeleteAllRolePower(request.RoleNo);
            _roleRepository.InsertRoleMenus(request.RoleNo, request.Menus);
            _roleRepository.InsertRoleHandles(request.RoleNo,request.Handles);
            _roleRepository.InsertRoleMenuFather(request.RoleNo);
            response.ResponseStatus.ErrorCode = "OK";
        }
    }
}
