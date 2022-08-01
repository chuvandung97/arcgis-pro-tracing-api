using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class APIManagementItems
    {
        public string URLKey { get; set; }
        public string ServiceType { get; set; }
        public string URL { get; set; }
        public string Module { get; set; }
        public string FunctionName { get; set; }
        public string Functionality { get; set; }
        public string ADGroup { get; set; }
        public string Role { get; set; }
        public Int32 ADGroupKey { get; set; }
        public Int32 FunctionalityKey { get; set; }
        public Int32 APIKey { get; set; }
        public string FunctionalityMultipleKeys { get; set; }
        public string APIMultipleKeys { get; set; }
        public Nullable<int> IsTracking { get; set; }
    }
}
