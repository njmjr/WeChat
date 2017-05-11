using WeChat.ServiceModel.Attributes;

namespace WeChat.ServiceModel.Base
{
    [CustomRequestFilter]
    public class BaseRequest
    {
        public string Token { get; set; }
        public string CurrOper { get; set; }

        public string CurrDept { get; set; }
    }
}
