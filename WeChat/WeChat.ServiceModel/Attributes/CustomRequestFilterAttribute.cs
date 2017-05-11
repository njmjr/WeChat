using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using ServiceStack;
using ServiceStack.Web;
using WeChat.Utility;
using WeChat.Utility.Secutiry;

namespace WeChat.ServiceModel.Attributes
{
    public class CustomRequestFilterAttribute : RequestFilterAttribute
    {
        public CustomRequestFilterAttribute() { }

        public CustomRequestFilterAttribute(ApplyTo applyTo)
        : base(applyTo)
        { }
        public override void Execute(IRequest req, IResponse res, object responseDto)
        {
            bool isTest= Convert.ToBoolean(ConfigurationManager.AppSettings["IsTest"]);
            if (isTest)
            {
                return;
            }
            //验证请求的token
            List<PropertyInfo> columnPropertyList = new List<PropertyInfo>();
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
            string tokenKey = ConfigurationManager.AppSettings["TokenKey"];
            if (dtoOrigDomain != null)
            {
                string tempTokenKey = ConfigurationManager.AppSettings[dtoOrigDomain.ToString().ToUpper() + "_TokenKey"];
                if (!string.IsNullOrEmpty(tempTokenKey))
                {
                    tokenKey = tempTokenKey;
                }
            }
            string token = TokenHelper.GetSignStr(req.Dto, tokenKey);

            if (dtoToken == null || token.Trim().ToLower() != dtoToken.ToString().Trim().ToLower())
            {
                throw new WeChatException("TOKEN_ERROR", "TOKEN_ERROR");
            }
        }
    }
}
