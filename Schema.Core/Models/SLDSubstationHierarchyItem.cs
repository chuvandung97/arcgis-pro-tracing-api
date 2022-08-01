using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class SLDSubstationHierarchyItem
    {
        public int FeederId { get; set; }
        public string FeederName { get; set; }
        public int PanelNo { get; set; }
        public string SwitchBoard_GlobalId { get; set; }
        public int Level { get; set; }
        public string SheetID { get; set; }
        public int EID { get; set; }
        public int? Parent { get; set; }
        public int? DPD { get; set; }
        public string SWID { get; set; }
        public string SubstationName { get; set; }
        public string SubstationMRC { get; set; }
        public int? DayLoad { get; set; }
        public int? NightLoad { get; set; }
        public int? PrevDayMinLoad { get; set; }
        public int? PrevDayMaxLoad { get; set; }
        public int? PrevMonMinLoad { get; set; }
        public int? PrevMonMaxLoad { get; set; }

        public HashSet<SLDSubstationHierarchyItem> Children { get; set; }

        public SLDSubstationHierarchyItem(Dictionary<string, object> item)
        {
            if (item.ContainsKey("level") && item["level"] != null)
                Level = Convert.ToInt32(item["level"]);

            if (item.ContainsKey("sheet_id") && item["sheet_id"] != null)
                SheetID = Convert.ToString(item["sheet_id"]);

            if (item.ContainsKey("eid") && item["eid"] != null)
                EID = Convert.ToInt32(item["eid"]);

            if (item.ContainsKey("parent") && item["parent"] != null)
                Parent = Convert.ToInt32(item["parent"]);

            if (item.ContainsKey("substation_name") && item["substation_name"] != null)
                SubstationName = Convert.ToString(item["substation_name"]);

            if (item.ContainsKey("substation_mrc") && item["substation_mrc"] != null)
                SubstationMRC = Convert.ToString(item["substation_mrc"]);

            if (item.ContainsKey("dpd") && item["dpd"] != null)
                DPD = Convert.ToInt32(item["dpd"]);

            if (item.ContainsKey("dayload") && item["dayload"] != null)
                DayLoad = Convert.ToInt32(item["dayload"]);

            if (item.ContainsKey("nightload") && item["nightload"] != null)
                NightLoad = Convert.ToInt32(item["nightload"]);

            if (item.ContainsKey("prevdayminload") && item["prevdayminload"] != null)
                PrevDayMinLoad = Convert.ToInt32(item["prevdayminload"]);

            if (item.ContainsKey("prevdaymaxload") && item["prevdaymaxload"] != null)
                PrevDayMaxLoad = Convert.ToInt32(item["prevdaymaxload"]);

            if (item.ContainsKey("prevmonminload") && item["prevmonminload"] != null)
                PrevMonMinLoad = Convert.ToInt32(item["prevmonminload"]);

            if (item.ContainsKey("prevmonmaxload") && item["prevmonmaxload"] != null)
                PrevMonMaxLoad = Convert.ToInt32(item["prevmonmaxload"]);
        }
    }
}
