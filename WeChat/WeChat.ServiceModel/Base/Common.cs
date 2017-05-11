using System.Collections.Generic;
using ServiceStack;
using WeChat.Models;

namespace WeChat.ServiceModel.Base
{
    /// <summary>
    /// 通用请求及相应DTO
    /// </summary>
    [Route("/common")]
    public class Common : BaseRequest
    {
        public short RequestType { get; set; }
    }

    public class CommonResponse : BaseResponse
    {
        public List<MenuView> Menus { get; set; }

        public IEnumerable<WxService> Services { get; set; }

        public IEnumerable<Role> Roles { get; set; }

    }
    public enum CommonRequestType : short
    {
        All = 0,
        MenuView = 1,
        WxService = 2,
        Role = 3
    }
}
