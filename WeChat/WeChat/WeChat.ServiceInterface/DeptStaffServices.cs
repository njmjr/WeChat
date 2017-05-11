using ServiceStack;
using WeChat.DomainService.Application.IService;
using WeChat.ServiceModel.PrivilegePR;

namespace WeChat.ServiceInterface
{
    /// <summary>
    /// 获取领用员工
    /// </summary>
    public class DeptStaffServices : Service
    {
        private readonly IDeptStaffService _deptStaffService;

        public DeptStaffServices(IDeptStaffService deptStaffService)
        {
            _deptStaffService = deptStaffService;
        }

        public override void Dispose()
        {
            base.Dispose();
            _deptStaffService.Dispose();
        }

        public object Post(DeptStaff request)
        {
            DeptStaffResponse rsp = new DeptStaffResponse();
            if (request.RequestType == (short)DeptStaffRequestType.CardStockOut)
            {
                _deptStaffService.GetAssignedStaff(request, rsp);
            }
            else if (request.RequestType == (short)DeptStaffRequestType.Report)
            {
                _deptStaffService.GetDepartsAndStaffs(request, rsp);
            }
            else if (request.RequestType == (short)DeptStaffRequestType.CheckToken)
            {
                _deptStaffService.CheckToken(request, rsp);
            }
            return rsp;
        }

        public object Post(ForeVerify request)
        {
            ForeVerifyResponse rsp = new ForeVerifyResponse();
            _deptStaffService.Verify(request, rsp);
            return rsp;
        }
    }
     
}
