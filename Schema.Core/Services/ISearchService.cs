using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Services
{
    public interface ISearchService
    {
        Task<HashSet<Dictionary<string, object>>> GetSearchResultsAsync(string SearchType, string Username, string Text, string UserType = null);
        Task<HashSet<Dictionary<string, object>>> GetRoadJunctionsAsync(string RoadName);
        Task<HashSet<Dictionary<string, object>>> GetRoadInfoAsync(string RoadName);
        Task<Dictionary<string, object>> InsertSearchedLocationInDBAsync(object JsonObj);
    }
}
