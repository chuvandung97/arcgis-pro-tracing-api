using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using Schema.Core.Utilities;
using Schema.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Core.Extensions;
using System.Web.Mvc;
using System.Collections;
using System.Web;

namespace Schema.Services
{
    public class TraceService : ITraceService
    {
        ILoggingService _loggingService;
        ITraceDataService _traceDataService;

        Dictionary<string, object> errorLogInfo;
        public TraceService(ILoggingService LoggingService, ITraceDataService TraceDataService)
        {
            _loggingService = LoggingService;
            _traceDataService = TraceDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        public IUserService UserService
        {
            get
            {
                return DependencyResolver.Current.GetService<IUserService>();
            }
        }
        public async Task<HashSet<Dictionary<string, object>>> PlanningSelectCablesAsync(string Geometry, string Voltage, string Subtypecd)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                var voltages = Array.ConvertAll(Voltage.Split('|'), int.Parse);
                var subTypes = Subtypecd.Split('|');
                for (int i = 0; i < voltages.Length; i++)
                {
                    int[] subs = Array.ConvertAll(subTypes[i].Split(','), int.Parse);
                    result.UnionWith(await _traceDataService.PlanningSelectCablesAsync(Geometry, Convert.ToInt32(voltages[i]), subs));
                }
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }

        public async Task<HashSet<Dictionary<string, object>>> PlanningSelectBarriersAsync(string Geometry)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _traceDataService.PlanningSelectBarriersAsync(Geometry);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }

        //public async Task<HashSet<Dictionary<string, object>>> SubstationsAsync(int Eid, int Fid)
        //{
        //    HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
        //    try
        //    {
        //        result = await _traceDataService.SubstationsAsync(Eid, Fid);
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingService.Error(ex);
        //        throw new Exception("Error");
        //    }
        //    return result;
        //}

        //public async Task<SchemaResult> DistributionTraceAsync(string Geometry, string Voltage, string Subtypecd)
        //{
        //    SchemaResult result = new SchemaResult();
        //    int voltLength = 0;
        //    int totalVal = 0;

        //    HashSet<Dictionary<string, object>> traceResult = new HashSet<Dictionary<string, object>>();
        //    try
        //    {
        //        if (Voltage.Contains("|"))
        //            voltLength = Voltage.Split('|').Length;
        //        else
        //            voltLength = 1;

        //        for (int x = 0; x <= voltLength - 1; x++)
        //        {
        //            string subType = Subtypecd.Split('|')[x];
        //            int[] subTypeArray = new int[] { Convert.ToInt16(subType) };

        //            int cablesCnt = await _traceDataService.CablesCountAsync(Geometry, Convert.ToInt16(Voltage.Split('|')[x]), subTypeArray);
        //            totalVal = totalVal + cablesCnt;
        //        }
        //        if (totalVal > 10)
        //        {
        //            result.Result = null;
        //            result.Message = "Max 10 cables are allowed to be selected in any given extent.Total selected cables are " + totalVal + "";// for voltages " + Voltage + "";
        //        }
        //        else
        //        {
        //            for (int y = 0; y <= voltLength - 1; y++)
        //            {
        //                string subType = Subtypecd.Split('|')[y];
        //                int[] subTypeArray = new int[] { Convert.ToInt16(subType) };

        //                foreach (Dictionary<string, object> trace in await _traceDataService.CablesAsync(Geometry, Convert.ToInt32(Voltage.Split('|')[y]), subTypeArray))
        //                {
        //                    traceResult.UnionWith(await _traceDataService.DistributionTraceAsync(Convert.ToInt32(trace["eid"]), Convert.ToInt16(trace["fid"])));
        //                }
        //            }
        //            result.Result = traceResult;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingService.Error(ex);
        //        throw new Exception("Error");
        //    }
        //    return result;
        //}

        //public async Task<SchemaResult> DistributionTraceAsync(string GlobalIds)
        //{
        //    SchemaResult result = new SchemaResult();

        //    HashSet<Dictionary<string, object>> traceResults = new HashSet<Dictionary<string, object>>();
        //    try
        //    {
        //        var lstGlobalIds = new HashSet<string>(GlobalIds.Split(','));

        //        if (lstGlobalIds.Count > 10)
        //        {
        //            result.Message = "Max 10 cables are allowed to be selected for tracing.";
        //            return result;
        //        }

        //        foreach (var globalId in lstGlobalIds)
        //        {
        //            Dictionary<string, object> trace = new Dictionary<string, object>();
        //            Dictionary<string, object> traceSummary = new Dictionary<string, object>();
        //            trace.Add("Cable", globalId);

        //            var traceData = await _traceDataService.DistributionTraceAsync(globalId);
        //            if (traceData.Count > 0)
        //            {
        //                var traceItems = new HashSet<S2STraceItem>(traceData.Select(s => new S2STraceItem(s)).ToArray());

        //                var firstCable = traceItems.Where(s => s.FcId > 14 && s.FcId <= 18).OrderBy(s => s.Sequence).FirstOrDefault();
        //                traceSummary.Add("Voltage", firstCable.Voltage);
        //                traceSummary.Add("CableType", firstCable.Attributes["cabletype"]);
        //                if (traceItems.Where(s => s.FcId == 5).Count() > 0 && firstCable.Attributes["cabletype"] != null && firstCable.Attributes["cabletype"].ToString().Trim().ToUpper() == "AUXILLIARY CABLE")
        //                {
        //                    traceSummary.Add("Source", traceItems.Where(s => s.FcId == 5).OrderBy(s => s.Sequence).FirstOrDefault().Attributes["substation_name"]);
        //                    traceSummary.Add("Target", traceItems.Where(s => s.FcId == 5).OrderBy(s => s.Sequence).LastOrDefault().Attributes["substation_name"]);

        //                    traceSummary.Add("Source_Substation", traceItems.Where(s => s.FcId == 5).OrderBy(s => s.Sequence).FirstOrDefault().Attributes["substation_globalid"]);
        //                    traceSummary.Add("Target_Substation", traceItems.Where(s => s.FcId == 5).OrderBy(s => s.Sequence).LastOrDefault().Attributes["substation_globalid"]);
        //                }
        //                else if (traceItems.Where(s => s.FcId < 4).Count() > 0)
        //                {
        //                    traceSummary.Add("Source", traceItems.Where(s => s.FcId < 4).OrderBy(s => s.Sequence).FirstOrDefault().Attributes["substation_name"]);
        //                    traceSummary.Add("Target", traceItems.Where(s => s.FcId < 4).OrderBy(s => s.Sequence).LastOrDefault().Attributes["substation_name"]);

        //                    traceSummary.Add("Source_Substation", traceItems.Where(s => s.FcId < 4).OrderBy(s => s.Sequence).FirstOrDefault().Attributes["substation_globalid"]);
        //                    traceSummary.Add("Target_Substation", traceItems.Where(s => s.FcId < 4).OrderBy(s => s.Sequence).LastOrDefault().Attributes["substation_globalid"]);

        //                }
        //                traceSummary.Add("CableSize", firstCable.Attributes["cablesize"]);

        //                trace.Add("TraceGeometries", traceItems.GroupBy(c => c.Id).Select(s => new { s.First().FeatureClssName, s.First().ObjectId, s.First().Sequence, s.First().GlobalId, s.First().Geometry }));
        //                trace.Add("TraceReportSummary", traceSummary);

        //                var cables = traceItems.Where(s => s.FcId > 14 && s.FcId <= 18).OrderBy(s => s.Sequence);
        //                var totalLength = cables.Where(c => c.Attributes.ContainsKey("cablelength")).Sum(c => Convert.ToDouble(c.Attributes["cablelength"]));

        //                HashSet<int> _processedCJ = new HashSet<int>();
        //                HashSet<Dictionary<string, object>> traceReportItems = new HashSet<Dictionary<string, object>>();

        //                int cnt = 1;
        //                double accLenDown = 0;
        //                double accLenDownPercent = 0;
        //                double accLenUp = totalLength;
        //                double accLenUpPercent = 100;
        //                S2STraceItem cjPrevious = null;
        //                foreach (var cable in cables)
        //                {
        //                    Dictionary<string, object> traceReportItem = new Dictionary<string, object>();
        //                    traceReportItem.Add("No", cnt);

        //                    if (cnt == 1)
        //                    {
        //                        traceReportItem.Add("Origin", "Source");
        //                    }

        //                    var cj = traceItems.Where(s => s.FcId == 6 && s.Sequence == cable.Sequence && !_processedCJ.Contains(s.Id)).FirstOrDefault();
        //                    if (cj != null && cnt != cables.Count())
        //                    {
        //                        _processedCJ.Add(cj.Id);

        //                        if (cnt > 1 && cjPrevious != null)
        //                        {
        //                            traceReportItem.Add("Origin", cjPrevious.Attributes["mrc"].ToString());
        //                        }

        //                        if (cnt == 1)
        //                        {
        //                            traceReportItem.Add("Destination", string.Format("{0}({1})", cj.Attributes["mrc"], cnt));
        //                        }
        //                        else
        //                            traceReportItem.Add("Destination", string.Format("{0}({1})", cj.Attributes["mrc"], cnt));

        //                        if (cj.Attributes.ContainsKey("installationdate") && cj.Attributes["installationdate"] != null)
        //                            traceReportItem.Add("JointDate", cj.Attributes["installationdate"].ToString().Split(' ')[0]);
        //                        else
        //                            traceReportItem.Add("JointDate", "");

        //                        cjPrevious = cj;
        //                    }

        //                    if (cnt == cables.Count())
        //                    {
        //                        traceReportItem.Add("Origin", cjPrevious.Attributes["mrc"].ToString());
        //                        traceReportItem.Add("Destination", "Destination");

        //                        if (cjPrevious.Attributes.ContainsKey("installationdate") && cjPrevious.Attributes["installationdate"] != null)
        //                            traceReportItem.Add("JointDate", cjPrevious.Attributes["installationdate"].ToString().Split(' ')[0]);
        //                        else
        //                            traceReportItem.Add("JointDate", "");
        //                    }

        //                    if (cable.Attributes.ContainsKey("cablesize") && cable.Attributes["cablesize"] != null)
        //                        traceReportItem.Add("CableSize", cable.Attributes["cablesize"].ToString());
        //                    else
        //                        traceReportItem.Add("CableSize", "");

        //                    if (cable.Attributes.ContainsKey("layingdate") && cable.Attributes["layingdate"] != null)
        //                        traceReportItem.Add("LayingDate", cable.Attributes["layingdate"].ToString().Split(' ')[0]);
        //                    else
        //                        traceReportItem.Add("LayingDate", "");

        //                    var cableLength = Convert.ToDouble(cable.Attributes["cablelength"] != null ? cable.Attributes["cablelength"] : 0.0);
        //                    accLenDown += cableLength;
        //                    accLenDownPercent = (accLenDown / totalLength) * 100;
        //                    if (cnt == 1)
        //                        accLenUp = totalLength;
        //                    else
        //                        accLenUp -= cableLength;

        //                    accLenUpPercent = (accLenUp / totalLength) * 100;

        //                    traceReportItem.Add("Length", Math.Round(cableLength, 2));

        //                    traceReportItem.Add("AccumulatedLengthDown", Math.Round(accLenDown, 2));
        //                    traceReportItem.Add("AccumulatedLengthDownPercent", string.Format("{0}%", Math.Round(accLenDownPercent, 2)));

        //                    traceReportItem.Add("AccumulatedLengthUp", Math.Round(accLenUp, 2));
        //                    traceReportItem.Add("AccumulatedLengthUpPercent", string.Format("{0}%", Math.Round(accLenUpPercent, 2)));

        //                    cnt++;
        //                    traceReportItems.Add(traceReportItem);
        //                }

        //                trace.Add("TraceReportItems", traceReportItems);
        //            }
        //            traceResults.Add(trace);
        //        }

        //        result.Result = traceResults;
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingService.Error(ex);
        //        throw new Exception("Error");
        //    }
        //    return result;
        //}
        //public async Task<SchemaResult> TransmissionTraceAsync(string Geometry, string Voltage, string Subtypecd)
        //{
        //    SchemaResult result = new SchemaResult();
        //    int voltLength = 0;
        //    int totalVal = 0;

        //    HashSet<Dictionary<string, object>> traceResult = new HashSet<Dictionary<string, object>>();
        //    try
        //    {
        //        if (Voltage.Contains("|"))
        //            voltLength = Voltage.Split('|').Length;
        //        else
        //            voltLength = 1;

        //        for (int x = 0; x <= voltLength - 1; x++)
        //        {
        //            string subType = Subtypecd.Split('|')[x];
        //            int[] subTypeArray = new int[] { Convert.ToInt16(subType) };

        //            int cablesCnt = await _traceDataService.GetTransmissionCablesCountByGeometry(Geometry, Convert.ToInt16(Voltage.Split('|')[x]), subTypeArray);
        //            totalVal = totalVal + cablesCnt;
        //        }
        //        if (totalVal > 10)
        //        {
        //            result.Result = null;
        //            result.Message = "Max 10 cables are allowed to be selected in any given extent.Total selected cables are " + totalVal + ""; //for voltages " + Voltage + "";
        //        }
        //        else
        //        {
        //            for (int y = 0; y <= voltLength - 1; y++)
        //            {
        //                string subType = Subtypecd.Split('|')[y];
        //                int[] subTypeArray = new int[] { Convert.ToInt16(subType) };

        //                foreach (Dictionary<string, object> trace in _traceDataService.GetTransmissionCablesByGeometry(Geometry, Convert.ToInt32(Voltage.Split('|')[y]), subTypeArray))
        //                {
        //                    traceResult.UnionWith(await _traceDataService.TransmissionTraceAsync(Convert.ToInt32(trace["eid"])));
        //                }
        //            }
        //            result.Result = traceResult;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingService.Error(ex);
        //        throw new Exception("Error");
        //    }
        //    return result;
        //}
        public async Task<SchemaResult> PlanningDownstreamTraceAsync(int EdgeId, int FeederId, int VoltageDepth, string Barriers)
        {
            SchemaResult result = new SchemaResult();
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            try
            {
                HashSet<int> barriersList = new HashSet<int>();
                if (!string.IsNullOrEmpty(Barriers))
                {
                    foreach (var barrier in Barriers.Split(','))
                    {
                        barriersList.Add(Convert.ToInt32(barrier));
                    }
                }
                HashSet<Dictionary<string, object>> traceResult = await _traceDataService.DownstreamTraceAsync(EdgeId, FeederId, VoltageDepth, barriersList.ToArray());
                resultData.Add("TraceItems", traceResult);

                //changed by sandip from default value "40" value to VoltageDepth
                var reportData = await _traceDataService.DownstreamTraceReportAsync(EdgeId, FeederId, VoltageDepth, barriersList.ToArray());

                var substationData = new HashSet<SubstationHierarchyItem>(reportData.Select(s => new SubstationHierarchyItem(s)).ToArray());

                SubstationHierarchyHelper helper = new SubstationHierarchyHelper(substationData, EdgeId, false, VoltageDepth);
                resultData.Add("TraceReport", helper.GetHierarchy());
                result.Result = resultData;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        public async Task<SchemaResult> PlanningUpstreamTraceAsync(int EdgeId, int FeederId)
        {
            SchemaResult result = new SchemaResult();
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            try
            {
                HashSet<Dictionary<string, object>> traceResult = await _traceDataService.UpstreamTraceAsync(EdgeId, FeederId);
                resultData.Add("TraceItems", traceResult);

                var reportData = await _traceDataService.UpstreamTraceReportAsync(EdgeId, FeederId);

                var substationData = new HashSet<SubstationHierarchyItem>(reportData.Select(s => new SubstationHierarchyItem(s)).ToArray());

                SubstationHierarchyHelper helper = new SubstationHierarchyHelper(substationData, EdgeId, true, 40);
                resultData.Add("TraceReport", helper.GetHierarchy());
                result.Result = resultData;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //public async Task<SchemaResult> SLDRingTraceAsync(string Geometry)
        //{
        //    SchemaResult resultData = new SchemaResult();
        //    HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

        //    List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
        //    try
        //    {
        //        result = await _traceDataService.SLDRingTraceAsync(Geometry);

        //        var newList = result.GroupBy(g => g["switchboard_globalid"]).Select(g => g.Aggregate((i1, i2) => i1["switchboard_globalid"] == i2["switchboard_globalid"] & Convert.ToInt32(i1["panel_no"]) < Convert.ToInt32(i2["panel_no"]) ? i1 : i2)).ToList();

        //        foreach (var feeder in newList)
        //        {
        //            feeder.Add("TraceItems", await _traceDataService.SLDRingItemsAsync(Convert.ToInt32(feeder["fid"])));
        //            var reportData = await _traceDataService.SLDRingReportAsync(Convert.ToInt32(feeder["fid"]));

        //            var substationData = new HashSet<SLDSubstationHierarchyItem>(reportData.Select(s => new SLDSubstationHierarchyItem(s)).ToArray());
        //            SLDSubstationHierarchyHelper helper = new SLDSubstationHierarchyHelper(substationData);

        //            feeder.Add("TraceReport", helper.GetHierarchy());
        //            resultList.Add(feeder);
        //        }
        //        resultData.Result = resultList;
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingService.Error(ex);
        //        throw new Exception("Error");
        //    }
        //    return resultData;
        //}
        //public async Task<SchemaResult> SLDMultiRingTraceAsync(string Geometry)
        //{
        //    SchemaResult resultData = new SchemaResult();
        //    HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
        //    List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
        //    try
        //    {
        //        result = await _traceDataService.SLDMultiRingTraceAsync(Geometry);

        //        List<Dictionary<string, object>> newList = result.ToList();

        //        //var newList = result.GroupBy(g => g["switchboard_globalid"]).Select(g => g.Aggregate((i1, i2) => i1["switchboard_globalid"] == i2["switchboard_globalid"] & Convert.ToInt32(i1["panel_no"]) < Convert.ToInt32(i2["panel_no"]) ? i1 : i2)).ToList();

        //        foreach (var feeder in newList)
        //        {
        //            feeder.Add("TraceItems", await _traceDataService.SLDRingItemsAsync(Convert.ToInt32(feeder["fid"])));
        //            var reportData = await _traceDataService.SLDRingReportAsync(Convert.ToInt32(feeder["fid"]));

        //            var substationData = new HashSet<SLDSubstationHierarchyItem>(reportData.Select(s => new SLDSubstationHierarchyItem(s)).ToArray());
        //            SLDSubstationHierarchyHelper helper = new SLDSubstationHierarchyHelper(substationData);

        //            feeder.Add("TraceReport", helper.GetHierarchy());
        //            resultList.Add(feeder);
        //        }
        //        resultData.Result = resultList;
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingService.Error(ex);
        //        throw new Exception("Error");
        //    }
        //    return resultData;
        //}
        //public async Task<SchemaResult> SLDXVoltageTraceAsync(string Geometry)
        //{
        //    SchemaResult result = new SchemaResult();

        //    try
        //    {
        //        HashSet<Dictionary<string, object>> resultData = new HashSet<Dictionary<string, object>>();

        //        var feeders = await _traceDataService.SLDRingTraceAsync(Geometry);

        //        var newList = feeders.GroupBy(g => g["switchboard_globalid"]).Select(g => g.Aggregate((i1, i2) => i1["switchboard_globalid"] == i2["switchboard_globalid"] & Convert.ToInt32(i1["panel_no"]) < Convert.ToInt32(i2["panel_no"]) ? i1 : i2)).ToList();

        //        foreach (var feeder in newList)
        //        {
        //            feeder.Add("TraceItems", await _traceDataService.SLDXVoltageItemsAsync(Convert.ToInt32(feeder["fid"])));

        //            var reportData = await _traceDataService.SLDXVoltageReportAsync(Convert.ToInt32(feeder["fid"]));

        //            //var substationData = new HashSet<XVoltageReportTreeItem>(reportData.Select(s => new XVoltageReportTreeItem(s)).ToArray());
        //            //XVoltageHierarchyHelper helper = new XVoltageHierarchyHelper(substationData);
        //            //feeder.Add("TraceReport", helper.GetHierarchy());

        //            resultData.Add(feeder);
        //        }

        //        result.Result = resultData;
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingService.Error(ex);
        //        throw new Exception("Error");
        //    }
        //    return result;
        //}

        //public async Task<SchemaResult> SLDRingTraceDesktopAsync(string GlobalIds)
        //{
        //    SchemaResult result = new SchemaResult();
        //    HashSet<Dictionary<string, object>> traceResults = new HashSet<Dictionary<string, object>>();
        //    try
        //    {
        //        var lstGlobalIds = new HashSet<string>(GlobalIds.Split(','));

        //        if (lstGlobalIds.Count > 10)
        //        {
        //            result.Message = "Max 10 cables are allowed to be selected for tracing.";
        //            return result;
        //        }

        //        var list = await _traceDataService.SLDRingTraceDesktopAsync(lstGlobalIds.ToArray());

        //        var feeders = list.GroupBy(g => g["switchboard_globalid"]).Select(g => g.Aggregate((i1, i2) => i1["switchboard_globalid"] == i2["switchboard_globalid"] & Convert.ToInt32(i1["panel_no"]) < Convert.ToInt32(i2["panel_no"]) ? i1 : i2)).ToList();

        //        foreach (var feeder in feeders)
        //        {
        //            feeder.Add("TraceItems", await _traceDataService.SLDRingItemsDesktopAsync(Convert.ToInt32(feeder["fid"])));
        //            //var reportData = await _traceDataService.SLDRingReportAsync(Convert.ToInt32(feeder["fid"]));

        //            //var substationData = new HashSet<SLDSubstationHierarchyItem>(reportData.Select(s => new SLDSubstationHierarchyItem(s)).ToArray());
        //            // SLDSubstationHierarchyHelper helper = new SLDSubstationHierarchyHelper(substationData);

        //            //feeder.Add("TraceReport", helper.GetHierarchy());
        //            traceResults.Add(feeder);
        //        }
        //        result.Result = traceResults;
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingService.Error(ex);
        //        throw new Exception("Error");
        //    }
        //    return result;
        //}

        //public async Task<SchemaResult> SLDMultiRingTraceDesktopAsync(string GlobalIds)
        //{
        //    SchemaResult result = new SchemaResult();
        //    HashSet<Dictionary<string, object>> traceResults = new HashSet<Dictionary<string, object>>();
        //    try
        //    {
        //        var lstGlobalIds = new HashSet<string>(GlobalIds.Split(','));

        //        if (lstGlobalIds.Count > 10)
        //        {
        //            result.Message = "Max 10 cables are allowed to be selected for tracing.";
        //            return result;
        //        }

        //        var feeders = await _traceDataService.SLDMultiRingDesktopAsync(lstGlobalIds.ToArray());

        //        //var feeders = list.GroupBy(g => g["switchboard_globalid"]).Select(g => g.Aggregate((i1, i2) => i1["switchboard_globalid"] == i2["switchboard_globalid"] & Convert.ToInt32(i1["panel_no"]) < Convert.ToInt32(i2["panel_no"]) ? i1 : i2)).ToList();

        //        foreach (var feeder in feeders)
        //        {
        //            feeder.Add("TraceItems", await _traceDataService.SLDRingItemsDesktopAsync(Convert.ToInt32(feeder["fid"])));
        //            //var reportData = await _traceDataService.SLDRingReportAsync(Convert.ToInt32(feeder["fid"]));

        //            //var substationData = new HashSet<SLDSubstationHierarchyItem>(reportData.Select(s => new SLDSubstationHierarchyItem(s)).ToArray());
        //            // SLDSubstationHierarchyHelper helper = new SLDSubstationHierarchyHelper(substationData);

        //            //feeder.Add("TraceReport", helper.GetHierarchy());
        //            traceResults.Add(feeder);
        //        }
        //        result.Result = traceResults;
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingService.Error(ex);
        //        throw new Exception("Error");
        //    }
        //    return result;
        //}

        //public async Task<SchemaResult> GISRingTraceForDesktopAsync(int SLDFeederID)
        //{
        //    SchemaResult result = new SchemaResult();
        //    try
        //    {
        //        var traceResult = await _traceDataService.GISRingTraceItemsForDesktopAsync(SLDFeederID);
        //        result.Result = traceResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingService.Error(ex);
        //        throw new Exception("Error");
        //    }
        //    return result;
        //}

        public async Task<SchemaResult> CableTraceAsync(string Geometry = null, string Voltage = null, string Subtypecd = null, string GlobalIds = null)
        {
            SchemaResult result = new SchemaResult();
            HashSet<Dictionary<string, object>> traceResult = new HashSet<Dictionary<string, object>>();
            try
            {
                traceResult = await AuthorizeAPI(Voltage);
                if (traceResult.Count == 0)
                {
                    int[] voltages = null;
                    List<string> NetworkTypes = new List<string>();
                    HashSet<Dictionary<string, object>> _cables = new HashSet<Dictionary<string, object>>();
                    if (!string.IsNullOrEmpty(GlobalIds))
                    {
                        var lstGlobalIds = new HashSet<string>(GlobalIds.Split(','));
                        if (lstGlobalIds.Count > 10)
                        {
                            result.Message = "Max 10 cables are allowed to be selected for tracing.";
                            return result;
                        }

                        _cables = await _traceDataService.CablesByGlobalIdsAsync(lstGlobalIds.ToArray());
                    }
                    else if (!string.IsNullOrEmpty(Geometry) && !string.IsNullOrEmpty(Voltage) && !string.IsNullOrEmpty(Subtypecd))
                    {
                        voltages = Array.ConvertAll(Voltage.Split('|'), int.Parse);

                        var subTypes = Subtypecd.Split('|');
                        for (int i = 0; i < voltages.Length; i++)
                        {
                            //WriteErrorLog("Start " + (i + 1).ToString() + " Request :-");
                            int[] subs = Array.ConvertAll(subTypes[i].Split(','), int.Parse);
                            _cables.UnionWith(await _traceDataService.CablesByGeometryAsync(Geometry, Convert.ToInt32(voltages[i]), subs));
                            //WriteErrorLog("End " + (i + 1).ToString() + " Request :-");
                        }
                    }
                    else
                    {
                        result.Message = "Required parameters missing. Please send valid parameters.";
                        return result;
                    }

                    if (voltages != null && voltages.Length > 0)
                    {
                        foreach (int voltage in voltages)
                        {
                            if (voltage > 330 && !NetworkTypes.Contains("HT Transmission"))
                                NetworkTypes.Add("HT Transmission");
                            else if (voltage <= 330 && voltage >= 150 && !NetworkTypes.Contains("HT Distribution"))
                                NetworkTypes.Add("HT Distribution");
                            else if (voltage < 150 && !NetworkTypes.Contains("LV"))
                                NetworkTypes.Add("LV");
                        }
                    }

                    if (_cables.Count > 10)
                    {
                        result.Message = "Max 10 cables are allowed to be selected for tracing.";
                        return result;
                    }
                    else if (_cables.Count == 0)
                    {
                        result.Message = string.Format("Select {0} Cable", string.Join("/", NetworkTypes));
                        return result;
                    }
                    //WriteErrorLog("Cable Start Request :-");
                    long[] _cableIds = _cables.Select(c => Convert.ToInt64(c["eid"])).ToArray();

                    var cableTraceData = await _traceDataService.CablesTraceAsync(_cableIds);
                    //WriteErrorLog("Cable End Request :-");

                    HashSet<Dictionary<string, object>> traceResults = new HashSet<Dictionary<string, object>>();
                    foreach (var _cableTrace in cableTraceData)
                    {
                        if (_cableTrace.ContainsKey("result"))
                            traceResults.Add(_cableTrace["result"] as Dictionary<string, object>);
                    }
                    //WriteErrorLog("Cable Trace Final Request :-");
                    result.Result = traceResults;
                }
                else
                    result.Message = "Authorization has been denied for this request.";
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                result.Message = "An error has occurred. Please contact administrator.";
            }
            return result;
        }
        public async Task<SchemaResult> ContextMenuCableTraceAsync(string CableIds)
        {
            SchemaResult result = new SchemaResult();
            Dictionary<string, object> resultData = new Dictionary<string, object>();

            try
            {
                HashSet<long> CableId = new HashSet<long>();
                if (!string.IsNullOrEmpty(CableIds))
                {
                    foreach (var barrier in CableIds.Split(','))
                    {
                        CableId.Add(Convert.ToInt32(barrier));
                    }
                }

                var cableTraceData = await _traceDataService.ContextMenuCableTraceAsync(CableId.ToArray());
                HashSet<Dictionary<string, object>> traceResults = new HashSet<Dictionary<string, object>>();

                foreach (var _cableTrace in cableTraceData)
                {
                    if (_cableTrace.ContainsKey("result"))
                        traceResults.Add(_cableTrace["result"] as Dictionary<string, object>);
                }
                if (traceResults.Count > 2)
                {
                    result.Message = "Max 2 cables are allowed to be selected for tracing.";
                    return result;
                }
                else
                {
                    result.Result = traceResults;
                }
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
            }
            return result;
        }
        public async Task<SchemaResult> SLDSingleRingTraceAsync(string Geometry = null, string GlobalIds = null)
        {
            SchemaResult result = new SchemaResult();
            HashSet<Dictionary<string, object>> traceResults = new HashSet<Dictionary<string, object>>();
            try
            {
                HashSet<Dictionary<string, object>> _feeders = new HashSet<Dictionary<string, object>>();
                if (!string.IsNullOrEmpty(GlobalIds))
                {
                    var lstGlobalIds = new HashSet<string>(GlobalIds.Split(','));
                    if (lstGlobalIds.Count > 10)
                    {
                        result.Message = "Max 10 cables are allowed to be selected for tracing.";
                        return result;
                    }

                    _feeders = await _traceDataService.GetSLDSingleFeederAsync(null, lstGlobalIds.ToArray());
                }
                else if (!string.IsNullOrEmpty(Geometry))
                {
                    _feeders = await _traceDataService.GetSLDSingleFeederAsync(Geometry);
                }
                else
                {
                    result.Message = "Required parameters missing. Please send valid parameters.";
                    return result;
                }

                if (_feeders.Count == 0)
                {
                    result.Message = "No rings selected.";
                    return result;
                }

                foreach (var feeder in _feeders)
                {
                    int feederId = Convert.ToInt32(feeder["fid"]);
                    feeder.Add("TraceItems", await _traceDataService.SLDRingItemsAsync(feederId));
                    //var reportData = await _traceDataService.SLDRingReportAsync(feederId);
                    //if (reportData != null && reportData.Count == 1)
                    //{
                    //    feeder.Add("TraceReport", reportData.First().GetValue("report"));
                    //}

                    traceResults.Add(feeder);
                }

                result.Result = traceResults;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw;
            }
            return result;
        }

        public async Task<SchemaResult> SLDMultiRingTraceAsync(string Geometry = null, string GlobalIds = null)
        {
            SchemaResult result = new SchemaResult();
            HashSet<Dictionary<string, object>> traceResults = new HashSet<Dictionary<string, object>>();
            try
            {
                HashSet<Dictionary<string, object>> _feeders = new HashSet<Dictionary<string, object>>();
                if (!string.IsNullOrEmpty(GlobalIds))
                {
                    var lstGlobalIds = new HashSet<string>(GlobalIds.Split(','));
                    if (lstGlobalIds.Count > 10)
                    {
                        result.Message = "Max 10 cables are allowed to be selected for tracing.";
                        return result;
                    }

                    _feeders = await _traceDataService.GetSLDMultiFeederAsync(null, lstGlobalIds.ToArray());
                }
                else if (!string.IsNullOrEmpty(Geometry))
                {
                    _feeders = await _traceDataService.GetSLDMultiFeederAsync(Geometry);
                }
                else
                {
                    result.Message = "Required parameters missing. Please send valid parameters.";
                    return result;
                }

                if (_feeders.Count == 0)
                {
                    result.Message = "No rings selected.";
                    return result;
                }

                foreach (var feeder in _feeders)
                {
                    int feederId = Convert.ToInt32(feeder["fid"]);
                    feeder.Add("TraceItems", await _traceDataService.SLDRingItemsAsync(feederId));

                    //var reportData = await _traceDataService.SLDRingReportAsync(feederId);
                    //if (reportData != null && reportData.Count == 1)
                    //{
                    //    feeder.Add("TraceReport", reportData.First().GetValue("report"));
                    //}

                    traceResults.Add(feeder);
                }

                result.Result = traceResults;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw;
            }
            return result;

            //SchemaResult resultData = new SchemaResult();
            //HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            //List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
            //try
            //{
            //    result = await _traceDataService.SLDMultiRingTraceAsync(Geometry);

            //    List<Dictionary<string, object>> newList = result.ToList();

            //    //var newList = result.GroupBy(g => g["switchboard_globalid"]).Select(g => g.Aggregate((i1, i2) => i1["switchboard_globalid"] == i2["switchboard_globalid"] & Convert.ToInt32(i1["panel_no"]) < Convert.ToInt32(i2["panel_no"]) ? i1 : i2)).ToList();

            //    foreach (var feeder in newList)
            //    {
            //        feeder.Add("TraceItems", await _traceDataService.SLDRingItemsAsync(Convert.ToInt32(feeder["fid"])));
            //        var reportData = await _traceDataService.SLDRingReportAsync(Convert.ToInt32(feeder["fid"]));

            //        var substationData = new HashSet<SLDSubstationHierarchyItem>(reportData.Select(s => new SLDSubstationHierarchyItem(s)).ToArray());
            //        SLDSubstationHierarchyHelper helper = new SLDSubstationHierarchyHelper(substationData);

            //        feeder.Add("TraceReport", helper.GetHierarchy());
            //        resultList.Add(feeder);
            //    }
            //    resultData.Result = resultList;
            //}
            //catch (Exception ex)
            //{
            //    _loggingService.Error(ex);
            //    throw new Exception("Error");
            //}
            //return resultData;
        }

        public async Task<SchemaResult> SLDRingReportAsync(string FeederIds)
        {
            SchemaResult result = new SchemaResult();
            HashSet<Dictionary<string, object>> traceResults = new HashSet<Dictionary<string, object>>();
            try
            {
                HashSet<Dictionary<string, object>> _feeders = new HashSet<Dictionary<string, object>>();
                if (!string.IsNullOrEmpty(FeederIds))
                {
                    foreach (var feeder in FeederIds.TrimEnd(',').Split(','))
                    {
                        try
                        {
                            var _feeder = new Dictionary<string, object>();
                            int feederId = Convert.ToInt32(feeder);
                            _feeder.Add("fid", feederId);

                            var reportData = await _traceDataService.SLDRingReportAsync(feederId);
                            if (reportData != null && reportData.Count == 1)
                            {
                                _feeder.Add("TraceReport", reportData.First().GetValue("report"));
                            }
                            traceResults.Add(_feeder);
                        }
                        catch (Exception ex)
                        {
                            _loggingService.Error(ex);
                        }
                    }
                }
                result.Result = traceResults;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw;
            }
            return result;

            //SchemaResult resultData = new SchemaResult();
            //HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            //List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
            //try
            //{
            //    result = await _traceDataService.SLDMultiRingTraceAsync(Geometry);

            //    List<Dictionary<string, object>> newList = result.ToList();

            //    //var newList = result.GroupBy(g => g["switchboard_globalid"]).Select(g => g.Aggregate((i1, i2) => i1["switchboard_globalid"] == i2["switchboard_globalid"] & Convert.ToInt32(i1["panel_no"]) < Convert.ToInt32(i2["panel_no"]) ? i1 : i2)).ToList();

            //    foreach (var feeder in newList)
            //    {
            //        feeder.Add("TraceItems", await _traceDataService.SLDRingItemsAsync(Convert.ToInt32(feeder["fid"])));
            //        var reportData = await _traceDataService.SLDRingReportAsync(Convert.ToInt32(feeder["fid"]));

            //        var substationData = new HashSet<SLDSubstationHierarchyItem>(reportData.Select(s => new SLDSubstationHierarchyItem(s)).ToArray());
            //        SLDSubstationHierarchyHelper helper = new SLDSubstationHierarchyHelper(substationData);

            //        feeder.Add("TraceReport", helper.GetHierarchy());
            //        resultList.Add(feeder);
            //    }
            //    resultData.Result = resultList;
            //}
            //catch (Exception ex)
            //{
            //    _loggingService.Error(ex);
            //    throw new Exception("Error");
            //}
            //return resultData;
        }

        public async Task<SchemaResult> SLDPropagateRingAsync(int SLDFeederID)
        {
            SchemaResult result = new SchemaResult();
            try
            {
                var traceResult = await _traceDataService.SLDPropagateRingAsync(SLDFeederID);
                result.Result = traceResult;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw;
            }
            return result;
        }

        public async Task<SchemaResult> SLDXVoltSubstationsAsync(string Name = null, string RequestFlag = "XVOLT")
        {
            SchemaResult result = new SchemaResult();
            try
            {
                var traceResult = await _traceDataService.SLDXVoltSubstationsAsync(Name, RequestFlag);
                result.Result = traceResult;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw;
            }
            return result;
        }

        public async Task<SchemaResult> SLDXVoltTransformersAsync(string MRC)
        {
            SchemaResult result = new SchemaResult();
            try
            {
                var transformersList = new HashSet<XVoltageTransformerItem>();

                var dataResult = await _traceDataService.SLDXVoltTransformersAsync(MRC);

                var res = new HashSet<XVoltageTransformerItem>(dataResult.Select(s => new XVoltageTransformerItem(s)));

                var groupedList = res.GroupBy(r => r.TransformerNo);

                foreach (var t in groupedList)
                {
                    if (t.Count() == 1)
                        transformersList.UnionWith(t);
                    else
                    {
                        var trans = t.OrderByDescending(v => v.Voltage).ThenBy(s => s.SubTypeCD).FirstOrDefault();
                        if (trans != null)
                        {
                            trans.Type = "22kV/6.6kV";
                            trans.SheetName = string.Join("/", t.OrderByDescending(v => v.Voltage).Select(s => s.SheetName).Distinct());
                            trans.MVARating = t.OrderBy(v => v.Voltage).ThenBy(s => s.SubTypeCD).Select(s => s.MVARating).FirstOrDefault();
                            transformersList.Add(trans);
                        }
                    }
                }
                result.Result = transformersList;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw;
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> SLDSingleFeedersAync(string Geometry, bool IsMultiFeeder = false)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

            HashSet<Dictionary<string, object>> finalResult = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _traceDataService.SLDSingleFeedersAync(Geometry, IsMultiFeeder);
                Int64 fid = 0;
                Int64 eid = 0;
                string feederName = string.Empty;
                string destination = string.Empty;

                Int64 fid1 = 0;
                Int64 eid1 = 0;
                string feederName1 = string.Empty;
                string destination1 = string.Empty;

                int count = 0;

                foreach (Dictionary<string, object> item in (IEnumerable)result)
                {
                    if (item["fid"] != null)
                        fid = Convert.ToInt64(item["fid"]);

                    if (item["eid"] != null)
                        eid = Convert.ToInt64(item["eid"]);

                    if (item["feedername"] != null)
                        feederName = item["feedername"].ToString();

                    if (item["destination"] != null)
                        destination = item["destination"].ToString();

                    if (count == 0)
                    {
                        fid1 = fid; eid1 = eid; feederName1 = feederName; destination1 = destination;
                        finalResult.Add(item);
                    }
                    count++;

                    if (count > 1)
                    {
                        if ((fid1 == fid) && (eid1 == eid) && (feederName1 == feederName) && (destination1 == destination))
                        { }
                        else
                        {
                            fid1 = fid; eid1 = eid; feederName1 = feederName; destination1 = destination;
                            finalResult.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return finalResult;
        }
        public async Task<HashSet<Dictionary<string, object>>> SLDXVoltageSelectBarriersAsync(string Geometry)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _traceDataService.SLDXVoltageSelectBarriersAsync(Geometry);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }

        public async Task<SchemaResult> SLDXVoltTraceAsync(int TransformerId, int Voltage, string Barriers = null)
        {
            SchemaResult result = new SchemaResult();
            var traceResult = new Dictionary<string, object>();
            try
            {
                var lstBarriers = new HashSet<int>();
                if (!string.IsNullOrEmpty(Barriers))
                {
                    lstBarriers = new HashSet<int>(Array.ConvertAll(Barriers.TrimEnd(',').Split(','), int.Parse));
                }

                var trace = await _traceDataService.SLDXVoltTraceFeedersAsync(TransformerId, Voltage, lstBarriers.Count > 0 ? lstBarriers.ToArray() : null);

                if (trace.Count == 1)
                {
                    traceResult.Add("TraceItems", trace.First().GetValue("graphics"));
                    traceResult.Add("Substations", trace.First().GetValue("substations"));
                    traceResult.Add("Transformers", trace.First().GetValue("transformers"));
                    traceResult.Add("Address", trace.First().GetValue("address"));
                }
                //var reportData = await _traceDataService.SLDXVoltTraceFeedersReportAsync(TransformerId, Voltage, lstBarriers.ToArray());

                //var substationData = new HashSet<XVoltageReportTreeItem>(reportData.Select(s => new XVoltageReportTreeItem(s)).ToArray());
                //XVoltageHierarchyHelper helper = new XVoltageHierarchyHelper(substationData);
                //traceResult.Add("TraceReport", helper.GetHierarchy(Voltage));    

                result.Result = traceResult;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw;
            }
            return result;
        }

        public async Task<SchemaResult> LVOGBTraceAsync(int Mode, string Geometry = null, string GlobalId = null)
        {
            SchemaResult result = new SchemaResult();
            var traceResult = new Dictionary<string, object>();
            var validModes = new int[] { 0, 1, 2, 3 };
            var endPointFeatureClasses = new int[] { 9, 10, 11, 12, 13, 53 };
            try
            {
                if (!validModes.Contains(Mode))
                    result.Message = "Invalid direction parameter.";
                if (string.IsNullOrEmpty(Geometry) && string.IsNullOrEmpty(GlobalId))
                    result.Message = "Missing required parameters.";
                if (!string.IsNullOrEmpty(result.Message))
                    return result;

                var selectedOGB = await _traceDataService.GetOGBAsync(Geometry, GlobalId);

                if (selectedOGB.Count == 1)
                {
                    traceResult.Add("SelectedOGB", selectedOGB);
                    long switchBoardId = selectedOGB.First().GetValue<long>("id");
                    HashSet<Dictionary<string, object>> traceData = new HashSet<Dictionary<string, object>>();
                    if (Mode == 0)
                    {
                        //First leg
                        traceData = await _traceDataService.OGBFirstLegTraceAsync(switchBoardId);
                        if (traceData != null && traceData.Count == 1)
                        {
                            traceResult.Add("TraceItems", traceData.First().GetValue("graphics"));
                            traceResult.Add("SchematicItems", traceData.First().GetValue("schematics"));
                            traceResult.Add("TraceReport", traceData.First().GetValue("report"));
                        }
                    }
                    else if (Mode == 1)
                    {
                        //Upstream
                        traceData = await _traceDataService.OGBUpstreamTraceAsync(switchBoardId);

                        var upstreamData = new HashSet<OGBTraceItem>(traceData.Select(s => new OGBTraceItem(s)).ToArray());

                        var newUpstreamData = new HashSet<OGBTraceItem>();
                        foreach (var traceItem in upstreamData.OrderByDescending(t => t.Level))
                        {
                            newUpstreamData.Add(traceItem);
                            if (traceItem.Fcid == 53 && traceItem.Properties != null && traceItem.Properties.GetValue<int>("subtypecd") == 1)
                                break;
                        }

                        //long swid = 0; int level = 0;
                        //foreach (var traceItem in upstreamData.OrderByDescending(t => t.Level))
                        //{
                        //    if (traceItem.Fcid == 53 && traceItem.Properties != null && traceItem.Properties.GetValue<int>("subtypecd") == 1)
                        //    {
                        //        swid = traceItem.Id;
                        //        level = traceItem.Level;
                        //        break;
                        //    }
                        //    else
                        //        newUpstreamData.Add(traceItem);
                        //}
                        //newUpstreamData.UnionWith(upstreamData.Where(s => s.Level == level && s.Id != swid));

                        traceResult.Add("TraceItems", newUpstreamData);

                        HashSet<Dictionary<string, object>> suppy_report = new HashSet<Dictionary<string, object>>();
                        foreach (var item in newUpstreamData)
                        {
                            if (item.Fcid == 53)
                            {
                                Dictionary<string, object> supply_item = new Dictionary<string, object>();
                                supply_item.Add("id", item.Id);
                                supply_item.Add("mrc", item.Properties.GetValue("mrc"));
                                supply_item.Add("fcid", item.Fcid);
                                supply_item.Add("type", item.Properties.GetValue("type"));
                                supply_item.Add("boardno", item.Properties.GetValue("boardno"));
                                supply_item.Add("subtypecd", item.Properties.GetValue("subtypecd"));
                                supply_item.Add("substation_name", item.Properties.GetValue("substation_name"));
                                supply_item.Add("geojson", item.Shape);
                                suppy_report.Add(supply_item);
                            }
                        }
                        traceResult.Add("SuppyReport", suppy_report);
                        //OGBHierarchyHelper helper = new OGBHierarchyHelper(newUpstreamData);
                        //traceResult.Add("TraceReport", helper.GetHierarchy());
                    }
                    else if (Mode == 2)
                    {
                        //Downstream
                        traceData = await _traceDataService.OGBDownstreamTraceAsync(switchBoardId);
                        if (traceData != null && traceData.Count == 1)
                        {
                            traceResult.Add("TraceItems", traceData.First().GetValue("graphics"));
                            traceResult.Add("SuppyReport", traceData.First().GetValue("supply"));
                            traceResult.Add("LoadReport", traceData.First().GetValue("load"));
                        }

                        //var downstreamData = new HashSet<OGBTraceItem>(traceData.Select(s => new OGBTraceItem(s)).ToArray());
                        //traceResult.Add("TraceItems", downstreamData);

                        //OGBHierarchyHelper helper = new OGBHierarchyHelper(downstreamData);
                        //traceResult.Add("TraceReport", helper.GetHierarchy());

                    }
                    else if (Mode == 3)
                    {
                        //Both Upstream and Downstream
                        traceData = await _traceDataService.OGBUpstreamTraceAsync(switchBoardId);
                        var upstreamData = new HashSet<OGBTraceItem>(traceData.Select(s => new OGBTraceItem(s)).ToArray());

                        var upstreamLVB = upstreamData.Where(t => t.Fcid == 53 && t.Properties != null && t.Properties.GetValue<int>("subtypecd") == 1).OrderByDescending(t => t.Level).FirstOrDefault();
                        if (upstreamLVB != null)
                        {
                            traceData = await _traceDataService.OGBDownstreamTraceAsync(upstreamLVB.Id);

                            if (traceData != null && traceData.Count == 1)
                            {
                                traceResult.Add("TraceItems", traceData.First().GetValue("graphics"));
                                traceResult.Add("SuppyReport", traceData.First().GetValue("supply"));
                                traceResult.Add("LoadReport", traceData.First().GetValue("load"));
                            }

                            //var downstreamData = new HashSet<OGBTraceItem>(traceData.Select(s => new OGBTraceItem(s)).ToArray());
                            //traceResult.Add("TraceItems", downstreamData);

                            //OGBHierarchyHelper helper = new OGBHierarchyHelper(downstreamData);
                            //traceResult.Add("TraceReport", helper.GetHierarchy());
                        }
                    }
                }
                else if (selectedOGB.Count == 0)
                    result.Message = "No OGB selected.";
                else if (selectedOGB.Count > 1)
                    result.Message = "More than one OGB selected. Please select only one OGB.";

                result.Result = traceResult;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw;
            }
            return result;
        }
        /*public async Task<SchemaResult> SLDCustomerCountTraceAsync(string Geometry)
        {
            SchemaResult result = new SchemaResult();

            try
            {
                HashSet<Dictionary<string, object>> traceResults = new HashSet<Dictionary<string, object>>();
                HashSet<Dictionary<string, object>> _feeders = new HashSet<Dictionary<string, object>>();

                if (!string.IsNullOrEmpty(Geometry))
                {
                    _feeders = await _traceDataService.GetSLDSingleFeederAsync(Geometry);
                }
                else
                {
                    result.Message = "Required parameters missing. Please send valid parameters.";
                    return result;
                }

                if (_feeders.Count == 0)
                {
                    result.Message = "No rings selected.";
                    return result;
                }
                foreach (var feeder in _feeders)
                {
                    var traceData = await _traceDataService.SLDCustomerCountTraceItemsAsync(Convert.ToInt32(feeder["fid"]));
                    if (traceData != null && traceData.Count == 1)
                    {
                        feeder.Add("TraceItems", traceData.First().GetValue("graphics"));
                        var reportData = traceData.First().GetValue<Dictionary<string, object>[]>("report");

                        var substationData = new HashSet<XVoltageReportTreeItem>(reportData.Select(s => new XVoltageReportTreeItem(s)).ToArray());
                        XVoltageHierarchyHelper helper = new XVoltageHierarchyHelper(substationData);
                        feeder.Add("TraceReport", helper.GetHierarchy(60));
                    }
                    //var reportData = await _traceDataService.SLDCustomerCountTraceReportAsync(Convert.ToInt32(feeder["fid"]));
                    //var substationData = new HashSet<XVoltageReportTreeItem>(reportData.Select(s => new XVoltageReportTreeItem(s)).ToArray());
                    //XVoltageHierarchyHelper helper = new XVoltageHierarchyHelper(substationData);
                    //feeder.Add("TraceReport", helper.GetHierarchy(60));

                    traceResults.Add(feeder);
                }

                result.Result = traceResults;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }*/
        //Sandip
        public async Task<SchemaResult> SLDCustomerCountTraceAsync(int FeederId, int ElementId, string Barriers = null, int Direction = 0)
        {
            SchemaResult result = new SchemaResult();
            Dictionary<string, object> feeder = new Dictionary<string, object>();
            HashSet<Dictionary<string, object>> traceResults = new HashSet<Dictionary<string, object>>();
            var httpContext = HttpContext.Current;
            try
            {
                //WriteErrorLog("Sandip :-" + httpContext.User.Identity.Name);
                //added by Sandip on 13/08/2018 to handle customer count with Barriers in SLD          
                HashSet<int> barriersList = new HashSet<int>();
                if (!string.IsNullOrEmpty(Barriers))
                {
                    foreach (var barrier in Barriers.Split(','))
                    {
                        barriersList.Add(Convert.ToInt32(barrier));
                    }
                }
                var traceData = await _traceDataService.SLDCustomerCountTraceItemsAsync(FeederId, ElementId, barriersList.ToArray(), Direction);
                if (traceData != null && traceData.Count == 1)
                {
                    feeder.Add("TraceItems", traceData.First().GetValue("graphics"));
                    feeder.Add("Substation", traceData.First().GetValue("subs"));
                    feeder.Add("Transformer", traceData.First().GetValue("trfs"));
                    feeder.Add("Address", traceData.First().GetValue("adrs"));

                    var reportData = traceData.First().GetValue<Dictionary<string, object>[]>("report");
                    if (reportData != null)
                    {
                        var substationData = new HashSet<XVoltageReportTreeItem>(reportData.Select(s => new XVoltageReportTreeItem(s)).ToArray());
                        XVoltageHierarchyHelper helper = new XVoltageHierarchyHelper(substationData);
                        feeder.Add("TraceReport", helper.GetHierarchy(60, ElementId, Direction));
                        traceResults.Add(feeder);
                    }
                }
                result.Result = traceResults;
            }
            catch (Exception ex)
            {
                try
                {
                    errorLogInfo = new Dictionary<string, object>();
                    if (ex.Message.Length > 2000)
                        errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                    else
                    {
                        errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                    }
                }
                catch (Exception ex1)
                {
                    feeder.Add("Response", "Request Timeout!");
                    traceResults.Add(feeder);
                    result.Result = traceResults;
                }
                //_loggingService.Error(ex);
                //throw new Exception("Error");           
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> AuthorizeAPI(string Voltage)
        {
            HashSet<Dictionary<string, object>> userType = new HashSet<Dictionary<string, object>>();
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            string securityClearance = string.Empty;
            try
            {
                var httpContext = HttpContext.Current;
                string Username = httpContext.User.Identity.Name;
                if (Username.Contains('\\'))
                    Username = Username.Split('\\')[1].ToString();

                userType = await UserService.GetUserTypeAsync(Username);
                foreach (Dictionary<string, object> item in (IEnumerable)userType)
                {
                    if (item["security"] != null)
                        securityClearance = item["security"].ToString();
                }
                if ((securityClearance.ToUpper() == "CAT2") && (Voltage.Contains("480") || Voltage.Contains("540") || Voltage.Contains("600")))
                {
                    Dictionary<string, object> item1 = new Dictionary<string, object>();
                    item1.Add("Message", "Authorization has been denied for this request.");
                    result.Add(item1);
                }
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                _loggingService.Error(ex);
                throw new Exception("Error");
            }
            return result;
        }
        /*public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
    }
}
