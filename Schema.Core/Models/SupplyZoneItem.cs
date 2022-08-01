using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class SupplyZoneItem
    {
        public string eventid { get; set; }
        public string eventname { get; set; }
        public string eventdate { get; set; }
        public string mrc { get; set; }
        public string poutageflag { get; set; }
        public string outageflag { get; set; }
        public string restoreflag { get; set; }
        public string eventrestore { get; set; }
        public string dterestore { get; set; }
        public string supplystation { get; set; }
        public string eventremarks { get; set; }
        public string dteupdate { get; set; }
        public string dtepoutage { get; set; }
        public string dteoutage { get; set; }
        public string empid { get; set; }
        public string username { get; set; }
        public string role { get; set; }
        public string userid { get; set; }

    }
}
