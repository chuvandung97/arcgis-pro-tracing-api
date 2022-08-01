using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Schema.Core.Models
{
    public class XVoltageTransformerItem
    {
        public long Id;
        public string Type;
        public int TransformerNo;
        public int SubTypeCD;
        public string SheetId;
        public string SheetName;
        public int Voltage;
        public double MVARating;

        public IDictionary<string, object> Geometry;

        public XVoltageTransformerItem(IDictionary<string, object> item)
        {
            Regex pattern = new Regex(@"(?<voltage>\d+)(?<zone>[A-z]+)(?<sheetno>\d+)");

            if (item.ContainsKey("id") && item["id"] != null)
                Id = Convert.ToInt64(item["id"]);

            if (item.ContainsKey("type") && item["type"] != null)
                Type = Convert.ToString(item["type"]);

            if (item.ContainsKey("transformer_no") && item["transformer_no"] != null)
                TransformerNo = Convert.ToInt32(item["transformer_no"]);

            if (item.ContainsKey("subtypecd") && item["subtypecd"] != null)
                SubTypeCD = Convert.ToInt32(item["subtypecd"]);

            if (item.ContainsKey("sheet_id") && item["sheet_id"] != null)
            {
                SheetId = Convert.ToString(item["sheet_id"]);
                Match match = pattern.Match(SheetId);

                int voltage = int.Parse(match.Groups["voltage"].Value);
                string zone = match.Groups["zone"].Value;
                string sheetNo = match.Groups["sheetno"].Value;

                SheetName = string.Empty;

                if (voltage == 4)
                    SheetName = "22kV";
                else if (voltage == 2)
                    SheetName = "6.6kV";

                SheetName += " " + zone + " " + sheetNo;
            }

            if (item.ContainsKey("voltage") && item["voltage"] != null)
                Voltage = Convert.ToInt32(item["voltage"]);

            if (item.ContainsKey("geojson") && item["geojson"] != null)
                Geometry = item["geojson"] as IDictionary<string, object>;

            if (item.ContainsKey("mvarating") && item["mvarating"] != null)
                MVARating = Convert.ToDouble(item["mvarating"]);
        }
    }
}
