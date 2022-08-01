using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Data
{
    public interface IAdminDataService
    {
        Task<HashSet<Dictionary<string, object>>> GetMapServiceCount();
        Task<HashSet<Dictionary<string, object>>> GetFunctionUsageCount();
        Task<HashSet<Dictionary<string, object>>> GetLoginCount();
        Task<HashSet<Dictionary<string, object>>> GetErrorCount();
        Task<HashSet<Dictionary<string, object>>> GetWeeklyLoginActivityAsync();
        Task<HashSet<Dictionary<string, object>>> GetEachMapServiceUsageAsync();
        Task<HashSet<Dictionary<string, object>>> GetEachFunctionUsageAync();
        Task<HashSet<Dictionary<string, object>>> GetEachErrorUsageAsync();
        Task<HashSet<Dictionary<string, object>>> GetDailyUserActivitiesAsync(int Year, int Month);
        Task<HashSet<Dictionary<string, object>>> GetBrowserActivitiesAsync(int Year, int Month);
        Task<Dictionary<string, object>> InsertConfigUrlMappingAsync(string URLKey, string ServiceType, string URL, int? IsTracking);
        Task<Dictionary<string, object>> UpdateConfigUrlMappingAsync(string URLKey, string ServiceType, string URL, int? IsTracking);
        Task<Dictionary<string, object>> DeleteConfigUrlMappingAsync(string URLKey);
        Task<HashSet<Dictionary<string, object>>> GetUrlKeysFromConfigURLAsync();
        Task<Dictionary<string, object>> CheckIfAPIExists(string APIName);
        Task<Dictionary<string, object>> InsertACMAPIAsync(string Module, string FunctionName);
        Task<Dictionary<string, object>> UpdateACMAPIAsync(Int32 ApiKey, string Module, string FunctionName);
        Task<Dictionary<string, object>> DeleteACMAPIAsync(Int32 ApiKey);
        Task<HashSet<Dictionary<string, object>>> GetAPINameAsync();
        Task<Dictionary<string, object>> InsertACMFunctionalityAsync(string Module, string Functionality);
        Task<Dictionary<string, object>> UpdateACMFunctionalityAsync(Int32 FunctionalityKey, string Module, string Functionality);
        Task<Dictionary<string, object>> DeleteACMFunctionalityAsync(Int32 FunctionalityKey);
        Task<HashSet<Dictionary<string, object>>> GetACMModuleNameAsync();
        Task<Dictionary<string, object>> CheckIfADGroupExists(string ADGroup);
        Task<Dictionary<string, object>> CheckIfADGroupExistsInACMADGroup(string ADGroup);
        Task<Dictionary<string, object>> InsertACMADGroupAsync(string ADGroup, string Role);
        Task<Dictionary<string, object>> UpdateACMADGroupAsync(Int32 ADGroupKey, string ADGroup, string Role);
        Task<Dictionary<string, object>> DeleteACMADGroupAsync(Int32 ADGRoupKey);
        Task<HashSet<Dictionary<string, object>>> GetACMADGroupAsync();      
        Task<Dictionary<string, object>> InsertADGroupToFunctionMappingAsync(Int32 ADGroupKey, string FunctionalityMultipleKeys);
        Task<Dictionary<string, object>> DeleteADGroupToFunctionMappingAsync(Int32 ADGroupKey, Int32 FunctionalityKey);
        Task<HashSet<Dictionary<string, object>>> GetADGroupToFunctionMappingAsync(int FunctionalityKey, int ADGroupKey);
        Task<Dictionary<string, object>> InsertADGroupToAPIMappingAsync(Int32 ADGroupKey, string APIMultipleKeys);
        Task<Dictionary<string, object>> DeleteADGroupToAPIMappingAsync(Int32 ADGroupKey, Int32 APIKey);
        Task<HashSet<Dictionary<string, object>>> GetADGroupToAPIMappingAsync(int APIKey,int ADGroupKey);
        Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetUserListAsync();
        //added by Sandip on 20th Sept 2019 for RFC0021956 for PO Verfication
        Task<HashSet<Dictionary<string, object>>> GetPOMEAListAsync(string ProjectOfficerID, string UserType);
        Task<HashSet<Dictionary<string, object>>> GetCompletePOJobInfoDataAsync(string UserType, string OfficerID);
        Task<HashSet<Dictionary<string, object>>> GetAllPOVIDSToUpdatePOInfoAsync(string RejectSession = null);
        Task<Dictionary<string, object>> UpdatePOIDAsync(List<POVerficationListItem> rowInfo);
        Task<Dictionary<string, object>> UpdateMEARejectSessionAsync(List<POVerficationListItem> rowInfo);
        Task<HashSet<Dictionary<string, object>>> GetPOVContractorInfoAsync();
        Task<Dictionary<string, object>> UpdatePOVContractorInfoAsync(List<POVerficationListItem> rowInfo, string OfficerID);
        Task<Dictionary<string, object>> InsertPOVContractorInfoAsync(List<POVerficationListItem> rowInfo, string OfficerID);
        Task<HashSet<Dictionary<string, object>>> GetPOVSessionHistoryAsync(string UserType = null, string OfficerID = null, string FromDate = null, string ToDate = null, string POVID = null);
        Task<Dictionary<string, object>> UpdatePOVJobStatusAsync(List<POVerficationListItem> rowInfo, string OfficerID);
        //ends here for PO Verification
        Task<HashSet<Dictionary<string, object>>> GetJobSyncAsync();
        Task<Dictionary<string, object>> InsertJobSyncAsync(List<IncidentManagementItems> rowInfo, string UserID);
        Task<Dictionary<string, object>> UpdateJobSyncAsync(List<IncidentManagementItems> rowInfo, string UserID);
        Task<HashSet<Dictionary<string, object>>> GetProblematicCableJointAsync();
        Task<HashSet<Dictionary<string, object>>> GetProblematicCableJointReportPathAsync(string ReportPath);
        Task<HashSet<Dictionary<string, object>>> GetValidateProblematicCableJointDataAsync(string JsonObj);
        Task<Dictionary<string, object>> InsertProblematicCableJointDataAsync(string JsonObj, string UserID);

        /*Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetReportAsync(string InputDate, string ReportType, string Username);
        Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetSummaryReportAsync(string Username);
        Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetGeometryAsync(string InputDate, string ReportType, string Username);*/
    }
}
