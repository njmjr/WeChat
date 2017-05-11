using ServiceStack;
using WeChat.ServiceModel.Base;

namespace WeChat.ServiceModel.Wx
{
    [Route("/AccessToken")]
    public class TemplateMessage : BaseRequest
    {
        public string touser { get; set; }
        public string template_id { get; set; }
        public string url { get; set; }

        public string keynote1 { get; set; }
        public string keynote2 { get; set; }
    }

    public class TmResponse
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }

        public string msgid { get; set; }
    }
    public class TemplateMessageResponse : BaseResponse
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }

}
