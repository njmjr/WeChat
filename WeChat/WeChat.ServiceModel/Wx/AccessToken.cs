using ServiceStack;
using WeChat.ServiceModel.Base;

namespace WeChat.ServiceModel.Wx
{
    [Route("/AccessToken")]
    public  class AccessToken :BaseRequest
    {
    }

    public class AtResponse
    {
        public string Access_token { get; set; }
        public string Expires_in { get; set; }

        public string Errcode { get; set; }

        public string Errmsg { get; set; }
    }
    public class AccessTokenResponse : BaseResponse
    {
        public string AccessToken { get; set; }
        public string ExpiresIn { get; set; }
    }

}
