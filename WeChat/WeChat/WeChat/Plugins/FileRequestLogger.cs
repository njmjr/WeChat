using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Web;

namespace WeChat.Plugins
{
    /// <summary>
    /// 日志插件
    /// 文件记录请求及响应信息
    /// 参考<ServiceStack.Host.InMemoryRollingRequestLogger/>
    /// 访问压力大时，不建议使用此日志
    /// dongx
    /// 20150615
    /// </summary>
    public class FileRequestLogger :  IRequestLogger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FileRequestLogger));  
 
        public bool EnableSessionTracking { get; set; }

        public bool EnableRequestBodyTracking { get; set; }

        public bool EnableResponseTracking { get; set; }

        public bool EnableErrorTracking { get; set; }

        public string[] RequiredRoles { get; set; }

        public Type[] ExcludeRequestDtoTypes { get; set; }

        public Type[] HideRequestBodyForRequestDtoTypes { get; set; }

        public FileRequestLogger() { } 

        public virtual void Log(IRequest request, object requestDto, object response, TimeSpan requestDuration)
        {
             var requestType = requestDto != null ? requestDto.GetType() : null;

            if (ExcludeRequestType(requestType))
                return; 
            var entry = CreateEntry(request, requestDto, response, requestDuration, requestType);

            log.InfoFormat(@"HttpMethod:{0}
                            AbsoluteUri:{1}
                            PathInfo:{2}
                            IpAddress:{3}
                            ForwardedFor:{4}
                            RequestBody:{5}
                            ResponseDto:{6}
                            ErrorResponse:{7}
                            RequestDuration:{8}",
                            entry.HttpMethod,entry.AbsoluteUri,entry.PathInfo,entry.IpAddress,entry.ForwardedFor,entry.RequestBody,
                            entry.ResponseDto.ToJson(),entry.ErrorResponse,entry.RequestDuration.TotalSeconds);
       
        }

        protected RequestLogEntry CreateEntry(IRequest request, object requestDto, object response, TimeSpan requestDuration, Type requestType)
        {
            var entry = new RequestLogEntry
            { 
                DateTime = DateTime.UtcNow,
                RequestDuration = requestDuration,
            };

            if (request != null)
            {
                entry.HttpMethod = request.Verb;
                entry.AbsoluteUri = request.AbsoluteUri;
                entry.PathInfo = request.PathInfo;
                entry.IpAddress = request.UserHostAddress;
                entry.ForwardedFor = request.Headers[HttpHeaders.XForwardedFor];
                entry.Referer = request.Headers[HttpHeaders.Referer];
                entry.Headers = request.Headers.ToDictionary();
                entry.UserAuthId = request.GetItemOrCookie(HttpHeaders.XUserAuthId);
                entry.SessionId = request.GetSessionId();
                entry.Items = SerializableItems(request.Items);
                entry.Session = EnableSessionTracking ? request.GetSession() : null;
            }

            if (HideRequestBodyForRequestDtoTypes != null
                && requestType != null
                && !HideRequestBodyForRequestDtoTypes.Contains(requestType))
            {
                entry.RequestDto = requestDto;
                if (request != null)
                {
                    entry.FormData = request.FormData.ToDictionary();

                    if (EnableRequestBodyTracking)
                    {
                        entry.RequestBody = request.GetRawBody();
                    }
                }
            }
            if (!response.IsErrorResponse())
            {
                if (EnableResponseTracking)
                    entry.ResponseDto = response;
            }
            else
            {
                if (EnableErrorTracking)
                    entry.ErrorResponse = ToSerializableErrorResponse(response);
            }

            return entry;
        }

        protected bool ExcludeRequestType(Type requestType)
        {
            return ExcludeRequestDtoTypes != null
                   && requestType != null
                   && ExcludeRequestDtoTypes.Contains(requestType);
        }

        public Dictionary<string, string> SerializableItems(Dictionary<string, object> items)
        {
            var to = new Dictionary<string, string>();
            foreach (var item in items)
            {
                var value = item.Value == null
                    ? "(null)"
                    : item.Value.ToString();

                to[item.Key] = value;
            }

            return to;
        }

        public virtual List<RequestLogEntry> GetLatestLogs(int? take)
        {
            return null;
        }

        public static object ToSerializableErrorResponse(object response)
        {
            var errorResult = response as IHttpResult;
            if (errorResult != null)
                return errorResult.Response;

            var ex = response as Exception;
            return ex != null ? ex.ToResponseStatus() : null;
        }
    }
}