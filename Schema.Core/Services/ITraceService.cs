using Schema.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schema.Core.Services
{
    public interface ITraceService
    {
        //Task<HashSet<Dictionary<string, object>>> CablesAsync(string Geometry, string Voltage, string Subtypecd);
        //Task<HashSet<Dictionary<string, object>>> BarriersAsync(string Geometry);
        //Task<HashSet<Dictionary<string, object>>> SubstationsAsync(int Eid, int Fid);
        //Task<SchemaResult> DistributionTraceAsync(string Geometry, string Voltage, string Subtypecd);
        //Task<SchemaResult> DistributionTraceAsync(string GlobalIds);
        //Task<SchemaResult> TransmissionTraceAsync(string Geometry, string Voltage, string Subtypecd);
        //Task<SchemaResult> DownstreamTraceAsync(int EdgeId, int FeederId, int VoltageDepth, string Barriers);
        //Task<SchemaResult> UpstreamTraceAsync(int EdgeId, int FeederId);
        ////Task<SchemaResult> GISRingTraceForDesktopAsync(int SLDFeederID);
        //Task<SchemaResult> SLDRingTraceAsync(string Geometry);
        //Task<SchemaResult> SLDRingTraceDesktopAsync(string GlobalIds);
        //Task<SchemaResult> SLDMultiRingTraceDesktopAsync(string GlobalIds);
        ////Task<SchemaResult> SLDMultiRingTraceAsync(string Geometry);
        //Task<SchemaResult> SLDXVoltageTraceAsync(string Geometry);
        //New 
        Task<SchemaResult> CableTraceAsync(string Geometry = null, string Voltage = null, string Subtypecd = null, string GlobalIds = null);
        Task<SchemaResult> ContextMenuCableTraceAsync(string CableIds);
        Task<SchemaResult> SLDSingleRingTraceAsync(string Geometry = null, string GlobalIds = null);
        Task<SchemaResult> SLDMultiRingTraceAsync(string Geometry = null, string GlobalIds = null);
        Task<SchemaResult> SLDRingReportAsync(string FeederIds);
        Task<SchemaResult> SLDPropagateRingAsync(int SLDFeederID);
        Task<SchemaResult> SLDXVoltSubstationsAsync(string Name = null, string RequestFlag = "XVOLT");
        Task<SchemaResult> SLDXVoltTransformersAsync(string MRC);
        Task<HashSet<Dictionary<string, object>>> SLDSingleFeedersAync(string Geometry, bool IsMultiFeeder = false);
        Task<HashSet<Dictionary<string, object>>> SLDXVoltageSelectBarriersAsync(string Geometry);
        Task<SchemaResult> SLDXVoltTraceAsync(int TransformerId, int Voltage, string Barriers = null);
        Task<SchemaResult> LVOGBTraceAsync(int Direction, string Geometry = null, string GlobalId = null);
        Task<HashSet<Dictionary<string, object>>> PlanningSelectCablesAsync(string Geometry, string Voltage, string Subtypecd);
        Task<HashSet<Dictionary<string, object>>> PlanningSelectBarriersAsync(string Geometry);
        Task<SchemaResult> PlanningDownstreamTraceAsync(int EdgeId, int FeederId, int VoltageDepth, string Barriers);
        Task<SchemaResult> PlanningUpstreamTraceAsync(int EdgeId, int FeederId);
        //Task<SchemaResult> SLDCustomerCountTraceAsync(string Geometry);
        //Sandip
        Task<SchemaResult> SLDCustomerCountTraceAsync(int FeederId, int ElementId, string Barriers = null, int Direction = 0);
    }
}
