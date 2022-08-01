using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class S2STraceItem
    {
        public int Sequence { get; set; }
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public string GlobalId { get; set; }
        public string Voltage { get; set; }
        public int FcId { get; set; }
        public string FeatureClssName { get; set; }
        public IDictionary<string, object> Attributes { get; set; }
        public object Geometry { get; set; }

        public S2STraceItem(IDictionary<string, object> item)
        {
            if (item.ContainsKey("seq") && item["seq"] != null)
                Sequence = Convert.ToInt32(item["seq"]);

            if (item.ContainsKey("id") && item["id"] != null)
                Id = Convert.ToInt32(item["id"]);

            if (item.ContainsKey("oid") && item["oid"] != null)
                ObjectId = Convert.ToInt32(item["oid"]);

            if (item.ContainsKey("globalid") && item["globalid"] != null)
                GlobalId = Convert.ToString(item["globalid"]);

            if (item.ContainsKey("ovolt") && item["ovolt"] != null)
                Voltage = Convert.ToString(item["ovolt"]);

            if (item.ContainsKey("fcid") && item["fcid"] != null)
                FcId = Convert.ToInt32(item["fcid"]);

            if (item.ContainsKey("featureclassname") && item["featureclassname"] != null)
                FeatureClssName = Convert.ToString(item["featureclassname"]);

            if (item.ContainsKey("properties") && item["properties"] != null)
                Attributes = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, object>>(Convert.ToString(item["properties"]));

            if (item.ContainsKey("geojson") && item["geojson"] != null)
                Geometry = item["geojson"];
        }
    }
}
