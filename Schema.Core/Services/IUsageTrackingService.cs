using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Services
{
    public interface IUsageTrackingService
    {
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingUserThresholdAync(string ReportOfficerID);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingSectionThresholdAync(string ReportOfficerID);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingSchemaUserAync(string ReportOfficerID, string FromDate, string ToDate);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingSummaryAsync(string ReportOfficerID, string FromDate, string ToDate);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingMapSheetsAsync(string UserID, string FromDate, string ToDate);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingDormantUserAync(string ReportOfficerID);
        Task<Dictionary<string, object>> UpdateUsageTrackingUnBlockUserAsync(object JsonObj, string ReportOfficerID);
        Task<Dictionary<string, object>> UpdateUsageTrackingUserThresholdAsync(object JsonObj, string ReportOfficerID, int isAdmin);
        Task<Dictionary<string, object>> UpdateUsageTrackingSectionThresholdAsync(object JsonObj, string ReportOfficerID, int isAdmin);
        Task<Dictionary<string, object>> DeleteUsageTrackingUserThresholdAsync(object JsonObj, string ReportOfficerID, int isAdmin);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingJobInfoAync();
        Task<Dictionary<string, object>> UpdateUsageTrackingJobInfoAsync();
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingListOfUsernameAsync(string ReportOfficerID, Int16 Type, string FromDate, string ToDate, Int16 DirectFlag = 0);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingBlockedUserHistoryAsync(string ReportOfficerID, Int16 Type, Int16 DirectFlag = 0);
    }
}
