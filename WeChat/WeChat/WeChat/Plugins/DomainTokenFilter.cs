using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using ServiceStack.Text;
using ServiceStack.Web;
using WeChat.Utility;
using WeChat.Utility.Redis;
using WeChat.Utility.Secutiry;

namespace WeChat.Plugins
{
    /// <summary>
    /// 验证令牌
    /// </summary>
    public class DomainTokenFilter
    {
        public void DoFilter(IRequest req, IResponse res, object responseDto)
        {
            bool isTest= Convert.ToBoolean(ConfigurationManager.AppSettings["IsTest"]);
            if (isTest)
            {
                return;
            }
            //检查post的json数据的每一个key，是否在dto中有定义。
            string postData = req.GetRawBody();
            IsPostKeyExist(req.Dto, postData);
            //验证请求的token 
            PropertyInfo[] props = req.Dto.GetType().GetProperties();
            object dtoToken = null;
            object dtoOrigDomain = null;
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name.ToUpper() == "TOKEN")
                {
                    dtoToken = prop.GetValue(req.Dto, null);
                }
                if (prop.Name.ToUpper() == "ORIGDOMAIN")
                {
                    dtoOrigDomain = prop.GetValue(req.Dto, null);
                }
            }
            if (dtoOrigDomain == null)
            {
                throw new WeChatException("发起方应用域代码错误");
            }
            RedisConfigData configData = RedisHelper.Instance.GetDomainConfigData(dtoOrigDomain);
            if (string.IsNullOrEmpty(configData.Domain))
            {
                throw new WeChatException("REDIS_CONFIG_NULL", "Domain无相关配置信息");
            }
            //检查访问权限
            string dtoTypeName = responseDto.GetType().FullName.ToLower();
            if (configData.ConfigData.WhiteServiceList.Count > 0)//如果有白名单（不管有没有黑名单），不在白名单内统统拦截
            {
                if (!configData.ConfigData.WhiteServiceList.Contains(dtoTypeName))
                {
                    throw new WeChatException("SERVICE_ACCESS_DENIED", "服务无访问权限");
                }
            }
            else if (configData.ConfigData.WhiteServiceList.Count == 0 &&
                     configData.ConfigData.BlackServiceList.Count > 0)//如果只有黑名单（没有白名单），黑名单内拦截
            {
                if (configData.ConfigData.BlackServiceList.Contains(dtoTypeName))
                {
                    throw new WeChatException("SERVICE_ACCESS_DENIED", "服务无访问权限");
                }
            }
            //else:既没有白名单，也没有黑名单，都不拦截

            //检查Token是否正确，如果Token为空则不检查
            string tokenKey = configData.ConfigData.TokenKey;
            if (!string.IsNullOrEmpty(tokenKey))
            {
                string token = TokenHelper.GetSignStr(req.Dto, tokenKey);

                if (dtoToken == null || token.Trim().ToLower() != dtoToken.ToString().Trim().ToLower())
                {
                    throw new WeChatException("TOKEN_ERROR", "令牌错误");
                }
            }
        }

        private void IsPostKeyExist(object requestDto,string postData)
        {
            //解析json数据
            var jsonObj = JsonSerializer.DeserializeFromString<Dictionary<string, object>>(postData);
            PropertyInfo[] props = requestDto.GetType().GetProperties();
            foreach (string key in jsonObj.Keys)
            {
                bool isExist = false;
                foreach (PropertyInfo prop in props)
                {
                    if (prop.Name.ToUpper() == key.ToUpper())
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    throw new WeChatException("POSTKEY_NOT_DEFINED", "传入的KEY在对象中未定义");
                }
            }
        }
    }
}
