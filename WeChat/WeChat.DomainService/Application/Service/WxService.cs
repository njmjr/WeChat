using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using WeChat.DomainService.Application.IService;
using WeChat.ServiceModel.Http;
using WeChat.ServiceModel.Wx;
using WeChat.Utility;
using WeChat.Utility.Data;
using WeChat.Utility.Secutiry;

namespace WeChat.DomainService.Application.Service
{
    public class WxService : ApplicationService,IWxService
    {
        private const string AccessTokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        private const string TemplateMessageUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";

        private const String UrlUnifiedorder = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        public void GetAccessToken(AccessToken request, AccessTokenResponse response)
        {
            AtResponse wxr = GetAcc();
            if (String.IsNullOrEmpty(wxr.Expires_in))
            {
                response.ResponseStatus.ErrorCode = wxr.Errcode;
                response.ResponseStatus.Message = wxr.Errmsg;
            }
            else
            {
                response.AccessToken = wxr.Access_token;
                response.ExpiresIn = wxr.Expires_in;
                response.ResponseStatus.ErrorCode = "OK";
            }
        }
        private AtResponse GetAcc()
        {
            var appId = ConfigurationManager.AppSettings["APPID"];
            var appsecret = ConfigurationManager.AppSettings["APPSECRET"];
            String wxResult = HttpRequestUtil.HttpGet(string.Format(AccessTokenUrl, appId, appsecret));
            AtResponse wxr = JsonHelper.Deserialize<AtResponse>(wxResult);
            return wxr;
        }
        public void SendTemplateMessage(TemplateMessage request, TemplateMessageResponse response)
        {
            AtResponse wxr = GetAcc();
            if (String.IsNullOrEmpty(wxr.Expires_in))
            {
                throw new WeChatException("GET_ACCESSTOKEN_FAIL", "获取AccessToken失败");
            }

            Hashtable s1 = new Hashtable { { @"keynote1", request.keynote1 }, { @"keynote2", request.keynote2 } };
            string data = XmlAndJsonToHash.HashTableToXml(s1);
            Hashtable s2 = new Hashtable
            {
                {@"touser", request.touser},
                {@"template_id", request.template_id},
                {@"url", request.url},
                {@"data", data}
            };
            string postData = XmlAndJsonToHash.HashTableToXml(s2);
            String wxResult = HttpRequestUtil.HttpPost(string.Format(TemplateMessageUrl, wxr.Access_token), postData);
            TmResponse tr = JsonHelper.Deserialize<TmResponse>(wxResult);
            if (tr.errmsg.ToUpper() == "OK")
            {
                response.errcode = tr.errcode;
                response.errmsg = tr.errmsg;
                response.ResponseStatus.ErrorCode = "OK";
            }
            else
            {
                throw new WeChatException("SendTemplateMessage_fail", "发送模板消息失败");
            }
        }

        /// <summary>
        /// 统一下单接口
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public void CreateOrders(CreateOrder request, CreateOrderResponse response)
        {
            DateTime dt = DateTime.Now;
            string timeStart = string.Format("{0:yyyyMMddHHmmss}",dt);
            string timeExpire = string.Format("{0:yyyyMMddHHmmss}", dt.AddSeconds(300));
            SortedDictionary<string, string> ht = new SortedDictionary<string, string>
            {
                {"appid", ConfigurationManager.AppSettings["APPID"]},
                {"mch_id", ConfigurationManager.AppSettings["MCHID"]},
                {"device_info", "WEB"},
                {"nonce_str", RandomHelper.GenerateString(32)},
                {"sign_type", "MD5"},
                {"body", request.Body},
                {"detail", request.Detail},
                {"attach", request.Attach},
                {"out_trade_no", request.OutTradeNo},
                {"fee_type", "CNY"},
                {"total_fee", "" + request.TotalFee},
                {"spbill_create_ip", request.SpbillCreateIp},
                {"time_start", timeStart},
                {"time_expire", timeExpire},
                {"goods_tag", request.GoodsTag},
                {"notify_url", ConfigurationManager.AppSettings["NOTIFYURL"]},
                {"trade_type", "JSAPI"},
                {"openid", request.Openid},
                {"product_id", request.ProductId}
            };
            if (string.IsNullOrEmpty(request.LimitPay))
            {
                if (request.LimitPay == "no_credit")
                {
                    ht.Add("limit_pay", "no_credit");
                }
            }

            string sign = Md5Helper.UserMd5(WxPayHelper.CreateSign(ht));
            ht.Add("sign",sign);
            string postXml = WxPayHelper.CreateXmlRequest(ht);
            string wxResult = HttpRequestUtil.HttpPost(UrlUnifiedorder, postXml);
            Hashtable htt = XmlAndJsonToHash.XmlToHashTable(wxResult);
            wxResult = JsonHelper.ToJson(htt);
            CoResponse tr = JsonHelper.Deserialize<CoResponse>(wxResult);
            response.ResponseStatus.ErrorCode = tr.return_code;
            response.ResponseStatus.Message = tr.return_msg;
            if (tr.return_code == "SUCCESS" && tr.result_code == "SUCCESS")
            {
                response.PrepayId = tr.prepay_id;
            }
            else
            {
                response.ResponseStatus.ErrorCode = tr.err_code;
                response.ResponseStatus.Message = tr.err_code_des;
            }
        }
    }

}