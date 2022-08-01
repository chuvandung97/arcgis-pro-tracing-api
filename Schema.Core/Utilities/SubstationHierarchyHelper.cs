using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Utilities
{
    public class SubstationHierarchyHelper
    {
        HashSet<SubstationHierarchyItem> _substations;
        List<int?> _processedList;
        int _startID;
        bool _isUpstream;
        Dictionary<string, int> _voltages;
        int _voltageLimit;
        public SubstationHierarchyHelper(HashSet<SubstationHierarchyItem> substationList, int startID, bool isUpstream, int voltage)
        {
            _substations = substationList;
            _startID = startID;
            _processedList = new List<int?>();
            _isUpstream = isUpstream;
            _voltages = new Dictionary<string, int>();
            _voltages.Add("230 V", 40);
            _voltages.Add("400 V", 60);
            _voltages.Add("6.6 kV", 150);
            _voltages.Add("11 kV", 220);
            _voltages.Add("22 kV", 330);
            _voltages.Add("38 kV", 400);
            _voltages.Add("66 kV", 480);
            _voltages.Add("230 KV", 540);
            _voltages.Add("400 KV", 600);
            _voltageLimit = voltage;
        }

        public HashSet<SubstationHierarchyItem> GetHierarchy()
        {
            HashSet<SubstationHierarchyItem> list = new HashSet<SubstationHierarchyItem>();
            int initilaLevel = 0;
            var startList = _isUpstream ? _substations.Where(e => e.Level == 1).OrderBy(o => o.Level) : _substations.Where(e => e.EdgeId == _startID).OrderBy(o => o.Level);
            foreach (var item in startList)
            {
                if (initilaLevel == 0) initilaLevel = item.Level;
                if (initilaLevel == item.Level)
                {
                    var i = GetDownStreamElements(item);
                    i = FilterByVoltage(i);
                    var lst = UpdateDownstreamSunstations(i, null);
                    if (lst.Count > 0) list.UnionWith(lst);
                }
            }
            HashSet<SubstationHierarchyItem> tempList = new HashSet<SubstationHierarchyItem>();
            foreach (var item in list)
            {
                if (!tempList.Contains(item, new SubstationEqualityComparer()))
                {
                    tempList.Add(RemoveDuplicateChildren(item));
                }
            }
            list = tempList;

            return list;
        }

        private SubstationHierarchyItem RemoveDuplicateChildren(SubstationHierarchyItem Substation)
        {
            if (Substation.Children != null)
            {
                HashSet<SubstationHierarchyItem> tempList = new HashSet<SubstationHierarchyItem>();
                foreach (var item in Substation.Children)
                {
                    if (!tempList.Contains<SubstationHierarchyItem>(item, new SubstationEqualityComparer()))
                        tempList.Add(RemoveDuplicateChildren(item));
                }
                Substation.Children = tempList;
            }

            return Substation;
        }

        private HashSet<SubstationHierarchyItem> UpdateDownstreamSunstations(SubstationHierarchyItem Substation, SubstationHierarchyItem parent)
        {
            HashSet<SubstationHierarchyItem> list = new HashSet<SubstationHierarchyItem>();
            HashSet<SubstationHierarchyItem> substation_children = new HashSet<SubstationHierarchyItem>();

            foreach (var item in Substation.Children)
            {
                var children = UpdateDownstreamSunstations(item, Substation.Id.HasValue ? Substation : parent);
                substation_children.UnionWith(children);
            }

            Substation.Children = new HashSet<SubstationHierarchyItem>(substation_children);

            if (Substation.Id.HasValue)
            {
                if (parent == null || !parent.Id.HasValue || parent.Id.Value != Substation.Id.Value)
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

        private SubstationHierarchyItem GetDownStreamElements(SubstationHierarchyItem Substation)
        {
            Substation.Children = new HashSet<SubstationHierarchyItem>();

            //Added by Suresh on 3rd Aug 2018 to remove duplicate entries in the array
            _processedList.Add(Substation.EdgeId);

            foreach (var item in _substations.Where(e => e.ParentEdgeId == Substation.EdgeId && e.Level > Substation.Level))
            {
                if (!_processedList.Contains(item.EdgeId))//Added by Suresh on 3rd Aug 2018 to remove duplicate entries in the array
                {
                    Substation.Children.Add(GetDownStreamElements(item));
                    Substation.CustomerCount = Substation.Children.Sum(s => s.CustomerCount);
                }
            }

            return Substation;
        }

        private SubstationHierarchyItem FilterByVoltage(SubstationHierarchyItem Substation)
        {
            if (_voltageLimit != 0 && _voltageLimit > 60)
            {
                var children = Substation.Children;
                Substation.Children = new HashSet<SubstationHierarchyItem>();
                foreach (var item in children.Where(e => e.OperatingVoltage == null || (_voltages.ContainsKey(e.OperatingVoltage) && _voltages[e.OperatingVoltage] >= _voltageLimit)))
                {
                    Substation.Children.Add(FilterByVoltage(item));
                }
            }

            return Substation;
        }
    }
}
