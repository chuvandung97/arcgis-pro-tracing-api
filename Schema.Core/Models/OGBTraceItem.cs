using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Core.Extensions;

namespace Schema.Core.Models
{
    public class OGBTraceItem
    {
        public long Id { get; set; }
        public int Fcid { get; set; }
        public int OID { get; set; }
        public int Level { get; set; }
        public long? ParentId { get; set; }
        public bool IsEdge { get; set; }
        public string Label { get; set; }
        public int CustomerCount { get; set; }
        public double? Length { get; set; }
        public string Voltage { get; set; }
        public string GlobalId { get; set; }
        public IDictionary<string, object> Properties { get; set; }
        public IDictionary<string, object> Shape { get; set; }
        public ICollection<OGBTraceItem> Children { get; set; }

        public OGBTraceItem()  { }

        public OGBTraceItem(Dictionary<string, object> item)
        {
            Id = item.GetValue<long>("id");
            Fcid = item.GetValue<int>("fcid");
            OID = item.GetValue<int>("oid");
            Level = item.GetValue<int>("level");
            ParentId = item.GetValue<long?>("parent");
            IsEdge = item.GetValue<bool>("isedge");
            CustomerCount = item.GetValue<int>("cust_count");
            Voltage = item.GetValue<string>("voltage");
            GlobalId = item.GetValue<string>("globalid");
            Properties = item.GetValue<Dictionary<string, object>>("properties");
            Shape = item.GetValue<Dictionary<string, object>>("shape");

            if(IsEdge)
            {
                Length = Properties.GetValue<double?>("cablelength");
            }
            switch (Fcid)
            {
                case 13:
                    Label = Properties.GetValue<string>("equipment_type") + Properties.GetValue<string>("mrc");
                    break;
                case 51:
                    Label = Properties.GetValue<string>("substation_name");
                    break;
                case 53:
                    Label = Properties.GetValue<string>("boardname");
                    break;
            }
        }
    }
}
