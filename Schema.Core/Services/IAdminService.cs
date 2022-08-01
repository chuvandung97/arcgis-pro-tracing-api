using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Services
{
    public interface IAdminService
    {
        Task<Dictionary<string, object>> GetHitCountAsync();
        Task<HashSet<Dictionary<string, object>>> GetWeeklyLoginActivityAsync();
        Task<HashSet<Dictionary<string, object>>> GetEachMapServiceUsageAsync();
        Task<HashSet<Dictionary<string, object>>> GetEachFunctionUsageAync();
        Task<HashSet<Dictionary<string, object>>> GetEachErrorUsageAsync();
        Task<HashSet<Dictionary<string, object>>> GetDailyUserActivitiesAsync(int Year, int Month);
        Task<HashSet<Dictionary<string, object>>> GetBrowserActivitiesAsync(int Year, int Month);
        Task<Dictionary<string, object>> InsertConfigUrlMappingAsync(object JsonObj);
        Task<Dictionary<string, object>> UpdateConfigUrlMappingAsync(object JsonObj);
        Task<Dictionary<string, object>> DeleteConfigUrlMappingAsync(object JsonObj);
        Task<HashSet<Dictionary<string, object>>> GetUrlKeysFromConfigURLAsync();
        Task<SchemaResult> InsertACMAPIAsync(object JsonObj);
        Task<SchemaResult> UpdateACMAPIAsync(object JsonObj);
        Task<Dictionary<string, object>> DeleteACMAPIAsync(object JsonObj);
        Task<HashSet<Dictionary<string, object>>> GetAPINameAsync();
        Task<Dictionary<string, object>> InsertACMFunctionalityAsync(object JsonObj);
        Task<Dictionary<string, object>> UpdateACMFunctionalityAsync(object JsonObj);
        Task<Dictionary<string, object>> DeleteACMFunctionalityAsync(object JsonObj);
        Task<HashSet<Dictionary<string, object>>> GetACMModuleNameAsync();
        Task<SchemaResult> InsertACMADGroupAsync(object JsonObj);
        Task<Dictionary<string, object>> UpdateACMADGroupAsync(object JsonObj);
        Task<Dictionary<string, object>> DeleteACMADGroupAsync(object JsonObj);
        Task<HashSet<Dictionary<string, object>>> GetACMADGroupAsync();
        Task<Dictionary<string, object>> InsertADGroupToFunctionMappingAsync(object JsonObj);
        Task<Dictionary<string, object>> DeleteADGroupToFunctionMappingAsync(object JsonObj);
        Task<HashSet<Dictionary<string, object>>> GetADGroupToFunctionMappingAsync(int FunctionalityKey, int ADGroupKey);
        Task<Dictionary<string, object>> InsertADGroupToAPIMappingAsync(object JsonObj);
        Task<Dictionary<string, object>> DeleteADGroupToAPIMappingAsync(object JsonObj);
        Task<HashSet<Dictionary<string, object>>> GetADGroupToAPIMappingAsync(int APIKey, int ADGroupKey);
        Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetUserListAsync();
        //added by Sandip on 20th Sept 2019 for RFC0021956 for PO Verfication
        Task<HashSet<Dictionary<string, object>>> GetPOMEAListAsync(string ProjectOfficerID, string UserType);
        Task<HashSet<Dictionary<string, object>>> GetCompletePOJobInfoDataAsync(string UserType, string OfficerID);
        Task<HashSet<Dictionary<string, object>>> GetAllPOVIDSToUpdatePOInfoAsync(string RejectSession = null);
        Task<Dictionary<string, object>> UpdatePOIDAsync(object JsonObj);
        Task<Dictionary<string, object>> UpdateMEARejectSessionAsync(object JsonObj);
        Task<HashSet<Dictionary<string, object>>> GetPOVContractorInfoAsync();
        Task<Dictionary<string, object>> UpdatePOVContractorInfoAsync(object JsonObj, string OfficerID);
        Task<Dictionary<string, object>> InsertPOVContractorInfoAsync(object JsonObj, string OfficerID);
        Task<HashSet<Dictionary<string, object>>> GetPOVSessionHistoryAsync(string UserType = null, string OfficerID = null, string FromDate = null, string ToDate = null, string POVID = null);
        Task<Dictionary<string, object>> UpdatePOVJobStatusAsync(object JsonObj, string OfficerID);
        Task<HashSet<Dictionary<string, object>>> GetJobSyncAsync();
        Task<Dictionary<string, object>> InsertJobSyncAsync(object JsonObj, string UserID);
        Task<Dictionary<string, object>> UpdateJobSyncAsync(object JsonObj, string UserID);
        //ends here for PO Verification
        Task<HashSet<Dictionary<string, object>>> GetProblematicCableJointAsync();
        Task<string> GetProblematicCableJointReportPathAsync();
        Task<SchemaResult> InsertProblematicCableJointDataAsync(object JsonObj, string UserID);
        /*Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetReportAsync(string InputDate, string ReportType, string Username);
        Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetSummaryReportAsync(string Username);
        Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetGeometryAsync(string InputDate, string ReportType, string Username);*/
    }
}
