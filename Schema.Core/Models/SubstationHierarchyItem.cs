using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class SubstationHierarchyItem
    {
        public int EdgeId { get; set; }
        public int ParentEdgeId { get; set; }
        public int Level { get; set; }
        public int CustomerCount { get; set; }
        public int? Id { get; set; }
        public int? OID { get; set; }
        public int? FCID { get; set; }
        public int? SubTypeCD { get; set; }
        public string MRC { get; set; }
        public string BoardName { get; set; }
        public string OperatingVoltage { get; set; }
        public string SubstationName { get; set; }
        public object Geometry { get; set; }
        public object EdgeGeometry { get; set; }
        public object BoardEids { get; set; }
        public HashSet<SubstationHierarchyItem> Children { get; set; }

        public SubstationHierarchyItem(Dictionary<string, object> item)
        {
            //if(item.ContainsKey("") & item[""] != null)
            if (item.ContainsKey("edgeid") && item["edgeid"] != null)
                EdgeId = Convert.ToInt32(item["edgeid"]);

            if (item.ContainsKey("parent") && item["parent"] != null)
                ParentEdgeId = Convert.ToInt32(item["parent"]);

            if (item.ContainsKey("level") && item["level"] != null)
                Level = Convert.ToInt32(item["level"]);

            if (item.ContainsKey("id") && item["id"] != null)
                Id = Convert.ToInt32(item["id"]);

            if (item.ContainsKey("oid") && item["oid"] != null)
                OID = Convert.ToInt32(item["oid"]);

            if (item.ContainsKey("fcid") && item["fcid"] != null)
                FCID = Convert.ToInt32(item["fcid"]);

            if (item.ContainsKey("cust_count") && item["cust_count"] != null)
                CustomerCount = Convert.ToInt32(item["cust_count"]);

            if (item.ContainsKey("subtypecd") && item["subtypecd"] != null)
                SubTypeCD = Convert.ToInt32(item["subtypecd"]);

            if (item.ContainsKey("mrc") && item["mrc"] != null)
                MRC = Convert.ToString(item["mrc"]);

            if (item.ContainsKey("boardname") && item["boardname"] != null)
                BoardName = Convert.ToString(item["boardname"]);

            if (item.ContainsKey("operatingvoltage") && item["operatingvoltage"] != null)
                OperatingVoltage = Convert.ToString(item["operatingvoltage"]);

            if (item.ContainsKey("substation_name") && item["substation_name"] != null)
                SubstationName = Convert.ToString(item["substation_name"]);

            if (item.ContainsKey("shape") && item["shape"] != null)
                Geometry = item["shape"];
            
            if (item.ContainsKey("edgeshape") && item["edgeshape"] != null)
                EdgeGeometry = item["edgeshape"];

            if (item.ContainsKey("boardeids") && item["boardeids"] != null)
                BoardEids = item["boardeids"];
        }
    }
}
