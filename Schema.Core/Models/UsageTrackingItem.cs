using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class UsageTrackingItem
    {
        public string userId { get; set; }
        public bool blockedFlag { get; set; }
        public string blockedUser { get; set; }
        public string emailID { get; set; }
        public string searchIndex { get; set; }
        public string searchLocation { get; set; }
        public string xCoord { get; set; }
        public string yCoord { get; set; }
    }
}
