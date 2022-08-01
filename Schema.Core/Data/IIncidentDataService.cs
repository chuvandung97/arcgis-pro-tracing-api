using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Data
{
    public interface IIncidentDataService
    {
        Task<HashSet<Dictionary<string, object>>> GetHTOutageIncidentAsync(string RequestType, string IncidentID, string FromDate = null, string ToDate = null);
        Task<HashSet<Dictionary<string, object>>> GetHTOutageIncidentDetailAsync(Int64 IncidentID);
        Task<Dictionary<string, object>> CreateHTOutageIncidentAsync(List<IncidentManagementItems> rowInfo, string UserID);
        Task<Dictionary<string, object>> CreateHTOutageIncidentByUserAsync(List<IncidentManagementItems> rowInfo, string UserID);
        Task<Dictionary<string, object>> UpdateHTOutageIncidentAsync(List<IncidentManagementItems> rowInfo, string UserID);
        Task<Dictionary<string, object>> UpdateHTOutageAffectedCustomerStatusAsync(List<IncidentManagementItems> rowInfo, string UserID);
        Task<Dictionary<string, object>> DeleteHTOutageIncidentByUserAsync(List<IncidentManagementItems> rowInfo, string UserID);
        Task<Dictionary<string, object>> DeleteHTOutageIncidentAsync(List<IncidentManagementItems> rowInfo, string UserID);
        Task<Dictionary<string, object>> GenerateIncidentBoundaryAsync(List<IncidentManagementItems> rowInfo, string UserID);
        Task<HashSet<Dictionary<string, object>>> GetIncidentTransformersAsync(string MRC);
        Task<HashSet<Dictionary<string, object>>> GetIncidentCustomerTraceAsync(Int64 IncidentID, string UserID, string TRFIDs = null, string FID = null, string EID = null, string DirectionFlag = null, int[] SLDBarriers = null, int[] GISBarriers = null, string OverwriteFlag = null);
        Task<HashSet<Dictionary<string, object>>> GetIncidentTransformersOnMapSelectAsync(string Geometry);
        Task<HashSet<Dictionary<string, object>>> GetHTOutageBuildingSummaryAsync(Int64 IncidentID);
        Task<Dictionary<string, object>> InsertFeedbackAsync(List<IncidentManagementItems> rowInfo, string UserID, string UserName, string EmailID);
    }
}
