using System.Collections.Generic;
using ServiceStack;
using WeChat.Models;
using WeChat.ServiceModel.Base;

namespace WeChat.ServiceModel.PrivilegePR
{
    [Route("/StaffRole")]
    public class StaffRole : BaseRequest
    {
        [ApiMember(Name = "RequestType", Description = "请求方法", DataType = "string", IsRequired = true)]
        public short RequestType { get; set; }

        [ApiMember(Name = "StaffNo", Description = "员工编号", DataType = "string", IsRequired = true)]
        public string StaffNo { get; set; }

        [ApiMember(Name = "Roles", Description = "权限", DataType = "IList<string>", IsRequired = true)]
        public IList<string> Roles { get; set; }


    }

    public class StaffRoleResponse : BaseResponse
    {
        [ApiMember(Name = "QueryData", Description = "返回权限信息", DataType = "List", IsRequired = true)]
        public Report QueryData { get; set; }
    }

    public enum StaffRoleRequestType : short
    {
        QueryStaffRole = 0,
        ModifyStaffRole = 1
    }
}
