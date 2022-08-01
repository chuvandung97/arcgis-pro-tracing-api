using Schema.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class XVoltageReportTreeItem
    {
        public long Id { get; set; }
        public int FcId { get; set; }
        public long? Parent { get; set; }
        public int CustomerCount { get; set; }
        public string SheetId { get; set; }
        public string BoardName { get; set; }
        public string SubStationName { get; set; }
        public string Voltage { get; set; }
        public int? OVolt { get; set; }
        public object Geometry { get; set; }
        //added by Sandip on 20th May 2020 for incident management
        public object BoardEids { get; set; }
        public object EdgeGeometry { get; set; }
        //ends here
        public HashSet<XVoltageReportTreeItem> Children { get; set; }
        
        public XVoltageReportTreeItem(Dictionary<string, object> item)
        {
            Id = item.GetValue<long>("id");
            FcId = item.GetValue<int>("fcid");
            Parent = item.GetValue<long?>("parent");
            CustomerCount = item.GetValue<int>("cust_count");
            SheetId = item.GetValue<string>("sheet_id");
            BoardName = item.GetValue<string>("boardname");
            SubStationName = item.GetValue<string>("substation_name");
            Voltage = item.GetValue<string>("voltage");
            OVolt = item.GetValue<int?>("ovolt");
            Geometry = item.GetValue("shape");
            //added by Sandip on 20th May 2020 for incident management
            BoardEids = item.GetValue("boardeids");
            EdgeGeometry = item.GetValue("edgeshape");
            //ends here
        }
    }
}
