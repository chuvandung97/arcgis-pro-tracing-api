using Schema.Core.Extensions;
using Schema.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Schema.Core.Utilities
{
    public class OGBHierarchyHelper
    {
        HashSet<OGBTraceItem> _substations;
        ILookup<long, OGBTraceItem> _childrenLookup;
        HashSet<long> _processedList;
        HashSet<int> _endPointFcs;
        HashSet<OGBTraceItem> _targetFeatures;

        public OGBHierarchyHelper(HashSet<OGBTraceItem> substationList)
        {
            _substations = new HashSet<OGBTraceItem>(substationList.Select(s => s.DeepClone()));
            _childrenLookup = _substations.Where(s => s.ParentId.HasValue).ToLookup(s => s.ParentId.Value);
            _processedList = new HashSet<long>();

            _endPointFcs = new HashSet<int> { 1, 2, 3, 9, 10, 11, 12, 13 };

            _targetFeatures = new HashSet<OGBTraceItem>();
        }

        public HashSet<OGBTraceItem> GetHierarchy()
        {
            HashSet<OGBTraceItem> list = new HashSet<OGBTraceItem>();

            OGBTraceItem root = _substations.Where(s => s.IsEdge).OrderBy(s => s.Level).FirstOrDefault();

            if (root != null)
            {
                root = GetDownStreamElements(root);
                var lst = UpdateDownstreamSubstations(root, null);
                if (lst.Count > 0) list.UnionWith(lst);
            }

            HashSet<OGBTraceItem> tempList = new HashSet<OGBTraceItem>();
            foreach (var item in list)
            {
                if (!tempList.Contains(item, new OGBTreeItemEqualityComparer()))
                {
                    tempList.Add(RemoveDuplicateChildren(item));
                }
            }
            list = tempList;
            return list;
        }

        public HashSet<OGBTraceItem> GetSchematicGeometries()
        {
            HashSet<OGBTraceItem> list = new HashSet<OGBTraceItem>();

            OGBTraceItem root = _substations.Where(s => s.IsEdge).OrderBy(s => s.Level).FirstOrDefault();

            if (root != null)
            {
                root = GetDownStreamPoints(root, 0);
                var tempList = UpdateDownstreamPoints(root, null);

                if (tempList != null)
                {
                    foreach (var item in tempList)
                    {
                        if (!item.IsEdge && item.Children != null && item.Children.Count > 0)
                        {
                            foreach (var chaild in item.Children)
                            {
                                if (!chaild.IsEdge)
                                {
                                    chaild.Children = null;
                                    list.Add(chaild.DeepClone());
                                    list.Add(GetLineByPoints(item, chaild));
                                }
                            }
                            item.Children = null;
                            list.Add(item.DeepClone());
                        }
                    }
                }
            }
            return list;
        }

        public IEnumerable<OGBTraceItem> GetSingleLegReport()
        {
            return _targetFeatures.Where(s => !string.IsNullOrEmpty(s.Label));
        }

        private OGBTraceItem RemoveDuplicateChildren(OGBTraceItem Substation)
        {
            if (Substation.Children != null)
            {
                HashSet<OGBTraceItem> tempList = new HashSet<OGBTraceItem>();
                foreach (var item in Substation.Children)
                {
                    if (!tempList.Contains(item, new OGBTreeItemEqualityComparer()))
                        tempList.Add(RemoveDuplicateChildren(item));
                }
                Substation.Children = tempList;
            }

            return Substation;
        }

        private HashSet<OGBTraceItem> UpdateDownstreamSubstations(OGBTraceItem Substation, OGBTraceItem parent)
        {
            HashSet<OGBTraceItem> list = new HashSet<OGBTraceItem>();
            HashSet<OGBTraceItem> substation_children = new HashSet<OGBTraceItem>();

            foreach (var item in Substation.Children)
            {
                var children = UpdateDownstreamSubstations(item, !string.IsNullOrEmpty(Substation.Label) ? Substation : parent);
                substation_children.UnionWith(children);
            }

            Substation.Children = new HashSet<OGBTraceItem>(substation_children);

            if (!string.IsNullOrEmpty(Substation.Label))
            {
                if (parent == null || string.IsNullOrEmpty(parent.Label) || parent.Label != Substation.Label)
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

        private HashSet<OGBTraceItem> UpdateDownstreamPoints(OGBTraceItem Item, OGBTraceItem parent)
        {
            HashSet<OGBTraceItem> list = new HashSet<OGBTraceItem>();
            HashSet<OGBTraceItem> _children = new HashSet<OGBTraceItem>();

            if (Item?.Children != null)
                foreach (var item in Item.Children)
                {
                    var children = UpdateDownstreamPoints(item, !Item.IsEdge ? Item : parent);
                    _children.UnionWith(children);
                }

            Item.Children = new HashSet<OGBTraceItem>(_children);

            if (_children.Count == 0)
                _targetFeatures.Add(Item.DeepClone());

            if (!Item.IsEdge)
            {
                if (parent == null || parent.IsEdge || parent.Id != Item.Id)
                    list.Add(Item);
                else
                    list.UnionWith(_children);
            }
            else
                list.UnionWith(_children);

            return list;
        }

        private OGBTraceItem GetDownStreamPoints(OGBTraceItem Item, double? Length)
        {
            _processedList.Add(Item.Id);

            if (_childrenLookup.Contains(Item.Id))
            {
                var newItem = Item.DeepClone();
                var _endPoint = _childrenLookup[Item.Id].Where(s => _endPointFcs.Contains(s.Fcid)).OrderBy(s => s.Fcid).FirstOrDefault();

                if (_endPoint != null)
                    newItem = _endPoint.DeepClone();

                newItem.Length = Item.Length.HasValue ? Item.Length + Length : Length + 0;
                if (string.IsNullOrEmpty(newItem.Label))
                    newItem.Label = _childrenLookup[Item.Id].Where(s => s.Fcid == 53 || s.Fcid == 51).OrderBy(s => s.Fcid).FirstOrDefault()?.Label;

                newItem.Children = new HashSet<OGBTraceItem>();

                foreach (var _item in _childrenLookup[Item.Id].Where(s => s.IsEdge))
                {
                    if (!_processedList.Contains(_item.Id))
                        newItem.Children.Add(GetDownStreamPoints(_item, newItem.Length));
                }
                newItem.Length += newItem.Children.Sum(s => s.Length);

                Item = newItem;
            }

            return Item;
        }

        private OGBTraceItem GetDownStreamElements(OGBTraceItem Item)
        {
            _processedList.Add(Item.Id);

            if (_childrenLookup.Contains(Item.Id))
            {
                var newItem = new OGBTraceItem();
                var _substation = _childrenLookup[Item.Id].Where(s => s.Fcid == 51).FirstOrDefault();
                var _switchboard = _childrenLookup[Item.Id].Where(s => s.Fcid == 53).FirstOrDefault();
                var _customer = _childrenLookup[Item.Id].Where(s => s.Fcid == 13).FirstOrDefault();

                if (_substation != null)
                    newItem = _substation;
                else if (_switchboard != null)
                    newItem = _switchboard;
                else if (_customer != null)
                    newItem = _customer;

                newItem.CustomerCount = Item.CustomerCount;

                newItem.Children = new HashSet<OGBTraceItem>();

                foreach (var item in _childrenLookup[Item.Id].Where(s => s.IsEdge))
                {
                    if (!_processedList.Contains(item.Id))
                        newItem.Children.Add(GetDownStreamElements(item));
                }
                newItem.CustomerCount += newItem.Children.Sum(s => s.CustomerCount);

                Item = newItem;
            }

            return Item;
        }

        private OGBTraceItem GetLineByPoints(OGBTraceItem source, OGBTraceItem target)
        {
            OGBTraceItem result = new OGBTraceItem();

            Dictionary<string, object> geometry = new Dictionary<string, object>();
            geometry.Add("type", "LineString");

            var lineCoord = new object[2];
            lineCoord[0] = source.Shape.GetValue("coordinates");
            lineCoord[1] = source.Shape.GetValue("coordinates");

            geometry.Add("coordinates", lineCoord);

            result.Shape = geometry;

            return result;
        }
    }
}
