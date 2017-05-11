using WeChat.ServiceModel.Wx;

namespace WeChat.DomainService.Application.IService
{
    public interface IWxService : IApplicationService
    {
        void GetAccessToken(AccessToken request, AccessTokenResponse response);

        void SendTemplateMessage(TemplateMessage request, TemplateMessageResponse response);

        void CreateOrders(CreateOrder request, CreateOrderResponse response);
    }
}
