using System.Collections.Generic;
using ServiceStack;

namespace WeChat.ServiceModel.Base
{
    /// <summary>
    /// 响应DTO 基类
    /// </summary>
    public class BaseResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public string TradeId { get; set; }
        public BaseResponse()
        {
            ResponseStatus = new ResponseStatus() { Errors = new List<ResponseError>() };
        }
    }
}
