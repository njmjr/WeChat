using System.Collections.Generic;

namespace WeChat.Models
{
    public class Report
    {
        public int total { get; set; }

        public IEnumerable<dynamic> rows { get; set; }
        public IEnumerable<dynamic> footer { get; set; }
    }
}
