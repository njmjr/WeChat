using Dapper.Contrib.Extensions;

namespace WeChat.Models
{
    [Table("TD_M_SERVICE")]
    public class WxService
    {
        [Key]
        public string SERVICEID { get; set; }
        public string URI { get; set; }
        public string VERBS { get; set; }
        public string DESCRIPTION { get; set; }
        public string IP { get; set; }
        public string PORT { get; set; }
        public string VHOST { get; set; }
        public string CALLCOUNT { get; set; } 
        public string RUNNINGSTATES { get; set; }

    }
}
