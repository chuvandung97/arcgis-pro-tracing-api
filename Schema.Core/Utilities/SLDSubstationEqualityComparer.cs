using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Utilities
{
    public class SLDSubstationEqualityComparer: IEqualityComparer<SLDSubstationHierarchyItem>
    {
        public bool Equals(SLDSubstationHierarchyItem x, SLDSubstationHierarchyItem y)
        {
            return x.SubstationName == y.SubstationName;
        }

        public int GetHashCode(SLDSubstationHierarchyItem obj)
        {
            return obj.SubstationName.GetHashCode();
        }
    }
}
