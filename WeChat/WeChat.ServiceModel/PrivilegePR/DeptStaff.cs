using System.Collections.Generic;
using ServiceStack;
using WeChat.Models;
using WeChat.ServiceModel.Base;

namespace WeChat.ServiceModel.PrivilegePR
{
    /// <summary>
    /// 员工部门
    /// </summary>
    [Route("/DeptStaff")]
    public class DeptStaff : BaseRequest
    {
        public short RequestType { get; set; }

        public string StaffNo { get; set; }

        public string LinkToken { get; set; }
    }

    public class DeptStaffResponse : BaseResponse
    {
        public IEnumerable<InsideStaff> Staffs { get; set; }
        public IEnumerable<InsideDepart> Departs { get; set; }
        public string LinkToken { get; set; }
        public string Result { get; set; }
    }

    public enum DeptStaffRequestType : short
    {
        Report,
        CardStockOut,
        CheckToken
    }
}
