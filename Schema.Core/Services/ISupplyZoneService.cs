using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Services
{
    public interface ISupplyZoneService
    {
        Task<HashSet<Dictionary<string, object>>> BindEventDataAsync(string EventID);
        Task<HashSet<Dictionary<string, object>>> GetEffectedBoundaryAsync(string EventID);
        Task<HashSet<Dictionary<string, object>>> RetrieveSummaryDataAsync(string EventID);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventAllAsync();
        //Task<SchemaResult> RetrieveLatestEventAsync();
        Task<HashSet<Dictionary<string, object>>> RetrieveEventTotalCountAsync(string EventID);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventTotalMRCCountAsync(string MRC);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventBuildingCountAsync(string EventID);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventStreetCountAsync(string EventID);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventBuildingCountMRCAsync(string MRC);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventStreetCountMRCAsync(string MRC);
        Task<HashSet<Dictionary<string, object>>> RetrieveEventStreetCountUsingStreetCodeAsync(string StreetCode, string EventID, string MRC);
        //Task<HashSet<Dictionary<string, object>>> LayerMappingAsync();
        Task<SchemaResult> CreateEventAsync(object JsonObj, string EmpID);
        Task<SchemaResult> UpdateEventAsync(object JsonObj, string EmpID);
        /*Task<HashSet<Dictionary<string, object>>> BindUserDetailsAsync();
        Task<HashSet<Dictionary<string, object>>> GetUserGroupAsync(string UserID);
        Task<Dictionary<string, object>> AddUserAsync(object JsonObj);
        Task<Dictionary<string, object>> DeleteUserAsync(object JsonObj);
        Task<Dictionary<string, object>> UpdateUserRoleAsync(object JsonObj);*/
    }
}
