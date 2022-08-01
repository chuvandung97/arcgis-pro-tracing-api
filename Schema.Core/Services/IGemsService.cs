using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Services
{
    public interface IGemsService
    {
        Task<HashSet<Dictionary<string, object>>> AsBuiltDrawingsAsync(string Voltage, string Geometry, string SubtypeCD);
        Task<HashSet<Dictionary<string, object>>> GetXSectionInfoAsync(string Geometry, string Query, int CriteriaID);
        /*Task<HashSet<Dictionary<string, object>>> GetGridGeometryAsync(string SegmentID);
        Task<HashSet<Dictionary<string, object>>> GetSchematicSegmentGeomAsync(int SegNum, string SheetID);*/
        Task<HashSet<Dictionary<string, object>>> GetNMACSDetailsAsync(double X, double Y, string Voltage, string VoltageType, string Geometry, string MapSheetID);
        Task<HashSet<Dictionary<string, object>>> GetCustomerCountAsync(string UserID, string Geometry = null, double X = 0.0, double Y = 0.0);
        Task<HashSet<Dictionary<string, object>>> Get66kvSupplyZoneLayersAsync();
        Task<HashSet<Dictionary<string, object>>> Get66KVCustomerCountAsync(double X, double Y);
        //Task<HashSet<Dictionary<string, object>>> GetAMIOutageCustomerCountAsync(object JsonObj);
    }
}
