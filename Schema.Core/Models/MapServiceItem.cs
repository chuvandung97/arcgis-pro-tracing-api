using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class MapServiceItem
    {
        public int id;
        public string username { get; set; }
        public string url { get; set; }        
        public string clientip { get; set; }
        public string referrer { get; set; }
        public string browser { get; set; }
        public string useragent { get; set; }
        public double responsetime { get; set; }
        public string type { get; set; }
        public string parameter { get; set; }
        public string functionname { get; set; }
        public string errormsg { get; set; }
    }
}
