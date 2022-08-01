using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Data
{
    public interface ITraceDataService
    {
        //Task<HashSet<Dictionary<string, object>>> CablesAsync(string Geometry, int Voltage, int[] Subtypecd);
        //Task<int> CablesCountAsync(string Geometry, int Voltage, int[] Subtypecd);
        //Task<HashSet<Dictionary<string, object>>> BarriersAsync(string Geometry);
        //Task<HashSet<Dictionary<string, object>>> SubstationsAsync(int Eid, int Fid);
        //Task<HashSet<Dictionary<string, object>>> DistributionTraceAsync(Int32 eid, Int16 fid);
        //Task<HashSet<Dictionary<string, object>>> DistributionTraceAsync(string GlobalId);
        //Task<HashSet<Dictionary<string, object>>> TransmissionTraceAsync(Int32 eid);
        //HashSet<Dictionary<string, object>> GetTransmissionCablesByGeometry(string Geometry, int Voltage, int[] Subtypecd);
        //Task<int> GetTransmissionCablesCountByGeometry(string Geometry, int Voltage, int[] Subtypecd);
        Task<HashSet<Dictionary<string, object>>> DownstreamTraceAsync(int EdgeId, int FeederId, int VoltageDepth, int[] Barriers);
        Task<HashSet<Dictionary<string, object>>> DownstreamTraceReportAsync(int EdgeId, int FeederId, int VoltageDepth, int[] Barriers);
        Task<HashSet<Dictionary<string, object>>> UpstreamTraceAsync(int EdgeId, int FeederId);
        Task<HashSet<Dictionary<string, object>>> UpstreamTraceReportAsync(int EdgeId, int FeederId);
        //Task<HashSet<Dictionary<string, object>>> GISRingTraceItemsForDesktopAsync(int SLDFeederID);
        //Task<HashSet<Dictionary<string, object>>> SLDRingTraceAsync(string Geometry);
        //Task<HashSet<Dictionary<string, object>>> SLDRingTraceDesktopAsync(string[] GlobalIds);
        Task<HashSet<Dictionary<string, object>>> SLDRingItemsAsync(int feederID);
        //Task<HashSet<Dictionary<string, object>>> SLDRingItemsDesktopAsync(int feederID);
        Task<HashSet<Dictionary<string, object>>> SLDRingReportAsync(int feederID);
        //Task<HashSet<Dictionary<string, object>>> SLDMultiRingTraceAsync(string Geometry);
        //Task<HashSet<Dictionary<string, object>>> SLDMultiRingDesktopAsync(string[] GlobalIds);
        //Task<HashSet<Dictionary<string, object>>> SLDXVoltageItemsAsync(int feederID);
        //Task<HashSet<Dictionary<string, object>>> SLDXVoltageReportAsync(int feederID);
        //New
        Task<HashSet<Dictionary<string, object>>> CablesByGeometryAsync(string Geometry, int Voltage, int[] Subtypecd);//Need to combine logic to select by geometry and globalids
        Task<HashSet<Dictionary<string, object>>> CablesByGlobalIdsAsync(string[] GlobalIds);//Need to combine logic to select by geometry and globalids
        Task<HashSet<Dictionary<string, object>>> CablesTraceAsync(long[] CableIds);
        Task<HashSet<Dictionary<string, object>>> ContextMenuCableTraceAsync(long[] CableIds);
        Task<HashSet<Dictionary<string, object>>> GetSLDSingleFeederAsync(string Geometry = null, string[] GlobalIds = null);
        Task<HashSet<Dictionary<string, object>>> GetSLDMultiFeederAsync(string Geometry = null, string[] GlobalIds = null);
        Task<HashSet<Dictionary<string, object>>> SLDPropagateRingAsync(int SLDFeederID);
        Task<HashSet<Dictionary<string, object>>> SLDXVoltSubstationsAsync(string Name = null,string RequestFlag = "XVOLT");
        Task<HashSet<Dictionary<string, object>>> SLDXVoltTransformersAsync(string MRC);
        Task<HashSet<Dictionary<string, object>>> SLDSingleFeedersAync(string Geometry, bool IsMultiFeeder = false);
        Task<HashSet<Dictionary<string, object>>> SLDXVoltageSelectBarriersAsync(string Geometry);
        Task<HashSet<Dictionary<string, object>>> SLDXVoltTraceFeedersAsync(int TransformerId, int Voltage, int[] Barriers = null);
        Task<HashSet<Dictionary<string, object>>> SLDXVoltTraceFeedersReportAsync(int TransformerId, int Voltage, string[] Barriers = null);
        Task<HashSet<Dictionary<string, object>>> GetOGBAsync(string Geometry = null, string GlobalId = null);
        Task<HashSet<Dictionary<string, object>>> OGBUpstreamTraceAsync(long SwitchBoardID);
        Task<HashSet<Dictionary<string, object>>> OGBDownstreamTraceAsync(long SwitchBoardID);
        Task<HashSet<Dictionary<string, object>>> OGBFirstLegTraceAsync(long SwitchBoardID);
        Task<HashSet<Dictionary<string, object>>> PlanningSelectCablesAsync(string Geometry, int Voltage, int[] Subtypecd);
        Task<HashSet<Dictionary<string, object>>> PlanningSelectBarriersAsync(string Geometry);
        //Task<HashSet<Dictionary<string, object>>> SLDCustomerCountTraceItemsAsync(int feederID);        
        //Sandip
        Task<HashSet<Dictionary<string, object>>> SLDCustomerCountTraceItemsAsync(int FeederId, int ElementId, int[] Barriers, int Direction = 0);
        Task<HashSet<Dictionary<string, object>>> SLDCustomerCountTraceReportAsync(int feederID);
        
    }
}
