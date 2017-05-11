using System;
using Castle.DynamicProxy;
using ServiceStack;
using ServiceStack.Logging;
using WeChat.ServiceModel.Base;
using WeChat.Utility;

namespace WeChat.DomainService.Interceptors
{
    public class ExceptionInterceptor :IInterceptor
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ExceptionInterceptor));
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (WeChatException weChatException)
            {
                BaseResponse response = ((BaseResponse) invocation.Arguments[1]);
                response.ResponseStatus.ErrorCode = weChatException.ErrorCode;

                if (weChatException.Errors.Count > 0)
                {
                    foreach (var error in weChatException.Errors)
                    {
                        response.ResponseStatus.Errors.Add(new ResponseError
                        {
                            ErrorCode = weChatException.ErrorCode,
                            Message = error
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Log.WarnFormat(e.ToString());
                BaseResponse response = ((BaseResponse)invocation.Arguments[1]);
                response.ResponseStatus.ErrorCode = "ERROR";
                response.ResponseStatus.Errors.Add(new ResponseError
                {
                    ErrorCode = "ERROR",
                    Message = "系统异常，请联系技术人员"
                });
            }
        }
    }
}
