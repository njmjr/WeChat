using WeChat.ServiceModel.Base;

namespace WeChat.DomainService.Application.IService
{
    /// <summary>
    /// 通用应用服务接口
    /// </summary>
    public interface ICommonService :IApplicationService
    {
        void GetAll(Common request, CommonResponse response);

        void GetMenu(Common request, CommonResponse response);

        void GetService(Common request, CommonResponse response);

        void GetRole(Common request, CommonResponse response);

    }
}
