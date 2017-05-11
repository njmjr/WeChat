using ServiceStack;
using WeChat.ServiceModel.Base;

namespace WeChat.ServiceModel.PrivilegePR
{
    /// <summary>
    /// 前台页面验证
    /// </summary>
    [Route("/ForeVerify")]
    public class ForeVerify : BaseRequest
    {
        //员工号
        public string StaffNo { get; set; }
        //部门号
        public string DepartNo { get; set; }
        //验证页面
        public string Url { get; set; }

    }

    public class ForeVerifyResponse : BaseResponse
    { 
    }
}
