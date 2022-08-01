using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Services
{
    public interface IIncidentService
    {
        Task<HashSet<Dictionary<string, object>>> GetHTOutageIncidentAsync(string RequestType, string IncidentID, string FromDate = null, string ToDate = null);
        Task<SchemaResult> GetHTOutageIncidentDetailAsync(Int64 IncidentID);
        Task<Dictionary<string, object>> CreateHTOutageIncidentAsync(object JsonObj, string UserID);
        Task<SchemaResult> CreateHTOutageIncidentByUserAsync(object JsonObj, string UserID);
        Task<Dictionary<string, object>> UpdateHTOutageIncidentAsync(object JsonObj, string UserID);
        Task<Dictionary<string, object>> UpdateHTOutageAffectedCustomerStatusAsync(object JsonObj, string UserID);
        Task<Dictionary<string, object>> DeleteHTOutageIncidentByUserAsync(object JsonObj, string UserID);
        Task<Dictionary<string, object>> DeleteHTOutageIncidentAsync(object JsonObj, string UserID);
        Task<Dictionary<string, object>> GenerateIncidentBoundaryAsync(object JsonObj, string UserID);
        Task<HashSet<Dictionary<string, object>>> GetIncidentTransformersAsync(string MRC);
        Task<SchemaResult> GetIncidentCustomerTraceAsync(Int64 IncidentID, string UserID, string TRFIDs = null, string FID = null, string EID = null, string DirectionFlag = null, string SLDBarriers = null, string GISBarriers = null, string OverwriteFlag = null);
        Task<HashSet<Dictionary<string, object>>> GetIncidentTransformersOnMapSelectAsync(string Geometry);
        Task<Dictionary<string, object>> InsertFeedbackAsync(object JsonObj, string UserID,string UserName, string EmailID);

    }
}
