using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Utilities
{
    public class OGBTreeItemEqualityComparer : IEqualityComparer<OGBTraceItem>
    {
        public bool Equals(OGBTraceItem x, OGBTraceItem y)
        {
            return x.Label == y.Label;
        }

        public int GetHashCode(OGBTraceItem obj)
        {
            return obj.Label.GetHashCode();
        }
    }
}
