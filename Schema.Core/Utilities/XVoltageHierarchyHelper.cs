using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Schema.Core.Utilities
{
    public class XVoltageHierarchyHelper
    {
        HashSet<XVoltageReportTreeItem> _substations;
        ILookup<long, XVoltageReportTreeItem> _childrenLookup;
        HashSet<long> _processedList;
        int _voltageDepth;
        public XVoltageHierarchyHelper(HashSet<XVoltageReportTreeItem> substationList)
        {
            _substations = substationList;
            _childrenLookup = _substations.Where(s => s.Parent.HasValue).ToLookup(s => s.Parent.Value);
            _processedList = new HashSet<long>();
        }

        public HashSet<XVoltageReportTreeItem> GetHierarchy(int Voltage, Int64 ElementId, int Direction)
        {
            _voltageDepth = Voltage;
            HashSet<XVoltageReportTreeItem> list = new HashSet<XVoltageReportTreeItem>();

            XVoltageReportTreeItem root = _substations.Where(s => !s.Parent.HasValue && s.FcId == 210).FirstOrDefault();

            //added by sandip on 13th Nov if root is null
            if (root == null && Direction == 1)
            {
                //changed by sandip on 28th July 2020
                //root = _substations.Where(s => s.Parent == ElementId).FirstOrDefault();
                root = _substations.Where(s => s.Id == ElementId).FirstOrDefault();
                //if (string.IsNullOrEmpty(root.BoardName))
                //    root.BoardName = null;
            }
            else if (root == null && Direction == -1)
                root = _substations.Where(s => s.Id == ElementId).FirstOrDefault();
            //ends here

            if (root != null)
            {
                root = GetDownStreamElements(root);
                var lst = UpdateDownstreamSubstations(root, null);
                if (lst.Count > 0) list.UnionWith(lst);
            }

            HashSet<XVoltageReportTreeItem> tempList = new HashSet<XVoltageReportTreeItem>();
            foreach (var item in list)
            {
                if (!tempList.Contains(item, new XVoltageTreeItemEqualityComparer()))
                {
                    tempList.Add(RemoveDuplicateChildren(item));
                }
            }
            list = tempList;

            return list;
        }

        private XVoltageReportTreeItem RemoveDuplicateChildren(XVoltageReportTreeItem Substation)
        {
            if (Substation.Children != null)
            {
                HashSet<XVoltageReportTreeItem> tempList = new HashSet<XVoltageReportTreeItem>();
                foreach (var item in Substation.Children)
                {
                    if (!tempList.Contains(item, new XVoltageTreeItemEqualityComparer()))
                        tempList.Add(RemoveDuplicateChildren(item));
                }
                Substation.Children = tempList;
            }

            return Substation;
        }

        private HashSet<XVoltageReportTreeItem> UpdateDownstreamSubstations(XVoltageReportTreeItem Substation, XVoltageReportTreeItem parent)
        {
            HashSet<XVoltageReportTreeItem> list = new HashSet<XVoltageReportTreeItem>();
            HashSet<XVoltageReportTreeItem> substation_children = new HashSet<XVoltageReportTreeItem>();

            if (Substation.OVolt.HasValue && Substation.OVolt.Value < _voltageDepth)
                return list;

            foreach (var item in Substation.Children)
            {
                var children = UpdateDownstreamSubstations(item, !string.IsNullOrEmpty(Substation.BoardName) ? Substation : parent);
                substation_children.UnionWith(children);
            }

            Substation.Children = new HashSet<XVoltageReportTreeItem>(substation_children);

            if (!string.IsNullOrEmpty(Substation.BoardName))
            {
                if (parent == null || string.IsNullOrEmpty(parent.BoardName) || parent.BoardName != Substation.BoardName)
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

        private XVoltageReportTreeItem GetDownStreamElements(XVoltageReportTreeItem Substation)
        {
            Substation.Children = new HashSet<XVoltageReportTreeItem>();

            _processedList.Add(Substation.Id);
            //_substations.RemoveWhere(s => s.Id == Substation.Id);
            if (_childrenLookup.Contains(Substation.Id))
                foreach (var item in _childrenLookup[Substation.Id])
                {
                    if (!_processedList.Contains(item.Id))
                    {
                        Substation.Children.Add(GetDownStreamElements(item));
                        Substation.CustomerCount = Substation.Children.Sum(s => s.CustomerCount);
                    }
                }

            return Substation;
        }
    }
}
