using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Utilities
{
    public class XVoltageTreeItemEqualityComparer : IEqualityComparer<XVoltageReportTreeItem>
    {
        public bool Equals(XVoltageReportTreeItem x, XVoltageReportTreeItem y)
        {
            return x.BoardName == y.BoardName;
        }

        public int GetHashCode(XVoltageReportTreeItem obj)
        {
            return obj.BoardName.GetHashCode();
        }
    }
}
