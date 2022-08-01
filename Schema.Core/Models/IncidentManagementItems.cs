using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class IncidentManagementItems
    {
        public Int64 incidentid { get; set; }
        public string incidentdtlsids { get; set; }
        public Int64 incidentdtlsid { get; set; }
        public Int16 manualflag { get; set; }
        public Int16 status { get; set; }
        public string publishflag { get; set; }
        public string manualinput { get; set; }
        public string incidentname { get; set; }
        public string incidentno { get; set; }
        public string incidentdate { get; set; }
        public string restoredate { get; set; }
        public Int32 estaffectedcustomers { get; set; }
        public string remarks { get; set; }
        public string postalcode { get; set; }
        public string blockno { get; set; }
        public string streetname { get; set; }
        //Admin Job Sync
        public Int64 id { get; set; }
        public string keyname { get; set; }
        public string keyvalue { get; set; }
        public string keydesc { get; set; }
        public string estrestoreduration { get; set; }
        public string fullrestorationdate { get; set; }
        public string feedback { get; set; }

    }
}
