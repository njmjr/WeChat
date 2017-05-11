using System;
using ServiceStack;
using WeChat.Models;
using WeChat.ServiceModel.Base;

namespace WeChat.ServiceModel.PrivilegePR
{
    [Route("/DepartConfig")]
    public class DepartConfig : BaseRequest
    {
        [ApiMember(Name = "RequestType", Description = "请求方法", DataType = "string", IsRequired = true)]
        public short RequestType { get; set; }

        [ApiMember(Name = "DepartNo", Description = "部门编号", DataType = "string", IsRequired = true)]
        public string DepartNo { get; set; }
        [ApiMember(Name = "DepartName", Description = "部门名称", DataType = "string", IsRequired = true)]
        public string DepartName { get; set; }
        [ApiMember(Name = "Updatestaffno", Description = "更新员工", DataType = "string", IsRequired = true)]
        public string Updatestaffno { get; set; }
        [ApiMember(Name = "Updatetime", Description = "更新时间", DataType = "string", IsRequired = true)]
        public DateTime Updatetime { get; set; }
        [ApiMember(Name = "Remark", Description = "备注", DataType = "string", IsRequired = true)]
        public string Remark { get; set; }
        [ApiMember(Name = "Usetag", Description = "有效标识", DataType = "string", IsRequired = true)]
        public string Usetag { get; set; }
    }

    public class DepartConfigResponse : BaseResponse
    {
        [ApiMember(Name = "QueryData", Description = "返回部门信息", DataType = "List", IsRequired = true)]
        public Report QueryData { get; set; }
    }

    public enum DepartConfigRequestType : short
    {
        QueryDepart = 0,
        AddDepart = 1,
        ModifyDepart = 2,
        DeleteDepart = 3
    }
}
