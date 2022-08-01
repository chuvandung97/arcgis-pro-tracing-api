using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Data
{
    public interface IUsageTrackingDataService
    {
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingUserThresholdAync(string ReportOfficerID);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingSectionThresholdAync(string ReportOfficerID);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingSchemaUserAync(string ReportOfficerID, string FromDate, string ToDate);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingSummaryAsync(string ReportOfficerID, string FromDate, string ToDate);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingMapSheetsAsync(string UserID, string FromDate, string ToDate);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingDormantUserAync(string ReportOfficerID);
        Task<Dictionary<string, object>> UpdateUsageTrackingUnBlockUserAsync(List<UCONListItem> rowInfo, string ReportOfficerID);
        Task<Dictionary<string, object>> UpdateUsageTrackingUserThresholdAsync(List<UCONListItem> rowInfo, string ReportOfficerID, int isAdmin);
        Task<Dictionary<string, object>> UpdateUsageTrackingSectionThresholdAsync(List<UCONListItem> rowInfo, string ReportOfficerID, int isAdmin);
        Task<Dictionary<string, object>> DeleteUsageTrackingUserThresholdAsync(List<UCONListItem> rowInfo, string ReportOfficerID, int isAdmin);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingJobInfoAync();
        Task<Dictionary<string, object>> UpdateUsageTrackingJobInfoAsync();
        Task<HashSet<Dictionary<string, object>>> GetUserEmailIDAsync(string UserID);
        Task<HashSet<Dictionary<string, object>>> GetEmailIDAsync(string ReportOfficerID, string DirectFlag, bool NextLevelFlag);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingListOfUsernameAsync(string ReportOfficerID, Int16 Type, string FromDate, string ToDate, Int16 DirectFlag = 0);
        Task<HashSet<Dictionary<string, object>>> GetUsageTrackingBlockedUserHistoryAsync(string ReportOfficerID, Int16 Type, Int16 DirectFlag = 0);
    }
}
