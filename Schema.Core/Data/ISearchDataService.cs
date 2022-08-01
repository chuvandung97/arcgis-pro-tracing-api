using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Data
{
    public interface ISearchDataService
    {
        Task<HashSet<Dictionary<string, object>>> SearchSchemaDetailsAsync(string SearchType, string Text, string UserType = null);
        /*Task<HashSet<Dictionary<string, object>>> SearchAddressPointAsync(string Text);
        Task<HashSet<Dictionary<string, object>>> SearchPostalCodeAsync(string Text);
        Task<HashSet<Dictionary<string, object>>> SearchBuildingAsync(string Text);
        Task<HashSet<Dictionary<string, object>>> SearchMapSheetsAsync(string Text);
        Task<HashSet<Dictionary<string, object>>> SearchRoadNamesShortAsync(string Text);
        Task<HashSet<Dictionary<string, object>>> SearchRoadNamesLongAsync(string Text);
        Task<HashSet<Dictionary<string, object>>> SearchMukimLotsAsync(string Text);
        Task<HashSet<Dictionary<string, object>>> SearchSubstationAsync(string Text, string UserType);
        Task<HashSet<Dictionary<string, object>>> SearchOverGroundBoxAsync(string Text);*/
        Task<HashSet<Dictionary<string, object>>> SearchDMISJobIDAsync(string Username, string Text);
        Task<HashSet<Dictionary<string, object>>> GetRoadJunctionsAsync(string RoadName);
        Task<HashSet<Dictionary<string, object>>> GetRoadInfoAsync(string RoadName);
        Task<Dictionary<string, object>> InsertSearchedLocationInDBAsync(List<UsageTrackingItem> rowinfo);
        //Task<string> SearchAddressPointAsync(string Text);
        //Task<string> SearchAddressPointAsync(string Text);
        //Task<string> SearchAddressPointAsync(string Text);
        //Task<HashSet<Dictionary<string, object>>> SearchRoadNamesAsync(string Text);
    }
}
