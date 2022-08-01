using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.TracingCore.Models
{
    public class ElementItem
    {
        public int NetworkSourceId { get; set; }
        public string GlobalId { get; set; }
        public long ObjectId { get; set; }
        public long TerminalId { get; set; }
        public long AssetGroupCode { get; set; }
        public long AssetTypeCode { get; set; }
    }
}
