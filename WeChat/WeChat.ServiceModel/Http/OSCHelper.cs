using System;
using ServiceStack;
using WeChat.ServiceModel.Base;

namespace WeChat.ServiceModel.Http
{
    /// <summary>
    /// 调用接口类
    /// </summary>
    public static class SxxxHelper
    {
        /// <summary>
        /// GET方式调用服务
        /// </summary>
        /// <typeparam name="TRequestDto">请求类</typeparam>
        /// <typeparam name="TResponseDto">响应类</typeparam>
        /// <param name="request">请求实例</param>
        /// <param name="url"></param>
        /// <returns>响应实例</returns>
        public static TResponseDto CallService<TRequestDto, TResponseDto>(TRequestDto request, string url)
        {
            var client = new JsonServiceClient(url);
            TResponseDto responseDto = client.Get<TResponseDto>(request); 
            return responseDto;
        }

        /// <summary>
        /// POST方式调用服务
        /// </summary>
        /// <typeparam name="TRequestDto">请求类</typeparam>
        /// <typeparam name="TResponseDto">响应类</typeparam>
        /// <param name="request">请求实例</param>
        /// <param name="url"></param>
        /// <returns>响应实例</returns>
        public static TResponseDto PostService<TRequestDto, TResponseDto>(TRequestDto request, string url) where TRequestDto : BaseRequest
        {
            var client = new JsonServiceClient(url) {Timeout = new TimeSpan(0, 0, 90)};
            TResponseDto responseDto = client.Post<TResponseDto>(request);
            return responseDto;
        }

        /// <summary>
        /// POST方式调用服务 直接返回JSON
        /// </summary>
        /// <typeparam name="TRequestDto">请求类</typeparam>
        /// <param name="request">请求实例</param>
        /// <param name="url"></param>
        /// <returns>响应实例</returns>
        public static string PostService<TRequestDto>(TRequestDto request, string url) where TRequestDto : BaseRequest
        {
            var client = new JsonServiceClient(url) {Timeout = new TimeSpan(0, 0, 90)};
            string responseJson = client.Post<string>(request);
            return responseJson;
        }
          
    }
}