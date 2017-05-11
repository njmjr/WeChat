using System;
using System.Collections.Generic;
using WeChat.DomainService.Application.IService;
using WeChat.DomainService.Domain.IService;
using WeChat.DomainService.Interceptors;
using WeChat.Models;
using WeChat.ServiceModel.PrivilegePR;

namespace WeChat.DomainService.Application.Service
{
    public class DeptStaffService : ApplicationService, IDeptStaffService
    {
        private readonly IDeptStaffManager _deptStaffManager;

        public DeptStaffService(IDeptStaffManager deptStaffManager)
        {
            _deptStaffManager = deptStaffManager;
            TransientDependencies.Add(_deptStaffManager);
        }

        public virtual void GetAssignedStaff(DeptStaff request, DeptStaffResponse response)
        {
            response.Staffs = _deptStaffManager.InsideStaffRepository.GetAssignedStaff();
        }

        public virtual void GetDepartsAndStaffs(DeptStaff request, DeptStaffResponse response)
        {
            IEnumerable<InsideDepart> departs;
            IEnumerable<InsideStaff> staffs;
            if (_deptStaffManager.RolePowerRepository.HasOperPower("201001", request.CurrOper))//主管
            {
                //初始化部门
                departs = _deptStaffManager.InsideDepartRepository.GetDepartList();
                staffs = _deptStaffManager.InsideStaffRepository.GetStaffList();
            }
            else if (_deptStaffManager.RolePowerRepository.HasOperPower("201002", request.CurrOper))//经理
            {
                departs = _deptStaffManager.InsideDepartRepository.GetDepartList(request.CurrDept);
                staffs = _deptStaffManager.InsideStaffRepository.GetStaffList(request.CurrDept);
            }
            else//员工
            {
                departs = _deptStaffManager.InsideDepartRepository.GetDepartList(request.CurrDept);
                staffs = _deptStaffManager.InsideStaffRepository.GetStaffList(staffNo: request.CurrOper);
            }
            response.Departs = departs;
            response.Staffs =  staffs;
        }

        [Transaction]
        public virtual void CheckToken(DeptStaff request, DeptStaffResponse response)
        {
            //验证旧Token
            var staff = _deptStaffManager.InsideStaffRepository.GetStaffByToken(request.StaffNo, request.LinkToken); 
            if (staff == null)
            {
                throw new Utility.WeChatException("SELECT_STAFF", "查询用户失败");
            }
            staff.TOKEN = Guid.NewGuid().ToString();
            _deptStaffManager.InsideStaffRepository.Update(staff); 
            response.LinkToken = staff.TOKEN; 
            response.Result = "OK";
        }

        public void Verify(ForeVerify request, ForeVerifyResponse response)
        {
            //验证员工页面权限 
            response.ResponseStatus.ErrorCode = _deptStaffManager.RolePowerRepository.Verify(request.StaffNo, request.Url.ToUpper()) ? "OK" : "Error";
        }
    }
}
