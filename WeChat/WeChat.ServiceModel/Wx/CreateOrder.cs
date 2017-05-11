using ServiceStack;
using WeChat.ServiceModel.Base;

namespace WeChat.ServiceModel.Wx
{
    [Route("/CreateOrder")]
    public class CreateOrder : BaseRequest
    {
        //[ApiMember(Name = "Appid", Description = "微信开放平台审核通过的应用APPID", DataType = "string", IsRequired = true)]
        //public string Appid { get; set; }

        //[ApiMember(Name = "MchId", Description = "微信支付分配的商户号", DataType = "string", IsRequired = true)]
        //public string MchId { get; set; }

        //[ApiMember(Name = "DeviceInfo", Description = "设备号", DataType = "string", IsRequired = false)]
        //public string DeviceInfo { get; set; }

        //[ApiMember(Name = "NonceStr", Description = "随机字符串，不长于32位", DataType = "string", IsRequired = true)]
        //public string NonceStr { get; set; }

        //[ApiMember(Name = "Sign", Description = "签名", DataType = "string", IsRequired = true)]
        //public string Sign { get; set; }

        //[ApiMember(Name = "SignType", Description = "签名类型", DataType = "string", IsRequired = false)]
        //public string SignType { get; set; }

        [ApiMember(Name = "Body", Description = "商品描述", DataType = "string", IsRequired = true)]
        public string Body { get; set; }

        [ApiMember(Name = "Detail", Description = "商品详情", DataType = "string", IsRequired = false)]
        public string Detail { get; set; }

        [ApiMember(Name = "Attach", Description = "附加数据", DataType = "string", IsRequired = false)]
        public string Attach { get; set; }

        [ApiMember(Name = "OutTradeNo", Description = "商户订单号", DataType = "string", IsRequired = true)]
        public string OutTradeNo { get; set; }

        //[ApiMember(Name = "FeeType", Description = "货币类型", DataType = "string", IsRequired = false)]
        //public string FeeType { get; set; }

        [ApiMember(Name = "TotalFee", Description = "总金额", DataType = "int", IsRequired = true)]
        public int TotalFee { get; set; }

        [ApiMember(Name = "SpbillCreateIp", Description = "终端IP", DataType = "string", IsRequired = true)]
        public string SpbillCreateIp { get; set; }

        //[ApiMember(Name = "TimeStart", Description = "交易起始时间,格式为yyyyMMddHHmmss", DataType = "string", IsRequired = false)]
        //public string TimeStart { get; set; }

        //[ApiMember(Name = "TimeExpire", Description = "交易结束时间,格式为yyyyMMddHHmmss", DataType = "string", IsRequired = false)]
        //public string TimeExpire { get; set; }

        [ApiMember(Name = "GoodsTag", Description = "商品标记", DataType = "string", IsRequired = false)]
        public string GoodsTag { get; set; }

        //[ApiMember(Name = "NotifyUrl", Description = "接收微信支付异步通知回调地址", DataType = "string", IsRequired = true)]
        //public string NotifyUrl { get; set; }

        //[ApiMember(Name = "TradeType", Description = "交易类型", DataType = "string", IsRequired = true)]
        //public string TradeType { get; set; }

        [ApiMember(Name = "Openid", Description = "用户标识", DataType = "string", IsRequired = true)]
        public string Openid { get; set; }
        [ApiMember(Name = "LimitPay", Description = "指定支付方式", DataType = "string", IsRequired = true)]
        public string LimitPay { get; set; }
        [ApiMember(Name = "ProductId", Description = "商品ID", DataType = "string", IsRequired = false)]
        public string ProductId { get; set; }
    }

    public class CoResponse
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }
        public string appid { get; set; }
        public string mch_id { get; set; }
        public string device_info { get; set; }
        public string nonce_str { get; set; }
        public string sign { get; set; }
        public string result_code { get; set; }
        public string err_code { get; set; }
        public string err_code_des { get; set; }
        public string trade_type { get; set; }
        public string prepay_id { get; set; }
        public string code_url { get; set; }
    }
    public class CreateOrderResponse : BaseResponse
    {
        [ApiMember(Name = "PrepayId", Description = "预支付交易会话标识", DataType = "string", IsRequired = true)]
        public string PrepayId { get; set; }
    }

}
