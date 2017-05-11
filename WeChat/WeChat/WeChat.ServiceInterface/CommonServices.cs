using ServiceStack;
using WeChat.DomainService.Application.IService;
using WeChat.ServiceModel.Base;

namespace WeChat.ServiceInterface
{
    /// <summary>
    /// 卡信息相关服务
    /// </summary>
    public class CommonServices : Service
    {
        private readonly ICommonService _commonService;

        public CommonServices(ICommonService commonService)
        {
            _commonService = commonService; 
        } 

        public override void Dispose()
        {
            base.Dispose();
            _commonService.Dispose();
        }

        //获取通用信息
        public object Get(Common request)
        {
            CommonResponse rsp = new CommonResponse();
            if (request.RequestType == (short)CommonRequestType.All)
            {
                _commonService.GetAll(request, rsp); 
            }
            else if (request.RequestType == (short)CommonRequestType.MenuView)
            {
                _commonService.GetMenu(request, rsp);
            }
            else if (request.RequestType == (short)CommonRequestType.WxService)
            {
                _commonService.GetService(request, rsp);
            }
            else if (request.RequestType == (short)CommonRequestType.Role)
            {
                _commonService.GetRole(request, rsp);
            }
            return rsp;
        }



    }
}
