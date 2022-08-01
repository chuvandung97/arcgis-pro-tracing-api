using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Utilities
{
    public class SubstationEqualityComparer : IEqualityComparer<SubstationHierarchyItem>
    {
        public bool Equals(SubstationHierarchyItem x, SubstationHierarchyItem y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(SubstationHierarchyItem obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
