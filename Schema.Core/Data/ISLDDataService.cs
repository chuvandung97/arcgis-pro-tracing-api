using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Data
{
    public interface ISLDDataService
    {
        Task<HashSet<Dictionary<string, object>>> GetSubstationListAsync(int Segment = 0, string SubstationName = null, string SheetID = null, string Voltage = null);
        Task<HashSet<Dictionary<string, object>>> PropagateSLDToGISAsync(string Voltage, string Geometry, string MapSheetID);
        Task<HashSet<Dictionary<string, object>>> PropagateGISToSLDAsync(string Voltage, string Geometry);
        /*Task<HashSet<Dictionary<string, object>>> SheetIDsListingAsync();
        Task<HashSet<Dictionary<string, object>>> JumpToDestinationAsync(string Geometry);*/
        Task<HashSet<Dictionary<string, object>>> GetCableLengthAsync(string Voltage, string Geometry, string MapSheetID);
        Task<Dictionary<string, object>> InsertSearchedLocationInDBAsync(List<UsageTrackingItem> rowinfo);
    }
}
