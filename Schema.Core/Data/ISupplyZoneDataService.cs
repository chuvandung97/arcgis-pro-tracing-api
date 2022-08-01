using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Data
{
    public interface ISupplyZoneDataService
    {
        Task<HashSet<Dictionary<string, object>>> BindEventDataAsync(string EventID);
        Task<HashSet<Dictionary<string, object>>> GetEffectedBoundaryAsync(string EventID);
        Task<HashSet<Dictionary<string, object>>> RetrieveSummaryDataAsync(string EventID);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventAllAsync();
        //Task<HashSet<Dictionary<string, object>>> RetrieveLatestEventAsync();
        Task<HashSet<Dictionary<string, object>>> RetrieveEventTotalCountAsync(string EventID);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventTotalMRCCountAsync(string MRC);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventBuildingCountAsync(string EventID);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventStreetCountAsync(string EventID);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventBuildingCountMRCAsync(string MRC);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventStreetCountMRCAsync(string MRC);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventStreetCountUsingStreetCodeAsync(string StreetCode, string EventID, string MRC);
        //Task<HashSet<Dictionary<string, object>>> LayerMappingAsync();
        Task<Int64> GenerateEventID();
        //Task<HashSet<Dictionary<string, object>>> CheckEventName(string EventName);
        Task<Dictionary<string, object>> CreateEventAsync(List<SupplyZoneItem> rowInfo, string EmpID, Int64 EventID);
        Task<Dictionary<string, object>> CreateEventDetailsAsync(SupplyZoneItem rowInfo, string EmpID, Int64 EventID);
        Task<Dictionary<string, object>> UpdateEventDetailsAsync(SupplyZoneItem rowInfo, string EmpID);
        /*Task<HashSet<Dictionary<string, object>>> BindUserDetailsAsync();
        Task<HashSet<Dictionary<string, object>>> GetUserGroupAsync(string UserID);
        Task<Dictionary<string, object>> AddUserAsync(List<SupplyZoneItem> rowInfo);
        Task<Dictionary<string, object>> DeleteUserAsync(List<SupplyZoneItem> rowInfo);
        Task<Dictionary<string, object>> UpdateUserRoleAsync(List<SupplyZoneItem> rowInfo);*/
    }
}
