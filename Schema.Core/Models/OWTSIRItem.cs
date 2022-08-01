using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class OWTSIRItem
    {
        public int id;
        public string readingdate { get; set; }
        public string officer { get; set; }
        public string maxpd { get; set; }
        public string comments { get; set; }
        public Nullable<Int64> irafterb { get; set; }
        public Nullable<Int64> iraftery { get; set; }
        public Nullable<Int64> irafterr { get; set; }
        public Nullable<Int64> irbeforeb { get; set; }
        public Nullable<Int64> irbeforey { get; set; }
        public Nullable<Int64> irbeforer { get; set; }
        public string creationuser { get; set; }
        public string lastuser { get; set; }
        public string datemodified { get; set; }
        public string officerremarks { get; set; }
        public string assigndate { get; set; }
        public Nullable<int> feederno { get; set; }
        public string jobscope { get; set; }
        public Nullable<int> statuscode { get; set; }
        public Nullable<int> owtsirflag { get; set; }
        public Nullable<double> distance { get; set; }
        public Nullable<Int64> selectedinfoid { get; set; }
        public Nullable<int> selectedfcid { get; set; }
        public Nullable<int> voltage { get; set; }
        public string srcstationname { get; set; }
        public Nullable<int> srcstationboardno { get; set; }
        public Nullable<int> srcstationpanelno { get; set; }
        public string tgtstationname { get; set; }
        public Nullable<int> tgtstationboardno { get; set; }
        public Nullable<int> tgtstationpanelno { get; set; }
        public Nullable<int> isDeleted { get; set; }
    }
}
