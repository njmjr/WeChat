using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace WeChat.Utility.Secutiry
{
    public static class WxPayHelper
    {
        /// <summary>
        /// 创建签名
        /// </summary>
        /// <param name="kvs"></param>
        /// <returns></returns>
        public static String CreateSign(SortedDictionary<string,string> kvs)
        {
            StringBuilder signsb = new StringBuilder();
            foreach (var kv in kvs)
            {
                if (kv.Value==null)
                {
                    continue;
                }
                if ("sign".Equals(kv.Key))
                {
                    continue;
                }
                signsb.Append(kv.Key);
                signsb.Append("=");
                signsb.Append(kv.Value);
                signsb.Append("&");
            }

            signsb.Append("key=" + ConfigurationManager.AppSettings["APPKEY"]);
            return signsb.ToString();
        }
        /// <summary>
        /// 创建xml请求报文
        /// </summary>
        /// <param name="kvs"></param>
        /// <returns></returns>
        public static String CreateXmlRequest(SortedDictionary<string, string> kvs)
        {
            StringBuilder signsb = new StringBuilder();
            signsb.Append("<xml>");
            foreach (var kv in kvs)
            {
                if (kv.Value==null)
                {
                    continue; 
                }
                signsb.Append("<").Append(kv.Key).Append(">");
                signsb.Append(kv.Value);
                signsb.Append("</").Append(kv.Key).Append(">");
            }
            signsb.Append("</xml>");

            return signsb.ToString();
        }

        /// <summary>
        /// 创建notify响应
        /// </summary>
        /// <param name="returnCode"></param>
        /// <param name="returnMsg"></param>
        /// <returns></returns>
        public static String CreateNotifyResponseXml(String returnCode, String returnMsg)
        {
            StringBuilder signsb = new StringBuilder();
            signsb.Append("<xml>");
            signsb.Append("<return_code>").Append(returnCode).Append("</return_code>");
            signsb.Append("<return_msg>").Append(returnMsg).Append("</return_msg>");
            signsb.Append("</xml>");
            return signsb.ToString();
        }
    }
}
