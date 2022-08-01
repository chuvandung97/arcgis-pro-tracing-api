using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class DMISProcessItem
    {
        public string ObjectID { get; set; }
        public string geometry { get; set; }
        public string approverName { get; set; }
        public string approverRemarks { get; set; }
        public string approvalStatus { get; set; }
        public string assignedEditor { get; set; }
        public string jobIDs { get; set; }
    }
}
