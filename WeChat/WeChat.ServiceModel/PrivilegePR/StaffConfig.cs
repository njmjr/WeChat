using ServiceStack;
using WeChat.Models;
using WeChat.ServiceModel.Base;

namespace WeChat.ServiceModel.PrivilegePR
{
    [Route("/StaffConfig")]
    public class StaffConfig : BaseRequest
    {
        [ApiMember(Name = "RequestType", Description = "请求方法", DataType = "string", IsRequired = true)]
        public short RequestType { get; set; }

        [ApiMember(Name = "StaffNo", Description = "员工编号", DataType = "string", IsRequired = true)]
        public string StaffNo { get; set; }

        [ApiMember(Name = "StaffName", Description = "员工姓名", DataType = "string", IsRequired = true)]
        public string StaffName { get; set; }

        [ApiMember(Name = "OperatorCardNo", Description = "操作员卡号", DataType = "string", IsRequired = true)]
        public string OperatorCardNo { get; set; }

        [ApiMember(Name = "OperCardPwd", Description = "员工密码", DataType = "string", IsRequired = true)]
        public string OperCardPwd { get; set; }

        [ApiMember(Name = "DepartNo", Description = "部门编号", DataType = "string", IsRequired = true)]
        public string DepartNo { get; set; }

        [ApiMember(Name = "DimissionTag", Description = "是否离职", DataType = "string", IsRequired = true)]
        public string DimissionTag { get; set; }
    }

    public class StaffConfigResponse : BaseResponse
    {
        [ApiMember(Name = "QueryData", Description = "返回员工信息", DataType = "List", IsRequired = true)]
        public Report QueryData { get; set; }
    }

    public enum StaffConfigRequestType : short
    {
        QueryStaffInfo = 0,
        AddStaffInfo = 1,
        ModifyStaffInfo = 2,
        DeleteStaffInfo = 3
    }
}
