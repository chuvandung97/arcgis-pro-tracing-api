using Schema.Core.Models;
using Schema.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Helpers
{
    public class SLDSubstationHierarchyHelper
    {
        HashSet<SLDSubstationHierarchyItem> _sldsubstations;
        List<int> _processedList;

        public SLDSubstationHierarchyHelper(HashSet<SLDSubstationHierarchyItem> substationList)
        {
            _sldsubstations = substationList;
            _processedList = new List<int>();
        }

        public HashSet<SLDSubstationHierarchyItem> GetHierarchy()
        {
            HashSet<SLDSubstationHierarchyItem> list = new HashSet<SLDSubstationHierarchyItem>();

            foreach (var item in _sldsubstations.Where(e => e.Level == 1 && !e.Parent.HasValue && !e.DPD.HasValue))
            {
                var i = GetDownStreamElements(item);
                var lst = UpdateDownstreamSubstations(i, null);
                if (lst.Count > 0) list.UnionWith(lst);
            }
            HashSet<SLDSubstationHierarchyItem> tempList = new HashSet<SLDSubstationHierarchyItem>();
            foreach (var item in list)
            {
                if (!tempList.Contains(item, new SLDSubstationEqualityComparer()))
                {
                    tempList.Add(RemoveDuplicateChildren(item));
                }
            }
            list = tempList;

            return list;
        }

        private SLDSubstationHierarchyItem RemoveDuplicateChildren(SLDSubstationHierarchyItem Substation)
        {
            if (Substation.Children != null)
            {
                HashSet<SLDSubstationHierarchyItem> tempList = new HashSet<SLDSubstationHierarchyItem>();
                foreach (var item in Substation.Children)
                {
                    if (!tempList.Contains<SLDSubstationHierarchyItem>(item, new SLDSubstationEqualityComparer()))
                        tempList.Add(RemoveDuplicateChildren(item));
                }
                Substation.Children = tempList;
            }

            return Substation;
        }

        private HashSet<SLDSubstationHierarchyItem> UpdateDownstreamSubstations(SLDSubstationHierarchyItem Substation, SLDSubstationHierarchyItem parent)
        {
            HashSet<SLDSubstationHierarchyItem> list = new HashSet<SLDSubstationHierarchyItem>();
            HashSet<SLDSubstationHierarchyItem> substation_children = new HashSet<SLDSubstationHierarchyItem>();

            foreach (var item in Substation.Children)
            {
                var children = UpdateDownstreamSubstations(item, !string.IsNullOrEmpty(Substation.SubstationName) ? Substation : parent);
                substation_children.UnionWith(children);
            }

            Substation.Children = new HashSet<SLDSubstationHierarchyItem>(substation_children);

            if (!string.IsNullOrEmpty(Substation.SubstationName))
            {
                if (parent == null || string.IsNullOrEmpty(parent.SubstationName) || parent.SubstationName != Substation.SubstationName)
                {
                    list.Add(Substation);
                }
                else
                    list.UnionWith(substation_children);
            }
            else
                list.UnionWith(substation_children);

            return list;
        }

        private SLDSubstationHierarchyItem GetDownStreamElements(SLDSubstationHierarchyItem SLDSubstation)
        {
            SLDSubstation.Children = new HashSet<SLDSubstationHierarchyItem>();
            foreach (var item in _sldsubstations.Where(e => e.Parent == SLDSubstation.EID && e.Level > SLDSubstation.Level))
            {
                SLDSubstation.Children.Add(GetDownStreamElements(item));
            }

            return SLDSubstation;
        }
    }
}