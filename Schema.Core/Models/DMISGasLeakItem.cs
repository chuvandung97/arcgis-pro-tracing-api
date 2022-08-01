using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class DMISGasLeakItem
    {      
        public int objectid;
        public string jobid { get; set; }
        public string dmisno { get; set; }
        public string actiontaken { get; set; }
        public string actiontakendesc { get; set; }
        public string buildingname { get; set; }
        public string causedesc { get; set; }
        public string causeoffeedback { get; set; }
        public string leakclassification { get; set; }
        public string comclassification { get; set; }
        public string datecompleted { get; set; }
        public string timeatt { get; set; }
        public string timerec { get; set; }
        public string timerest { get; set; }
        public string area { get; set; }
        public string gangatt { get; set; }
        public string gasleakdetails { get; set; }
        public string hseno { get; set; }
        public string locdesc { get; set; }
        public string locleak { get; set; }
        public string material { get; set; }
        public string followuprepair { get; set; }
        public string natureofcom { get; set; }
        public Nullable<int> noofcaller { get; set; }
        public Nullable<int> noofcustaffected { get; set; }
        public Nullable<int> pipesize { get; set; }
        public string pipetype { get; set; }
        public string pressureregime { get; set; }
        public string remarks { get; set; }
        public string rootcause { get; set; }
        public string sourcetype { get; set; }
        public string streetname { get; set; }
        public Nullable<int> installyear { get; set; }
        public int status { get; set; }
        public string username { get; set; }
        public string geometry { get; set; }
        public string transmainguid { get; set; }
        public string distmainguid { get; set; }
        public string datemodified { get; set; }
    }
}
