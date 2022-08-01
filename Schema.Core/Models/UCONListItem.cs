using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class UCONListItem
    {
        public string userid { get; set; }
        public Nullable<int> dsid { get; set; }
        public string workingzone { get; set; }
        public Nullable<int> mainzonegisthreshold { get; set; }
        public Nullable<int> otherzonegisthreshold { get; set; }
        public Nullable<int> mainzonesldthreshold { get; set; }
        public Nullable<int> otherzonesldthreshold { get; set; }
        public Nullable<int> officehourfrom { get; set; }
        public Nullable<int> officehourto { get; set; }
        public string oldthresholdvalues { get; set; }
    }
}
