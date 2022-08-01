using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class POVerficationListItem
    {
        public Nullable<Int64> povid { get; set; }
        public string jsonname { get; set; }
        public string sessionname { get; set; }
        public Int64 sessionid { get; set; }
        public string poids { get; set; }
        public string poname { get; set; }
        public string poemail { get; set; }
        public string poremarks { get; set; }
        public string contractorid { get; set; }
        public string contractorname { get; set; }
        public string contractoremail { get; set; }
        public string meaeditorid { get; set; }
        public string meaeditorname { get; set; }
        public string meaeditoremail { get; set; }
        public string mearemarks { get; set; }
        public string postatus { get; set; }
        public string imageattachment { get; set; }
        public string postedby { get; set; }
        public string jobstatus { get; set; }
        public string pdfname { get; set; }
        public string status { get; set; }
        public string email { get; set; }
        public string description { get; set; }
        public string imageorpdffile { get; set; }
        public string usertype { get; set; }
        public string totalfeatures { get; set; }
        public string totallbfeatures { get; set; }
        public string totallberror { get; set; }
        public string qaqcpct { get; set; }
        public string hpno { get; set; }

    }
}
