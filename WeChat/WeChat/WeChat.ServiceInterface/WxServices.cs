using ServiceStack;
using WeChat.DomainService.Application.IService;
using WeChat.ServiceModel.Wx;

namespace WeChat.ServiceInterface
{
    public class WxServices : Service
    {
        private readonly IWxService _wxService;

        public WxServices(IWxService wxService)
        {
            _wxService = wxService;
        }

        public object Get(AccessToken request)
        {
            AccessTokenResponse rsp = new AccessTokenResponse();
            _wxService.GetAccessToken(request, rsp);
            return rsp;
        }

        public object Post(TemplateMessage request)
        {
            TemplateMessageResponse rsp = new TemplateMessageResponse();
            _wxService.SendTemplateMessage(request, rsp);
            return rsp;
        }

        public object Post(CreateOrder request)
        {
            CreateOrderResponse rsp = new CreateOrderResponse();
            _wxService.CreateOrders(request, rsp);
            return rsp;
        }
    }
}